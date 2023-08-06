using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging
{
    internal class DiagnosticLoggerProvider : ILoggerProvider
    {
        private readonly IMessageSink _messageSink;

        public DiagnosticLoggerProvider(IMessageSink messageSink)
        {
            _messageSink = messageSink;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DiagnosticLogger(_messageSink);
        }

        public void Dispose()
        {

        }
    }
}
