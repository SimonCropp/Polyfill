
#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantAttributeSuffix

using System;
using System.Reflection;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

static partial class PolyfillExtensions
{
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo.hassamemetadatadefinitionas")]
    public static bool HasSameMetadataDefinitionAs(this MemberInfo target, MemberInfo other)
    {
#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0
        return target.MetadataToken == other.MetadataToken &&
               target.Module.Equals(other.Module);
#else
        return target.HasSameMetadataDefinitionAs(other);
#endif
    }

    /// <summary>
    /// Gets a value that indicates whether the current Type represents a type parameter in the definition of a generic method.
    /// </summary>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter")]
    public static bool IsGenericMethodParameter(this Type target)
    {
#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0
        return target.IsGenericParameter &&
               target.DeclaringMethod != null;
#else
        return target.IsGenericMethodParameter;
#endif
    }

#if !NET6_0_OR_GREATER

    /// <summary>
    /// Searches for the MemberInfo on the current Type that matches the specified MemberInfo.
    /// </summary>
    /// <param name="type">The MemberInfo to find on the current Type.</param>
    /// <param name="member">The MemberInfo to find on the current Type.</param>
    /// <returns>An object representing the member on the current Type that matches the specified member.</returns>
    [DescriptionAttribute("https://learn.microsoft.com/en-us/dotnet/api/system.type.getmemberwithsamemetadatadefinitionas")]
    internal static MemberInfo GetMemberWithSameMetadataDefinitionAs(this Type type, MemberInfo member)
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