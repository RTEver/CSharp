using System;
using System.Reflection;
using System.Net.Sockets;

namespace _1_Work_with_Socket
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            OutputPropertiesInfo(socket);
        }

        private static void OutputPropertiesInfo(Object obj)
        {
            var type = obj.GetType();

            var properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var value = default(Object);

                try
                {
                    value = property.GetValue(obj);
                }
                catch
                {
                    value = "no value";
                }

                Console.WriteLine("{2,-35} {0,-20}: {1,-20}", property.Name, value?.ToString(), property.PropertyType);
            }
        }
    }
}