# Cross-Platform .NET and CLI

Historically, C# was tied to Windows and the heavy **.NET Framework**. Today, the ecosystem is entirely cross-platform.

## From .NET Framework to .NET Core / .NET 8/9
In 2016, Microsoft released .NET Core, an open-source, cross-platform rewrite. Starting with .NET 5, Microsoft unified the frameworks. 
- **Legacy**: .NET Framework 4.x (Windows-only, closed source).
- **Modern**: .NET 8/9 (Cross-platform, open-source, high-performance).

## The Modern dotnet CLI
You no longer need Visual Studio to manage C# projects. The `dotnet` Command Line Interface (CLI) compiles and runs projects natively on macOS, Linux, and Windows:
```bash
# Create a new Console application
dotnet new console -o HelloModernCS

# Change directory
cd HelloModernCS

# Run the application
dotnet run

# Build the application
dotnet build
```

## Editor Tooling
While **Visual Studio** remains popular on Windows, many developers use **Visual Studio Code (VS Code)** with the **C# Dev Kit** extension or JetBrains **Rider** on macOS and Linux.
