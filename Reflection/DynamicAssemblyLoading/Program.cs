using System;
using System.Reflection;

namespace DynamicAssemblyLoading
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            GetClassNamesFromAssembly(@"SomeAsseblyName.dll");
        }

        private static void GetClassNamesFromAssembly(String assemblyFile)
        {
            var assembly = Assembly.LoadFrom(assemblyFile);

            var types = assembly.GetTypes();

            foreach (Type type in types)
            {
                Console.WriteLine(type.Name);
            }
        }
    }
}
