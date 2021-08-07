using System;
using System.IO;
using System.IO.Compression;

namespace Work_with_GZipStream
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            string sourceFile = "book.pdf"; // исходный файл
            string compressedFile = "book.gz"; // сжатый файл
            string targetFile = "book_new.pdf"; // восстановленный файл

            // создание сжатого файла
            Compress(sourceFile, compressedFile);

            // чтение из сжатого файла
            Decompress(compressedFile, targetFile);
        }

        private static void Compress(String sourceFile, String compressedFile)
        {
            // поток для чтения исходного файла
            using (var sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                // поток для записи сжатого файла
                using (var targetStream = File.Create(compressedFile))
                {
                    // поток архивации
                    using (var compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой

                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            }
        }

        private static void Decompress(String compressedFile, String targetFile)
        {
            // поток для чтения из сжатого файла
            using (var sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                // поток для записи восстановленного файла
                using (var targetStream = File.Create(targetFile))
                {
                    // поток разархивации
                    using (var decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);

                        Console.WriteLine("Восстановлен файл: {0}", targetFile);
                    }
                }
            }
        }
    }
}