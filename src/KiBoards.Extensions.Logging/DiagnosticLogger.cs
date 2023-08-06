using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace KiBoards.Extensions.Logging
{
    internal class DiagnosticLogger : ILogger
    {
        private readonly IMessageSink _messageSink;

        public DiagnosticLogger(IMessageSink messageSink)
        {
            _messageSink = messageSink;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _messageSink != null;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                var message = $"{logLevel}: {formatter(state, exception)}";
                var diagnosticMessage = new DiagnosticMessage(message);
                _messageSink.OnMessage(diagnosticMessage);
            }
        }
    }
}
