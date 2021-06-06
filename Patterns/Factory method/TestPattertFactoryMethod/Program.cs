using System;
using System.Collections.Generic;
using FactoryMethodTestLibrary;

namespace TestPattertFactoryMethod
{
    public static class Program : Object
    {
        public static void Main(String[] args)
        {
            var factories = new List<BonusFactory>
            {
                new AmmoBonusFactory(),
                new ArmorBonusFactory(),
                new DamageBonusFactory(),
                new ReloadSpeedBonusFactory(),
            };

            foreach (BonusFactory factory in factories)
            {
                Console.WriteLine(factory);

                // Create bonus
                var bonus = factory.CreateBonus();

                Console.WriteLine(bonus);

                // Activate bonus
                bonus.Activate();

                Console.WriteLine();
            }

            Bonus bonus_1 = new ArmorBonus();

            ArmorBonus bonus_2 = bonus_1 as ArmorBonus;

            Console.WriteLine(bonus_1.Equals(bonus_2));
        }
    }
}
