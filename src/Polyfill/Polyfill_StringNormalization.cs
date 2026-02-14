#if !NET10_0_OR_GREATER && FeatureMemory

namespace Polyfills;

using System;
using System.Text;

//TODO: Add XML documentation
static partial class Polyfill
{
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.getnormalizedlength?view=net-11.0#system-stringnormalizationextensions-getnormalizedlength(system-readonlyspan((system-char))-system-text-normalizationform)
    public static int GetNormalizedLength(this ReadOnlySpan<char> target, NormalizationForm normalizationForm = NormalizationForm.FormC) =>
        target.ToString().Normalize(normalizationForm).Length;

    /// <summary>
    /// Indicates whether the specified string is in Unicode normalization form C.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.isnormalized?view=net-11.0#system-stringnormalizationextensions-isnormalized(system-readonlyspan((system-char))-system-text-normalizationform)
    public static bool IsNormalized(this ReadOnlySpan<char> target, NormalizationForm normalizationForm = NormalizationForm.FormC) =>
        target.ToString().IsNormalized(normalizationForm);

    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.trynormalize?view=net-11.0#system-stringnormalizationextensions-trynormalize(system-readonlyspan((system-char))-system-span((system-char))-system-int32@-system-text-normalizationform)
    public static bool TryNormalize(this ReadOnlySpan<char> target, Span<char> destination, out int charsWritten, NormalizationForm normalizationForm = NormalizationForm.FormC)
    {
        var normalize = target.ToString().Normalize(normalizationForm).AsSpan();
        if (normalize.TryCopyTo(destination))
        {
            charsWritten = normalize.Length;
            return true;
        }

        charsWritten = 0;
        return false;
    }
}
#endif
