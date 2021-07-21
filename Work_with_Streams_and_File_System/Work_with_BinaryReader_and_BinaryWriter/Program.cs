using System;
using System.IO;

namespace Work_with_BinaryReader_and_BinaryWriter
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var path = "states.dat";

            Test_BinaryWriter_and_BinaryReader(path);
        }

        private static void Test_BinaryWriter_and_BinaryReader(String path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }    

            try
            {
                var states = new[]
                {
                    new State("Германия", "Берлин", 357168, 80.8),
                    new State("Франция" , "Париж" , 640679, 64.7),
                };

                // создаем объект BinaryWriter
                using (var bw = new BinaryWriter(
                    File.Open(path, FileMode.OpenOrCreate)))
                {
                    // записываем в файл значение каждого поля структуры
                    foreach (State state in states)
                    {
                        bw.Write(state.Name);
                        bw.Write(state.Capital);
                        bw.Write(state.Area);
                        bw.Write(state.People);
                    }
                }

                // создаем объект BinaryReader
                using (var br = new BinaryReader(
                    File.Open(path, FileMode.Open)))
                {
                    // пока не достигнут конец файла
                    // считываем каждое значение из файла
                    while (br.PeekChar() > -1)
                    {
                        var name = br.ReadString();
                        var capital = br.ReadString();
                        var area = br.ReadInt32();
                        var population = br.ReadDouble();

                        Console.WriteLine("Страна: {1}{0}Столица: {2}{0}Площадь: {3} кв. км{0}Численность населения: {4} млн. чел.{0}",
                            Environment.NewLine, name, capital, area, population);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
