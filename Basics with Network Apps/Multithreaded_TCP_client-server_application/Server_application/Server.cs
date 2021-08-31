using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server_application
{
    internal sealed class Server : Object
    {
        private readonly TcpListener tcpListener;

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
            tcpListener.Start();

            Task.Run(Listening);
        }

        internal void Stop()
        {
            tcpListener?.Stop();
        }

        private void Listening()
        {
            while (true)
            {
                var tcpClient = tcpListener.AcceptTcpClient();

                var client = new Client(tcpClient);

                Task.Run(client.Process);
            }
        }
    }
}