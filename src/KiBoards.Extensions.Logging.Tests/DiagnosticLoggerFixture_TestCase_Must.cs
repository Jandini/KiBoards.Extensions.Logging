using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging.Tests
{
    public class DiagnosticLoggerFixture_TestCase_Must : IClassFixture<DiagnosticLoggerFixture>
    {
        private readonly ILogger<DiagnosticLoggerFixture_TestCase_Must> _logger;

        public DiagnosticLoggerFixture_TestCase_Must(ITestOutputHelper outputHelper, DiagnosticLoggerFixture diagnosticLoggerFixture)
        {            
            var provider = new ServiceCollection()
                .AddDiagnosticLogger(diagnosticLoggerFixture, outputHelper, (config) => config.SetMinimumLevel(LogLevel.Debug) )
                .BuildServiceProvider();

            _logger = provider.GetRequiredService<ILogger<DiagnosticLoggerFixture_TestCase_Must>>();
        }

        [Fact]
        public void Log_Information_HelloWorld()
        {
            _logger.LogInformation("Hello World");
        }



        [Fact]
        public void Log_Debug_HelloWorld()
        {
            _logger.LogDebug("Hello World");

        }
    }
}