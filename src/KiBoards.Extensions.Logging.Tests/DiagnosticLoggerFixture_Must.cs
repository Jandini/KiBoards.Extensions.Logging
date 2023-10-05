using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KiBoards.Extensions.Logging.Tests
{
    public class DiagnosticLoggerFixture_Must : IClassFixture<DiagnosticLoggerFixture>
    {
        private readonly ILogger<DiagnosticLoggerFixture_Must> _logger;

        public DiagnosticLoggerFixture_Must(DiagnosticLoggerFixture diagnosticLoggerFixture)
        {            
            var provider = new ServiceCollection()
                .AddDiagnosticLogger(diagnosticLoggerFixture, LogLevel.Debug)
                .BuildServiceProvider();

            _logger = provider.GetRequiredService<ILogger<DiagnosticLoggerFixture_Must>>();
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