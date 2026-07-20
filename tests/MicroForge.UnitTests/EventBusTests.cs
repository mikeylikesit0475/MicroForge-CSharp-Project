using FluentAssertions;
using MicroForge.Infrastructure.Events;
using Microsoft.Extensions.Logging.Abstractions;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the EventBus — pub/sub with handler isolation.
/// </summary>
public class EventBusTests
{
    private readonly EventBus _bus = new(NullLogger<EventBus>.Instance);

    [Fact]
    public async Task publish_with_no_subscribers_does_not_throw()
    {
        var act = async () => await _bus.PublishAsync(new TestEvent { Message = "hello" });

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task subscribe_and_publish_delivers_event_to_handler()
    {
        TestEvent? received = null;
        _bus.Subscribe<TestEvent>(e => { received = e; return Task.CompletedTask; });

        var sentevent = new TestEvent { Message = "test message" };
        await _bus.PublishAsync(sentevent);

        received.Should().NotBeNull();
        received!.Message.Should().Be("test message");
    }

    [Fact]
    public async Task multiple_subscribers_all_receive_event()
    {
        var received1 = new List<TestEvent>();
        var received2 = new List<TestEvent>();

        _bus.Subscribe<TestEvent>(e => { received1.Add(e); return Task.CompletedTask; });
        _bus.Subscribe<TestEvent>(e => { received2.Add(e); return Task.CompletedTask; });

        await _bus.PublishAsync(new TestEvent { Message = "broadcast" });

        received1.Should().HaveCount(1);
        received2.Should().HaveCount(1);
        received1[0].Message.Should().Be("broadcast");
        received2[0].Message.Should().Be("broadcast");
    }

    [Fact]
    public async Task handler_exception_does_not_prevent_other_handlers()
    {
        var secondhandlercalled = false;

        _bus.Subscribe<TestEvent>(e => throw new InvalidOperationException("boom"));
        _bus.Subscribe<TestEvent>(e => { secondhandlercalled = true; return Task.CompletedTask; });

        await _bus.PublishAsync(new TestEvent { Message = "error test" });

        secondhandlercalled.Should().BeTrue();
    }

    [Fact]
    public async Task different_event_types_are_isolated()
    {
        TestEvent? testeventreceived = null;
        OtherEvent? othereventreceived = null;

        _bus.Subscribe<TestEvent>(e => { testeventreceived = e; return Task.CompletedTask; });
        _bus.Subscribe<OtherEvent>(e => { othereventreceived = e; return Task.CompletedTask; });

        await _bus.PublishAsync(new TestEvent { Message = "type A" });
        await _bus.PublishAsync(new OtherEvent { Value = 42 });

        testeventreceived.Should().NotBeNull();
        testeventreceived!.Message.Should().Be("type A");
        othereventreceived.Should().NotBeNull();
        othereventreceived!.Value.Should().Be(42);
    }

    private class TestEvent
    {
        public string Message { get; set; } = string.Empty;
    }

    private class OtherEvent
    {
        public int Value { get; set; }
    }
}