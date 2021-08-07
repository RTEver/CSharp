using System;

namespace Pointers_on_Structures_and_Classes_and_Arrays
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example_3();
        }

        private static void Example_1()
        {
            unsafe
            {
                People people;

                people.age = 18;

                var p = &people;

                p->age = 20;

                Console.WriteLine(p->age);

                (*p).age = 21;

                Console.WriteLine((*p).age);
            }
        }

        private static void Example_2()
        {
            unsafe
            {
                const Int32 size = 7;

                var factorial = stackalloc Int32[size]; // выделяем память в стеке под семь объектов Int32

                var p = factorial;

                *(p++) = 1; // присваиваем первой ячейке значение 1 и
                            // увеличиваем указатель на 1

                for (var i = 2; i <= size; i++, p++)
                {
                    // считаем факториал числа
                    *p = p[-1] * i;
                }

                for (var i = 0; i < size; ++i)
                {
                    Console.WriteLine(factorial[i]);
                }
            }
        }

        private static void Example_3()
        {
            unsafe
            {
                Person person = new Person();

                person.age = 28;

                person.height = 178;

                // блок фиксации указателя
                // Оператор fixed создает блок, в котором фиксируется указатель на поле объекта person.
                // После завершения блока fixed закрепление с переменных снимается, и они могут быть подвержены сборке мусора.
                fixed (int* p = &person.age)
                {
                    if (*p < 30)
                    {
                        *p = 30;
                    }
                }

                Console.WriteLine(person.age); // 30
            }
        }

        private static void Example_4()
        {
            unsafe
            {
                int[] nums = { 0, 1, 2, 3, 7, 88 };

                var str = "Привет мир";

                fixed (int* p = nums)
                {
                    int third = *(p + 2);     // получим третий элемент
                    Console.WriteLine(third); // 2
                }

                fixed (char* p = str)
                {
                    char forth = *(p + 3);    // получим четвертый элемент
                    Console.WriteLine(forth); // в
                }
            }
        }
    }
}