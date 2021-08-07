using System;

using System.ComponentModel.DataAnnotations;

namespace Basics
{
    internal sealed class User : Object
    {
        [Required(ErrorMessage = "User has no id")]
        public Int32 Id { get; set; }

        [Required()]
        [StringLength(10, MinimumLength = 2)]
        public String Name { get; set; }

        [Required()]
        [Range(0, 1000)]
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
    }
}