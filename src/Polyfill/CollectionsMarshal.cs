#pragma warning disable

#if !NET5_0_OR_GREATER

namespace System.Runtime.InteropServices;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

/// <summary>
/// An unsafe class that provides a set of methods to access the underlying data representations of collections.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static class CollectionsMarshal
{
#if FeatureMemory && !WINDOWS_UWP

    /// <summary>
    /// Gets a <see cref="Span{T}"/> view over the data in a list. Items should not be added or removed from the <see cref="List{T}"/> while the <see cref="Span{T}"/> is in use.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.collectionsmarshal.asspan?view=net-11.0
    //Note: Reads the list's private backing array via reflection on this target.
    public static Span<T> AsSpan<T>(List<T>? list)
    {
        if (list == null)
        {
            return default;
        }

        var items = (T[]) ItemsAccessor<T>.Field.GetValue(list)!;
        return new(items, 0, list.Count);
    }

    static class ItemsAccessor<T>
    {
        public static readonly FieldInfo Field = typeof(List<T>)
            .GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic)!;
    }

#endif

    /// <summary>
    /// Sets the count of the <see cref="List{T}"/> to the specified value.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.collectionsmarshal.setcount?view=net-11.0
    //Note: When growing, new elements are default(T); the BCL exposes uninitialized data.
    public static void SetCount<T>(List<T> list, int count)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        var current = list.Count;
        if (count < current)
        {
            list.RemoveRange(count, current - count);
        }
        else if (count > current)
        {
            if (list.Capacity < count)
            {
                list.Capacity = count;
            }

            for (var index = current; index < count; index++)
            {
                list.Add(default!);
            }
        }
    }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.InteropServices.CollectionsMarshal))]
#endif
