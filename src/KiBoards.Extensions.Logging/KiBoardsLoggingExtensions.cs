using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging
{
    public static class KiBoardsLoggingExtensions
    {
     
        public static IServiceCollection AddKiBoardsLogging(this IServiceCollection services, ITestOutputHelper testOutputHelper, Func<ILoggingBuilder, ILoggingBuilder> configure)
        {
            KiBoardsTestCaseContext testCaseContext = null;

            if (testOutputHelper != null)
            {
                var testCase = testOutputHelper.GetTestCase();
                
                if (testCase != null)
                {
                    testCaseContext = new KiBoardsTestCaseContext()
                    {
                        RunId = Startup.Instance.RunId,
                        TestCase = new KiBoardsTestCase()
                        {
                            Id = (Startup.Instance.RunId + testCase.UniqueID).ComputeMD5(),
                            DisplayName = testCase.DisplayName,
                            UniqueId = testCase.UniqueID,
                            Traits = testCase.Traits,
                            Method = new KiBoardsTestCaseMethod()
                            {
                                Name = testCase.TestMethod.Method.Name,
                            },
                            Class = new KiBoardsTestCaseClass()
                            {
                                Name = testCase.TestMethod.TestClass.Class.Name,
                                Assembly = new KiBoardsTestCaseAssembly()
                                {
                                    Name = testCase.TestMethod.TestClass.Class.Assembly.Name,
                                    AssemblyPath = testCase.TestMethod.TestClass.Class.Assembly.AssemblyPath
                                }
                            },
                            Collection = new KiBoardsTestCaseCollection()
                            {
                                DisplayName = testCase.TestMethod.TestClass.TestCollection.DisplayName,
                                UniqueId = testCase.TestMethod.TestClass.TestCollection.UniqueID,
                            }
                        }
                    };
                }
            }                     


            var elasticOptions = new ElasticsearchSinkOptions(new Uri(Environment.GetEnvironmentVariable("KIB_ELASTICSEARCH_HOST") ?? "http://localhost:9200"))
            {
                IndexFormat = $"kiboards-testlogs-{DateTime.UtcNow:yyyy-MM}",
                AutoRegisterTemplate = true,
            };

            return services.AddLogging(builder => configure(builder).AddSerilog(new LoggerConfiguration()
                .WriteTo.Elasticsearch(elasticOptions)
                .Enrich.WithProperty("TestContext", testCaseContext , true)
                .CreateLogger(), true));
        }


        public static IServiceCollection AddKiBoardsLogging(this IServiceCollection services) => AddKiBoardsLogging(services, null, (config) => config);
        public static IServiceCollection AddKiBoardsLogging(this IServiceCollection services, LogLevel minimumLogLevel) => AddKiBoardsLogging(services, null, (config) => config.SetMinimumLevel(minimumLogLevel));
        public static IServiceCollection AddKiBoardsLogging(this IServiceCollection services, ITestOutputHelper testOutputHelper) => AddKiBoardsLogging(services, testOutputHelper, (config) => config);
        public static IServiceCollection AddKiBoardsLogging(this IServiceCollection services, ITestOutputHelper testOutputHelper, LogLevel minimumLogLevel) => AddKiBoardsLogging(services, testOutputHelper, (config) => config.SetMinimumLevel(minimumLogLevel));       
    }
}
