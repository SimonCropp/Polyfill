
#pragma warning disable

namespace Polyfills;

using System;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
#if FeatureMemory && (NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X)
#endif

#if (FeatureMemory && (NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X)) || NET6_0
    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat")]
    public static bool TryFormat(this DateTimeOffset target, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = default)
    {
        string result;

        if (format.Length == 0)
        {
            result = target.ToString(provider);
        }
        else
        {
            result = target.ToString(format.ToString(), provider);
        }

        return CopyToSpan(destination, out charsWritten, result);
    }

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat")]
    public static bool TryFormat(this DateTime target, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = default)
    {
        string result;

        if (format.Length == 0)
        {
            result = target.ToString(provider);
        }
        else
        {
            result = target.ToString(format.ToString(), provider);
        }

        return CopyToSpan(destination, out charsWritten, result);
    }

#endif
#if NET6_0

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat")]
    public static bool TryFormat(this DateOnly target, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = default)
    {
        string result;

        if (format.Length == 0)
        {
            result = target.ToString(provider);
        }
        else
        {
            result = target.ToString(format.ToString(), provider);
        }

        return CopyToSpan(destination, out charsWritten, result);
    }

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat")]
    public static bool TryFormat(this TimeOnly target, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default, IFormatProvider? provider = default)
    {
        string result;

        if (format.Length == 0)
        {
            result = target.ToString(provider);
        }
        else
        {
            result = target.ToString(format.ToString(), provider);
        }

        return CopyToSpan(destination, out charsWritten, result);
    }
#endif

#if NET6_0 || (FeatureMemory && (NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X))
    static bool CopyToSpan(Span<char> destination, out int charsWritten, string result)
    {
        if (result.Length == 0)
        {
            charsWritten = 0;
            return true;
        }

        charsWritten = result.Length;
        return result.TryCopyTo(destination);
    }
#endif
}