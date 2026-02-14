#nullable enable

#if !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;

static partial class Polyfill
{

#if !NET7_0_OR_GREATER

    extension(DateTimeOffset target)
    {
        /// <summary>
        /// Gets the nanosecond component of the time represented by the current <see cref="DateTimeOffset"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond?view=net-11.0
        public int Nanosecond =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 100;

        /// <summary>
        /// Gets the microsecond component of the time represented by the current <see cref="DateTimeOffset"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond?view=net-11.0
        public int Microsecond =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;

        long TicksComponent()
        {
            var noSeconds = new DateTimeOffset(target.Year, target.Month, target.Day, target.Hour, target.Minute, 0, target.Offset);
            var secondsPart = target - noSeconds;
            return secondsPart.Ticks;
        }
    }

#endif

    extension(DateTimeOffset)
    {
#if !NET7_0_OR_GREATER
        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-11.0#system-datetimeoffset-tryparse(system-string-system-iformatprovider-system-datetimeoffset@)
        public static bool TryParse(string? s, IFormatProvider? provider, out DateTimeOffset result) =>
            DateTimeOffset.TryParse(s, provider, DateTimeStyles.None, out result);
#endif

#if FeatureMemory

#if !NET8_0_OR_GREATER
        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-11.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetimeoffset@)
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out DateTimeOffset result) =>
            DateTimeOffset.TryParse(s.ToString(), provider, DateTimeStyles.None, out result);
#endif

#if !(NETSTANDARD2_1 || NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)
        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-11.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-datetimeoffset@)
        public static bool TryParse(ReadOnlySpan<char> input, out DateTimeOffset result) =>
            DateTimeOffset.TryParse(input.ToString(), null, DateTimeStyles.None, out result);

        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-11.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@)
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, DateTimeStyles styles, out DateTimeOffset result) =>
            DateTimeOffset.TryParse(s.ToString(), provider, styles, out result);

        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact?view=net-11.0#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@)
        public static bool TryParseExact(ReadOnlySpan<char> input, string format, IFormatProvider? provider, DateTimeStyles styles, out DateTimeOffset result) =>
            DateTimeOffset.TryParseExact(input.ToString(), format, provider, styles, out result);
#endif

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact?view=net-11.0#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@)
        public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider? provider, DateTimeStyles styles, out DateTimeOffset result) =>
            DateTimeOffset.TryParseExact(input.ToString(), format.ToString(), provider, styles, out result);
#endif
#endif
    }
}
#endif
