using System;
using System.IO;
using System.Text;
using System.Drawing;

namespace ImageGenerator
{
    public static class Program : Object
    {
        private static void Main(String[] args)
        {
            //var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            //var generator = new Generator();

            //var bitmaps = generator.GetBitmaps("Letters");

            //for (var i = 0; i < bitmaps.Length; i += 2)
            //{
            //    generator.GenerateCopies($"Copies\\{alphabet[i / 2]}", bitmaps[i], $"{alphabet[i / 2]}");
            //    generator.GenerateCopies($"Copies\\{alphabet[i / 2]}_small", bitmaps[i + 1], $"{alphabet[i / 2]}_small");
            //}

            //var generator = new Generator();

            //var bitmaps = generator.GetBitmaps("Test figures/Squares");

            //var offset = generator.GetOffset(bitmaps[0]);

            //Console.WriteLine("width: {0} height: {1}", offset.Item1, offset.Item2);

            var generator = new Generator();

            var bitmaps = generator.GetBitmaps("Test figures/Squares");

            for (var i = 0; i < bitmaps.Length; ++i)
            {
                var color = (i == 0) ? "Blue" :
                            (i == 1) ? "Green" : "Red";

                generator.GenerateCopies($"Test figures/Squares/{color}", bitmaps[i], color);
            }
        }
        
        //private static void Generate()
        //{
        //    var image = Image.FromFile($"{args[0]}.bmp");

        //    var bitmap = new Bitmap(image);

        //    var version = default(Int32);

        //    var columnsLength = 6 - Int32.Parse(args[1]);
        //    var rawsLength = 6 - Int32.Parse(args[2]);

        //    for (var offsetColumn = 0; offsetColumn < Int32.Parse(args[1]) + 1; ++offsetColumn)
        //    {
        //        for (var offsetRaw = 0; offsetRaw < Int32.Parse(args[2]) + 1; ++offsetRaw)
        //        {
        //            var newBitmap = new Bitmap(6, 6);

        //            for (var column = 0; column < columnsLength; ++column)
        //            {
        //                for (var raw = 0; raw < rawsLength; ++raw)
        //                {
        //                    //Console.WriteLine(bitmap.GetPixel(column, raw));

        //                    newBitmap.SetPixel(column + offsetColumn, raw + offsetRaw, bitmap.GetPixel(column, raw));
        //                }
        //            }

        //            newBitmap.Save($"{args[3]}\\{args[0]}_v{version++}.bmp");
        //        }
        //    }
        //}
    }
}