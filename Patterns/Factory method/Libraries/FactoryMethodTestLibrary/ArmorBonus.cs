namespace FactoryMethodTestLibrary
{
    public class ArmorBonus : Bonus
    {
        public ArmorBonus()
            : base()
        {
        }

        public override void Activate()
        {
            System.Console.WriteLine("Your person get more armor!");
        }
    }
}