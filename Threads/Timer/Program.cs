using System;
using System.Threading;

namespace Threads_Timer
{
    internal static class Program : Object
    {
        private static Int32 count = 0;

        private static void Main(String[] args)
        {
            Console.WriteLine("Threads");

            TimerCallback someThread = new TimerCallback(SomeThread);

            Timer timer = new Timer(someThread, null, 0, 1000);

            Thread.Sleep(10000);

            timer.Dispose();

            Console.WriteLine("Main thread exits.");
        }

        private static void SomeThread(Object obj)
        {
            count = 0;

            for (var i = 0; i < 5; ++i)
            {
                count++;

                Console.WriteLine(Thread.CurrentThread.Name + ": " + count);

                Thread.Sleep(200);
            }
        }
    }
}