using System;

namespace Finalized_Objects
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example();

            Example_1();
        }

        private static void Example()
        {
            var people = default(People);

            try
            {
                people = new People("Vasily", 12);
            }
            finally
            {
                people?.Dispose();
            }

            // OR

            using (var anotherPeople = new People("Vasily", 12))
            { }

            // OR

            using var andAnotherPeople = new People("Vasily", 12);
        }

        private static void Example_1()
        {
            using (var people_1 = new People("Vasily", 12))
            using (var people_2 = new People("Andrey", 14))
            { }
        }
    }
}