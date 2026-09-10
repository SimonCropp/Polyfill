#pragma warning disable

// ImmutableDictionary is in the shared framework from netcoreapp2.1. Below that it is a separate
// package, so there is nothing to extend without taking a dependency Polyfill does not have.
#if NETCOREAPP2_1_OR_GREATER && !NET10_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

static partial class Polyfill
{
    extension(ImmutableDictionary)
    {
        /// <summary>
        /// Creates a new <see cref="ImmutableDictionary{TKey, TValue}"/> from the specified items, where later entries overwrite earlier ones with the same key.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutabledictionary.createrangewithoverwrite?view=net-11.0#system-collections-immutable-immutabledictionary-createrangewithoverwrite-2(system-readonlyspan((system-collections-generic-keyvaluepair((-0-1)))))
        //Note: Only available on netcoreapp2.1 and later, since ImmutableDictionary is a separate package below that.
        public static ImmutableDictionary<TKey, TValue> CreateRangeWithOverwrite<TKey, TValue>(ReadOnlySpan<KeyValuePair<TKey, TValue>> items)
            where TKey : notnull =>
            CreateRangeWithOverwriteCore<TKey, TValue>(null, items);

        /// <summary>
        /// Creates a new <see cref="ImmutableDictionary{TKey, TValue}"/> from the specified items using the specified key comparer, where later entries overwrite earlier ones with the same key.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutabledictionary.createrangewithoverwrite?view=net-11.0#system-collections-immutable-immutabledictionary-createrangewithoverwrite-2(system-collections-generic-iequalitycomparer((-0))-system-readonlyspan((system-collections-generic-keyvaluepair((-0-1)))))
        //Note: Only available on netcoreapp2.1 and later, since ImmutableDictionary is a separate package below that.
        public static ImmutableDictionary<TKey, TValue> CreateRangeWithOverwrite<TKey, TValue>(IEqualityComparer<TKey>? keyComparer, ReadOnlySpan<KeyValuePair<TKey, TValue>> items)
            where TKey : notnull =>
            CreateRangeWithOverwriteCore(keyComparer, items);
    }

    static ImmutableDictionary<TKey, TValue> CreateRangeWithOverwriteCore<TKey, TValue>(IEqualityComparer<TKey>? keyComparer, ReadOnlySpan<KeyValuePair<TKey, TValue>> items)
        where TKey : notnull
    {
        var builder = ImmutableDictionary.CreateBuilder<TKey, TValue>(keyComparer);
        foreach (var item in items)
        {
            // the builder indexer names this parameter "key", where the BCL overload reports the
            // KeyValuePair member as "Key"
            if (item.Key is null)
            {
                throw new ArgumentNullException(nameof(item.Key));
            }

            builder[item.Key] = item.Value;
        }

        return builder.ToImmutable();
    }
}

#endif
