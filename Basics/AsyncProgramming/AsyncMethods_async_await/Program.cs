using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncMethods_async_await
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Console.WriteLine("Starting Main method...");

            Console.WriteLine("Test async method...");

            Console.WriteLine("Call FactorialAsync...");

            FactorialAsync();

            Console.WriteLine("Input some string:");

            String input = Console.ReadLine();

            Console.WriteLine("You entered '{0}'", input);

            Console.WriteLine("Ending Main method...");

            Console.ReadLine();
        }

        private static async void FactorialAsync()
        {
            Console.WriteLine("Starting FactorialAsync method...");

            Thread.Sleep(2000);

            Int32 factorial = await Task<Int32>.Run(() => Factorial()); // .Run(Factorial);

            Console.WriteLine("5! = {0}", factorial);

            Console.WriteLine("Ending FactorialAsync method...");
        }

        private static Int32 Factorial()
        {
            Console.WriteLine("Factorial start computing...");

            Thread.Sleep(8000);

            Console.WriteLine("Factorial end computing...");

            return 120;
        }
    }
}
