using System;

namespace GarbageCollector_in_CSharp
{
    internal class People : Object
    {
        private String name;

        private Int32 age;

        public People(String name, Int32 age)
            : base()
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.name = name;

            this.age = age;
        }

        public String Name => name;

        public Int32 Age => age;

        public void SayHello()
        {
            Console.WriteLine("Hello!");
        }
    }
}
