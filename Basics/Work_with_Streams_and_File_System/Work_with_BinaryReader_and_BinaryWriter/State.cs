using System;

namespace Work_with_BinaryReader_and_BinaryWriter
{
    internal struct State
    {
        private String name;
        private String capital;
        private Int32  area;
        private Double people;

        public State(String name, String capital, Int32 area, Double people)
        {
            this.name    = name;
            this.capital = capital;
            this.people  = people;
            this.area    = area;
        }

        public String Name => name;

        public String Capital => capital;

        public Int32 Area => area;

        public Double People => people;
    }
}
