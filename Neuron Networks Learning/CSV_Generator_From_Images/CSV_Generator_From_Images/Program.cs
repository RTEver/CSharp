using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace CSV_Generator_From_Images
{
    internal static class Program : Object
    {
        private const String folderName_CSV = "CSV files";

        private static void Main(String[] args)
        {
            GenerateCSV("Minuses.csv", "Images/Squares/Red", "Images/Squares/Green", "Images/Squares/Blue");
        }
        
        private static void GenerateCSV(String filename, params String[] folderNames)
        {
            var sets = new List<(Single[], Int32)>();

            var @class = 1;

            foreach (String folderName in folderNames)
            {
                var bitmaps = GetBitmapsFromFolder(folderName);

                foreach (Bitmap bitmap in bitmaps)
                {
                    var rgbVector = GetRGBVectorFromBitmap(bitmap);

                    sets.Add((rgbVector, @class));
                }

                @class++;
            }

            GenerateCSVFromBitmaps(filename, sets.ToArray());
        }

        private static void GenerateCSVFromBitmaps(String filename, params (Single[], Int32)[] sets)
        {
            var pathToCSVFolder = Path.Combine(Directory.GetCurrentDirectory(), folderName_CSV);

            var pathToCSVFile = Path.Combine(pathToCSVFolder, filename);

            foreach ((Single[], Int32) set in sets)
            {
                var content = String.Join(',', set.Item1) + "," + set.Item2 + Environment.NewLine;
                
                File.AppendAllText(pathToCSVFile, content);
            }
        }

        private static Bitmap[] GetBitmapsFromFolder(String folderName)
        {
            var pathToFolder = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var images = Directory.GetFiles(pathToFolder);

            var bitmaps = new Bitmap[images.Length];

            for (var i = 0; i < images.Length; ++i)
            {
                bitmaps[i] = new Bitmap(images[i]);
            }

            return bitmaps;
        }

        private static Single[] GetRGBVectorFromBitmap(Bitmap bitmap)
        {
            var vector = new List<Single>();

            for (var y = 0; y < bitmap.Height; ++y)
            {
                for (var x = 0; x < bitmap.Width; ++x)
                {
                    vector.AddRange(GetRGBVectorFromPixel(bitmap.GetPixel(x, y)));
                }
            }

            return vector.ToArray();
        }

        private static Single[] GetRGBVectorFromPixel(Color pixel)
        {
            var vector = new Single[3];

            vector[0] = pixel.R;
            vector[1] = pixel.G;
            vector[2] = pixel.B;

            return vector;
        }
    }
}