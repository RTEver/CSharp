using System;

namespace Finalized_Objects
{
    internal class People : Object, IDisposable
    {
        private Boolean disposed;

        private String name;

        private Int32 age;

        public People(String name, Int32 age)
            : base()
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.name = name;

            this.age = age;

            disposed = false;
        }

        public String Name => name;

        public Int32 Age => age;

        public void SayHello()
        {
            Console.WriteLine("Hello!");
        }

        // Реализация интерфейса IDisposable.
        public void Dispose()
        {
            Dispose(true);

            // Подавляем финализацию.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Освобождаем управляемые ресурсы.
                }

                // Освобождаем неуправляемые объекты.

                disposed = true;
            }
        }

        // Деструктор.
        ~People()
        {
            Dispose(false);
        }
    }
}