using System;
using System.Net;
using System.Threading.Tasks;

namespace NetConsoleApp
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Listen("http://localhost:8888/");

            Console.Read();
        }

        private static async Task Listen(params String[] prefixes)
        {
            if (prefixes == null)
            {
                throw new ArgumentNullException("prefixes");
            }

            var httpListener = new HttpListener();

            foreach (String prefix in prefixes)
            {
                httpListener.Prefixes.Add(prefix);
            }

            httpListener.Start();

            Console.WriteLine("Ожидание подключений...");

            while (true)
            {
                var context = await httpListener.GetContextAsync();

                var response = context.Response;

                var responseString = "<html><head><meta charset='utf8'></head><body>Привет мир!</body></html>";

                var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

                response.ContentLength64 = buffer.Length;

                using (var outputStream = response.OutputStream)
                {
                    outputStream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}