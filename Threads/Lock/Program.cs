using System;
using System.Threading;

namespace Threads_Lock
{
    internal static class Program : Object
    {
        private static Object locker = new Object();

        private static Int32 commonNumber = 0;

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
            lock (locker)
            {
                commonNumber = 0;

                for (var i = 0; i < 5; ++i)
                {
                    commonNumber++;

                    Console.WriteLine(Thread.CurrentThread.Name + ": " + commonNumber);

                    Thread.Sleep(200);
                }
            }
        }
    }
}