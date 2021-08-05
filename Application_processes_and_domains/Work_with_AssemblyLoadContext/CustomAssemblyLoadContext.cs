using System.Runtime.Loader;

namespace Work_with_AssemblyLoadContext
{
    internal sealed class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public CustomAssemblyLoadContext()
            : base(isCollectible: true)
        { }
    }
}