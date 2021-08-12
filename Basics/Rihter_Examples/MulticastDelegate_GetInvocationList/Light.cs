using System;

namespace MulticastDelegate_GetInvocationList
{
    internal sealed class Light : Object
    {
        public Light()
            : base()
        { }

        public String SwitchPosition()
        {
            return "The light is off.";
        }
    }
}