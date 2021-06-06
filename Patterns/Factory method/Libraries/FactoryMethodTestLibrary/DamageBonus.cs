namespace FactoryMethodTestLibrary
{
    public class DamageBonus : Bonus
    {
        public DamageBonus()
            : base()
        {
        }

        public override void Activate()
        {
            System.Console.WriteLine("Your person get power!");
        }
    }
}