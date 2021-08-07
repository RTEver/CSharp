using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace CreateValidationAttributes
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            ExecutablePart();
        }

        private static void ExecutablePart()
        {
            var users = new[]
            {
                new User(1, "Vitaly", 21),
                new User(2, "Andrew", 20),
                new User(3, "Vlad", 15),
                new User(4, "Stas", 15),
                new User(5, "Admin", 15),
                new User(),
            };

            foreach (User user in users)
            {
                Console.WriteLine($"'{user.Name}'");

                Validate(user);
            }
        }

        private static void Validate(User user)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (ValidationResult result in results)
                {
                    Console.WriteLine('\t' + result.ErrorMessage);
                }
            }
            else
            {
                Console.WriteLine('\t' + "User valid");
            }
        }
    }
}