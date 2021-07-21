using System;
using System.IO;
using System.Threading.Tasks;

namespace Work_with_StreamReader_and_StreamWriter
{
    internal static class Program : Object
    {
        private static async Task Main(String[] args)
        {
            var text = "Hello, World!";

            var path = @"temp.txt";

            await WriteInFileAsync(text, path);

            await ReadFromFileAsync(path);

            await ReadFromFile_2nd_Async(path);
        }

        private static async Task WriteInFileAsync(String text, String path)
        {
            try
            {
                using (var sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    await sw.WriteLineAsync(text);
                }

                using (var sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                {
                    await sw.WriteLineAsync("Дозапись");

                    await sw.WriteAsync("4.5");
                }

                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static async Task ReadFromFileAsync(String path)
        {
            try
            {
                using (var sr = new StreamReader(path, System.Text.Encoding.UTF8))
                {
                    Console.WriteLine(await  sr.ReadToEndAsync());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static async Task ReadFromFile_2nd_Async(String path)
        {
            using (var sr = new StreamReader(path, System.Text.Encoding.UTF8))
            {
                var line = default(String);

                while ((line = await sr.ReadLineAsync()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
