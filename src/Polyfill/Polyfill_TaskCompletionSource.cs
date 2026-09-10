#if !NET9_0_OR_GREATER

namespace Polyfills;

using System;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if !NET
    /// <summary>
    /// Transitions the underlying <see cref="Task{TResult}"/> into the <see cref="TaskStatus.Canceled"/> state
    /// using the specified token.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.setcanceled?view=net-11.0#system-threading-tasks-taskcompletionsource-1-setcanceled(system-threading-cancellationtoken)
    public static void SetCanceled<T>(
        this TaskCompletionSource<T> target,
        CancellationToken cancellationToken)
    {
        if (target.TrySetCanceled(cancellationToken))
        {
            return;
        }

        throw new InvalidOperationException("An attempt was made to transition a task to a final state when it had already completed.");
    }
#endif

    /// <summary>
    /// Transitions the underlying <see cref="Task"/> into the same completion state as the supplied task.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource.setfromtask?view=net-11.0
    //Note: When the supplied task is canceled, the canceling token is recovered by observing that task, which costs a throw and catch that the BCL avoids.
    public static void SetFromTask(
        this TaskCompletionSource target,
        Task completedTask)
    {
        if (!target.TrySetFromTask(completedTask))
        {
            throw new InvalidOperationException("An attempt was made to transition a task to a final state when it had already completed.");
        }
    }

    /// <summary>
    /// Attempts to transition the underlying <see cref="Task"/> into the same completion state as the supplied task.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource.trysetfromtask?view=net-11.0
    //Note: When the supplied task is canceled, the canceling token is recovered by observing that task, which costs a throw and catch that the BCL avoids.
    public static bool TrySetFromTask(
        this TaskCompletionSource target,
        Task completedTask)
    {
        GuardCompletedTask(completedTask);

        return completedTask.Status switch
        {
            TaskStatus.RanToCompletion => target.TrySetResult(),
            TaskStatus.Faulted => target.TrySetException(completedTask.Exception!.InnerExceptions),
            _ => target.TrySetCanceled(CancellationTokenOf(completedTask))
        };
    }

    /// <summary>
    /// Transitions the underlying <see cref="Task{TResult}"/> into the same completion state as the supplied task.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.setfromtask?view=net-11.0
    //Note: When the supplied task is canceled, the canceling token is recovered by observing that task, which costs a throw and catch that the BCL avoids.
    public static void SetFromTask<T>(
        this TaskCompletionSource<T> target,
        Task<T> completedTask)
    {
        if (!target.TrySetFromTask(completedTask))
        {
            throw new InvalidOperationException("An attempt was made to transition a task to a final state when it had already completed.");
        }
    }

    /// <summary>
    /// Attempts to transition the underlying <see cref="Task{TResult}"/> into the same completion state as the supplied task.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.trysetfromtask?view=net-11.0
    //Note: When the supplied task is canceled, the canceling token is recovered by observing that task, which costs a throw and catch that the BCL avoids.
    public static bool TrySetFromTask<T>(
        this TaskCompletionSource<T> target,
        Task<T> completedTask)
    {
        GuardCompletedTask(completedTask);

        return completedTask.Status switch
        {
            TaskStatus.RanToCompletion => target.TrySetResult(completedTask.Result),
            TaskStatus.Faulted => target.TrySetException(completedTask.Exception!.InnerExceptions),
            _ => target.TrySetCanceled(CancellationTokenOf(completedTask))
        };
    }

    static void GuardCompletedTask(Task completedTask)
    {
        if (completedTask == null)
        {
            throw new ArgumentNullException(nameof(completedTask));
        }

        if (!completedTask.IsCompleted)
        {
            throw new ArgumentException("The provided task must have already completed.", nameof(completedTask));
        }
    }

    // Task does not expose the token that canceled it, but the OperationCanceledException
    // thrown when the completed task is observed does.
    static CancellationToken CancellationTokenOf(Task canceledTask)
    {
        try
        {
            canceledTask.GetAwaiter().GetResult();
        }
        catch (OperationCanceledException exception)
        {
            return exception.CancellationToken;
        }
        catch
        {
        }

        return default;
    }
}
#endif
