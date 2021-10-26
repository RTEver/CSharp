using System;

using Microsoft.Extensions.Logging;

namespace Logging
{
    public static class ILoggerFactoryExtensions : Object
    {
        public static ILoggerFactory AddFile(this ILoggerFactory loggerFactory, String pathToFile)
        {
            if (String.IsNullOrEmpty(pathToFile))
            {
                throw new ArgumentNullException(pathToFile);
            }

            loggerFactory.AddProvider(new FileLoggerProvider(pathToFile));

            return loggerFactory;
        }
    }
}