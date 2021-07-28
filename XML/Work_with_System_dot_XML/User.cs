using System;

namespace Work_with_System_dot_XML
{
    public sealed class User : Object
    {
        public String Name { get; set; }

        public Int32 Age { get; set; }

        public String Company { get; set; }
        
        public User()
            : base()
        { }

        public override String ToString()
        {
            return $"Name: {Name}\n" +
                $"Age: {Age}\n" +
                $"Company: {Company}\n";
        }
    }
}