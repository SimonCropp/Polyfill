#if !NET11_0_OR_GREATER && (NET471_OR_GREATER || NETCOREAPP || NETSTANDARD2_1_OR_GREATER)

namespace Polyfills;

using System.Runtime.CompilerServices;

static partial class Polyfill
{
    extension(RuntimeFeature)
    {
        /// <summary>
        /// Gets a value that indicates whether the runtime supports multithreading.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.runtimefeature.ismultithreadingsupported?view=net-11.0
        public static bool IsMultithreadingSupported => true;
    }
}

#endif
