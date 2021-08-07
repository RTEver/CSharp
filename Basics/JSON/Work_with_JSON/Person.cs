using System;

namespace Work_with_JSON
{
    internal class Person : Object
    {
        private String name;
        private Int32 age;

        public Person()
            : base()
        {
            name = null;
            age = 0;
        }

        public Person(String name, Int32 age)
            : base()
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (age < 0)
            {
                throw new ArgumentOutOfRangeException("age");
            }

            this.name = name;
            this.age = age;
        }

        public String Name
        {
            get => name ?? "null";

            set => name = value;
        }

        public Int32 Age
        {
            get => age;

            set
            {
                if (value < 0)
                {
                    age = 0;
                }
                else
                {
                    age = value;
                }
            }
        }
    }
}
