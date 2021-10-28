using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageGenerator
{
    public sealed class Generator : Object
    {
        public Generator()
            : base()
        { }

        public Bitmap[] GetBitmaps(String pathToFolder)
        {
            var files = Directory.GetFiles(pathToFolder);

            var bitmaps = new Bitmap[files.Length];

            for (var i = 0; i < bitmaps.Length; ++i)
            {
                var image = Image.FromFile(files[i]);

                bitmaps[i] = new Bitmap(image);
            }

            return bitmaps;
        }

        public Single[] GetPixelVector(Bitmap bitmap)
        {
            var vector = new List<Single>();

            for (var column = 0; column < bitmap.Width; ++column)
            {
                for (var raw = 0; raw < bitmap.Height; ++raw)
                {
                    var pixel = bitmap.GetPixel(column, raw);

                    if (pixel.Name == "ff000000" || pixel.Name == "00000000")
                    {
                        vector.Add(1);
                    }
                    else
                    {
                        vector.Add(0);
                    }
                }
            }

            return vector.ToArray();
        }

        public (Int32, Int32) GetOffset(Bitmap bitmap)
        {
            var offsetX = default(Int32);
            var offsetY = default(Int32);

            for (var y = 0; y < bitmap.Height; ++y)
            {
                var offsetXCurrentRaw = default(Int32);

                for (var x = 0; x < bitmap.Width; ++x)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    
                    if (pixel.Name != "ffffffff")
                    {
                        offsetXCurrentRaw = x;
                    }
                }
                
                if (offsetX < offsetXCurrentRaw)
                {
                    offsetX = offsetXCurrentRaw;
                }
            }

            for (var x = 0; x < bitmap.Width; ++x)
            {
                var offsetYCurrentColumn = default(Int32);

                for (var y = 0; y < bitmap.Height; ++y)
                {
                    var pixel = bitmap.GetPixel(x, y);

                    if (pixel.Name != "ffffffff")
                    {
                        offsetYCurrentColumn = y;
                    }
                }

                if (offsetY < offsetYCurrentColumn)
                {
                    offsetY = offsetYCurrentColumn;
                }
            }

            //for (var column = 0; column < bitmap.Width; ++column)
            //{
            //    var offsetYOnCurrentColumn = default(Int32);

            //    for (var raw = 0; raw < bitmap.Height; ++raw)
            //    {
            //        var pixel = bitmap.GetPixel(column, raw);

            //        if (pixel.Name == "ff000000" || pixel.Name == "00000000")
            //        {
            //            offsetYOnCurrentColumn = raw;
            //        }
            //    }

            //    if (offsetYOnCurrentColumn > offsetY)
            //    {
            //        offsetY = offsetYOnCurrentColumn;
            //    }
            //}

            //for (var raw = 0; raw < bitmap.Height; ++raw)
            //{
            //    var offsetXOnCurrentRaw = default(Int32);

            //    for (var column = 0; column < bitmap.Width; ++column)
            //    {
            //        var pixel = bitmap.GetPixel(column, raw);

            //        if (pixel.Name == "ff000000" || pixel.Name == "00000000")
            //        {
            //            offsetXOnCurrentRaw = column;
            //        }
            //    }

            //    if (offsetXOnCurrentRaw > offsetX)
            //    {
            //        offsetX = offsetXOnCurrentRaw;
            //    }
            //}

            offsetX = bitmap.Width - offsetX - 1;
            offsetY = bitmap.Height - offsetY - 1;

            return (offsetX, offsetY);
        }

        public void GenerateCopies(String pathToFolder, Bitmap bitmap, String filename)
        {
            if (!Directory.Exists(pathToFolder))
            {
                Directory.CreateDirectory(pathToFolder);
            }

            var offset = GetOffset(bitmap);

            var version = default(Int32);

            //var width = bitmap.Width - offset.Item1;
            //var height = bitmap.Height - offset.Item2;

            for (var offsetY = 0; offsetY <= offset.Item2; ++offsetY)
            {
                for (var offsetX = 0; offsetX <= offset.Item1; ++offsetX)
                {
                    var newBitmap = GenerateBitmapCopy(bitmap, (offsetX, offsetY));

                    newBitmap.Save($"{pathToFolder}\\{filename}_v{version++}.bmp");
                }
            }
            
            //for (var offsetColumn = 0; offsetColumn <= offset.Item1; ++offsetColumn)
            //{
            //    for (var offsetRaw = 0; offsetRaw <= offset.Item2; ++offsetRaw)
            //    {
            //        var newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            //        for (var column = 0; column < width; ++column)
            //        {
            //            for (var raw = 0; raw < height; ++raw)
            //            {
            //                newBitmap.SetPixel(column + offsetColumn, raw + offsetRaw, bitmap.GetPixel(column, raw));
            //            }
            //        }

            //        newBitmap.Save($"{pathToFolder}\\{filename}_v{version++}.bmp");
            //    }
            //}
        }

        public Bitmap GenerateBitmapCopy(Bitmap bitmap, (Int32, Int32) offset)
        {
            var newBitmap = GenerateWhiteBitmap(bitmap);

            for (var y = 0; y < bitmap.Height - offset.Item2; ++y)
            {
                for (var x = 0; x < bitmap.Width - offset.Item1; ++x)
                {
                    var color = bitmap.GetPixel(x, y);

                    newBitmap.SetPixel(x + offset.Item1, y + offset.Item2, color);
                }
            }

            return newBitmap;
        }

        public Bitmap GenerateWhiteBitmap(Bitmap bitmap)
        {
            var newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (var y = 0; y < bitmap.Height; ++y)
            {
                for (var x = 0; x < bitmap.Width; ++x)
                {
                    newBitmap.SetPixel(x, y, Color.White);
                }
            }

            return newBitmap;
        }
    }
}