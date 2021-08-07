using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Work_with_IValidatableObject
{
    internal sealed class User : Object, IValidatableObject
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Int32 Age { get; set; }

        public User()
            : base()
        {
            Id = 0;
            Name = String.Empty;
            Age = 0;
        }

        public User(Int32 id, String name, Int32 age)
            : base()
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (age < 0)
            {
                throw new ArgumentOutOfRangeException("age");
            }

            Id = id;
            Name = name;
            Age = age;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (String.IsNullOrWhiteSpace(Name))
            {
                results.Add(new ValidationResult("No name"));
            }

            if (Id < 0)
            {
                results.Add(new ValidationResult("Incorrect identificator"));
            }

            if (Age < 0)
            {
                results.Add(new ValidationResult("Incorrect age"));
            }

            return results;
        }
    }
}