using System;
using System.Threading;

namespace Threads_AutoResetEvent
{
    internal static class Program : Object
    {
        private static AutoResetEvent waitHandler = new AutoResetEvent(true);

        private static Int32 count = 0;

        private static void Main(String[] args)
        {
            Console.WriteLine("Threads");

            for (var i = 1; i <= 5; ++i)
            {
                Thread thread = new Thread(new ThreadStart(SomeThread));

                thread.Name = "Thread " + i;

                thread.Start();
            }

            Console.WriteLine("Main thread exits.");
        }

        private static void SomeThread()
        {
            waitHandler.WaitOne();

            count = 0;

            for (var i = 0; i < 5; ++i)
            {
                count++;

                Console.WriteLine(Thread.CurrentThread.Name + ": " + count);

                Thread.Sleep(200);
            }

            waitHandler.Set();
        }
    }
}