namespace FactoryMethodTestLibrary
{
    public class ReloadSpeedBonus : Bonus
    {
        public ReloadSpeedBonus()
            : base()
        {
        }

        public override void Activate()
        {
            System.Console.WriteLine("Your person get speed potion!");
        }
    }
}