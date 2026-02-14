namespace Polyfills;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Linq;

static partial class Polyfill
{
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo.hassamemetadatadefinitionas?view=net-11.0
    public static bool HasSameMetadataDefinitionAs(this MemberInfo target, MemberInfo other) =>
        target.MetadataToken == other.MetadataToken &&
        target.Module.Equals(other.Module);
#endif

#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0 || WINDOWS_UWP
    /// <summary>
    /// Searches for the specified method whose parameters match the specified generic parameter count, argument types and modifiers, using the specified binding constraints.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.type.getmethod?view=net-11.0#system-type-getmethod(system-string-system-int32-system-reflection-bindingflags-system-reflection-binder-system-type()-system-reflection-parametermodifier())
    public static MethodInfo? GetMethod(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)] this Type target,
        string name,
        int genericParameterCount,
        BindingFlags bindingAttr,
        Binder? binder,
        Type[] types,
        ParameterModifier[]? modifiers)
    {
        var methods = target.GetMethods(bindingAttr);
        if (genericParameterCount == 0)
        {
            foreach (var method in methods)
            {
                if (method.IsGenericMethod)
                {
                    continue;
                }

                if (IsMatch(method))
                {
                    return method;
                }
            }
        }
        else
        {
            foreach (var method in methods)
            {
                if (!method.IsGenericMethod)
                {
                    continue;
                }

                var genericArguments = method.GetGenericArguments();
                if (genericParameterCount != genericArguments.Length)
                {
                    continue;
                }

                if (IsMatch(method))
                {
                    return method;
                }
            }
        }

        return null;

        bool IsMatch(MethodInfo method) =>
            name == method.Name &&
            method.GetParameters().Select(_ => _.ParameterType).SequenceEqual(types);
    }
#endif

#if !NET9_0_OR_GREATER
    /// <summary>
    /// Searches for the specified method whose parameters match the specified generic parameter count, argument types and modifiers, using the specified binding constraints.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.type.getmethod?view=net-11.0#system-type-getmethod(system-string-system-int32-system-reflection-bindingflags-system-type())
    public static MethodInfo? GetMethod([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)] this Type target, string name, int genericParameterCount, BindingFlags bindingAttr, Type[] types) =>
        target.GetMethod(name, genericParameterCount, bindingAttr, null, types, null);
#endif

    /// <summary>
    /// Generic version of Type.IsAssignableTo https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto.
    /// </summary>
    public static bool IsAssignableTo<T>(this Type target) =>
        typeof(T).IsAssignableFrom(target);

    /// <summary>
    /// Generic version of Type.IsAssignableFrom https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignablefrom.
    /// </summary>
    public static bool IsAssignableFrom<T>(this Type target) =>
        target.IsAssignableFrom(typeof(T));

#if !NET
    /// <summary>
    /// Determines whether the current type can be assigned to a variable of the specified targetType.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto?view=net-11.0
    public static bool IsAssignableTo(this Type target, [NotNullWhen(true)] Type? targetType) =>
        targetType?.IsAssignableFrom(target) ?? false;
#endif

#if !NET6_0_OR_GREATER

    /// <summary>
    /// Searches for the MemberInfo on the current Type that matches the specified MemberInfo.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.type.getmemberwithsamemetadatadefinitionas?view=net-11.0
    public static MemberInfo GetMemberWithSameMetadataDefinitionAs(
        this Type type,
        MemberInfo member)
    {
        const BindingFlags all = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
        foreach (var info in type.GetMembers(all))
        {
            if (info.HasSameMetadataDefinitionAs(member))
            {
                return info;
            }
        }

        throw new MissingMemberException(type.FullName, member.Name);
    }

#endif
}
