using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging
{
    public static class DiagnosticLoggerExtensions
    {
        public static IServiceCollection AddDiagnosticLogger(this IServiceCollection serviceCollection, DiagnosticLoggerFixture messageSinkFixture) => serviceCollection.AddLogging(builder => builder.AddDiagnosticLogger(messageSinkFixture.MessageSink));
        public static ILoggingBuilder AddDiagnosticLogger(this ILoggingBuilder loggingBuilder, IMessageSink messageSink) => loggingBuilder.AddProvider(new DiagnosticLoggerProvider(messageSink));
    }
}
