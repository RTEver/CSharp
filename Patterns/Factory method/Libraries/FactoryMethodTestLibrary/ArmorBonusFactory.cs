namespace FactoryMethodTestLibrary
{
    public class ArmorBonusFactory : BonusFactory
    {
        public ArmorBonusFactory()
            : base()
        {
        }

        public override Bonus CreateBonus()
        {
            return new ArmorBonus();
        }
    }
}