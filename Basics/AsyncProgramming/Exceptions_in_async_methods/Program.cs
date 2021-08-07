using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Don't work but sence is clear!

namespace Exceptions_in_async_methods
{
    internal static class Program : Object
    {
        private static async Task Main(String[] args)
        {
            await FactorialAsync();

            while (true) { Console.WriteLine("Some action..."); }
        }

        private static Int32 Factorial(Int32 n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n", n, "n variable should be longer than 0!");
            }

            return (n > 1) ? n * Factorial(n - 1) : 1;
        }

        private static async Task FactorialAsync()
        {
            var taskExceptions = default(Task);

            try
            {
                var tasks = new List<Task<Int32>>();
                
                tasks.Add(new Task<Int32>(() => Factorial(-1)));

                tasks.Add(new Task<Int32>(() => Factorial(-3)));

                tasks.Add(new Task<Int32>(() => Factorial(-5)));

                taskExceptions = Task.WhenAll(tasks);

                await taskExceptions;

                foreach (Task<Int32> task in tasks)
                {
                    Console.WriteLine("... = {0}", task.Result);
                }
            }
            catch (Exception ex)
            {
                await Task.Run(() => Console.WriteLine("{0}", ex.Message));

                await Task.Run(() => Console.WriteLine("IsFaulted: {0}", taskExceptions.IsFaulted));

                foreach (Exception innerEx in taskExceptions.Exception.InnerExceptions)
                {
                    await Task.Run(() => Console.WriteLine("\tInner exception: {0}", innerEx.Message));
                }
            }
            finally
            {
                await Task.Run(() => Console.WriteLine("Finally async method."));
            }
        }
    }
}
