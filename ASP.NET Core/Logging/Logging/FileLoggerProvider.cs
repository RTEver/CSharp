using System;

using Microsoft.Extensions.Logging;

namespace Logging
{
    public sealed class FileLoggerProvider : Object, ILoggerProvider
    {
        private readonly String pathToFile;

        public FileLoggerProvider(String pathToFile)
            : base()
        {
            if (String.IsNullOrEmpty(pathToFile))
            {
                throw new ArgumentNullException(pathToFile);
            }

            this.pathToFile = pathToFile;
        }

        public ILogger CreateLogger(String categoryName)
        {
            return new FileLogger(pathToFile);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}