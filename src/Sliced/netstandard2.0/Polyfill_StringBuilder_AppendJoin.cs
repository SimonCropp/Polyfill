
#pragma warning disable

namespace Polyfills;
using System;
using System.Text;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{


    /// <summary>Concatenates the strings of the provided array, using the specified separator between each string, then appends the result to the current instance of the string builder.</summary>
    /// <param name="separator">The string to use as a separator. separator is included in the joined strings only if values has more than one element.</param>
    /// <param name="values">An array that contains the strings to concatenate and append to the current instance of the string builder.</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-string-system-string())")]
    public static StringBuilder AppendJoin(
        this StringBuilder target,
        string separator,
        params string[] values) =>
        target.Append(string.Join(separator, values));

    /// <summary>Concatenates the string representations of the elements in the provided array of objects, using the specified separator between each member, then appends the result to the current instance of the string builder.</summary>
    /// <param name="separator">The string to use as a separator. separator is included in the joined strings only if values has more than one element.</param>
    /// <param name="values">An array that contains the strings to concatenate and append to the current instance of the string builder.</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-string-system-object())")]
    public static StringBuilder AppendJoin(
        this StringBuilder target,
        string separator,
        params Object[] values) =>
        target.Append(string.Join(separator, values));

    /// <summary>Concatenates the strings of the provided array, using the specified char separator between each string, then appends the result to the current instance of the string builder.</summary>
    /// <param name="separator">The character to use as a separator. separator is included in the joined strings only if values has more than one element.</param>
    /// <param name="values">An array that contains the strings to concatenate and append to the current instance of the string builder.</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-char-system-string())")]
    public static StringBuilder AppendJoin(
        this StringBuilder target,
        char separator,
        params string[] values) =>
        target.Append(string.Join(separator.ToString(), values));

    /// <summary>Concatenates the string representations of the elements in the provided array of objects, using the specified char separator between each member, then appends the result to the current instance of the string builder.</summary>
    /// <param name="separator">The character to use as a separator. separator is included in the joined strings only if values has more than one element.</param>
    /// <param name="values">An array that contains the strings to concatenate and append to the current instance of the string builder.</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-char-system-object())")]
    public static StringBuilder AppendJoin(
        this StringBuilder target,
        char separator,
        params object[] values) =>
        target.Append(string.Join(separator.ToString(), values));

    /// <summary>Concatenates and appends the members of a collection, using the specified char separator between each member.</summary>
    /// <param name="separator">The character to use as a separator. separator is included in the joined strings only if values has more than one element.</param>
    /// <param name="values">A collection that contains the objects to concatenate and append to the current instance of the string builder.</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0)))")]
    public static StringBuilder AppendJoin<T>(
        this StringBuilder target,
        char separator,
        params T[] values) =>
        target.Append(string.Join(separator.ToString(), values));

    /// <summary>Concatenates and appends the members of a collection, using the specified char separator between each member.</summary>
    /// <param name="separator">The string to use as a separator. separator is included in the concatenated and appended strings only if values has more than one element.</param>
    /// <param name="values">A collection that contains the objects to concatenate and append to the current instance of the string builder.</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0)))")]
    public static StringBuilder AppendJoin<T>(
        this StringBuilder target,
        string separator,
        params T[] values) =>
        target.Append(string.Join(separator.ToString(), values));

}