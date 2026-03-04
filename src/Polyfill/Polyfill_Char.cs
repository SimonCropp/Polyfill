#if !NET11_0_OR_GREATER
namespace Polyfills;

using System;

static partial class Polyfill
{
    /// <summary>
    /// Determines whether the specified character is equal to the current character, using the specified comparison type.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.char.equals?view=net-11.0#system-char-equals(system-char-system-stringcomparison)
    public static bool Equals(this char target, char other, StringComparison comparisonType) =>
        target.ToString().Equals(other.ToString(), comparisonType);
}

#endif
