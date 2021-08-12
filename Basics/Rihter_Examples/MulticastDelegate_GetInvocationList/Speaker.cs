using System;

namespace MulticastDelegate_GetInvocationList
{
    internal sealed class Speaker : Object
    {
        public Speaker()
            : base()
        { }

        public String Volume()
        {
            return "The volume is loud.";
        }
    }
}