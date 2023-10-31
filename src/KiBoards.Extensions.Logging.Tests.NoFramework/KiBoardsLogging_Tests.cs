using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging.Tests
{
    public class KiBoardsLogging_Tests 
    {
        private ITestOutputHelper _output;

        public KiBoardsLogging_Tests(ITestOutputHelper output)            
        {
            _output = output;
        }

        [Fact]
        public void AddKiBoardsLogging_WithOutput_WithoutAssemblyAttribute_MustNot_Fail()
        {
            new ServiceCollection()
                .AddKiBoardsLogging(_output)
                .BuildServiceProvider();            
        }
    }
}