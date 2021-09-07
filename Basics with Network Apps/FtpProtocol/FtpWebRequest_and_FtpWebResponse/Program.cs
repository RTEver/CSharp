using System;
using System.IO;
using System.Net;

namespace FtpConsoleClient
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var request = (FtpWebRequest)WebRequest.Create("ftp://127.0.0.1/test.txt");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            //request.Credentials = new NetworkCredential("login", "password");
            //request.EnableSsl = true;

            var response = (FtpWebResponse)request.GetResponse();

            using (var responseStream = response.GetResponseStream())
            using (var fileStream = new FileStream("newTest.txt", FileMode.Create))
            {
                var buffer = new Byte[64];

                var size = 0;

                while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, size);
                }
            }

            response.Close();

            Console.WriteLine("Downloading and saving was successfully.");

            Console.Read();
        }
    }
}