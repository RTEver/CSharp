using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DelegateReflection
{
    internal delegate Object TwoInt32s(Int32 n1, Int32 n2);
    internal delegate Object OneString(String s1);

    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            if (args.Length < 2)
            {
                var usage = 
                    @"Usage:" +
                    "{0} delType methodName [Arg1] [Arg2]" +
                    "{0}   where delType must be TwoInt32s or OneString" +
                    "{0}   if delType is TwoInt32s, methodName must be Add or Subtract" +
                    "{0}   if delType is OneString, methodName must be NumChars or Reverse" +
                    "{0}" +
                    "{0} Examples:" +
                    "{0}   TwoInt32s Add 123 321" +
                    "{0}   TwoInt32s Subtract 123 321" +
                    "{0}   OneString Numchars \"Hello there\"" +
                    "{0}   OneString Reverse \"Hello there\"";

                Console.WriteLine(usage, Environment.NewLine);

                return;
            }

            var delType = Type.GetType(args[0]);
        }

        private static Object Add(Int32 n1, Int32 n2)
        {
            return n1 + n2;
        }

        private static Object Subtract(Int32 n1, Int32 n2)
        {
            return n1 - n2;
        }

        private static Object NumChars(String s1)
        {
            return s1.Length;
        }

        private static Object Reverse(String s1)
        {
            return new String(s1.Reverse().ToArray());
        }
    }
}