
#pragma warning disable

namespace Polyfills;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Link = System.ComponentModel.DescriptionAttribute;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
static partial class DateTimeOffsetPolyfill
{
    /// <summary>
    /// Tries to parse a string into a value.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse#system-datetimeoffset-tryparse(system-string-system-iformatprovider-system-datetimeoffset@)")]
    public static bool TryParse(string? target, IFormatProvider? provider, out DateTimeOffset result) =>
        DateTimeOffset.TryParse(target, provider, DateTimeStyles.None, out result);

}