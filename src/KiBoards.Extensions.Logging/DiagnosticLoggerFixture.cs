using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging
{
    public class DiagnosticLoggerFixture
    {
        public IMessageSink MessageSink { get; private set; }

        public DiagnosticLoggerFixture(IMessageSink messageSink) 
        {
            MessageSink = messageSink;
        }
    }
}