using System;
using System.IO;

namespace Work_with_Directories
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Check_Subdirectories_and_Files_in_Directory("C:\\");

            CreateDirectory("D:\\Temp", "Test");

            GetInfoAboutDirectory("C:\\Program Files");

            RemoveDirectory("D:\\Temp", "D:\\Temp2");

            DeleteDirectory("D:\\Temp2");
        }

        private static void Check_Subdirectories_and_Files_in_Directory(String mainDirName)
        {
            if (Directory.Exists(mainDirName))
            {
                Console.WriteLine("Подкаталоги:");

                var dirNames = Directory.GetDirectories(mainDirName);

                foreach (String dirName in dirNames)
                {
                    Console.WriteLine(dirName);
                }

                Console.WriteLine();

                Console.WriteLine("Файлы:");

                var fileNames = Directory.GetFiles(mainDirName);
                  
                foreach (String fileName in fileNames)
                {
                    Console.WriteLine(fileName);
                }
            }
        }

        private static void CreateDirectory(String path, String subpath)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (subpath == null)
            {
                throw new ArgumentNullException("subpath");
            }

            var dirInfo = new DirectoryInfo(path);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            dirInfo.CreateSubdirectory(subpath);
        }

        private static void GetInfoAboutDirectory(String dirName)
        {
            var dirInfo = new DirectoryInfo(dirName);

            Console.WriteLine($"Название каталога: {dirInfo.Name}");
            Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
            Console.WriteLine($"Время создания каталога: {dirInfo.CreationTime}");
            Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
        }

        private static void RemoveDirectory(String dirName, String newPath)
        {
            var dirInfo = new DirectoryInfo(dirName);

            if (dirInfo.Exists && Directory.Exists(newPath) == false)
            {
                dirInfo.MoveTo(newPath);
            }
        }

        private static void DeleteDirectory(String dirName)
        {
            Directory.Delete(dirName, true);
        }
    }
}