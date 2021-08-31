using System;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Client_application
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

        internal void Connect(IPEndPoint remoteEP)
        {
            //if (remoteEP == null)
            //{
            //    throw new ArgumentNullException("remoteEP");
            //}

            //tcpClient.Connect(remoteEP);

            using (var networkStream = tcpClient.GetStream())
            {
                var userName = tcpClient.Client.LocalEndPoint.ToString();

                while (true)
                {
                    

                    Console.Write(userName + ": ");

                    var messageTo = userName + ": " + Console.ReadLine();

                    var bufferTo = Encoding.Unicode.GetBytes(messageTo);

                    networkStream.Write(bufferTo, 0, bufferTo.Length);

                    var messageFrom = new StringBuilder("Server: ");

                    do
                    {
                        var bufferFrom = new Byte[256];

                        var readedBytes = networkStream.Read(bufferFrom, 0, bufferFrom.Length);

                        messageFrom.Append(Encoding.Unicode.GetString(bufferFrom, 0, readedBytes));
                    }
                    while (networkStream.DataAvailable);

                    Console.WriteLine(messageFrom.ToString());
                }
            }
        }
    }
}