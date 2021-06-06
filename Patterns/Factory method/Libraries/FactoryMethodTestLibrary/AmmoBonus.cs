namespace FactoryMethodTestLibrary
{
    public class AmmoBonus : Bonus
    {
        public AmmoBonus()
            : base()
        {
        }

        public override void Activate()
        {
            System.Console.WriteLine("Your person get more ammunition!");
        }
    }
}