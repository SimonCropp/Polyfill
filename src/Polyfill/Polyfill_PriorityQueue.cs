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

        // Look for the element before copying anything, so a miss does not allocate
        var foundIndex = -1;
        var index = 0;
        foreach (var item in target.UnorderedItems)
        {
            if (equalityComparer.Equals(item.Element, element))
            {
                foundIndex = index;
                break;
            }

            index++;
        }

        if (foundIndex < 0)
        {
            removedElement = default!;
            priority = default!;
            return false;
        }

        var items = new (TElement Element, TPriority Priority)[target.Count];
        index = 0;
        foreach (var item in target.UnorderedItems)
        {
            items[index] = item;
            index++;
        }

        removedElement = items[foundIndex].Element;
        priority = items[foundIndex].Priority;

        // Enqueue one at a time, in the order the queue held them. EnqueueRange would heapify,
        // which can leave items of equal priority in a different order.
        target.Clear();
        for (var i = 0; i < items.Length; i++)
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
