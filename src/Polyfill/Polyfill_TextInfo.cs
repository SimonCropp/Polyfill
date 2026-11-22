// Rune ships from netcoreapp3.0, and Polyfill doesn't recreate it below that.
#if NETCOREAPP3_0_OR_GREATER && !NET11_0_OR_GREATER

namespace Polyfills;

using System.Globalization;
using System.Text;

static partial class Polyfill
{
    /// <summary>
    /// Converts the specified rune to lowercase.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.globalization.textinfo.tolower?view=net-11.0#system-globalization-textinfo-tolower(system-text-rune)
    //Note: A rune outside the Basic Multilingual Plane goes through ToLower(string), so it allocates where the BCL does not.
    public static Rune ToLower(this TextInfo target, Rune value)
    {
        if (value.IsBmp)
        {
            return new(target.ToLower((char) value.Value));
        }

        var lower = target.ToLower(value.ToString());
        return new(lower[0], lower[1]);
    }

    /// <summary>
    /// Converts the specified rune to uppercase.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.globalization.textinfo.toupper?view=net-11.0#system-globalization-textinfo-toupper(system-text-rune)
    //Note: A rune outside the Basic Multilingual Plane goes through ToUpper(string), so it allocates where the BCL does not.
    public static Rune ToUpper(this TextInfo target, Rune value)
    {
        if (value.IsBmp)
        {
            return new(target.ToUpper((char) value.Value));
        }

        var upper = target.ToUpper(value.ToString());
        return new(upper[0], upper[1]);
    }
}

#endif
