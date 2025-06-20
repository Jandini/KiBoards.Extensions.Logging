﻿using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace KiBoards.Extensions.Logging
{
    internal class DiagnosticLogger : ILogger
    {
        private readonly IMessageSink _messageSink;
        private readonly ITestOutputHelper _outputHelper;
        
        public DiagnosticLogger(IMessageSink messageSink, ITestOutputHelper outputHelper)
        {
            _messageSink = messageSink;
            _outputHelper = outputHelper;
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
                List<string> messageParts = new();
               
                if (_outputHelper != null)
                {
                    // This could be implemented as begin scope
                    ITest test = _outputHelper.GetTest();

                    // it is possible that logger will be called from the fixture
                    if (test != null)
                    {
                        var assemlbyName = Path.GetFileNameWithoutExtension(test.TestCase.TestMethod.TestClass.Class.Assembly.AssemblyPath);
                        messageParts.Add(test.DisplayName.Replace(assemlbyName, "").TrimStart('.'));
                    }
                }

                messageParts.Add(logLevel.ToString());
                messageParts.Add(formatter(state, exception));

                var message = string.Join(": ", messageParts.ToArray());
                var diagnosticMessage = new DiagnosticMessage(message);
                _messageSink.OnMessage(diagnosticMessage);
            }
        }
    }
}
