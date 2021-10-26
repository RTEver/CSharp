using System;
using System.IO;

using Microsoft.Extensions.Logging;

namespace Logging
{
    public sealed class FileLogger : Object, ILogger
    {
        private static Object locker = new Object();

        private readonly String pathToFile;

        public FileLogger(String pathToFile)
            : base()
        {
            if (String.IsNullOrEmpty(pathToFile))
            {
                throw new ArgumentNullException(pathToFile);
            }

            this.pathToFile = pathToFile;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Information;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter)
        {
            if (formatter != null)
            {
                lock (locker)
                {
                    File.AppendAllText(pathToFile, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}