using System;
using System.Net;
using System.Net.Sockets;

namespace Client_application
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var remoteEP = new IPEndPoint(IPAddress.Loopback, 8888);

            var tcpClient = new TcpClient();

            var client = new Client(tcpClient);

            client.Connect(remoteEP);
        }
    }
}