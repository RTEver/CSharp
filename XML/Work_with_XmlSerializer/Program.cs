using System;
using System.IO;
using System.Xml.Serialization;

namespace Work_with_XmlSerializer
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var people = new[]
            {
                new Person("Alexey", 12, new Company("Samsung"         )),
                new Person("Andrey", 15, new Company("Apple"           )),
                new Person("Vadim" , 22, new Company("Microsoft"       )),
                new Person("Vitaly", 21, new Company("GameIndustry"    )),
                new Person("Andrew", 22, new Company("FutureInvensions")),
            };

            SaveToXml<Person>("people.xml", people);

            SaveToXml<Person>("people.xml", null);

            var new_people = LoadFromXml<Person>("people.xml");

            foreach (Person person in people)
            {
                Console.WriteLine("Name: {1}{0}Age: {2}{0}Company: {3}{0}{0}",
                    Environment.NewLine,
                    person.Name,
                    person.Age,
                    person.Company.Name);
            }
        }

        private static void SaveToXml<TObjectType>(String filename, params TObjectType[] people)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            var formatter = new XmlSerializer(typeof(TObjectType[]));

            using (var fs = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(fs, people);
            }
        }

        private static TObjectType[] LoadFromXml<TObjectType>(String filename)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException();
            }

            var people = default(TObjectType[]);

            var formatter = new XmlSerializer(typeof(TObjectType[]));

            using (var fs = new FileStream(filename, FileMode.Open))
            {
                people = (TObjectType[])formatter.Deserialize(fs);
            }

            return people;
        }
    }
}