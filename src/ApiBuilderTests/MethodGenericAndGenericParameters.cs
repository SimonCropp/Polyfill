namespace Polyfills;

public static class MethodGenericAndGenericParameters
{
    extension<TKey, TValue>(ConcurrentDictionary<TKey, TValue> target)
        where TKey : notnull
    {
        /// <summary>
        /// Adds a key/value pair to the <see cref="ConcurrentDictionary{TKey,TValue}"/>
        /// if the key does not already exist.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd?view=net-10.0#system-collections-concurrent-concurrentdictionary-2-getoradd-1(-0-system-func((-0-0-1))-0)
        public TValue GetOrAdd<TArg>(TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument) =>
            default!;
    }
}
