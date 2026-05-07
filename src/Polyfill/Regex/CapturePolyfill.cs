#if FeatureMemory && !NET6_0_OR_GREATER

namespace Polyfills;

using System;
using System.Text.RegularExpressions;

static partial class Polyfill
{
    extension(Capture target)
    {
        /// <summary>
        /// Gets the captured substring from the input string as a <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.capture.valuespan?view=net-11.0
        public ReadOnlySpan<char> ValueSpan => target.Value.AsSpan();
    }
}

#endif
