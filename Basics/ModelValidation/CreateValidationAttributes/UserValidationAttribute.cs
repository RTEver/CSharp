using System;

using System.ComponentModel.DataAnnotations;

namespace CreateValidationAttributes
{
    internal sealed class UserValidationAttribute : ValidationAttribute
    {
        public override Boolean IsValid(Object value)
        {
            var user = value as User;

            if (user != null)
            {
                if (user.Name == "Admin" && user.Age < 18)
                {
                    ErrorMessage = "User can not has name 'Admin' and has age less 18";

                    return false;
                }

                return true;
            }

            return false;
        }
    }
}