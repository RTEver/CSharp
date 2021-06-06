using System;

namespace FactoryMethodTestLibrary
{
    public abstract class BonusFactory : Object
    {
        public BonusFactory()
            : base()
        {
        }

        public abstract Bonus CreateBonus();

        public override String ToString()
        {
            return $"Factory: {this.GetType()}";
        }
    }
}