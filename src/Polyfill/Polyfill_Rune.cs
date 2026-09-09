#if !NET11_0_OR_GREATER && NETCOREAPP3_0_OR_GREATER

namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
    /// <summary>
    /// Determines whether the specified Unicode scalar value is equal to the current one, using the specified comparison type.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.rune.equals?view=net-11.0#system-text-rune-equals(system-text-rune-system-stringcomparison)
    public static bool Equals(this Rune target, Rune other, StringComparison comparisonType) =>
        comparisonType switch
        {
            StringComparison.Ordinal => target == other,
            StringComparison.OrdinalIgnoreCase => ToOrdinalCase(target, toUpper: true) == ToOrdinalCase(other, toUpper: true),
            _ => target.ToString().Equals(other.ToString(), comparisonType)
        };
}

#endif
