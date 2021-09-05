using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

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
            if (remoteEP == null)
            {
                throw new ArgumentNullException("remoteEP");
            }

            tcpClient.Connect(remoteEP);

            Task.Run(ListeningIncomingMessages);

            using (var networkStream = tcpClient.GetStream())
            {
                while (true)
                {
                    var message = Console.ReadLine();

                    var bufferTo = Encoding.Unicode.GetBytes(message);

                    networkStream.Write(bufferTo, 0, bufferTo.Length);
                }
            }
        }

        private void ListeningIncomingMessages()
        {
            using (var networkStream = tcpClient.GetStream())
            {
                while (true)
                {
                    var message = new StringBuilder();

                    do
                    {
                        var buffer = new Byte[256];

                        var readedBytes = networkStream.Read(buffer, 0, buffer.Length);

                        message.Append(Encoding.Unicode.GetString(buffer, 0, readedBytes));
                    }
                    while (networkStream.DataAvailable);

                    if (!String.IsNullOrEmpty(message.ToString()))
                    {
                        Console.WriteLine(message.ToString());
                    }
                }
            }
        }
    }
}