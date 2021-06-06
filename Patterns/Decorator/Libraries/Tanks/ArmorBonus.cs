using System;
using System.Collections.Generic;
using System.Text;

namespace Tanks
{
    /// <summary>
    /// Concrete decorator
    /// </summary>
    public class ArmorBonus : Bonus
    {
        public ArmorBonus(Tank tank)
            : base(tank)
        {
        }

        public override Int32 GetArmor()
        {
            return base.GetArmor() + 1;
        }
    }
}
