using System;

namespace Introduction
{
    internal sealed class Temp : Object
    {
        public dynamic X { get; set; }

        public override string ToString()
        {
            return X.ToString();
        }
    }

    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            ExecutablePart();
        }
        
        private static void ExecutablePart()
        {
            dynamic x = null;

            Output(x);

            Output(x = 5);

            Output(x = 5.0);

            Output(x = 5.0M);

            Output(x = "Hello, World!");

            Output(x = '\u1232');

            Output(x = 5.501f);

            Output(x = new Temp() { X = "Hello, World! (v2.0)" });

            Output(x = new { Name = "Hello, World! (v3.0)" });
        }

        private static void Output(dynamic x)
        {
            Console.WriteLine("x = " + x + $" ({x?.GetType()})");
        }
    }
}
