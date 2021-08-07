using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncMethods_async_await_2
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            ReadWriteAsync();

            Boolean endOfCycle = false;

            Task.Run(() =>
            {
                Thread.Sleep(15000);

                endOfCycle = true;
            });

            while (!endOfCycle)
            {
                Console.WriteLine("Some action every second...");

                Thread.Sleep(1000);
            }
        }

        private static async void ReadWriteAsync()
        {
            String fileName = "hello.txt";

            String input = "Some long string...";

            using (var sw = new StreamWriter(fileName))
            {
                Console.WriteLine("Start writting text in file 'hello.txt'...");

                await sw.WriteLineAsync(input);

                Console.WriteLine("End writting text in file 'hello.txt'...");
            }

            using (var sr = new StreamReader(fileName))
            {
                Console.WriteLine("Start reading text in file 'hello.txt'...");

                String output = await sr.ReadToEndAsync();

                Console.WriteLine("Output: {0}", output);

                Console.WriteLine("End reading text in file 'hello.txt'...");
            }
        }
    }
}
