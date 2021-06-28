using System;
using System.Collections.Generic;

namespace ExpandoObject
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example();
        }
        
        private static void Example()
        {
            dynamic subject_No_1 = GetSubject("Vasya", 19, new[] { "Russian", "English", "British", "Ukraine" });

            Console.WriteLine(subject_No_1.Name + " - " + subject_No_1.Age);

            foreach (String language in subject_No_1.Languages)
            {
                Console.WriteLine(language);
            }

            subject_No_1.IncreaseAge.Invoke(1);

            Console.WriteLine("Age - " + subject_No_1.Age);
        }

        private static System.Dynamic.ExpandoObject GetSubject(String name, Int32 age, IEnumerable<String> languages)
        {
            dynamic subject = new System.Dynamic.ExpandoObject();

            subject.Name = name;

            subject.Age = age;

            subject.Languages = new List<String>(languages);

            subject.IncreaseAge = (Action<Int32>)((x) => subject.Age += x);

            return subject;
        }
    }
}
