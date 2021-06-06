using System;
using System.Threading.Tasks;

namespace Returns_value_from_async_method
{
    internal static class Program : Object
    {
        // Task - class; ValueTask - struct.
        private static async Task Main(String[] args)
        {
            var factorial_1 = await FactorialAsync(5);
            var factorial_2 = await FactorialAsync(15);
            var factorial_3 = await FactorialAsync(25);

            Console.WriteLine("Factorial {0} = {1}", 5, factorial_1);
            Console.WriteLine("Factorial {0} = {1}", 15, factorial_2);
            Console.WriteLine("Factorial {0} = {1}", 25, factorial_3);
        }

        private static Int32 Factorial(Int32 n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i < 10; ++i)
            {
                Console.WriteLine(n);
            }

            return (n > 1) ? n * Factorial(n - 1) : 1;
        }

        private static async Task<Int32> FactorialAsync(Int32 n)
        {
            return await Task.Run<Int32>(() => Factorial(n));
        }
    }
}
