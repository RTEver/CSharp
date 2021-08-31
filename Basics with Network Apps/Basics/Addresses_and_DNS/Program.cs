using System;
using System.Net;

namespace Addresses_and_DNS
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            GetHostNameAndIPAddresses("www.metanit.com");
        }

        private static void GetHostNameAndIPAddresses(String resourceName)
        {
            if (resourceName == null)
            {
                throw new ArgumentNullException("resourceName");
            }

            var host = Dns.GetHostEntry(resourceName);

            Console.WriteLine("Host name: {0}", host.HostName);

            foreach (IPAddress address in host.AddressList)
            {
                Console.WriteLine("{1}{0}", address.ToString(), '\t');
            }
        }
    }
}