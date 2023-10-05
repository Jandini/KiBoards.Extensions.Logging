using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging
{
    public static class DiagnosticLoggerExtensions
    {

        public static IServiceCollection AddDiagnosticLogger(this IServiceCollection serviceCollection, DiagnosticLoggerFixture messageSinkFixture, Func<ILoggingBuilder, ILoggingBuilder> configure) => serviceCollection.AddLogging(builder => configure(builder).AddDiagnosticLogger(messageSinkFixture.MessageSink));
        public static IServiceCollection AddDiagnosticLogger(this IServiceCollection serviceCollection, DiagnosticLoggerFixture messageSinkFixture, ITestOutputHelper outputHelper, Func<ILoggingBuilder, ILoggingBuilder> configure) => serviceCollection.AddLogging(builder => configure(builder).AddDiagnosticLogger(messageSinkFixture.MessageSink, outputHelper));        
        public static IServiceCollection AddDiagnosticLogger(this IServiceCollection serviceCollection, DiagnosticLoggerFixture messageSinkFixture, LogLevel minimumLogLevel) => serviceCollection.AddLogging(builder => builder.AddDiagnosticLogger(messageSinkFixture.MessageSink).SetMinimumLevel(minimumLogLevel));
        public static IServiceCollection AddDiagnosticLogger(this IServiceCollection serviceCollection, DiagnosticLoggerFixture messageSinkFixture) => serviceCollection.AddLogging(builder => builder.AddDiagnosticLogger(messageSinkFixture.MessageSink));        
        public static IServiceCollection AddDiagnosticLogger(this IServiceCollection serviceCollection, DiagnosticLoggerFixture messageSinkFixture, ITestOutputHelper outputHelper) => serviceCollection.AddLogging(builder => builder.AddDiagnosticLogger(messageSinkFixture.MessageSink, outputHelper));
        public static IServiceCollection AddDiagnosticLogger(this IServiceCollection serviceCollection, DiagnosticLoggerFixture messageSinkFixture, ITestOutputHelper outputHelper, LogLevel minimumLogLevel) => serviceCollection.AddLogging(builder => builder.AddDiagnosticLogger(messageSinkFixture.MessageSink, outputHelper).SetMinimumLevel(minimumLogLevel));
        public static ILoggingBuilder AddDiagnosticLogger(this ILoggingBuilder loggingBuilder, IMessageSink messageSink) => loggingBuilder.AddProvider(new DiagnosticLoggerProvider(messageSink, null));
        public static ILoggingBuilder AddDiagnosticLogger(this ILoggingBuilder loggingBuilder, IMessageSink messageSink, ITestOutputHelper outputHelper) => loggingBuilder.AddProvider(new DiagnosticLoggerProvider(messageSink, outputHelper));
    }
}
