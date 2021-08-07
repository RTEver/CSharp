using System;

namespace LINQ_to_XML
{
    internal sealed class Phone : Object
    {
        public String Name { get; set; }

        public Int32 Price { get; set; }

        public String Company { get; set; }

        public Phone()
            : base()
        { }
    }
}