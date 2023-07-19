#if NETFRAMEWORK || NETSTANDARD || NETCOREAPP || NET5_0

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantAttributeSuffix
// ReSharper disable RedundantTypeArgumentsOfMethod

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class PolyfillExtensions
{
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken)")]
    public static Task WaitAsync(this Task target, CancellationToken cancellationToken) =>
        target.WaitAsync(Timeout.InfiniteTimeSpan, cancellationToken);

    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan)")]
    public static async Task WaitAsync(this Task target, TimeSpan timeout)
    {
        var cancellationSource = new CancellationTokenSource();
        try
        {
            await target.WaitAsync(timeout, cancellationSource.Token);
        }
        finally
        {
            cancellationSource.Cancel();
            cancellationSource.Dispose();
        }
    }

    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan-system-threading-cancellationtoken)")]
    public static async Task WaitAsync(this Task task, TimeSpan timeout, CancellationToken cancellationToken)
    {
        var delayTask = Task.Delay(timeout, cancellationToken);
        var completedTask = await Task.WhenAny(task, delayTask);
        if (completedTask == delayTask)
        {
            throw new TimeoutException($"Execution did not complete within the time allotted {timeout.TotalMilliseconds} ms");
        }

        await task;
    }

    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken)")]
    public static Task<TResult> WaitAsync<TResult>(this Task<TResult> task, CancellationToken cancellationToken) =>
        task.WaitAsync<TResult>(Timeout.InfiniteTimeSpan, cancellationToken);

    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-threading-cancellationtoken)")]
    public static async Task<TResult> WaitAsync<TResult>(this Task<TResult> task, TimeSpan timeout)
    {
        var cancellationSource = new CancellationTokenSource();
        try
        {
            return await task.WaitAsync(timeout, cancellationSource.Token);
        }
        finally
        {
            cancellationSource.Cancel();
            cancellationSource.Dispose();
        }
    }

    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-timespan-system-threading-cancellationtoken)")]
    public static async Task<TResult> WaitAsync<TResult>(this Task<TResult> task, TimeSpan timeout, CancellationToken cancellationToken)
    {
        var delayTask = Task.Delay(timeout, cancellationToken);
        var completedTask = await Task.WhenAny(task, delayTask);
        if (completedTask == delayTask)
        {
            throw new TimeoutException($"Execution did not complete within the time allotted {timeout.TotalMilliseconds} ms");
        }

        return await task;
    }
}

#endif