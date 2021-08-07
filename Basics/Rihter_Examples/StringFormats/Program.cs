using System;
using System.Text;
using System.Threading;

namespace StringFormats
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var sb = new StringBuilder();

            sb.AppendFormat(new BoldInt32s(), "{0} {1} {2:M}", "Jeff", 123, DateTime.Now);

            Console.WriteLine(sb);
        }
    }

    internal sealed class BoldInt32s : IFormatProvider, ICustomFormatter
    {
        public Object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
        }

        public String Format(String format, Object arg, IFormatProvider formatProvider)
        {
            String s;

            IFormattable formattable = arg as IFormattable;

            if (formattable == null)
            {
                s = arg.ToString();
            }
            else
            {
                s = formattable.ToString(format, formatProvider);
            }

            if (arg.GetType() == typeof(Int32))
            {
                return "<B>" + s + "</B>";
            }

            return s;
        }
    }
}