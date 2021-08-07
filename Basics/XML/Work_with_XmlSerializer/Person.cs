using System;

namespace Work_with_XmlSerializer
{
    [Serializable()]
    public sealed class Person : Object
    {
        public String Name { get; set; }

        public Int32 Age { get; set; }

        public Company Company { get; set; }

        public Person()
            : base()
        { }

        public Person(String name, Int32 age, Company company)
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

            if (company == null)
            {
                throw new ArgumentNullException("company");
            }

            Name = name;
            Age = age;
            Company = company;
        }
    }
}