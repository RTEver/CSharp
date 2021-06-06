using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelCancellationToken
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            //Console.WriteLine("Starting Main...");

            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            //CancellationToken cancellationToken = cancellationTokenSource.Token;

            //var task = new Task(() =>
            //{
            //    while (true)
            //    {
            //        if (cancellationToken.IsCancellationRequested)
            //        {
            //            Console.WriteLine("Stop executing task...");

            //            return;
            //        }
            //    }
            //});

            //task.Start();

            //Console.Write("Enter Y that stop executing task: ");

            //String s = Console.ReadLine();

            //if (s == "Y")
            //{
            //    cancellationTokenSource.Cancel();
            //}

            //task.Wait();

            //Console.WriteLine("Ending Main...");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            new Task(() =>
            {
                Thread.Sleep(400);
                cancellationTokenSource.Cancel();
            }).Start();

            try
            {
                Parallel.ForEach<Int32>(new List<Int32>() { 1, 2, 3, 4, 5, 6, 7, 8 },
                                        new ParallelOptions { CancellationToken = cancellationToken }, Factorial);

                // Or
                //Parallel.For(1, 8, new ParallelOptions { CancellationToken = token }, Factorial);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);

                Console.WriteLine(ex.Data);

                Console.WriteLine("Operation cancalled...");
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }

            Console.ReadLine();
        }

        private static void Factorial(Int32 x)
        {
            var result = 1;

            for (var i = 1; i <= x; i++)
            {
                result *= i;
            }

            Console.WriteLine($"Factorial of number {x} equal {result}");

            Thread.Sleep(3000);
        }
    }
}
