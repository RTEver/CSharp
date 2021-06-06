using System;
using System.Collections.Generic;
using System.Text;

namespace Tanks
{
    /// <summary>
    /// Concrete component
    /// </summary>
    public class LightTank : Tank
    {
        public LightTank()
            : base(10, 1)
        {
        }
    }
}
