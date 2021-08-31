using System;
using System.Net;
using System.Threading;

namespace Server_application
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var tcpServer = new Server(IPAddress.Loopback, 8888);

            tcpServer.Start();

            Console.ReadKey();
        }
    }
}