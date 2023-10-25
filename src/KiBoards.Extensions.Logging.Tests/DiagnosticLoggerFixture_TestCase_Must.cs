using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

[assembly: TestFramework("KiBoards.TestFramework", "KiBoards.Xunit")]

namespace KiBoards.Extensions.Logging.Tests
{
    public class DiagnosticLoggerFixture_TestCase_Must : IClassFixture<DiagnosticLoggerFixture>
    {
        private readonly ILogger<DiagnosticLoggerFixture_TestCase_Must> _logger;
        private readonly ITestOutputHelper _output;

        public DiagnosticLoggerFixture_TestCase_Must(ITestOutputHelper outputHelper, DiagnosticLoggerFixture diagnosticLoggerFixture)
        {
            var provider = new ServiceCollection()
                .AddDiagnosticLogger(diagnosticLoggerFixture, outputHelper, (config) => config.SetMinimumLevel(LogLevel.Debug))
                .AddKiBoardsLogging(outputHelper)
                .BuildServiceProvider();

            _logger = provider.GetRequiredService<ILogger<DiagnosticLoggerFixture_TestCase_Must>>();
            _output = outputHelper;
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


        [Fact]
        public void Log_Information_WithScope()
        {
            using (_logger.BeginScope(new Dictionary<string, object>()
            {
                ["ComputerName"] = Environment.MachineName,
            }))
            {              
               _logger.LogInformation("Computer Name with Test Run Id");               
            }
        }


        [Fact]
        public void Log_Information_WithTestOutputHelper()
        {
            using (_logger.BeginScope(_output))
            {
                _logger.LogInformation("Hello World");
            }
        }
    }
}