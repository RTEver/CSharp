using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    internal sealed class AgeValidationAttribute : Attribute
    {
        private Int32 age;

        public AgeValidationAttribute(Int32 age)
            : base()
        {
            if (age < 0)
            {
                throw new ArgumentOutOfRangeException("'age'", age, "Variable 'age' has null value.");
            }

            this.age = age;
        }

        public Int32 Age => age;
    }
}
