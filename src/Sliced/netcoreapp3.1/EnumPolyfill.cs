

using System.Runtime.CompilerServices;

#pragma warning disable

namespace Polyfills;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using Link = System.ComponentModel.DescriptionAttribute;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
static partial class EnumPolyfill
{
    /// <summary>
    /// Retrieves an array of the values of the constants in a specified enumeration type.
    /// </summary>
    /// <returns>An array that contains the values of the constants in TEnum.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvalues")]
    public static TEnum[] GetValues<TEnum>()
        where TEnum : struct, Enum
    {
        return Enum.GetValues<TEnum>();
    }

    /// <summary>Returns a <see cref="bool"/> telling whether a given integral value exists in a specified enumeration.</summary>
    /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
    /// <param name="value">The value in <typeparamref name="TEnum"/>.</param>
    /// <returns><see langword="true"/> if a given integral value exists in a specified enumeration; <see langword="false"/>, otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.enum.isdefined#system-enum-isdefined-1(-0)")]
    public static bool IsDefined<TEnum> (TEnum value)
        where TEnum : struct, Enum =>
        Enum.IsDefined(typeof(TEnum), value);

    /// <summary>
    /// Retrieves an array of the names of the constants in a specified enumeration type.
    /// </summary>
    /// <returns>A string array of the names of the constants in TEnum.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.enum.getnames")]
    public static string[] GetNames<TEnum>()
        where TEnum : struct, Enum =>
        Enum.GetNames<TEnum>();

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants specified by TEnum to an equivalent enumerated object.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-string-system-boolean)")]
    public static TEnum Parse<TEnum>(string value)
        where TEnum : struct, Enum =>
        Enum.Parse<TEnum>(value);

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more enumerated constants specified by TEnum to an equivalent enumerated object.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-string-system-boolean)")]
    public static TEnum Parse<TEnum>(string value, bool ignoreCase)
        where TEnum : struct, Enum =>
        Enum.Parse<TEnum>(value, ignoreCase);

}