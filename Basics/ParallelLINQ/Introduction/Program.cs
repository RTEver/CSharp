using System;
using System.Linq;

namespace Introduction
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Console.WriteLine(Environment.NewLine + "------------------------------" + Environment.NewLine);
            
            Console.WriteLine("ForAll" + Environment.NewLine);

            Example_1();

            Console.WriteLine(Environment.NewLine + "------------------------------" + Environment.NewLine);

            Console.WriteLine("AsOrdered with ForAll" + Environment.NewLine);

            Example_2();

            Console.WriteLine(Environment.NewLine + "------------------------------" + Environment.NewLine);

            Console.WriteLine("AsOrdered with foreach" + Environment.NewLine);

            Example_2_2();

            Console.WriteLine(Environment.NewLine + "------------------------------" + Environment.NewLine);

            Console.WriteLine("AsOrdered + AsUnordered with foreach" + Environment.NewLine);

            Example_2_3();

            Console.WriteLine(Environment.NewLine + "------------------------------" + Environment.NewLine);

            Console.WriteLine("AsUnordered" + Environment.NewLine);

            Example_3();

            Console.WriteLine(Environment.NewLine + "------------------------------" + Environment.NewLine);

            Console.WriteLine("AsOrdered result" + Environment.NewLine);

            Example_3_2();

            Console.WriteLine(Environment.NewLine + "------------------------------" + Environment.NewLine);

            Console.WriteLine("AsOrdered with AsUnordered" + Environment.NewLine);

            Example_3_3();

            Console.WriteLine(Environment.NewLine + "------------------------------" + Environment.NewLine);
        }

        private static Int32 Factorial(Int32 n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return (n > 1) ? n * Factorial(n - 1) : 1;
        }

        private static void Example_1()
        {
            var numbers = new[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            (
             from number in numbers.AsParallel<Int32>()
             where number >= 0
             select ("Factorial " + number + " = " + Factorial(number))
            ).ForAll<String>(Console.WriteLine);
        }

        private static void Example_2()
        {
            var numbers = new[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            (
             from number in numbers.AsParallel<Int32>().AsOrdered<Int32>()
             where number >= 0
             select ("Factorial " + number + " = " + Factorial(number))
            ).ForAll<String>(Console.WriteLine);
        }

        private static void Example_2_2()
        {
            var numbers = new[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var factorials = from number in numbers.AsParallel<Int32>().AsOrdered<Int32>()
                             where number >= 0
                             select ("Factorial " + number + " = " + Factorial(number));

            foreach (String result in factorials)
            {
                Console.WriteLine(result);
            }
        }

        private static void Example_2_3()
        {
            var numbers = new[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var factorials = from number in numbers.AsParallel<Int32>().AsOrdered<Int32>().AsUnordered<Int32>()
                             where number >= 0
                             select ("Factorial " + number + " = " + Factorial(number));

            foreach (String result in factorials)
            {
                Console.WriteLine(result);
            }
        }

        private static void Example_3()
        {
            var numbers = new[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var factorials = from number in numbers.AsParallel<Int32>().AsOrdered<Int32>()
                             where number >= 0
                             select Factorial(number);

            (
             from factorial in factorials.AsUnordered<Int32>()
             where factorial > 100
             select factorial
            ).ForAll<Int32>(Console.WriteLine);
        }

        private static void Example_3_2()
        {
            var numbers = new[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var factorials = from number in numbers.AsParallel<Int32>().AsOrdered<Int32>()
                             where number >= 0
                             select Factorial(number);

            (
             from factorial in factorials
             where factorial > 100
             select factorial
            ).ForAll<Int32>(Console.WriteLine);
        }

        private static void Example_3_3()
        {
            var numbers = new[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var factorials = from number in numbers.AsParallel<Int32>().AsOrdered<Int32>().AsUnordered<Int32>()
                             where number >= 0
                             select Factorial(number);

            (
             from factorial in factorials
             where factorial > 100
             select factorial
            ).ForAll<Int32>(Console.WriteLine);
        }
    }
}