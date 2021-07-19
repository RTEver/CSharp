using System;
using System.IO;

namespace Work_with_Files
{
    internal static class Program : Object
    {
        private static void Main(String[] args) { }

        private static void GetInfoAboutFile(String path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("fileName");
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            var fileInfo = new FileInfo(path);

            Console.WriteLine("Имя файла: {0}"     , fileInfo.Name        );
            Console.WriteLine("Время создания: {0}", fileInfo.CreationTime);
            Console.WriteLine("Размер: {0}"        , fileInfo.Length      );
        }

        private static void DeleteFile(String path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("fileName");
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            var fileInfo = new FileInfo(path);

            fileInfo.Delete();
        }

        private static void MoveFile(String path, String newPath)
        {
            if (path == null)
            {
                throw new ArgumentNullException("fileName");
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            var fileInfo = new FileInfo(path);

            fileInfo.MoveTo(newPath);
        }

        private static void CopyFile(String path, String newPath, Boolean overwrite)
        {
            if (path == null)
            {
                throw new ArgumentNullException("fileName");
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            var fileInfo = new FileInfo(path);

            fileInfo.CopyTo(newPath, overwrite);
        }
    }
}
