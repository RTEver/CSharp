using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace UdpClientApp
{
    internal static class Program : Object
    {
        private static IPEndPoint remoteEP;

        private static Int32 localPort;

        private static void Main(String[] args)
        {
            try
            {
                Console.Write("Enter port for listening: ");
                localPort = Int32.Parse(Console.ReadLine());

                Console.Write("Enter remote end point: ");
                remoteEP = IPEndPoint.Parse(Console.ReadLine());

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
            UdpClient sender = new UdpClient();

            try
            {
                while (true)
                {
                    var message = Console.ReadLine();

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

            var remoteEP = default(IPEndPoint);

            try
            {
                while (true)
                {
                    var data = receiver.Receive(ref remoteEP);

                    var message = Encoding.Unicode.GetString(data);

                    Console.WriteLine("{1}: {0}", message, remoteEP.ToString());
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