using System;
using System.Linq;

namespace Basics
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example_1();

            Example_2();

            Example_3();
        }

        private static void Example_1()
        {
            var names = new[]
            {
                "Vitaly",
                "Andrey",
                "Denis",
                "Kirill",
                "Alexandre",
                "Alexey",
                "Danila",
                "Anna",
                "Elena",
                "Irina",
            };

            var namesStartWithLetterAOrdered = from name in names
                                               where name.StartsWith("A")
                                               orderby name
                                               select name;

            foreach (String name in namesStartWithLetterAOrdered)
            {
                Console.WriteLine(name);
            }
        }

        private static void Example_2()
        {
            var names = new[]
            {
                "Vitaly",
                "Andrey",
                "Denis",
                "Kirill",
                "Alexandre",
                "Alexey",
                "Danila",
                "Anna",
                "Elena",
                "Irina",
            };

            var namesStartWithLetterAOrdered = names.Where<String>(name => name.StartsWith("A")).OrderBy<String, String>(name => name);

            foreach (String name in namesStartWithLetterAOrdered)
            {
                Console.WriteLine(name);
            }
        }

        private static void Example_3()
        {
            var names = new[]
            {
                "Vitaly",
                "Andrey",
                "Denis",
                "Kirill",
                "Alexandre",
                "Alexey",
                "Danila",
                "Anna",
                "Elena",
                "Irina",
            };

            var countNameStartWithLetterA = (from name in names where name.StartsWith("A") orderby name select name).Count<String>();

            Console.WriteLine("Count names that start with letter '{0}' = {1}", 'A', countNameStartWithLetterA);
        }
    }
}