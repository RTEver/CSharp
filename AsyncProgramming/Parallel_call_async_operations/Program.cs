using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sequential_and_parallel_call_async_operations
{
    internal static class Program : Object
    {
        private static async Task Main(String[] args)
        {
            await FactorialAsync_Sequential(10);

            Console.WriteLine("----------------------------------------");

            await FactorialAsync_Parallel(10);

            Console.ReadKey();
        }

        private static Int32 Factorial(Int32 n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return (n > 1) ? n * Factorial(n - 1) : 1;
        }

        private static async Task FactorialAsync_Sequential(Int32 n)
        {
            for (var i = 0; i < n; ++i)
            {
                var factorial = await Task.Run<Int32>(() => Factorial(i));

                Console.WriteLine("Factorial {0} = {1}", i, factorial);
            }
        }

        private static async Task FactorialAsync_Parallel(Int32 n)
        {
            var tasks = new List<Task<KeyValuePair<Int32, Int32>>>((n < 0) ? 0 : n);

            for (var i = 0; i < n; ++i)
            {
                // Work with common variable
                tasks.Add(Task.Run<KeyValuePair<Int32, Int32>>(() =>
                    new KeyValuePair<Int32, Int32>(i, Factorial(i))));
            }

            await Task.WhenAll(tasks);

            foreach (Task<KeyValuePair<Int32, Int32>> task in tasks)
            {
                Console.WriteLine("Factorial {0} = {1}",
                    task.Result.Key, task.Result.Value);
            }
        }
    }
}