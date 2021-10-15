using System;
using System.IO;
using System.Drawing;

namespace ImageGenerator
{
    public static class Program : Object
    {
        private static void Main(String[] args)
        {
            var bitmaps = LoadImagesAsBitmapsFromDirectory("Images");

            CreateCopiesWithOffset(bitmaps[0]);
        }

        private static void CreateCopiesWithOffset(Bitmap bitmap)
        {
            var left = FindEdge(bitmap, Direction.Left);
            var up = FindEdge(bitmap, Direction.Up);

            var newBitmap = SetOffset(bitmap, left, up);
            
            newBitmap.Save($"Images\\Bitmap_v0.bmp");

            GenerateAllCases(newBitmap);
        }

        private static void GenerateAllCases(Bitmap bitmap)
        {
            var right = FindEdge(bitmap, Direction.Right);
            var down = FindEdge(bitmap, Direction.Down);

            var version = 1;

            for (var x = 0; x < bitmap.Width - down; ++x)
            {
                for (var y = 0; y < bitmap.Height - right; ++y)
                {
                    var newBitmap = SetOffset(bitmap, -x, -y);

                    newBitmap.Save($"Images\\Bitmap_v{version++}.bmp");
                }
            }
        }

        private static Bitmap SetOffset(Bitmap bitmap, Int32 left, Int32 up)
        {
            var bitmapWithOffset = new Bitmap(6, 6);

            for (var x = 0; x < bitmapWithOffset.Width; ++x)
            {
                for (var y = 0; y < bitmapWithOffset.Height; ++y)
                {
                    var color = Color.White;

                    var offsetX = x + left;
                    var offsetY = y + up;

                    if (0 <= offsetX && offsetX < bitmap.Width && 0 <= offsetY && offsetY < bitmap.Height)
                    {
                        color = bitmap.GetPixel(offsetX, offsetY);
                    }

                    bitmapWithOffset.SetPixel(x, y, color);
                }
            }

            return bitmapWithOffset;
        }

        private static Int32 FindEdge(Bitmap bitmap, Direction direction)
        {
            var result = -1;
            
            switch (direction)
            {
                case Direction.Left:
                    for (var x = 0; x < bitmap.Height; ++x)
                    {
                        var isWhiteRaw = true;

                        for (var y = 0; y < bitmap.Width; ++y)
                        {
                            var pixel = bitmap.GetPixel(x, y);
                            
                            if (pixel.Name != "ffffffff")
                            {
                                isWhiteRaw = false;
                                
                                break;
                            }
                        }

                        if (!isWhiteRaw)
                        {
                            result = x;

                            break;
                        }
                    }

                    break;
                case Direction.Right:
                    for (var x = bitmap.Height - 1; x >= 0; --x)
                    {
                        var isWhiteRaw = true;

                        for (var y = 0; y < bitmap.Width; ++y)
                        {
                            var pixel = bitmap.GetPixel(x, y);

                            if (pixel.Name != "ffffffff")
                            {
                                isWhiteRaw = false;

                                break;
                            }
                        }

                        if (!isWhiteRaw)
                        {
                            result = x;
                            
                            break;
                        }
                    }

                    break;
                case Direction.Down:
                    for (var x = bitmap.Width - 1; x >= 0; --x)
                    {
                        var isWhiteRaw = true;

                        for (var y = 0; y < bitmap.Height; ++y)
                        {
                            var pixel = bitmap.GetPixel(y, x);

                            if (pixel.Name != "ffffffff")
                            {
                                isWhiteRaw = false;

                                break;
                            }
                        }

                        if (!isWhiteRaw)
                        {
                            result = x;
                            
                            break;
                        }
                    }

                    break;
                case Direction.Up:
                    for (var x = 0; x < bitmap.Width; ++x)
                    {
                        var isWhiteRaw = true;

                        for (var y = 0; y < bitmap.Height; ++y)
                        {
                            var pixel = bitmap.GetPixel(y, x);

                            if (pixel.Name != "ffffffff")
                            {
                                isWhiteRaw = false;

                                break;
                            }
                        }

                        if (!isWhiteRaw)
                        {
                            result = x;
                            
                            break;
                        }
                    }

                    break;
            }

            return result;
        }

        private static Bitmap LoadImageAsBitmap(String filename)
        {
            var image = Image.FromFile("Images\\" + filename);

            return new Bitmap(image);
        }

        private static Bitmap[] LoadImagesAsBitmapsFromDirectory(String path)
        {
            var images = Directory.GetFiles(path);

            var bitmaps = new Bitmap[images.Length];
            
            for (var i = 0; i < bitmaps.Length; ++i)
            {
                bitmaps[i] = new Bitmap(images[i]);
            }

            return bitmaps;
        }
    }
}