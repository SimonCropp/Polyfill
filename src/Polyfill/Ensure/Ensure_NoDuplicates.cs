namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if PolyPublic
public
#endif
static partial class Ensure
{
    public static void NoDuplicates<T>(IEnumerable<T> value, [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        var seen = new HashSet<T>();
        foreach (var item in value)
        {
            if (!seen.Add(item))
            {
                throw new ArgumentException($"Duplicates not allowed. Duplicate value: {item}", name);
            }
        }
    }
}