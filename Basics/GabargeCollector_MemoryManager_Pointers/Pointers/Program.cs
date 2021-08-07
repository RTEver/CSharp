using System;

namespace Pointers
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example_1();
        }

        private static void Example_1()
        {
            unsafe
            {
                int* x = default(Int32*); // определение указателя

                Console.WriteLine("Address: {1}{0}Value: {2}{0}", Environment.NewLine, (UInt64)x, '-');

                int y = 10; // определяем переменную

                x = &y; // указатель x теперь указывает на адрес переменной y
                GetInfoAboutPointer(x);

                y = y + 20;
                GetInfoAboutPointer(x);

                *x = 50;
                GetInfoAboutPointer(x);
            }
        }
        
        private static void Example_2()
        {
            unsafe
            {
                int* x; // определение указателя

                int y = 10; // определяем переменную

                x = &y; // указатель x теперь указывает на адрес переменной y

                int** z = &x; // указатель z теперь указывает на адрес, который указывает и указатель x

                **z = **z + 40; // изменение указателя z повлечет изменение переменной y

                Console.WriteLine(y); // переменная y=50

                Console.WriteLine(**z); // переменная **z=50
            }
        }

        unsafe private static void GetInfoAboutPointer(Int32* pointer)
        {
            Console.WriteLine("Address: {1}{0}Value: {2}{0}",
                    Environment.NewLine, (UInt64)pointer, *pointer);
        }
    }
}
