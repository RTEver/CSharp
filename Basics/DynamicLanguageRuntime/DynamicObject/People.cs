using System;
using System.Dynamic;
using System.Collections.Generic;

namespace DynamicObject
{
    public sealed class People : System.Dynamic.DynamicObject
    {
        private Dictionary<String, Object> members;

        public People()
            : base()
        {
            members = new Dictionary<String, Object>();
        }

        public override Boolean TryGetMember(GetMemberBinder binder, out Object result)
        {
            result = null;

            if (members.ContainsKey(binder.Name))
            {
                result = members[binder.Name];

                return true;
            }

            return false;
        }

        public override Boolean TrySetMember(SetMemberBinder binder, Object value)
        {
            if (members.ContainsKey(binder.Name))
            {
                members[binder.Name] = value;
            }
            else
            {
                members.Add(binder.Name, value);
            }

            return true;
        }

        //public override Boolean TryInvoke(InvokeBinder binder, Object[] args, out Object result)
        //{
        //    // Reflection creating

        //    return base.TryInvoke(binder, args, out result);
        //}
    }
}
