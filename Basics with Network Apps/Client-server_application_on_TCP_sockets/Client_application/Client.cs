using System;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Client_application
{
    internal sealed class Client : Object
    {
        private readonly Socket socket;

        public Client()
            : base()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(IPEndPoint address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            socket.Connect(address);
        }

        public void SendMessage(String message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);

            socket.Send(data);
            socket.Send(data);

            data = new Byte[256];

            var answer = new StringBuilder("Server: ");

            do
            {
                var buffer = new Byte[256];

                var bytesReaded = socket.Receive(buffer);

                answer.Append(Encoding.UTF8.GetString(buffer, 0, bytesReaded));
            }
            while (socket.Available > 0);

            Console.WriteLine(answer);

            socket.Shutdown(SocketShutdown.Both);

            socket.Close();
        }
    }
}