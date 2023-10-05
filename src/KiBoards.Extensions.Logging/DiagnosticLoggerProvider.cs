using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging
{
    internal class DiagnosticLoggerProvider : ILoggerProvider
    {
        private readonly IMessageSink _messageSink;
        private readonly ITestOutputHelper _outputHelper;

        public DiagnosticLoggerProvider(IMessageSink messageSink, ITestOutputHelper outputHelper)
        {
            _messageSink = messageSink;
            _outputHelper = outputHelper;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DiagnosticLogger(_messageSink, _outputHelper);
        }

        public void Dispose()
        {

        }
    }
}
