using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MulticastApp
{
    internal static class Program : Object
    {
        private static IPAddress remoteAddress;

        private const Int32 remotePort = 8001;
        private const Int32 localPort = 8001;

        private static String username;

        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter username: ");
                username = Console.ReadLine();

                remoteAddress = IPAddress.Parse("127.0.0.1");

                Task.Run(ReceiveMessage);

                SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SendMessage()
        {
            var sender = new UdpClient();

            var remoteEP = new IPEndPoint(remoteAddress, remotePort);

            try
            {
                while (true)
                {
                    var message = Console.ReadLine();

                    message = String.Format("{0}: {1}", username, message);

                    var data = Encoding.Unicode.GetBytes(message);

                    sender.Send(data, data.Length, remoteEP);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        private static void ReceiveMessage()
        {
            var receiver = new UdpClient(localPort);

            receiver.JoinMulticastGroup(remoteAddress);

            receiver.MulticastLoopback = false;

            var remoteEP = default(IPEndPoint);

            try
            {
                while (true)
                {
                    var data = receiver.Receive(ref remoteEP);

                    var message = Encoding.Unicode.GetString(data);

                    Console.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }
    }
}