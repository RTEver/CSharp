using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Basics
{
    internal static class Program : Object
    {
        private static Int32 userId;

        static Program()
        {
            userId = 1;
        }

        private static void Main(String[] args)
        {
            GetUser();
        }

        private static User GetUser()
        {
            var id = userId++;

            Console.Write("Enter age: ");
            var age = Int32.Parse(Console.ReadLine());

            Console.Write("Enter name: ");
            var name = Console.ReadLine();

            var user = new User()
            {
                Id = id,
                Age = age,
                Name = name,
            };

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(user);

            if (!Validator.TryValidateObject(user, context, validationResults, true))
            {
                foreach (ValidationResult result in validationResults)
                {
                    Console.WriteLine("{0}: {1}", result.MemberNames, result.ErrorMessage);
                }

                userId--;

                return null;
            }

            return user;
        }

        private static void GetUserInfo(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            Console.WriteLine("Id: {1}{0}" +
                "Name: {2}{0}" +
                "Age: {3}{0}",
                Environment.NewLine, user.Id, user.Name, user.Age);
        }
    }
}