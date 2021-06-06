namespace FactoryMethodTestLibrary
{
    public class ReloadSpeedBonusFactory : BonusFactory
    {
        public ReloadSpeedBonusFactory()
            : base()
        {
        }

        public override Bonus CreateBonus()
        {
            return new ReloadSpeedBonus();
        }
    }
}