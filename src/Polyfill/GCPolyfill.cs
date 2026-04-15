#if !NET5_0_OR_GREATER

namespace Polyfills;

using System;
using System.Runtime.CompilerServices;

static partial class Polyfill
{
    extension(GC)
    {
        /// <summary>
        /// Allocates an array of a specified type and length. Provides source-level compatibility
        /// with newer APIs. On older runtimes the returned array is zero-initialized.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.gc.allocateuninitializedarray?view=net-11.0
        public static T[] AllocateUninitializedArray<T>(int length, bool pinned = false)
        {
            if (pinned)
            {
                throw new NotSupportedException("Pinned allocations are not supported on this runtime.");
            }

            return new T[length];
        }

    }
}
#endif
