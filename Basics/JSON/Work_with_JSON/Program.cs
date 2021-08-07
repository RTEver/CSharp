using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Work_with_JSON
{
    internal static class Program : Object
    {
        private static async Task Main(String[] args)
        {
            Test_1();

            await Test_2_Async();
        }

        private static void Test_1()
        {
            var person = new Person("Vitya", 21);

            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize<Person>(person, options);

            Console.WriteLine(json);

            var restoredPerson = JsonSerializer.Deserialize<Person>(json, options);

            Console.WriteLine("Name: {1}{0}Age: {2}", Environment.NewLine,
                restoredPerson.Name, restoredPerson.Age);
        }

        private static async Task Test_2_Async()
        {
            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                WriteIndented = true,
            };

            // сохранение данных
            using (var fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                var person = new Person() { Name = "Tom", Age = 35 };

                await JsonSerializer.SerializeAsync<Person>(fs, person, options);

                Console.WriteLine("Data has been saved to file");
            }

            // чтение данных
            using (var fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                var restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs, options);

                Console.WriteLine("Name: {1}{0}Age: {2}", Environment.NewLine,
                restoredPerson.Name, restoredPerson.Age);
            }
        }
    }
}

/*
 * Attributes to properties
 * 
 * [JsonIgnore]
 * [JsonPropertyName("Substitution property name")]
 */