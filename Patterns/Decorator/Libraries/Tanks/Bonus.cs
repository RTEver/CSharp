using System;
using System.Collections.Generic;
using System.Text;

namespace Tanks
{
    /// <summary>
    /// Base decorator
    /// </summary>
    public abstract class Bonus : Tank
    {
        private Tank tank;

        public Bonus(Tank tank)
            : base(tank.GetHealth(), tank.GetArmor())
        {
            this.tank = tank;
        }
    }
}
