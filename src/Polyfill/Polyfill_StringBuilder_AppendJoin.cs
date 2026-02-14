#nullable enable

namespace Polyfills;

using System;
using System.Text;
using System.Collections.Generic;

static partial class Polyfill
{

#if NETSTANDARD2_0 || NETFRAMEWORK

    /// <summary>Concatenates and appends the members of a collection, using the specified separator between each member.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0)))
    public static StringBuilder AppendJoin<T>(
        this StringBuilder target,
        char separator,
        IEnumerable<T> values) =>
        target.AppendJoinCore(separator, values);

    /// <summary>Concatenates and appends the members of a collection, using the specified char separator between each member.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0)))
    public static StringBuilder AppendJoin<T>(
        this StringBuilder target,
        string? separator,
        IEnumerable<T> values) =>
        target.AppendJoinCore(separator ?? string.Empty, values);

    /// <summary>Concatenates the strings of the provided array, using the specified separator between each string, then appends the result to the current instance of the string builder.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-string-system-string())
    public static StringBuilder AppendJoin(
        this StringBuilder target,
        string? separator,
        params string?[] values) =>
        target.AppendJoinCore(separator ?? string.Empty, values);

    /// <summary>Concatenates the string representations of the elements in the provided array of objects, using the specified separator between each member, then appends the result to the current instance of the string builder.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-string-system-object())
    public static StringBuilder AppendJoin(
        this StringBuilder target,
        string? separator,
        params object?[] values) =>
        target.AppendJoinCore(separator ?? string.Empty, values);

    /// <summary>Concatenates the strings of the provided array, using the specified char separator between each string, then appends the result to the current instance of the string builder.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-char-system-string())
    public static StringBuilder AppendJoin(
        this StringBuilder target,
        char separator,
        params string?[] values) =>
        target.AppendJoinCore(separator, values);

    /// <summary>Concatenates the string representations of the elements in the provided array of objects, using the specified char separator between each member, then appends the result to the current instance of the string builder.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin(system-char-system-object())
    public static StringBuilder AppendJoin(
        this StringBuilder target,
        char separator,
        params object?[] values) =>
        target.AppendJoinCore(separator, values);

    /// <summary>Concatenates and appends the members of a collection, using the specified char separator between each member.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0)))
    public static StringBuilder AppendJoin<T>(
        this StringBuilder target,
        char separator,
        params T[] values) =>
        target.AppendJoinCore(separator, values);

    /// <summary>Concatenates and appends the members of a collection, using the specified char separator between each member.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-11.0#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0)))
    public static StringBuilder AppendJoin<T>(
        this StringBuilder target,
        string separator,
        params T[] values) =>
        target.AppendJoinCore(separator, values);

    static StringBuilder AppendJoinCore<T>(
        this StringBuilder target,
        char separator,
        IEnumerable<T> values)
    {
        using var enumerator = values.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            return target;
        }

        var value = enumerator.Current;
        if (value != null)
        {
            target.Append(value);
        }

        while (enumerator.MoveNext())
        {
            target.Append(separator);
            value = enumerator.Current;
            if (value != null)
            {
                target.Append(value);
            }
        }

        return target;
    }

    static StringBuilder AppendJoinCore<T>(
        this StringBuilder target,
        char separator,
        T[] values)
    {
        if (values.Length == 0)
        {
            return target;
        }

        var first = values[0];
        if (first != null)
        {
            target.Append(first);
        }

        for (var i = 1; i < values.Length; i++)
        {
            target.Append(separator);
            var value = values[i];
            if (value != null)
            {
                target.Append(value);
            }
        }

        return target;
    }

    static StringBuilder AppendJoinCore<T>(
        this StringBuilder target,
        string separator,
        IEnumerable<T> values)
    {
        using var enumerator = values.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            return target;
        }

        var value = enumerator.Current;
        if (value != null)
        {
            target.Append(value);
        }

        while (enumerator.MoveNext())
        {
            target.Append(separator);
            value = enumerator.Current;
            if (value != null)
            {
                target.Append(value);
            }
        }

        return target;
    }

    static StringBuilder AppendJoinCore<T>(
        this StringBuilder target,
        string separator,
        T[] values)
    {
        if (values.Length == 0)
        {
            return target;
        }

        var first = values[0];
        if (first != null)
        {
            target.Append(first);
        }

        for (var i = 1; i < values.Length; i++)
        {
            target.Append(separator);
            var value = values[i];
            if (value != null)
            {
                target.Append(value);
            }
        }

        return target;
    }
#endif
}
