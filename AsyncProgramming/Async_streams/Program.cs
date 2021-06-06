using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_streams
{
    internal class Repository : Object
    {
        private readonly String[] data;

        public Repository()
            : base()
        {
            data = new[]
            {
                "Vitaly",
                "Andrey",
                "Denis",
                "Alexandre",
                "Danila",
            };
        }

        public async IAsyncEnumerable<String> GetDataAsync()
        {
            for (var i = 0; i < data.Length; ++i)
            {
                Console.WriteLine($"Get {i + 1} element from data-massive in GetDataAsync method.");

                await Task.Delay(500);

                yield return data[i];
            }
        }
    }

    internal static class Program : Object
    {
        private static async Task Main(String[] args)
        {
            var repo = new Repository();

            await foreach (String name in repo.GetDataAsync())
            {
                Console.WriteLine(name);
            }
        }
    }

    // Analog without async
    /*
        internal class Repository : Object
        {
            private readonly String[] data;

            public Repository()
                : base()
            {
                data = new[]
                {
                    "Vitaly",
                    "Andrey",
                    "Denis",
                    "Alexandre",
                    "Danila",
                };
            }

            public IEnumerable<String> GetData()
            {
                for (var i = 0; i < data.Length; ++i)
                {
                    Console.WriteLine($"Get {i + 1} element from data-massive in GetData method.");

                    Thread.Sleep(500);

                    yield return data[i];
                }
            }
        }

        internal static class Program : Object
        {
            private static void Main(String[] args)
            {
                var repo = new Repository();

                foreach (String name in repo.GetData())
                {
                    Console.WriteLine(name);
                }
            }
        }
     */
}
