using System;
using System.Reflection;

namespace LateBinding
{
    internal interface ISayable
    {
        public void Say(String phrase);
    }

    internal sealed class People : Object, ISayable
    {
        private String name;
        private Int32 age;

        public People(String name, Int32 age)
            : base()
        {
            if (name == null)
            {
                throw new ArgumentNullException(name, "Variable 'name' has null value.");
            }

            this.name = name;
            this.age = age;
        }

        public String Name => name;

        public Int32 Age => age;

        public void Say(String phrase)
        {
            Console.WriteLine(phrase);
        }
    }

    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Invoke("LateBinding.dll", "LateBinding.People", "Say", new Object[] { "Vitaly", 21 }, new Object[] { "Hello, World!" });
        }

        private static void Invoke(String assemblyFile, String typeName, String methodName,
            Object[] constructorParameters, Object[] parameters)
        {
            if (assemblyFile == null)
            {
                throw new ArgumentNullException(assemblyFile, "Variable 'assemblyFile' has null value.");
            }

            if (typeName == null)
            {
                throw new ArgumentNullException(typeName, "Variable 'typeName' has null value.");
            }

            if (methodName == null)
            {
                throw new ArgumentNullException(methodName, "Variable 'methodName' has null value.");
            }

            var assembly = Assembly.LoadFrom(assemblyFile);

            var type = assembly.GetType(typeName);

            var instance = Activator.CreateInstance(type, constructorParameters);

            var method = type.GetMethod(methodName);

            var result = method.Invoke(instance, parameters);

            if (result != null)
            {
                Console.WriteLine();
            }
        }
    }
}
