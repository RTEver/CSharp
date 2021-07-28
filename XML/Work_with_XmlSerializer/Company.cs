using System;

namespace Work_with_XmlSerializer
{
    [Serializable()]
    public sealed class Company : Object
    {
        public String Name { get; set; }

        public Company()
            : base()
        { }

        public Company(String name)
            : base()
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }
    }
}