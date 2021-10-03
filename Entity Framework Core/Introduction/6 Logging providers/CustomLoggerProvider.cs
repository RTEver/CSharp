using System;
using System.IO;

using Microsoft.Extensions.Logging;

namespace _6_Logging_providers
{
    internal sealed class CustomLoggerProvider : Object, ILoggerProvider
    {
        private sealed class CustomLogger : Object, ILogger
        {
            public IDisposable BeginScope<TState>(TState state) => null;

            public Boolean IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter)
            {
                File.AppendAllText("log.txt", formatter(state, exception));

                Console.WriteLine(formatter(state, exception));
            }
        }

        public ILogger CreateLogger(String categoryName) => new CustomLogger();

        public void Dispose() { }
    }
}