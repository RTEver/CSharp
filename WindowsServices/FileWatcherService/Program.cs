using System;
using System.ServiceProcess;

namespace FileWatcherService
{
    internal static class Program : Object
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            ServicesToRun = new ServiceBase[]
            {
                new Service()
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}