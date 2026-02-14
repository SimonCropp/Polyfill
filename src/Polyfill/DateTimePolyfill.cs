#if !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;

static partial class Polyfill
{
#if !NET7_0_OR_GREATER
    extension(DateTime target)
    {
        /// <summary>
        /// Gets the nanosecond component of the time represented by the current <see cref="DateTime"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond?view=net-11.0
        public int Nanosecond =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 100;

        /// <summary>
        /// Gets the microsecond component of the time represented by the current <see cref="DateTime"/> object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond?view=net-11.0
        public int Microsecond =>
            (int) (target.TicksComponent() % TicksPerMicrosecond) * 1000;

        long TicksComponent()
        {
            var noSeconds = new DateTime(target.Year, target.Month, target.Day, target.Hour, target.Minute, 0, target.Kind);
            var secondsPart = target - noSeconds;
            return secondsPart.Ticks;
        }
    }

#endif

    extension(DateTime)
    {
#if !NET7_0_OR_GREATER
        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-11.0#system-datetime-tryparse(system-string-system-iformatprovider-system-datetime@)
        public static bool TryParse(string? s, IFormatProvider? provider, out DateTime result) =>
            DateTime.TryParse(s, provider, DateTimeStyles.None, out result);
#endif

#if FeatureMemory

#if !NET8_0_OR_GREATER
        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-11.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetime@)
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out DateTime result) =>
            DateTime.TryParse(s.ToString(), provider, DateTimeStyles.None, out result);
#endif

#if !(NETSTANDARD2_1 || NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)
        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-11.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-datetime@)
        public static bool TryParse(ReadOnlySpan<char> s, out DateTime result) =>
            DateTime.TryParse(s.ToString(), null, DateTimeStyles.None, out result);

        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-11.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@)
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, DateTimeStyles styles, out DateTime result) =>
            DateTime.TryParse(s.ToString(), provider, styles, out result);

        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact?view=net-11.0#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@)
        public static bool TryParseExact(ReadOnlySpan<char> s, string format, IFormatProvider? provider, DateTimeStyles style, out DateTime result) =>
            DateTime.TryParseExact(s.ToString(), format, provider, style, out result);
#endif

#if !(NETCOREAPP2_1_OR_GREATER && NETSTANDARD2_1_OR_GREATER)
        /// <summary>
        /// Tries to parse a span of characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact?view=net-11.0#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@)
        public static bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider? provider, DateTimeStyles styles, out DateTime result) =>
            DateTime.TryParseExact(s.ToString(), format.ToString(), provider, styles, out result);
#endif

#endif
    }
}
#endif
