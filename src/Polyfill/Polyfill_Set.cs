#if NET9_0

namespace Polyfills;

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

static partial class Polyfill
{
    /// <summary>
    /// Returns a read-only <see cref="ReadOnlySet"/> wrapper for the specified set.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-11.0#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-iset((-0)))
    public static ReadOnlySet<T> AsReadOnly<T>(this ISet<T> target) => new(target);
}

#endif
