using System;

namespace Part_1_Migrations
{
    public static class Program : Object
    {
        public static void Main(String[] args)
        {
            var factory = new SampleContextFactory();

            using (var context = factory.CreateDbContext(null))
            {
                ShowUsers(context);
            }
        }

        private static void ShowUsers(ApplicationContext context)
        {
            var users = context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Id + ". " + user.Name + " — " + user.Age);
            }
        }

        private static void AddUser(ApplicationContext context, String name, Int32 age)
        {
            var users = context.Users;

            var user = new User()
            {
                Name = name,
                Age = age,
            };

            users.Add(user);

            context.SaveChanges();
        }
    }
}