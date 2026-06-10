#if FeatureMemory

#if !NET11_0_OR_GREATER

#pragma warning disable

namespace System.Text.Unicode;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>Provides static methods for validating UTF-16 text.</summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static class Utf16
{
    /// <summary>
    /// Returns the index in <paramref name="value"/> where the first ill-formed UTF-16 subsequence begins, or -1 if <paramref name="value"/> is well-formed.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.unicode.utf16.indexofinvalidsubsequence?view=net-11.0
    public static int IndexOfInvalidSubsequence(ReadOnlySpan<char> value)
    {
        for (var i = 0; i < value.Length; i++)
        {
            var c = value[i];
            if (char.IsHighSurrogate(c))
            {
                if (i + 1 >= value.Length ||
                    !char.IsLowSurrogate(value[i + 1]))
                {
                    return i;
                }

                // valid surrogate pair, skip the low surrogate
                i++;
            }
            else if (char.IsLowSurrogate(c))
            {
                // a low surrogate not preceded by a high surrogate
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="value"/> is well-formed UTF-16.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.unicode.utf16.isvalid?view=net-11.0
    public static bool IsValid(ReadOnlySpan<char> value) =>
        IndexOfInvalidSubsequence(value) < 0;
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Text.Unicode.Utf16))]
#endif

#endif
