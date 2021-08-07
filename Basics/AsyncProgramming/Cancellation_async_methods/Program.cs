using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cancellation_async_methods
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var cts = new CancellationTokenSource();

            var token = cts.Token;

            FactorialAsync(6, token);

            Thread.Sleep(3000);

            cts.Cancel();

            Console.ReadLine();
        }

        private static Int32 Factorial(Int32 n, CancellationToken token)
        {
            if (token.IsCancellationRequested == true)
            {
                Console.WriteLine("Active state of token aborts work of this method.");

                return -1;
            }

            if (n < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Console.WriteLine("Step of \"Factorial\" method...");

            Thread.Sleep(1000);

            return (n > 1) ? n * Factorial(n - 1, token) : 1;
        }

        private static async void FactorialAsync(Int32 n, CancellationToken token)
        {
            Int32 factorial = await Task<Int32>.Run(() => Factorial(n, token));

            Console.WriteLine("Factorial {0} = {1}", n,
                (factorial < 0) ? "???" : factorial.ToString());
        }
    }
}
