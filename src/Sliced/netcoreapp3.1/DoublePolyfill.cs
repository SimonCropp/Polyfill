
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
static partial class DoublePolyfill
{
    /// <summary>
    /// Tries to parse a string into a value.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-string-system-iformatprovider-system-double@)")]
    public static bool TryParse(string? target, IFormatProvider? provider, out double result) =>
        double.TryParse(target, NumberStyles.Float, provider, out result);

}