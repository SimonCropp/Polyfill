#pragma warning disable

// SetCount was added to the BCL CollectionsMarshal in net8.0, but the type itself
// exists from net5.0. For net5.0-net7.0 the type is present (so it cannot be
// recreated) and SetCount is added as a static extension member. For earlier
// targets SetCount lives on the recreated type in CollectionsMarshal.cs.
#if NET5_0_OR_GREATER && !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

static partial class Polyfill
{
    extension(CollectionsMarshal)
    {
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
}

#endif
