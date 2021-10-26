using System;

namespace Part_1_Migrations
{
    public sealed class User : Object
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Int32 Age { get; set; }
    }
}