using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Infrastructure.Events;

/// <summary>
/// Model representing an individual message logged in the SECS/GEM communication traffic buffer.
/// </summary>
public class SecsMessageLog
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Direction { get; set; } = "IN"; // IN or OUT
    public string MsgType { get; set; } = string.Empty;
    public string RawBytesHex { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Simulates an Equipment SECS/GEM interface over TCP using the HSMS-SS protocol specification.
/// Listens on port 5555 for incoming host client connections.
/// </summary>
public class SecsGemSimulator : BackgroundService
{
    private readonly ILogger<SecsGemSimulator> _logger;
    private TcpListener? _listener;
    
    public static ConcurrentQueue<SecsMessageLog> TrafficLogs { get; } = new();

    public SecsGemSimulator(ILogger<SecsGemSimulator> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _listener = new TcpListener(IPAddress.Any, 5555);
            _listener.Start();
            _logger.LogInformation("SECS/GEM HSMS Simulator listening on port 5555.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start SECS/GEM TCP listener on port 5555.");
            return;
        }

        // Background worker to keep the logs queue capped at 100 items
        _ = Task.Run(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000, stoppingToken);
                while (TrafficLogs.Count > 100)
                {
                    TrafficLogs.TryDequeue(out _);
                }
            }
        }, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var client = await _listener.AcceptTcpClientAsync(stoppingToken);
                _ = HandleClientAsync(client, stoppingToken);
            }
            catch (Exception ex) when (!(ex is OperationCanceledException))
            {
                _logger.LogError(ex, "Error accepting SECS/GEM connection.");
            }
        }
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken stoppingToken)
    {
        using (client)
        using (var stream = client.GetStream())
        {
            _logger.LogInformation("SECS/GEM client connected.");
            LogTraffic("IN", "CONNECT", "—", "Host established connection on TCP socket.");
            
            var buffer = new byte[1024];

            while (!stoppingToken.IsCancellationRequested && client.Connected)
            {
                try
                {
                    if (client.Available < 4)
                    {
                        await Task.Delay(100, stoppingToken);
                        continue;
                    }

                    var readLen = await stream.ReadAsync(buffer, 0, 4, stoppingToken);
                    if (readLen == 0) break;

                    // Length is big-endian 32-bit integer
                    int msgLen = (buffer[0] << 24) | (buffer[1] << 16) | (buffer[2] << 8) | buffer[3];
                    if (msgLen <= 0 || msgLen > 1000) continue;

                    int totalRead = 0;
                    while (totalRead < msgLen)
                    {
                        var readBytes = await stream.ReadAsync(buffer, 4 + totalRead, msgLen - totalRead, stoppingToken);
                        if (readBytes == 0) break;
                        totalRead += readBytes;
                    }

                    var header = new byte[10];
                    Array.Copy(buffer, 4, header, 0, 10);

                    var pType = header[9];
                    var directionText = BitConverter.ToString(buffer, 4, msgLen).Replace("-", " ");

                    if (pType == 1) // Select.req
                    {
                        // Reply Select.rsp
                        var rsp = new byte[] { 0, 0, 0, 10, header[0], header[1], header[2], header[3], header[4], 2, header[6], header[7], header[8], header[9] };
                        await stream.WriteAsync(rsp, stoppingToken);
                        
                        LogTraffic("IN", "Select.req", directionText, "HSMS Select Request from Host.");
                        LogTraffic("OUT", "Select.rsp", BitConverter.ToString(rsp, 4).Replace("-", " "), "HSMS Select Response (Success/Online).");
                    }
                    else if (pType == 5) // Linktest.req
                    {
                        // Reply Linktest.rsp
                        var rsp = new byte[] { 0, 0, 0, 10, header[0], header[1], header[2], header[3], header[4], 6, header[6], header[7], header[8], header[9] };
                        await stream.WriteAsync(rsp, stoppingToken);

                        LogTraffic("IN", "Linktest.req", directionText, "HSMS Link Test Keep-Alive Ping.");
                        LogTraffic("OUT", "Linktest.rsp", BitConverter.ToString(rsp, 4).Replace("-", " "), "HSMS Link Test Keep-Alive Pong.");
                    }
                    else if (pType == 0) // Data Message
                    {
                        var streamId = header[2] & 0x7F;
                        var functionId = header[3];
                        var msgName = $"S{streamId}F{functionId}";

                        LogTraffic("IN", msgName, directionText, $"SECS-II primary message stream {streamId} function {functionId}.");

                        if (streamId == 1 && functionId == 1) // S1F1 (Are You Online)
                        {
                            // Reply S1F2
                            var rspHeader = new byte[] { 0, 0, 0, 10, header[0], header[1], 1, 2, header[4], 0, header[6], header[7], header[8], header[9] };
                            await stream.WriteAsync(rspHeader, stoppingToken);
                            LogTraffic("OUT", "S1F2", BitConverter.ToString(rspHeader, 4).Replace("-", " "), "SECS-II S1F2 Online Response.");
                        }
                        else if (streamId == 2 && functionId == 21) // S2F21 (Remote Command)
                        {
                            // Reply S2F22
                            var rspHeader = new byte[] { 0, 0, 0, 10, header[0], header[1], 2, 22, header[4], 0, header[6], header[7], header[8], header[9] };
                            await stream.WriteAsync(rspHeader, stoppingToken);
                            LogTraffic("OUT", "S2F22", BitConverter.ToString(rspHeader, 4).Replace("-", " "), "SECS-II S2F22 Command Acknowledged.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing SECS/GEM connection stream.");
                    break;
                }
            }
            
            LogTraffic("IN", "DISCONNECT", "—", "Host disconnected from HSMS terminal.");
            _logger.LogInformation("SECS/GEM client disconnected.");
        }
    }

    public static void LogTraffic(string direction, string msgType, string hex, string desc)
    {
        TrafficLogs.Enqueue(new SecsMessageLog
        {
            Direction = direction,
            MsgType = msgType,
            RawBytesHex = hex,
            Description = desc
        });
    }

    public static void SendRemoteCommand(string commandName, string recipeName = "DRAM-16Gb-v3")
    {
        var timestampHex = DateTime.UtcNow.ToString("HH mm ss");
        if (commandName.Equals("REMOTE_START", StringComparison.OrdinalIgnoreCase))
        {
            LogTraffic("IN", "S2F21", $"00 00 00 0E 00 01 02 15 00 00 53 54 41 52 54", $"Host Command: REMOTE_START recipe={recipeName}");
            LogTraffic("OUT", "S2F22", $"00 00 00 0A 00 01 02 16 00 00 HCACK=0", $"Equipment ACK: Command Executed Successfully.");
        }
        else if (commandName.Equals("PAUSE_TOOL", StringComparison.OrdinalIgnoreCase))
        {
            LogTraffic("IN", "S2F21", $"00 00 00 0E 00 01 02 15 00 00 50 41 55 53 45", $"Host Command: PAUSE_TOOL chamber=Chamber-A1");
            LogTraffic("OUT", "S2F22", $"00 00 00 0A 00 01 02 16 00 00 HCACK=0", $"Equipment ACK: Chamber Paused Safely.");
        }
        else if (commandName.Equals("SELECT_RECIPE", StringComparison.OrdinalIgnoreCase))
        {
            LogTraffic("IN", "S2F21", $"00 00 00 12 00 01 02 15 00 00 52 45 43 49 50 45", $"Host Command: SELECT_RECIPE recipe={recipeName}");
            LogTraffic("OUT", "S2F22", $"00 00 00 0A 00 01 02 16 00 00 HCACK=0", $"Equipment ACK: Recipe Loaded.");
        }
        else
        {
            LogTraffic("IN", "S1F1", $"00 00 00 0A 00 01 01 01 00 00", $"Host Query: S1F1 Are You Online?");
            LogTraffic("OUT", "S1F2", $"00 00 00 0A 00 01 01 02 00 00", $"Equipment Reply: S1F2 Online Ack.");
        }
    }

    public override void Dispose()
    {
        _listener?.Stop();
        base.Dispose();
    }
}
