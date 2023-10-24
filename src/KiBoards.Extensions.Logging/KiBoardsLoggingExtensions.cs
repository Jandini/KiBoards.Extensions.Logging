using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging
{
    public static class KiBoardsLoggingExtensions
    {
        public static IDisposable TestScope<T>(this ILogger<T> logger, ITestOutputHelper output)
        {
            var testCase = output.GetTestCase();

            return logger.BeginScope(new Dictionary<string, object>()
            {
                ["TestCaseId"] = (KiBoardsTestRun.Instance.Id + testCase.UniqueID).ComputeMD5(),
                ["TestRunId"] = KiBoardsTestRun.Instance.Id,
                ["TestCaseUniqueId"] = testCase.UniqueID
            });
        }

        public static IServiceCollection AddKiBoardsLogging(this IServiceCollection services, Func<ILoggingBuilder, ILoggingBuilder> configure)
        {
            var elasticOptions = new ElasticsearchSinkOptions(new Uri(Environment.GetEnvironmentVariable("KIB_ELASTICSEARCH_HOST") ?? "http://localhost:9200"))
            {
                IndexFormat = $"kiboards-testlogs-{DateTime.UtcNow:yyyy-MM}",
                AutoRegisterTemplate = true,
            };

            return services.AddLogging(builder => configure(builder).AddSerilog(new LoggerConfiguration()
                .WriteTo.Elasticsearch(elasticOptions)
                .CreateLogger(), true));
        }

        public static IServiceCollection AddKiBoardsLogging(this IServiceCollection services) => AddKiBoardsLogging(services, (config) => config);
        public static IServiceCollection AddKiBoardsLogging(this IServiceCollection services, LogLevel minimumLogLevel) => AddKiBoardsLogging(services, (config) => config.SetMinimumLevel(minimumLogLevel));
       
    }
}
