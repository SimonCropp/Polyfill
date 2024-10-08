
#pragma warning disable

namespace Polyfills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{

    /// <summary>Returns a read-only <see cref="ReadOnlyCollection{T}" /> wrapper for the current collection.</summary>
    /// <returns>An object that acts as a read-only wrapper around the current <see cref="IList{T}" />.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-ilist((-0)))")]
    public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> target) =>
        new(target);

}