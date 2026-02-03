#if !NET6_0_OR_GREATER

namespace Polyfills;

using System.Diagnostics.CodeAnalysis;
using System;

static partial class Polyfill
{
    [DoesNotReturn]
    static void ThrowNoElementsException() =>
        throw new InvalidOperationException("Sequence contains no elements");
}

#endif
