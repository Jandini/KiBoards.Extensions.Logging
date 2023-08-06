# KiBoards.Extensions.Logging

[![Build](https://github.com/Jandini/KiBoards.Extensions.Logging/actions/workflows/build.yml/badge.svg)](https://github.com/Jandini/KiBoards.Extensions.Logging/actions/workflows/build.yml)
[![NuGet](https://github.com/Jandini/KiBoards.Extensions.Logging/actions/workflows/nuget.yml/badge.svg)](https://github.com/Jandini/KiBoards.Extensions.Logging/actions/workflows/nuget.yml)

A diagnostic logger for xUnit using Microsoft.Extensions.Logging.

---
## Quick Start

Use xUnit message sink to log diagnostic messages.

```c#
using KiBoards.Extensions.Logging;
using Xunit.Sdk;

namespace Demo;

public class UnitTest1 : IClassFixture<DiagnosticLoggerFixture>
{
    private readonly DiagnosticLoggerFixture _diagnosticLoggerFixture;

    public UnitTest1(DiagnosticLoggerFixture diagnosticLoggerFixture)
    {
        _diagnosticLoggerFixture = diagnosticLoggerFixture;
    }

    [Fact]
    public void Test1()
    {
        _diagnosticLoggerFixture.MessageSink.OnMessage(
        	new DiagnosticMessage("Hello World"));
    }
}
```

Create diagnostic logger with Microsoft dependency injection and logging.

```c#
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using KiBoards.Extensions.Logging;

namespace Demo;

public class UnitTest2 : IClassFixture<DiagnosticLoggerFixture>
{
    private readonly ILogger<UnitTest2> _logger;

    public UnitTest2(DiagnosticLoggerFixture diagnosticLoggerFixture)
    {
        var provider = new ServiceCollection()
            .AddDiagnosticLogger(diagnosticLoggerFixture)
            .BuildServiceProvider();

        _logger = provider.GetRequiredService<ILogger<UnitTest2>>();
    }

    [Fact]
    public void Test1()
    {
        _logger.LogInformation("Hello World");
    }
}
```



Created from [JandaBox](https://github.com/Jandini/JandaBox)
Log icon created by [Freepik - Flaticon](https://www.flaticon.com/free-icons/log)