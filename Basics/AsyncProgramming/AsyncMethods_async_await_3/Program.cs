using System;
using System.Threading.Tasks;

namespace AsyncMethods_async_await_3
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            for (var i = 0; i < 10; ++i)
            {
                FactorialAsync(i);
            }

            Console.ReadLine();
        }

        private static Int32 Factorial(Int32 n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return (n > 1) ? n * Factorial(n - 1) : 1;
        }

        private static async void FactorialAsync(Int32 n)
        {
            Int32 factorial = await Task<Int32>.Run(() => Factorial(n));

            Console.WriteLine("Factorial {0} = {1}", n, factorial);
        }
    }
}
