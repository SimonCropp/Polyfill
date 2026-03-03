#if !NET9_0_OR_GREATER

namespace Polyfills;

using System;
using System.ComponentModel;

static partial class Polyfill
{
    /// <summary>
    /// Provides an enumerator for the invocation list of a delegate.
    /// </summary>
    public struct InvocationListEnumerator<TDelegate>
        where TDelegate : Delegate
    {
        Delegate[]? delegates;
        int index = -1;

        internal InvocationListEnumerator(Delegate? target) =>
            delegates = target?.GetInvocationList();

        public TDelegate Current { get; private set; } = null!;

        public bool MoveNext()
        {
            if (delegates == null)
            {
                return false;
            }

            var index = this.index + 1;
            if (index == delegates.Length)
            {
                return false;
            }

            Current = (TDelegate) delegates[index];
            this.index = index;
            return true;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public InvocationListEnumerator<TDelegate> GetEnumerator() => this;
    }

    extension(Delegate)
    {
        /// <summary>
        /// Gets an enumerator for the invocation targets of this delegate.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.delegate.enumerateinvocationlist?view=net-11.0
        public static InvocationListEnumerator<TDelegate> EnumerateInvocationList<TDelegate>(TDelegate? target)
            where TDelegate : Delegate =>
            new(target);
    }

    extension(Delegate target)
    {
        /// <summary>
        /// Gets a value that indicates whether the Delegate has a single invocation target.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.delegate.hassingletarget?view=net-11.0
        public bool HasSingleTarget => target.GetInvocationList().Length == 1;
    }
}
#endif
