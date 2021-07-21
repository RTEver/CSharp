using System;

namespace Work_with_BinaryFormatter
{
    [Serializable()]
    internal class Person : Object
    {
        public String Name { get; set; }
        public Int32 Age { get; set; }

        public Person(String name, Int32 age)
        {
            Name = name;
            Age = age;
        }
    }
}
