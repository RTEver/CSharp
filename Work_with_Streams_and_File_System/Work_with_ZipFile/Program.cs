using System;
using System.IO.Compression;

namespace Work_with_ZipFile
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var sourceFolder = "Temp"; // исходная папка
            var zipFile = "Temp.zip"; // сжатый файл
            var targetFolder = "New Temp"; // папка, куда распаковывается файл

            ZipFile.CreateFromDirectory(sourceFolder, zipFile);
            Console.WriteLine($"Папка {sourceFolder} архивирована в файл {zipFile}");

            ZipFile.ExtractToDirectory(zipFile, targetFolder);
            Console.WriteLine($"Файл {zipFile} распакован в папку {targetFolder}");
        }
    }
}