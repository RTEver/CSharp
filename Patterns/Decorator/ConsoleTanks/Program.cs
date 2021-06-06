using System;
using Tanks;

namespace ConsoleTanks
{
    public static class Program : Object
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Test");

            Tank lightTank = new LightTank();
            Tank heavyTank = new HeavyTank();

            OutputTankInfo(lightTank);
            OutputTankInfo(heavyTank);

            Tank ligthTankWithArmorBonusLvl1 = new ArmorBonus(lightTank);
            Tank heavyTankWithArmorBonusLvl1 = new ArmorBonus(heavyTank);

            OutputTankInfo(ligthTankWithArmorBonusLvl1);
            OutputTankInfo(heavyTankWithArmorBonusLvl1);

            Tank ligthTankWithArmorBonusLvl2 = new ArmorBonus(ligthTankWithArmorBonusLvl1);
            Tank heavyTankWithArmorBonusLvl2 = new ArmorBonus(heavyTankWithArmorBonusLvl1);

            OutputTankInfo(ligthTankWithArmorBonusLvl2);
            OutputTankInfo(heavyTankWithArmorBonusLvl2);
        }

        public static void OutputTankInfo(Tank tank)
        {
            Console.WriteLine($"Tank: {tank.GetType().ToString()}");

            Console.WriteLine($"Health: {tank.GetHealth()}");

            Console.WriteLine($"Armor: {tank.GetArmor()}");
        }
    }
}
