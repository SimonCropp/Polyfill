
#pragma warning disable

namespace Polyfills;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Link = System.ComponentModel.DescriptionAttribute;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
static partial class ULongPolyfill
{
    /// <summary>
    /// Tries to parse a string into a value.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-string-system-iformatprovider-system-uint64@)")]
    public static bool TryParse(string? target, IFormatProvider? provider, out ulong result) =>
        ulong.TryParse(target, NumberStyles.Integer, provider, out result);

}