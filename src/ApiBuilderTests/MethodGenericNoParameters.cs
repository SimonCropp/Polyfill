namespace Polyfills;

public static class MethodGenericNoParameters
{
    extension<T>(ConcurrentBag<T> target)
    {
        /// <summary>
        /// Removes all values from the <see cref="ConcurrentBag{T}"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentbag-1.clear?view=net-10.0
        public void Clear()
        {
            while (!target.IsEmpty)
            {
                target.TryTake(out _);
            }
        }
    }
}
