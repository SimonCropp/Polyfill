namespace Polyfills;

using System;

static partial class Polyfill
{
    extension(ObjectDisposedException)
    {
#if !NET7_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.objectdisposedexception.throwif?view=net-11.0##system-objectdisposedexception-throwif(system-boolean-system-object)
        public static void ThrowIf(bool condition, object instance)
        {
            if (condition)
            {
                throw new ObjectDisposedException(instance.GetType().FullName);
            }
        }

        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.objectdisposedexception.throwif?view=net-11.0##system-objectdisposedexception-throwif(system-boolean-system-type)
        public static void ThrowIf(bool condition, Type type)
        {
            if (condition)
            {
                throw new ObjectDisposedException(type.FullName);
            }
        }
#endif
    }
}
