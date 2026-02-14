#if !NET

#nullable enable

namespace System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents the producer side of a <see cref="Task"/> unbound to a
/// delegate, providing access to the consumer side through the <see cref="Task"/> property.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource?view=net-11.0
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
class TaskCompletionSource
{
    TaskCompletionSource<object?> inner;

    /// <summary>Creates a <see cref="TaskCompletionSource"/>.</summary>
    public TaskCompletionSource() => inner = new();

    /// <summary>Creates a <see cref="TaskCompletionSource"/> with the specified options.</summary>
    public TaskCompletionSource(TaskCreationOptions creationOptions) :
        this(null, creationOptions)
    {
    }

    /// <summary>Creates a <see cref="TaskCompletionSource"/> with the specified state.</summary>
    public TaskCompletionSource(object? state) :
        this(state, TaskCreationOptions.None)
    {
    }

    /// <summary>Creates a <see cref="TaskCompletionSource"/> with the specified state and options.</summary>
    public TaskCompletionSource(object? state, TaskCreationOptions creationOptions) =>
        inner = new(state, creationOptions);

    /// <summary>
    /// Gets the <see cref="Task"/> created
    /// by this <see cref="TaskCompletionSource"/>.
    /// </summary>
    public Task Task => inner.Task;

    /// <summary>Transitions the underlying <see cref="Task"/> into the <see cref="TaskStatus.Faulted"/> state.</summary>
    public void SetException(Exception exception) => inner.SetException(exception);

    /// <summary>Transitions the underlying <see cref="Task"/> into the <see cref="TaskStatus.Faulted"/> state.</summary>
    public void SetException(IEnumerable<Exception> exceptions) => inner.SetException(exceptions);

    /// <summary>
    /// Attempts to transition the underlying <see cref="Task"/> into the <see cref="TaskStatus.Faulted"/> state.
    /// </summary>
    public bool TrySetException(Exception exception) => inner.TrySetException(exception);

    /// <summary>
    /// Attempts to transition the underlying <see cref="Task"/> into the <see cref="TaskStatus.Faulted"/> state.
    /// </summary>
    public bool TrySetException(IEnumerable<Exception> exceptions) => inner.TrySetException(exceptions);

    /// <summary>
    /// Transitions the underlying <see cref="Task"/> into the <see cref="TaskStatus.RanToCompletion"/> state.
    /// </summary>
    public void SetResult() => inner.SetResult(null);

    /// <summary>
    /// Attempts to transition the underlying <see cref="Task"/> into the <see cref="TaskStatus.RanToCompletion"/> state.
    /// </summary>
    public bool TrySetResult() => inner.TrySetResult(null);

    /// <summary>
    /// Transitions the underlying <see cref="Task"/> into the <see cref="TaskStatus.Canceled"/> state.
    /// </summary>
    public void SetCanceled() => SetCanceled(default);

    /// <summary>
    /// Transitions the underlying <see cref="Task"/> into the <see cref="TaskStatus.Canceled"/> state
    /// using the specified token.
    /// </summary>
    public void SetCanceled(CancellationToken cancellationToken) => inner.SetCanceled(cancellationToken);

    /// <summary>
    /// Attempts to transition the underlying <see cref="Task"/> into the <see cref="TaskStatus.Canceled"/> state.
    /// </summary>
    public bool TrySetCanceled() => TrySetCanceled(default);

    /// <summary>
    /// Attempts to transition the underlying <see cref="Task"/> into the <see cref="TaskStatus.Canceled"/> state.
    /// </summary>
    public bool TrySetCanceled(CancellationToken cancellationToken) => inner.TrySetCanceled(default);
}
#endif
