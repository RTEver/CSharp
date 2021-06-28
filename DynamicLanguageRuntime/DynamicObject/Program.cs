using System;

namespace DynamicObject
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example();
        }

        private static void Example()
        {
            dynamic subject_1 = new People();

            subject_1.Name = "Vasily";
            subject_1.Age = 21;

            Console.WriteLine(subject_1.Name + " - " + subject_1.Age);
        }
    }
}
