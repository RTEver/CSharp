using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server_application
{
    internal sealed class Server : Object
    {
        private readonly Socket socket;

        private Boolean isWork;

        public Server(IPAddress address, Int32 port)
            : base()
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            if (port < 0 || 65535 < port)
            {
                throw new ArgumentOutOfRangeException("port");
            }

            var localEP = new IPEndPoint(address, port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(localEP);

            isWork = false;
        }

        public Server(IPEndPoint address)
            : base()
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(address);

            isWork = false;
        }

        public void Start()
        {
            isWork = true;

            Task.Run(Listening);
        }

        public void Stop()
        {
            isWork = false;
        }

        private void Listening()
        {
            socket.Listen(10);

            while (isWork)
            {
                var handler = socket.Accept();

                var messageFromUser = new StringBuilder($"{handler.RemoteEndPoint}: ");

                do
                {
                    var buffer = new Byte[256];

                    var bytesReaded = handler.Receive(buffer);

                    messageFromUser.Append(Encoding.UTF8.GetString(buffer, 0, bytesReaded));
                }
                while (handler.Available > 0);

                Console.WriteLine(messageFromUser.ToString());

                var messageToUser = "Your message is delivered.";

                handler.Send(Encoding.UTF8.GetBytes(messageToUser));

                handler.Shutdown(SocketShutdown.Both);

                handler.Close();
            }
        }
    }
}