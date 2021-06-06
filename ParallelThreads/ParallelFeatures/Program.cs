using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelFeatures
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Console.WriteLine("Starting Main..." + Environment.NewLine);

            var tasks = new Task[]
            {
                new Task(ParallelFor),
                new Task(ParallelForEach),
            };

            foreach (Task task in tasks)
            {
                Console.WriteLine("Starting task...");

                task.Start();

                task.Wait();

                Console.WriteLine("Ending task..." + Environment.NewLine);
            }

            Console.WriteLine("Ending Main...");

            Console.ReadLine();
        }

        private static void ParallelFor()
        {
            var someInfo = Parallel.For(1, 10, (x, pls) =>
            {
                if (x > 5)
                {
                    pls.Break();
                }

                Console.WriteLine(x);
            });

            DisplayInfo(someInfo);
        }

        private static void ParallelForEach()
        {
            var someInfo = Parallel.ForEach<Int32>(new List<Int32>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                (x, pls) =>
                {
                    if (x > 5)
                    {
                        pls.Break();
                    }

                    Console.WriteLine(x);
                });

            DisplayInfo(someInfo);
        }

        private static void DisplayInfo(ParallelLoopResult parallelLoopResult)
        {
            Console.WriteLine("Info about for/foreach-ing:" + Environment.NewLine +
                "IsCompleted: {0}" + Environment.NewLine +
                "LowestBreakIteration: {1}", parallelLoopResult.IsCompleted, parallelLoopResult.LowestBreakIteration);
        }
    }
}
