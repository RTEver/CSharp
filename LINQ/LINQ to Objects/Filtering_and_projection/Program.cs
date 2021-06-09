using System;
using System.Linq;

namespace Filtering_and_projection
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example_4();
        }

        private static void Example_1()
        {
            var users = new[]
            {
                new { Name = "Vitaly", Age = 20 },
                new { Name = "Andrey", Age = 19 },
                new { Name = "Vasily", Age = 21 },
                new { Name = "Alexey", Age = 21 },
                new { Name = "Denis",  Age = 20 },
                new { Name = "Maxim",  Age = 19 },
            };

            var users_filtered = from user in users
                        let name = "Mr. " + user.Name
                        where user.Name.StartsWith("V")
                        orderby name
                        select new
                        {
                            Name = name,
                            Age = user.Age,
                        };

            foreach (var user in users_filtered)
            {
                Console.WriteLine("{0} - {1}", user.Name, user.Age);
            }
        }

        private static void Example_2()
        {
            var users = new[]
            {
                new { Name = "Vitaly", Age = 20 },
                new { Name = "Andrey", Age = 19 },
                new { Name = "Denis" , Age = 21 },
                new { Name = "Maxim" , Age = 19 },
                new { Name = "Alexey", Age = 22 },
                new { Name = "Putin" , Age = 46 },
            };

            var phones = new[]
            {
                new { Name = "Apple iPhone 12 mini"   , Company = "Apple"   },
                new { Name = "Apple iPhone 12"        , Company = "Apple"   },
                new { Name = "Apple iPhone 12 Pro"    , Company = "Apple"   },
                new { Name = "Apple iPhone 12 Pro Max", Company = "Apple"   },
                new { Name = "Apple iPhone SE 2020"   , Company = "Apple"   },
                new { Name = "Huawei Mate 40E"        , Company = "Huawei"  },
                new { Name = "Huawei Mate X2"         , Company = "Huawei"  },
                new { Name = "Lenovo Legion 2 Pro"    , Company = "Lenovo"  },
                new { Name = "Samsung Galaxy A22 5G"  , Company = "Samsung" },
                new { Name = "Samsung Galaxy A22 4G"  , Company = "Samsung" },
            };

            var selection = from user in users
                            from phone in phones
                            select new
                            {
                                User = user,
                                Phone = phone
                            };

            Console.WriteLine("Result of selection:");

            foreach (var select in selection)
            {
                Console.WriteLine("{0} {2} {1}", select.User.Name, select.Phone.Name, '—');
            }
        }

        private static void Example_3()
        {
            var users = new[]
            {
                new { Name = "Vitaly", Age = 20 },
                new { Name = "Andrey", Age = 19 },
                new { Name = "Denis" , Age = 21 },
                new { Name = "Maxim" , Age = 19 },
                new { Name = "Alexey", Age = 22 },
                new { Name = "Putin" , Age = 46 },
            };

            var names = from user in users
                        select user.Name;

            // or

            var names_2 = users.Select(user => user.Name);

            foreach (String name in names)
            {
                Console.WriteLine($"{name}");
            }

            Console.WriteLine("----------------------");

            foreach (String name in names_2)
            {
                Console.WriteLine($"{name}");
            }
        }

        private static void Example_4()
        {
            var users = new[]
            {
                new { Name = "Vitaly", Languages = new[] { "English", "Russian", "Chinease", "Japanise" } },
                new { Name = "Andrey", Languages = new[] { "English", "Russian" } },
                new { Name = "Alexey", Languages = new[] { "Russian" } },
                new { Name = "Denis" , Languages = new[] { "English", "Russian", "Polska" } },
            };

            var selection = from user in users
                            from lang in user.Languages
                            where user.Languages.Length > 1
                            select new
                            {
                                Name = user.Name,
                                Language = lang,
                            };

            foreach (var user in selection)
            {
                Console.WriteLine($"{user.Name} - {user.Language}");
            }
        }
    }
}