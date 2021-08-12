using System;

/// <summary>
/// Dynamic arrays
/// </summary>
namespace Arrays_with_non_zero_boundary
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example();
        }

        private static void Example()
        {
            Int32[] lowerBounds = { 2005, 1 };
            Int32[] lengths = { 5, 4 };

            var quarterlyRevenue = (Decimal[,])Array.CreateInstance(typeof(Decimal), lengths, lowerBounds);

            var firstYear = quarterlyRevenue.GetLowerBound(0);
            var lastYear = quarterlyRevenue.GetUpperBound(0);
            var firstQuarter = quarterlyRevenue.GetLowerBound(1);
            var lastQuarter = quarterlyRevenue.GetUpperBound(1);

            Console.WriteLine("{0,4} {1,9} {2,9} {3,9} {4,9}",
                "Year", "Q1", "Q2", "Q3", "Q4");

            for (var year = firstYear; year <= lastYear; ++year)
            {
                Console.Write(year + " ");

                for (var quarter = firstQuarter; quarter <= lastQuarter; ++quarter)
                {
                    Console.Write("{0,9:C}", quarterlyRevenue[year, quarter]);
                }

                Console.WriteLine();
            }
        }
    }
}