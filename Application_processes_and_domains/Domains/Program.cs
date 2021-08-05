using System;
using System.Reflection;

namespace Domains
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example();
        }

        private static void Example()
        {
            var currentDomain = AppDomain.CurrentDomain;

            Console.WriteLine("Id: {1}{0}" +
                "Name: {2}{0}" +
                "Base directory: {3}{0}",
                Environment.NewLine,
                currentDomain.Id,
                currentDomain.FriendlyName,
                currentDomain.BaseDirectory);

            var assemblies = currentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Console.WriteLine("Name: {0}",
                    assembly.GetName().Name);
            }
        }
    }
}