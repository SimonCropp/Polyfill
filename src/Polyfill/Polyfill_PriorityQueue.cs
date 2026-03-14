#if NET6_0_OR_GREATER && !NET9_0_OR_GREATER

namespace Polyfills;

using System.Collections.Generic;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Removes the first occurrence that equals the specified element from the <see cref="PriorityQueue{TElement, TPriority}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2.remove?view=net-11.0#system-collections-generic-priorityqueue-2-remove(-0,-0@,-1@,system-collections-generic-iequalitycomparer(-0))
    public static bool Remove<TElement, TPriority>(
        this PriorityQueue<TElement, TPriority> target,
        TElement element,
        out TElement removedElement,
        out TPriority priority,
        IEqualityComparer<TElement>? equalityComparer = null)
    {
        equalityComparer ??= EqualityComparer<TElement>.Default;

        var items = target.UnorderedItems.ToList();
        var foundIndex = -1;
        for (var i = 0; i < items.Count; i++)
        {
            if (equalityComparer.Equals(items[i].Element, element))
            {
                foundIndex = i;
                break;
            }
        }

        if (foundIndex < 0)
        {
            removedElement = default!;
            priority = default!;
            return false;
        }

        removedElement = items[foundIndex].Element;
        priority = items[foundIndex].Priority;

        target.Clear();
        for (var i = 0; i < items.Count; i++)
        {
            if (i != foundIndex)
            {
                target.Enqueue(items[i].Element, items[i].Priority);
            }
        }

        return true;
    }
}

#endif
