using System;
using System.Net;

namespace Server_application
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var server = new Server(IPAddress.Loopback, 12345);

            server.Start();

            Console.Read();

            server.Stop();
        }
    }
}