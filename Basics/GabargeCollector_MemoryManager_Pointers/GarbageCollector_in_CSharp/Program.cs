using System;

namespace GarbageCollector_in_CSharp
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            Example();
        }

        private static void Example()
        {
            GetTotalMemoryInfo();

            for (var entity = 0; entity < 10000; ++entity)
            {
                CreateEntity();
            }

            GetTotalMemoryInfo();

            GC.Collect(1, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

            GetTotalMemoryInfo();
        }

        private static void CreateEntity()
        {
            var people = new People("Vasily", 12);
        }

        private static void GetTotalMemoryInfo()
        {
            var totalMemory = GC.GetTotalMemory(false);

            Console.WriteLine(totalMemory);
        }
    }
}
