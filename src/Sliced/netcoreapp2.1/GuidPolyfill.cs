
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
static partial class GuidPolyfill
{
    /// <summary>
    /// Tries to parse a string into a value.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse#system-guid-tryparse(system-string-system-iformatprovider-system-guid@)")]
    public static bool TryParse(string? target, IFormatProvider? provider, out Guid result) =>
        Guid.TryParse(target, out result);

}