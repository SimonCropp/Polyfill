#if !NET9_0_OR_GREATER && FeatureAsyncInterfaces
namespace Polyfills;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    extension(Task)
    {
        /// <summary>
        /// Creates an async-enumerable that yields completed tasks from the provided collection as they complete.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.wheneach?view=net-11.0#system-threading-tasks-task-wheneach(system-collections-generic-ienumerable((system-threading-tasks-task)))
        public static IAsyncEnumerable<Task> WhenEach(
            IEnumerable<Task> tasks,
            CancellationToken cancellationToken = default)
        {
            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return WhenEachIterator(new List<Task>(tasks), cancellationToken);
        }

        /// <summary>
        /// Creates an async-enumerable that yields completed tasks from the provided collection as they complete.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.wheneach?view=net-11.0#system-threading-tasks-task-wheneach-1(system-collections-generic-ienumerable((system-threading-tasks-task((-0)))))
        public static IAsyncEnumerable<Task<TResult>> WhenEach<TResult>(
            IEnumerable<Task<TResult>> tasks,
            CancellationToken cancellationToken = default)
        {
            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return WhenEachIterator(new List<Task<TResult>>(tasks), cancellationToken);
        }
    }

    static async IAsyncEnumerable<T> WhenEachIterator<T>(
        List<T> tasks,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where T : Task
    {
        // Each task enqueues itself when it completes, so the next completed task is found without
        // re-scanning every remaining task, and the wait for it can observe the cancellation token.
        var completed = new ConcurrentQueue<T>();

        // Not disposed: tasks still pending when enumeration stops will release it later.
        var signal = new SemaphoreSlim(0);

        foreach (var task in tasks)
        {
            task.ContinueWith(
                _ =>
                {
                    completed.Enqueue(task);
                    signal.Release();
                },
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);
        }

        for (var index = 0; index < tasks.Count; index++)
        {
            await signal.WaitAsync(cancellationToken).ConfigureAwait(false);
            completed.TryDequeue(out var next);
            yield return next!;
        }
    }
}

#endif
