using System;
using System.Reflection;

namespace TestConsoleApplication
{
    internal sealed class People : Object
    {
        private String name;
        private Int32 age;

        public People(String name, Int32 age)
            : base()
        {
            this.name = name;
            this.age = age;
        }

        public String Name => name;

        public Int32 Age => age;

        public void SayHi()
        {
            Console.WriteLine("Hi!");
        }

        private void PrivateSayHi()
        {
            Console.WriteLine("pss.. hi!");
        }

        private Boolean PrivateTrueGetter()
        {
            return true;
        }
    }

    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            GetMembers_Example("TestConsoleApplication.People, TestConsoleApplication");
        }

        private static void GetMembers_Example(String typeName)
        {
            var type = Type.GetType(typeName, true, false);

            foreach (MemberInfo memberInfo in type.GetMembers())
            {
                Console.WriteLine($"{memberInfo.DeclaringType} {memberInfo.MemberType} {memberInfo.Name}");
            }
        }
    }
}
