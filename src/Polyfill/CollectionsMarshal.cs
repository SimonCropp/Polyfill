#pragma warning disable

#if !NET5_0_OR_GREATER

namespace System.Runtime.InteropServices;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
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

        var items = ItemsAccessor<T>.Get(list);
        return new(items, 0, list.Count);
    }

    static class ItemsAccessor<T>
    {
        // FieldInfo.GetValue is slow, so the field read is compiled once per T
        public static readonly Func<List<T>, T[]> Get = Create();

        static Func<List<T>, T[]> Create()
        {
            var field = typeof(List<T>)
                .GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic)!;
            try
            {
                var list = Expression.Parameter(typeof(List<T>), "list");
                return Expression
                    .Lambda<Func<List<T>, T[]>>(Expression.Field(list, field), list)
                    .Compile();
            }
            catch (Exception)
            {
                // Expressions can not be compiled here, so read the field through reflection
                return list => (T[]) field.GetValue(list)!;
            }
        }
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
            var capacity = list.Capacity;
            if (capacity < count)
            {
                // Grow geometrically, as List<T> does. Setting the capacity to exactly count would reallocate
                // on every call when the count is raised a little at a time.
                long grown = 4;
                if (capacity != 0)
                {
                    grown = 2L * capacity;
                }

                if (grown > 0X7FEFFFFF)
                {
                    grown = 0X7FEFFFFF;
                }

                if (grown < count)
                {
                    grown = count;
                }

                list.Capacity = (int) grown;
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
