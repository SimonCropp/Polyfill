#if !NET6_0_OR_GREATER

namespace Polyfills;

using System;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    const uint MaxSupportedTimeout = 0xfffffffe;

    /// <summary>Gets a <see cref="Task"/> that will complete when this <see cref="Task"/> completes or when the specified <see cref="CancellationToken"/> has cancellation requested.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-11.0#system-threading-tasks-task-waitasync(system-threading-cancellationtoken)
    public static Task WaitAsync(this Task target, CancellationToken cancellationToken) =>
        target.WaitAsync(Timeout.InfiniteTimeSpan, cancellationToken);

    /// <summary>Gets a <see cref="Task"/> that will complete when this <see cref="Task"/> completes or when the specified timeout expires.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-11.0#system-threading-tasks-task-waitasync(system-timespan)
    public static Task WaitAsync(
        this Task target,
        TimeSpan timeout) =>
        target.WaitAsync(timeout, default);

    /// <summary>Gets a <see cref="Task"/> that will complete when this <see cref="Task"/> completes, when the specified timeout expires, or when the specified <see cref="CancellationToken"/> has cancellation requested.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-11.0#system-threading-tasks-task-waitasync(system-timespan-system-threading-cancellationtoken)
    public static async Task WaitAsync(
        this Task target,
        TimeSpan timeout,
        CancellationToken cancellationToken)
    {
        var milliseconds = (long)timeout.TotalMilliseconds;
        if (milliseconds is < -1 or > MaxSupportedTimeout)
        {
            throw new ArgumentOutOfRangeException(nameof(timeout));
        }

        if (target.IsCompleted ||
            (!cancellationToken.CanBeCanceled && timeout == Timeout.InfiniteTimeSpan))
        {
            await target.ConfigureAwait(false);
            return;
        }

        if (cancellationToken.IsCancellationRequested)
        {
            await Task.FromCanceled(cancellationToken);
        }

        if (timeout == TimeSpan.Zero)
        {
            throw new TimeoutException();
        }

        using var cancelSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cancelSource.CancelAfter(timeout);

        var cancelTask = new TaskCompletionSource<bool>();
        using (cancelSource.Token.Register(tcs => ((TaskCompletionSource<bool>)tcs!).TrySetResult(true), cancelTask))
        {
            await Task.WhenAny(target, cancelTask.Task).ConfigureAwait(false);

            if (!target.IsCompleted)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    await Task.FromCanceled(cancellationToken);
                }

                throw new TimeoutException();
            }

            await target.ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Gets a <see cref="Task"/> that will complete when this <see cref="Task"/> completes, or when the specified <see cref="CancellationToken"/> has cancellation requested.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-11.0#system-threading-tasks-task-waitasync(system-threading-cancellationtoken)
    public static Task<TResult> WaitAsync<TResult>(
        this Task<TResult> target,
        CancellationToken cancellationToken) =>
        target.WaitAsync<TResult>(Timeout.InfiniteTimeSpan, cancellationToken);

    /// <summary>
    /// Gets a <see cref="Task"/> that will complete when this <see cref="Task"/> completes, or when the specified timeout expires.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync?view=net-11.0#system-threading-tasks-task-1-waitasync(system-timespan)
    public static Task<TResult> WaitAsync<TResult>(
        this Task<TResult> target,
        TimeSpan timeout) =>
        target.WaitAsync<TResult>(timeout, default);

    /// <summary>
    /// Gets a <see cref="Task"/> that will complete when this <see cref="Task"/> completes, when the specified timeout expires, or when the specified <see cref="CancellationToken"/> has cancellation requested.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync?view=net-11.0#system-threading-tasks-task-1-waitasync(system-timespan-system-threading-cancellationtoken)
    public static async Task<TResult> WaitAsync<TResult>(
        this Task<TResult> target,
        TimeSpan timeout,
        CancellationToken cancellationToken)
    {
        await ((Task) target).WaitAsync(timeout, cancellationToken);
        return target.Result;
    }
}

#endif
#if !NET8_0_OR_GREATER

    /// <summary>Configures an awaiter used to await this <see cref="Task"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.configureawait?view=net-11.0#system-threading-tasks-task-configureawait(system-threading-tasks-configureawaitoptions)
    public static ConfiguredTaskAwaitable ConfigureAwait(this Task target, ConfigureAwaitOptions options)
    {
        if ((options & ~(ConfigureAwaitOptions.ContinueOnCapturedContext |
                         ConfigureAwaitOptions.SuppressThrowing |
                         ConfigureAwaitOptions.ForceYielding)) != 0)
        {
            throw new ArgumentOutOfRangeException(nameof(options));
        }

        var task = target;

        if ((options & ConfigureAwaitOptions.ForceYielding) != 0)
        {
            task = ForceYieldAsync(task);

            static async Task ForceYieldAsync(Task t)
            {
                await Task.Yield();
                await t;
            }
        }

        if ((options & ConfigureAwaitOptions.SuppressThrowing) != 0)
        {
            task = SuppressThrowAsync(task);

            static async Task SuppressThrowAsync(Task t)
            {
                try
                {
                    await t;
                }
                catch
                {
                }
            }
        }

        return task.ConfigureAwait(
            (options & ConfigureAwaitOptions.ContinueOnCapturedContext) != 0);
    }

    /// <summary>Configures an awaiter used to await this <see cref="Task{TResult}"/>.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.configureawait?view=net-11.0#system-threading-tasks-task-1-configureawait(system-threading-tasks-configureawaitoptions)
    public static ConfiguredTaskAwaitable<TResult> ConfigureAwait<TResult>(
        this Task<TResult> target,
        ConfigureAwaitOptions options)
    {
        if ((options & ~(ConfigureAwaitOptions.ContinueOnCapturedContext |
                         ConfigureAwaitOptions.ForceYielding)) != 0)
        {
            throw new ArgumentOutOfRangeException(nameof(options));
        }

        var task = target;

        if ((options & ConfigureAwaitOptions.ForceYielding) != 0)
        {
            task = ForceYieldAsync(task);

            static async Task<TResult> ForceYieldAsync(Task<TResult> t)
            {
                await Task.Yield();
                return await t;
            }
        }

        return task.ConfigureAwait(
            (options & ConfigureAwaitOptions.ContinueOnCapturedContext) != 0);
    }

#endif
}

