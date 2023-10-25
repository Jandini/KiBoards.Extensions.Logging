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
            var testCase = testOutputHelper.GetTestCase();

            var elasticOptions = new ElasticsearchSinkOptions(new Uri(Environment.GetEnvironmentVariable("KIB_ELASTICSEARCH_HOST") ?? "http://localhost:9200"))
            {
                IndexFormat = $"kiboards-testlogs-{DateTime.UtcNow:yyyy-MM}",
                AutoRegisterTemplate = true,
            };

            return services.AddLogging(builder => configure(builder).AddSerilog(new LoggerConfiguration()
                .WriteTo.Elasticsearch(elasticOptions)
                .Enrich.WithProperty("TestCaseId", (KiBoardsTestRun.Instance.Id + testCase.UniqueID).ComputeMD5())
                .Enrich.WithProperty("TestRunId", KiBoardsTestRun.Instance.Id)
                .Enrich.WithProperty("TestCaseUniqueId", testCase.UniqueID)
                .CreateLogger(), true));
        }

        public static IServiceCollection AddKiBoardsLogging(this IServiceCollection services, ITestOutputHelper testOutputHelper) => AddKiBoardsLogging(services, testOutputHelper, (config) => config);
        public static IServiceCollection AddKiBoardsLogging(this IServiceCollection services, ITestOutputHelper testOutputHelper, LogLevel minimumLogLevel) => AddKiBoardsLogging(services, testOutputHelper, (config) => config.SetMinimumLevel(minimumLogLevel));       
    }
}
