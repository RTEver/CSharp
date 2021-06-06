using System;

namespace Tanks
{
    /// <summary>
    /// Base component
    /// </summary>
    public abstract class Tank : Object
    {
        private Int32 health;
        private Int32 armor;

        public Tank(Int32 health, Int32 armor)
            : base()
        {
            this.health = health;
            this.armor = armor;
        }

        public virtual Int32 GetHealth()
        {
            return health;
        }

        public virtual Int32 GetArmor()
        {
            return armor;
        }
    }
}
