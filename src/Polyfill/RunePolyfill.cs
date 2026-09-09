#if !NET11_0_OR_GREATER && NETCOREAPP3_0_OR_GREATER

namespace Polyfills;

using System.Text;

static partial class Polyfill
{
    extension(Rune)
    {
        /// <summary>
        /// Returns a copy of the Unicode scalar value converted to lowercase using ordinal casing rules.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.rune.tolowerordinal?view=net-11.0
        //Note: Derived from invariant casing, so the mapping follows the Unicode version of the running framework rather than the one net11 is built against.
        public static Rune ToLowerOrdinal(Rune value) =>
            ToOrdinalCase(value, toUpper: false);

        /// <summary>
        /// Returns a copy of the Unicode scalar value converted to uppercase using ordinal casing rules.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.rune.toupperordinal?view=net-11.0
        //Note: Derived from invariant casing, so the mapping follows the Unicode version of the running framework rather than the one net11 is built against.
        public static Rune ToUpperOrdinal(Rune value) =>
            ToOrdinalCase(value, toUpper: true);
    }

    static Rune ToOrdinalCase(Rune value, bool toUpper)
    {
        if (value.IsBmp)
        {
            var current = (char) value.Value;
            return new(toUpper ? ToUpperOrdinalChar(current) : ToLowerOrdinalChar(current));
        }

        var cased = ToOrdinalCaseSurrogatePair(value.ToString(), toUpper);
        return new(char.ConvertToUtf32(cased[0], cased[1]));
    }
}

#endif
