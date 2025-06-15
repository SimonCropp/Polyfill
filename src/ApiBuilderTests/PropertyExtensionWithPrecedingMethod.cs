
namespace Polyfills;
public static class PropertyExtensionWithPrecedingMethod
{
    extension(Delegate)
    {
        /// <summary>
        /// Gets an enumerator for the invocation targets of this delegate.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.delegate.enumerateinvocationlist?view=net-10.0
        public static bool EnumerateInvocationList<TDelegate>(TDelegate? target)
            where TDelegate : Delegate =>
            false;
    }
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool HasSingleTarget => true;
    }
}
