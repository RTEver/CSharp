using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkWithTask
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Console.WriteLine("Main method started...");

            var outer = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Outer task started...");

                var inner = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Inner task started...");
                    Thread.Sleep(2000);
                    Console.WriteLine("Inner task ended...");
                }, TaskCreationOptions.AttachedToParent);
            });

            outer.Wait();

            Console.WriteLine("Outer task ended...");

            Console.WriteLine("Main method ended...");
        }
    }
}
