#if !NETCOREAPP && !NETSTANDARD2_1_OR_GREATER

namespace Polyfills;

using System.Collections.Concurrent;

static partial class Polyfill
{
    /// <summary>
    /// Removes all values from the <see cref="ConcurrentQueue{T}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentqueue-1.clear?view=net-11.0
    public static void Clear<T>(this ConcurrentQueue<T> target)
    {
        while (target.TryDequeue(out _));
    }
}
#endif
