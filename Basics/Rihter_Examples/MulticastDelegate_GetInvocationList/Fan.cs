using System;

namespace MulticastDelegate_GetInvocationList
{
    internal sealed class Fan : Object
    {
        public Fan()
            : base()
        { }

        public String Speed()
        {
            throw new InvalidOperationException("The fan broke due to overheating.");
        }
    }
}