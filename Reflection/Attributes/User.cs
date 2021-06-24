using System;

namespace Attributes
{
    [AgeValidation(18)]
    internal sealed class User : Object, ISayable
    {
        private String name;
        private Int32 age;

        public User(String name, Int32 age)
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
}
