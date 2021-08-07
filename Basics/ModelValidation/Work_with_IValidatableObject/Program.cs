using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Work_with_IValidatableObject
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example();
        }

        private static void Example()
        {
            var user = new User()
            {
                Id = -1,
                Name = "",
                Age = -4,
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (ValidationResult result in results)
                {
                    Console.WriteLine(result.ErrorMessage);
                }
            }
            else
            {
                Console.WriteLine("User valid");
            }
        }
    }
}