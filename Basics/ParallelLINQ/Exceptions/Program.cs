using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Exceptions
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Console.WriteLine(                      "+++++++++++++++++++++++++++++" + Environment.NewLine);

            Exercise_1();

            Console.WriteLine(Environment.NewLine + "+++++++++++++++++++++++++++++" + Environment.NewLine);

            Exercise_2();

            Console.WriteLine(Environment.NewLine + "+++++++++++++++++++++++++++++" + Environment.NewLine);

            Exercise_3();

            Console.WriteLine(Environment.NewLine + "+++++++++++++++++++++++++++++" + Environment.NewLine);

            Exercise_4();

            Console.WriteLine(Environment.NewLine + "+++++++++++++++++++++++++++++");
        }

        private static Int32 Factorial(Int32 n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return (n > 1) ? n * Factorial(n - 1) : 1;
        }

        private static void Exercise_1()
        {
            var numbers = new Object[] { 1, 2, 3, 4, 5, "hello", "World!" };

            var factorials = from number in numbers.AsParallel<Object>()
                             let x = (Int32)number
                             select new
                             {
                                 StartNumber = number,
                                 Result = Factorial(x),
                             };
            try
            {
                factorials.ForAll(factorial => Console.WriteLine(factorial.StartNumber + "! = " + factorial.Result));
            }
            catch (AggregateException ex)
            {
                foreach (Exception e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void Exercise_2()
        {
            var cts = new CancellationTokenSource();

            new Task(() =>
            {
                Thread.Sleep(4);
                cts.Cancel();
            }).Start();

            try
            {
                var numbers = new Int32[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

                var factorials = from number in numbers.AsParallel().AsOrdered<Int32>().WithCancellation(cts.Token)
                                 select Factorial(number);

                foreach (Int32 number in factorials)
                {
                    Console.WriteLine(number);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Операция была прервана");
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions != null)
                {
                    foreach (Exception e in ex.InnerExceptions)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Non inner exception...");
                }
            }
            finally
            {
                cts.Dispose();
            }
        }

        private static void Exercise_3()
        {
            var cts = new CancellationTokenSource();

            new Task(() =>
            {
                Thread.Sleep(4);
                cts.Cancel();
            }).Start();

            try
            {
                var numbers = new Int32[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

                var factorials = from number in numbers.AsParallel().WithCancellation(cts.Token)
                                 select Factorial(number);

                foreach (Int32 number in factorials)
                {
                    Console.WriteLine(number);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Операция была прервана");
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions != null)
                {
                    foreach (Exception e in ex.InnerExceptions)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Non inner exception...");
                }
            }
            finally
            {
                cts.Dispose();
            }
        }

        private static void Exercise_4()
        {
            var cts = new CancellationTokenSource();

            new Task(() =>
            {
                Thread.Sleep(10000);
                cts.Cancel();
            }).Start();

            try
            {
                var numbers = new Object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, "Hello", "World!" };

                var factorials = from number in numbers.AsParallel<Object>().AsOrdered<Object>().WithCancellation(cts.Token)
                                 let x = (Int32)number
                                 select Factorial(x);

                foreach (Int32 number in factorials)
                {
                    Console.WriteLine(number);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Операция была прервана");
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions != null)
                {
                    foreach (Exception e in ex.InnerExceptions)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Non inner exception...");
                }
            }
            finally
            {
                cts.Dispose();
            }
        }
    }
}
