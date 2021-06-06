namespace FactoryMethodTestLibrary
{
    public class AmmoBonusFactory : BonusFactory
    {
        public AmmoBonusFactory()
            : base()
        {
        }

        public override Bonus CreateBonus()
        {
            return new AmmoBonus();
        }
    }
}