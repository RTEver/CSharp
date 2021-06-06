using System;

namespace FactoryMethodTestLibrary
{
    public abstract class Bonus : Object
    {
        public Bonus()
            : base()
        {
        }

        public abstract void Activate();

        public override String ToString()
        {
            return $"Bonus: {this.GetType()}";
        }
    }
}