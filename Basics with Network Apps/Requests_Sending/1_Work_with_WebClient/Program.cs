using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace _1_Work_with_WebClient
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Console.WriteLine("Start of program.");

            var address = new Uri("file:///D:/test_task_04/test_task_04/test2.A");

            var fileName = "test2.A";

            DownloadFileAsync(address, fileName).GetAwaiter().GetResult();

            OutputStreamInConsole(address);

            Console.WriteLine("End of program.");
        }

        private async static Task DownloadFileAsync(Uri address, String fileName)
        {
            if (address == null)
            {
                throw new ArgumentNullException("uri");
            }

            if (fileName == null)
            {
                throw new ArgumentNullException("fileNameToSaving");
            }

            var webClient = new WebClient();
            
            await webClient.DownloadFileTaskAsync(address, fileName);

            Console.WriteLine("File is downloaded.");
        }

        private static void OutputStreamInConsole(Uri address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("uri");
            }

            var webClient = new WebClient();

            using (var stream = webClient.OpenRead(address))
            using (var streamReader = new StreamReader(stream))
            {
                var line = String.Empty;

                while ((line = streamReader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}