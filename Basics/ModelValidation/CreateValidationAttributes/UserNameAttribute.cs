using System;

using System.ComponentModel.DataAnnotations;

namespace CreateValidationAttributes
{
    internal sealed class UserNameAttribute : ValidationAttribute
    {
        public override Boolean IsValid(Object value)
        {
            if (value != null)
            {
                var userName = value as String;

                if (userName.StartsWith("A"))
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "Name of user not start with 'A'";
                }
            }

            return false;
        }
    }
}