#nullable enable

#if !NET9_0_OR_GREATER

#pragma warning disable CS9216 // Casting Lock to object will use monitor-based locking - intentional for polyfill

namespace System.Threading;

using Diagnostics;
using Diagnostics.CodeAnalysis;

/// <summary>
/// Provides a way to get mutual exclusion in regions of code between different threads. A lock may be held by one thread at
/// a time.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.lock?view=net-11.0
#if PolyPublic
public
#endif
class Lock
{
    public bool IsHeldByCurrentThread => Monitor.IsEntered(this);

    /// <summary>
    /// Enters the lock. Once the method returns, the calling thread would be the only thread that holds the lock.
    /// </summary>
    public void Enter() => Monitor.Enter(this);

    /// <summary>
    /// Tries to enter the lock without waiting. If the lock is entered, the calling thread would be the only thread that
    /// holds the lock.
    /// </summary>
    public bool TryEnter() => Monitor.TryEnter(this);

    /// <summary>
    /// Tries to enter the lock, waiting for roughly the specified duration. If the lock is entered, the calling thread
    /// would be the only thread that holds the lock.
    /// </summary>
    public bool TryEnter(TimeSpan timeout) => Monitor.TryEnter(this, timeout);

    /// <summary>
    /// Tries to enter the lock, waiting for roughly the specified duration. If the lock is entered, the calling thread
    /// would be the only thread that holds the lock.
    /// </summary>
    public bool TryEnter(int millisecondsTimeout) =>
        TryEnter(TimeSpan.FromMilliseconds(millisecondsTimeout));

    /// <summary>
    /// Exits the lock.
    /// </summary>
    public void Exit() => Monitor.Exit(this);

    /// <summary>
    /// Enters the lock and returns a <see cref="Scope"/> that may be disposed to exit the lock. Once the method returns,
    /// the calling thread would be the only thread that holds the lock. This method is intended to be used along with a
    /// language construct that would automatically dispose the <see cref="Scope"/>, such as with the C# <code>using</code>
    /// statement.
    /// </summary>
    public Scope EnterScope()
    {
        Enter();
        return new(this);
    }

    /// <summary>
    /// A disposable structure that is returned by <see cref="EnterScope()"/>, which when disposed, exits the lock.
    /// </summary>
    public readonly ref struct Scope(Lock owner)
    {
        /// <summary>
        /// Exits the lock.
        /// </summary>
        public void Dispose() => owner.Exit();
    }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Threading.Lock))]
#endif
