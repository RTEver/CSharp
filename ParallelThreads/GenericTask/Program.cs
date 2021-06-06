using System;
using System.Threading.Tasks;

namespace GenericTask
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Console.WriteLine("Starting Main...");

            Int32 x = 0;

            do
            {
                Console.Write("Enter integer number: ");

                String input = Console.ReadLine();

                if (!Int32.TryParse(input, out x))
                {
                    Console.WriteLine("Input string not matching to integer value!");
                }
                else
                {
                    break;
                }

            } while (true);

            Console.WriteLine("Starting computing...");

            Task<Int32> computingFactorial = Task<Int32>.Factory.StartNew(() => Factorial(x));

            // Stoping main thread while computing (task working)
            Int32 result = computingFactorial.Result;

            Console.WriteLine("Ending computing...");

            Console.WriteLine("Factorial {0} = {1}", x, result);

            Console.WriteLine("Ending Main...");

            Console.ReadLine();
        }

        private static Int32 Factorial(Int32 x)
        {
            if (x <= 0)
            {
                throw new ArgumentOutOfRangeException("x", x, "x should be bigger than 0!");
            }

            return (x != 1) ? x * Factorial(x - 1) : 1;
        }
    }
}
