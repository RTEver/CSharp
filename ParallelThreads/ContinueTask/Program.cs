using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContinueTask
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Console.WriteLine("Starting Main...");

            //Task infinityTask = Task.Factory.StartNew(() => { while (true) { } });

            Task firstTask = new Task(() =>
            {
                Console.WriteLine("Start execute first task...");

                Console.WriteLine("Current task ID: {0}", Task.CurrentId);
            });

            // Continuation task
            Task secondTask = firstTask.ContinueWith((previous) =>
            {
                Console.WriteLine("End execute first task...");

                Console.WriteLine("Start execute second task...");

                Console.WriteLine("Previous task ID: {0}", previous.Id);
                Console.WriteLine("Current task ID: {0}", Task.CurrentId);

                Thread.Sleep(3000);
            });

            firstTask.Start();

            secondTask.Wait();

            Console.WriteLine("Ending execute second task...");

            Console.WriteLine("Ending Main...");

            Console.ReadLine();
        }
    }
}
