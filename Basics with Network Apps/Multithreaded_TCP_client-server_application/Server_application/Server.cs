using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server_application
{
    internal sealed class Server : Object
    {
        //internal event EventHandler<NewMessageEventArgs> NewMessage;

        private readonly List<Client> clients;

        private readonly TcpListener tcpListener;

        private Boolean IsWork { get; set; }

        internal Server(IPAddress localAddress, Int32 port)
            : base()
        {
            if (localAddress == null)
            {
                throw new ArgumentNullException("localAddress");
            }

            if (port < 0 || 65535 < port)
            {
                throw new ArgumentOutOfRangeException("port");
            }

            tcpListener = new TcpListener(localAddress, port);

            clients = new List<Client>();
        }

        internal Server(IPEndPoint localEP)
            : base()
        {
            if (localEP == null)
            {
                throw new ArgumentNullException("localEP");
            }

            tcpListener = new TcpListener(localEP);
        }

        internal void Start()
        {
            if (!IsWork)
            {
                IsWork = true;

                tcpListener.Start();

                Task.Run(Listening);

                Console.WriteLine("Server is starting.");
            }
        }

        internal void Stop()
        {
            if (IsWork)
            {
                IsWork = false;

                tcpListener.Stop();

                Console.WriteLine("Server is ending.");
            }
        }

        private void BroadcastMessage(Client client, String message) => clients.ForEach(c =>
        {
            if (c != client)
            {
                c.SendMessage(client, message);
            }
        });

        //private void OnNewMessage(NewMessageEventArgs e)
        //{
        //    var temp = Volatile.Read(ref NewMessage);

        //    temp?.Invoke(this, e);
        //}

        private void Listening()
        {
            while (true)
            {
                var tcpClient = tcpListener.AcceptTcpClient();

                var client = new Client(tcpClient);

                clients.Add(client);

                Task.Run(() =>
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

                            Console.WriteLine($"{tcpClient.Client.RemoteEndPoint}: {message}");

                            BroadcastMessage(client, message.ToString());

                            //var e = new NewMessageEventArgs(client, message.ToString());

                            //OnNewMessage(e);
                        }
                    }
                });
            }
        }
    }
}