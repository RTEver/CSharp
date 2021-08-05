using System;

namespace CustomMathLibrary
{
    public static class CustomMath : Object
    {
        public static Int32 Factorial(Int32 n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n");
            }

            return (n > 1) ? n * Factorial(n - 1) : 1;
        }
    }
}