using System;

namespace Work_with_stackalloc_keyword
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            StackallocDemo();
            InlineArrayDemo();
        }

        private static void StackallocDemo()
        {
            unsafe
            {
                const Int32 width = 20;

                Char* pc = stackalloc Char[width];

                var name = "Vitaly Talataj";

                for (var index = 0; index < width; ++index)
                {
                    pc[width - index - 1] =
                        (index < name.Length) ? name[index] : '.';
                }

                Console.WriteLine(new String(pc, 0, width));
            }
        }

        private static void InlineArrayDemo()
        {
            unsafe
            {
                CharArray ca;

                var widthInBytes = sizeof(CharArray);
                var width = widthInBytes / 2;

                var name = "Vitaly Talataj";

                for (var index = 0; index < width; ++index)
                {
                    ca.Characters[width - index - 1] =
                        (index < name.Length) ? name[index] : '.';
                }

                Console.WriteLine(new String(ca.Characters, 0, width));
            }
        }

        internal unsafe struct CharArray
        {
            public fixed Char Characters[20];
        }
    }
}