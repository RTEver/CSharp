using System;
using System.Text;

namespace Encoding_Examples
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example_1_UTF_8();

            Console.WriteLine(Environment.NewLine);

            Example_2_Encoding_Class_Properties();

            Console.WriteLine(Environment.NewLine);

            Example_3_Base64();
        }

        private static void Example_1_UTF_8()
        {
            var exampleString = "Hello, World!";
            
            var encodingUTF8 = Encoding.UTF8;
            
            var encodedBytes = encodingUTF8.GetBytes(exampleString);

            Console.WriteLine("Encoded bytes: " +
                BitConverter.ToString(encodedBytes));

            var decodedString = encodingUTF8.GetString(encodedBytes);

            Console.WriteLine("Decoded string: " + decodedString);
        }

        private static void Example_2_Encoding_Class_Properties()
        {
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding e = ei.GetEncoding();

                Console.WriteLine("{1}{0}" +
                    "\tCodePage={2}, WindowsCodePage={3}{0}" +
                    "\tWebName={4}, HeaderName={5}, BodyName={6}{0}" +
                    "\tIsBrowserDisplay={7}, IsBrowserSave={8}{0}" +
                    "\tIsMailNewsDisplay={9}, IsMailNewsSave={10}{0}",

                    Environment.NewLine,
                    e.EncodingName, e.CodePage, e.WindowsCodePage,
                    e.WebName, e.HeaderName, e.BodyName,
                    e.IsBrowserDisplay, e.IsBrowserSave,
                    e.IsMailNewsDisplay, e.IsMailNewsSave);
            }
        }

        private static void Example_3_Base64()
        {
            var bytes = new Byte[10];
            new Random().NextBytes(bytes);

            Console.WriteLine(BitConverter.ToString(bytes));

            var s = Convert.ToBase64String(bytes);
            Console.WriteLine(s);

            bytes = Convert.FromBase64String(s);
            Console.WriteLine(BitConverter.ToString(bytes));
        }
    }
}