using System;

namespace SqlCommand
{
    internal sealed class User : Object
    {
        private Int32 id;

        private String name;

        private Int32 age;

        internal User(Int32 id, String name, Int32 age)
            : base()
        {
            this.id = id;
            this.name = name;
            this.age = age;
        }

        internal Int32 Id
        {
            get => id;

            set => id = value;
        }

        internal String Name => name;

        internal Int32 Age => age;

        public override String ToString()
        {
            return $"{id}. {name} ({age} years)";
        }
    }
}