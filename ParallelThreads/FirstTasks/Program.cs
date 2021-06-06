using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstTasks
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var task = new Task(TestMethod);

            task.Start();

            var timerCallback = new TimerCallback(GetInfo);

            var timer = new Timer(timerCallback, task, 0, 2000);

            task.Wait();

            while (true)
            {
                // Plug
            }

            Console.WriteLine("Main method execute.");
        }

        private static void TestMethod()
        {
            while (DateTime.Now < new DateTime(2021, 5, 3, 21, 34, 10))
            {
                // Console.WriteLine($"{DateTime.Now}");
            }
        }

        private static void GetInfo(Object obj)
        {
            var task = obj as Task;

            Console.WriteLine(task.IsCompleted);
            Console.WriteLine(task.Id);
            Console.WriteLine(task.Status);
        }
    }
}
