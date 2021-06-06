using System;
using System.Collections.Generic;
using System.Text;

namespace Tanks
{
    /// <summary>
    /// Concrete component
    /// </summary>
    public class HeavyTank : Tank
    {
        public HeavyTank()
            : base(20, 3)
        {
        }
    }
}
