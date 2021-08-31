using System;
using System.Net;

namespace Client_application
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var client = new Client();

            var remoteEP = new IPEndPoint(IPAddress.Loopback, 12345);

            client.Connect(remoteEP);

            client.SendMessage("Hello, World!");

            Console.ReadLine();
        }
    }
}