
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
static partial class BytePolyfill
{
    /// <summary>
    /// Tries to parse a string into a value.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-string-system-iformatprovider-system-byte@)")]
    public static bool TryParse(string? target, IFormatProvider? provider, out byte result) =>
        byte.TryParse(target, NumberStyles.Integer, provider, out result);

}