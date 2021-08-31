using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client_server_application
{
    internal static class Program : Object
    {
        private static Int32 localPort;

        private static Int32 remotePort;

        private static Socket listeningSocket;

        private static void Main(String[] args)
        {
            Start();
        }

        private static void Start()
        {
            Console.Write("Введите порт для приема сообщений: ");
            localPort = Int32.Parse(Console.ReadLine());

            Console.Write("Введите порт для отправки сообщений: ");
            remotePort = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Для отправки сообщений введите сообщение и нажмите Enter\n");

            try
            {
                listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                Task.Run(Listen);

                while (true)
                {
                    var message = Console.ReadLine();

                    var data = Encoding.Unicode.GetBytes(message);

                    var remoteEP = new IPEndPoint(IPAddress.Loopback, remotePort);

                    listeningSocket.SendTo(data, remoteEP);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
        }

        private static void Listen()
        {
            try
            {
                var localEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), localPort);

                listeningSocket.Bind(localEP);

                while (true)
                {
                    var builder = new StringBuilder();

                    EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

                    do
                    {
                        var buffer = new Byte[256];

                        var readedBytes = listeningSocket.ReceiveFrom(buffer, ref remoteEP);

                        builder.Append(Encoding.Unicode.GetString(buffer, 0, readedBytes));
                    }
                    while (listeningSocket.Available > 0);

                    Console.WriteLine("{0} - {1}", remoteEP.ToString(), builder.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
        }

        private static void Close()
        {
            if (listeningSocket != null)
            {
                listeningSocket.Shutdown(SocketShutdown.Both);

                listeningSocket.Close();

                listeningSocket = null;
            }
        }
    }
}