#if FeatureMemory
namespace Polyfills;

using System.Text;
using System;
using System.Diagnostics.CodeAnalysis;

static partial class Polyfill
{
#if !NET8_0_OR_GREATER

    static bool DoFormat<T>(this T target, Span<byte> destination, out int written, [StringSyntax(StringSyntaxAttribute.TimeSpanFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = null)
        where T : IFormattable
    {
        string result;

        if (format.Length == 0)
        {
            result = target.ToString(null, provider);
        }
        else
        {
            result = target.ToString(format.ToString(), provider);
        }

        if (result.Length == 0)
        {
            written = 0;
            return true;
        }

        return Encoding.UTF8.TryGetBytes(result.AsSpan(), destination, out written);
    }

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat?view=net-11.0#system-timespan-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this TimeSpan target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.TimeSpanFormat)] ReadOnlySpan<char> format = default, IFormatProvider? formatProvider = null) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, formatProvider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat?view=net-11.0#system-guid-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char)))
    public static bool TryFormat(this Guid target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.GuidFormat)] ReadOnlySpan<char> format = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat?view=net-11.0#system-sbyte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this sbyte target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat?view=net-11.0#system-byte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this byte target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat?view=net-11.0#system-int16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this short target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat?view=net-11.0#system-uint16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this ushort target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat?view=net-11.0#system-int32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this int target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat?view=net-11.0#system-uint32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this uint target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat?view=net-11.0#system-int64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this long target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat?view=net-11.0#system-uint64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this ulong target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat?view=net-11.0#system-single-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this float target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat?view=net-11.0#system-double-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this double target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat?view=net-11.0#system-decimal-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this decimal target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.NumericFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat?view=net-11.0#system-datetimeoffset-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this DateTimeOffset target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.DateTimeFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat?view=net-11.0#system-datetime-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this DateTime target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.DateOnlyFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

#endif
#if NET6_0 || NET7_0

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat?view=net-11.0#system-dateonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this DateOnly target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.DateOnlyFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);

    /// <summary>
    /// Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat?view=net-11.0#system-timeonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)
    public static bool TryFormat(this TimeOnly target, Span<byte> utf8Destination, out int bytesWritten, [StringSyntax(StringSyntaxAttribute.TimeOnlyFormat)] ReadOnlySpan<char> format = default, IFormatProvider? provider = default) =>
        target.DoFormat(utf8Destination, out bytesWritten, format, provider);
#endif
}
#endif
