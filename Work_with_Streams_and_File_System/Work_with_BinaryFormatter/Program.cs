using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Work_with_BinaryFormatter
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Test();
        }

        private static void Test()
        {
            // массив для сериализации
            var people = new[]
            {
                new Person("Tom", 29),
                new Person("Bill", 25),
            };

            var formatter = new BinaryFormatter();

            using (var fs = new FileStream("people.dat", FileMode.OpenOrCreate))
            {
                // сериализуем весь массив people
                formatter.Serialize(fs, people);

                Console.WriteLine("Объекты сериализованы");
            }

            // десериализация
            using (var fs = new FileStream("people.dat", FileMode.OpenOrCreate))
            {
                var deserilizePeople = (Person[])formatter.Deserialize(fs);

                foreach (Person p in deserilizePeople)
                {
                    Console.WriteLine($"Имя: {p.Name} --- Возраст: {p.Age}");
                }
            }
        }
    }
}
