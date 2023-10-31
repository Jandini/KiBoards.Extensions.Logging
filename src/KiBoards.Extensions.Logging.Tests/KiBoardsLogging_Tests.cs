using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging.Tests
{
    public class KiBoardsLogging_Tests 
    {
        private readonly ILogger<KiBoardsLogging_Tests> _logger;
        private ITestOutputHelper _output;

        public KiBoardsLogging_Tests(ITestOutputHelper output)            
        {
            _output = output;

            var provider = new ServiceCollection()
                .AddKiBoardsLogging()
                .BuildServiceProvider();

            _logger = provider.GetRequiredService<ILogger<KiBoardsLogging_Tests>>();
        }

        [Fact]
        public void Log_Information_HelloWorld()
        {
            _logger.LogInformation("Hello World");
        }


        [Fact]
        public void Log_Debug_HelloWorld_WithTestCaseScope()
        {
            var provider = new ServiceCollection()
                .AddKiBoardsLogging(_output)
                .BuildServiceProvider();

            var logger = provider.GetRequiredService<ILogger<KiBoardsLogging_Tests>>();

            logger.LogInformation("Log with test case scope.");

        }

    }
}