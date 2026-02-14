#if !NET

namespace Polyfills;

using System;
using System.Reflection;

static partial class Polyfill
{
    /// <summary>
    /// Creates a delegate of type T with the specified target from this method.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate?view=net-11.0#system-reflection-methodinfo-createdelegate-1
    public static T CreateDelegate<T>(this MethodInfo method) where T : Delegate => (T)method.CreateDelegate(typeof(T));

    /// <summary>
    /// Creates a delegate of type T with the specified target from this method.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate?view=net-11.0#system-reflection-methodinfo-createdelegate-1(system-object)
    public static T CreateDelegate<T>(this MethodInfo method, object? target) where T : Delegate => (T)method.CreateDelegate(typeof(T), target);
}
#endif
