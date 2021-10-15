using System;
using System.IO;
using System.Drawing;

namespace ImageGenerator
{
    public static class Program : Object
    {
        private static void Main(String[] args)
        {
            LoadImageAsBitmap("Image/Letter_A_v0.png");
        }

        private static Bitmap LoadImageAsBitmap(String pathToFile)
        {
            var directory = Directory.GetCurrentDirectory();

            var fullPath = Path.Combine(directory, pathToFile);

            Console.WriteLine(fullPath);

            return null;
        }
    }
}