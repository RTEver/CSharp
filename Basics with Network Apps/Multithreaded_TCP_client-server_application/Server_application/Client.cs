using System;
using System.Text;
using System.Net.Sockets;

namespace Server_application
{
    internal sealed class Client : Object
    {
        private readonly TcpClient tcpClient;

        internal Client(TcpClient tcpClient)
            : base()
        {
            if (tcpClient == null)
            {
                throw new ArgumentNullException("tcpClient");
            }

            this.tcpClient = tcpClient;
        }

        internal void SendMessage(Client client, String message)
        {
            var networkStream = tcpClient.GetStream();

            var username = client.tcpClient.Client.RemoteEndPoint.ToString();

            var m =  username + ": " + message;

            var buffer = Encoding.Unicode.GetBytes(m);

            networkStream.Write(buffer);
        }
    }
}