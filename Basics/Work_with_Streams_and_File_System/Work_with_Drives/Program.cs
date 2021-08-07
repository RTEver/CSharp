using System;
using System.IO;

namespace Work_with_Drives
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example();
        }

        private static void Example()
        {
            var drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название: {drive.Name}");

                Console.WriteLine($"Тип: {drive.DriveType}");

                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем диска: {drive.TotalSize}");

                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");

                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }

                Console.WriteLine();
            }
        }
    }
}