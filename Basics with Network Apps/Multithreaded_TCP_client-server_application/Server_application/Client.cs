using System;
using System.Net.Sockets;
using System.Text;

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

        internal void Process()
        {
            using (var networkStream = tcpClient.GetStream())
            {
                while (true)
                {
                    var messageFrom = new StringBuilder();

                    do
                    {
                        var bufferFrom = new Byte[256];

                        var readedBytes = networkStream.Read(bufferFrom, 0, bufferFrom.Length);

                        messageFrom.Append(Encoding.Unicode.GetString(bufferFrom, 0, readedBytes));
                    }
                    while (networkStream.DataAvailable);

                    var messageTo = $"Your message is delivered. ({messageFrom.ToString()})";

                    var bufferTo = Encoding.Unicode.GetBytes(messageTo);

                    networkStream.Write(bufferTo, 0, bufferTo.Length);
                }
            }
        }
    }
}