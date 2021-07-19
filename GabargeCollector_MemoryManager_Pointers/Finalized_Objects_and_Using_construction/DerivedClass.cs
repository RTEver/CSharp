using System;

namespace Finalized_Objects
{
    internal sealed class DerivedClass : People
    {
        private Boolean disposed;

        public DerivedClass(String name, Int32 age)
            : base(name, age)
        {
            disposed = false;
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Clearing unmanaged resources
                }

                // Clearing managed resources

                disposed = true;

                base.Dispose(disposing);
            }
        }
    }
}