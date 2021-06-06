namespace FactoryMethodTestLibrary
{
    public class DamageBonusFactory : BonusFactory
    {
        public DamageBonusFactory()
            : base()
        {
        }

        public override Bonus CreateBonus()
        {
            return new DamageBonus();
        }
    }
}