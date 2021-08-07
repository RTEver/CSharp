using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelClass
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Action[] actions = new Action[5];

            Action consoleReadLine = () =>
            {
                Console.WriteLine("Current task ID: {0}", Task.CurrentId);

                Console.ReadLine();
            };

            for (var i = 0; i < 5; ++i)
            {
                actions[i] = consoleReadLine;
            }

            Parallel.Invoke(actions);

            Console.ReadLine();
        }
    }
}
