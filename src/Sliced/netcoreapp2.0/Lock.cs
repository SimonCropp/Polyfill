
#pragma warning disable

#nullable enable


namespace System.Threading;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Link = System.ComponentModel.DescriptionAttribute;

/// <summary>
/// Provides a way to get mutual exclusion in regions of code between different threads. A lock may be held by one thread at
/// a time.
/// </summary>
/// <remarks>
/// Threads that cannot immediately enter the lock may wait for the lock to be exited or until a specified timeout. A thread
/// that holds a lock may enter the lock repeatedly without exiting it, such as recursively, in which case the thread should
/// eventually exit the lock the same number of times to fully exit the lock and allow other threads to enter the lock.
/// </remarks>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[Link("https://learn.microsoft.com/en-us/dotnet/api/system.threading.lock")]
class Lock
{
    public bool IsHeldByCurrentThread => Monitor.IsEntered(this);

    /// <summary>
    /// Enters the lock. Once the method returns, the calling thread would be the only thread that holds the lock.
    /// </summary>
    /// <remarks>
    /// If the lock cannot be entered immediately, the calling thread waits for the lock to be exited. If the lock is
    /// already held by the calling thread, the lock is entered again. The calling thread should exit the lock as many times
    /// as it had entered the lock to fully exit the lock and allow other threads to enter the lock.
    /// </remarks>
    /// <exception cref="LockRecursionException">
    /// The lock has reached the limit of recursive enters. The limit is implementation-defined, but is expected to be high
    /// enough that it would typically not be reached when the lock is used properly.
    /// </exception>
    public void Enter() => Monitor.Enter(this);

    /// <summary>
    /// Tries to enter the lock without waiting. If the lock is entered, the calling thread would be the only thread that
    /// holds the lock.
    /// </summary>
    /// <returns>
    /// <code>true</code> if the lock was entered, <code>false</code> otherwise.
    /// </returns>
    /// <remarks>
    /// If the lock cannot be entered immediately, the method returns <code>false</code>. If the lock is already held by the
    /// calling thread, the lock is entered again. The calling thread should exit the lock as many times as it had entered
    /// the lock to fully exit the lock and allow other threads to enter the lock.
    /// </remarks>
    /// <exception cref="LockRecursionException">
    /// The lock has reached the limit of recursive enters. The limit is implementation-defined, but is expected to be high
    /// enough that it would typically not be reached when the lock is used properly.
    /// </exception>
    public bool TryEnter() => Monitor.TryEnter(this);

    /// <summary>
    /// Tries to enter the lock, waiting for roughly the specified duration. If the lock is entered, the calling thread
    /// would be the only thread that holds the lock.
    /// </summary>
    /// <param name="timeout">
    /// The rough duration for which the method will wait if the lock is not available. The timeout is converted to a number
    /// of milliseconds by casting <see cref="TimeSpan.TotalMilliseconds"/> of the timeout to an integer value. A value
    /// representing <code>0</code> milliseconds specifies that the method should not wait, and a value representing
    /// <see cref="Timeout.Infinite"/> or <code>-1</code> milliseconds specifies that the method should wait indefinitely
    /// until the lock is entered.
    /// </param>
    /// <returns>
    /// <code>true</code> if the lock was entered, <code>false</code> otherwise.
    /// </returns>
    /// <remarks>
    /// If the lock cannot be entered immediately, the calling thread waits for roughly the specified duration for the lock
    /// to be exited. If the lock is already held by the calling thread, the lock is entered again. The calling thread
    /// should exit the lock as many times as it had entered the lock to fully exit the lock and allow other threads to
    /// enter the lock.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="timeout"/>, after its conversion to an integer millisecond value, represents a value that is less
    /// than <code>-1</code> milliseconds or greater than <see cref="int.MaxValue"/> milliseconds.
    /// </exception>
    /// <exception cref="LockRecursionException">
    /// The lock has reached the limit of recursive enters. The limit is implementation-defined, but is expected to be high
    /// enough that it would typically not be reached when the lock is used properly.
    /// </exception>
    public bool TryEnter(TimeSpan timeout) =>
        Monitor.TryEnter(this, timeout);

    /// <summary>
    /// Tries to enter the lock, waiting for roughly the specified duration. If the lock is entered, the calling thread
    /// would be the only thread that holds the lock.
    /// </summary>
    /// <param name="millisecondsTimeout">
    /// The rough duration in milliseconds for which the method will wait if the lock is not available. A value of
    /// <code>0</code> specifies that the method should not wait, and a value of <see cref="Timeout.Infinite"/> or
    /// <code>-1</code> specifies that the method should wait indefinitely until the lock is entered.
    /// </param>
    /// <returns>
    /// <code>true</code> if the lock was entered, <code>false</code> otherwise.
    /// </returns>
    /// <remarks>
    /// If the lock cannot be entered immediately, the calling thread waits for roughly the specified duration for the lock
    /// to be exited. If the lock is already held by the calling thread, the lock is entered again. The calling thread
    /// should exit the lock as many times as it had entered the lock to fully exit the lock and allow other threads to
    /// enter the lock.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout"/> is less than <code>-1</code>.
    /// </exception>
    /// <exception cref="LockRecursionException">
    /// The lock has reached the limit of recursive enters. The limit is implementation-defined, but is expected to be high
    /// enough that it would typically not be reached when the lock is used properly.
    /// </exception>
    public bool TryEnter(int millisecondsTimeout) =>
        TryEnter(TimeSpan.FromMilliseconds(millisecondsTimeout));

    /// <summary>
    /// Exits the lock.
    /// </summary>
    /// <remarks>
    /// If the calling thread holds the lock multiple times, such as recursively, the lock is exited only once. The
    /// calling thread should ensure that each enter is matched with an exit.
    /// </remarks>
    /// <exception cref="SynchronizationLockException">
    /// The calling thread does not hold the lock.
    /// </exception>
    public void Exit() => Monitor.Exit(this);

    /// <summary>
    /// Enters the lock and returns a <see cref="Scope"/> that may be disposed to exit the lock. Once the method returns,
    /// the calling thread would be the only thread that holds the lock. This method is intended to be used along with a
    /// language construct that would automatically dispose the <see cref="Scope"/>, such as with the C# <code>using</code>
    /// statement.
    /// </summary>
    /// <returns>
    /// A <see cref="Scope"/> that may be disposed to exit the lock.
    /// </returns>
    /// <remarks>
    /// If the lock cannot be entered immediately, the calling thread waits for the lock to be exited. If the lock is
    /// already held by the calling thread, the lock is entered again. The calling thread should exit the lock, such as by
    /// disposing the returned <see cref="Scope"/>, as many times as it had entered the lock to fully exit the lock and
    /// allow other threads to enter the lock.
    /// </remarks>
    /// <exception cref="LockRecursionException">
    /// The lock has reached the limit of recursive enters. The limit is implementation-defined, but is expected to be high
    /// enough that it would typically not be reached when the lock is used properly.
    /// </exception>
    public Scope EnterScope()
    {
        Enter();
        return new Scope(this);
    }

    /// <summary>
    /// A disposable structure that is returned by <see cref="EnterScope()"/>, which when disposed, exits the lock.
    /// </summary>
    public readonly ref struct Scope(Lock owner)
    {
        /// <summary>
        /// Exits the lock.
        /// </summary>
        /// <remarks>
        /// If the calling thread holds the lock multiple times, such as recursively, the lock is exited only once. The
        /// calling thread should ensure that each enter is matched with an exit.
        /// </remarks>
        /// <exception cref="SynchronizationLockException">
        /// The calling thread does not hold the lock.
        /// </exception>
        public void Dispose() => owner.Exit();
    }
}
