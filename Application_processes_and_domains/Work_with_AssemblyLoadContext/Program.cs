using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Work_with_AssemblyLoadContext
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example_LoadAssembly(5);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            GetInfoAboutLoadingAssemblies();
        }

        private static void Example_LoadAssembly(Int32 n)
        {
            var context = new CustomAssemblyLoadContext();

            // Registered of event handler
            context.Unloading += Context_Unloading;

            // Get path to Assembly 'CustomMathLibrary'
            var assemblyPath = Path.Combine(@"C:\Users\Phoenix\Desktop\CSharp\Application_processes_and_domains\CustomMathLibrary\bin\Debug\netcoreapp3.1\CustomMathLibrary.dll");

            // Loading Assembly
            var assembly = context.LoadFromAssemblyPath(assemblyPath);

            // Get type of CustomMath class
            var customMathType = assembly.GetType("CustomMathLibrary.CustomMath", true, false);

            // Get method 'Factorial'
            var factorial = customMathType.GetMethod("Factorial");

            var result = (Int32)factorial.Invoke(null, new Object[] { n });

            Console.WriteLine("{0}! = {1}", n, result);

            GetInfoAboutLoadingAssemblies();

            context.Unload();
        }

        private static void GetInfoAboutLoadingAssemblies()
        {
            Console.WriteLine("Loading assemblies:");

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Console.WriteLine('\t' + assembly.GetName().Name);
            }
        }

        private static void Context_Unloading(AssemblyLoadContext context)
        {
            Console.WriteLine("Library 'CustomMathLibrary' is unloaded{0}", Environment.NewLine);
        }
    }
}