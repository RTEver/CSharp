using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace _2_Work_with_WebRequest_and_WebResponse
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            RequestAsync("https://www.microsoft.com").GetAwaiter().GetResult();
        }

        private async static Task RequestAsync(String address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            var uri = new Uri(address);

            await RequestAsync(uri);
        }

        private async static Task RequestAsync(Uri address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            var request = WebRequest.CreateHttp(address);

            var response = await request.GetResponseAsync();

            var headers = response.Headers;

            for (var i = 0; i < headers.Count; ++i)
            {
                Console.WriteLine("{0}: {1}", headers.GetKey(i), headers[i]);
            }

            using (var stream = response.GetResponseStream())
            using (var streamReader = new StreamReader(stream))
            {
                Console.WriteLine(streamReader.ReadToEnd());
            }

            response.Close();
        }
    }
}