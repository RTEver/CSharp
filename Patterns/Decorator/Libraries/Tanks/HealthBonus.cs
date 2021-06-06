using System;
using System.Collections.Generic;
using System.Text;

namespace Tanks
{
    /// <summary>
    /// Concrete decorator
    /// </summary>
    public class HealthBonus : Bonus
    {
        public HealthBonus(Tank tank)
            : base(tank)
        {
        }

        public override Int32 GetHealth()
        {
            return base.GetHealth() + 5;
        }
    }
}
