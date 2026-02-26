#if !NET9_0_OR_GREATER && FeatureAsyncInterfaces
namespace Polyfills;

using System;
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
        public static async IAsyncEnumerable<Task> WhenEach(
            IEnumerable<Task> tasks,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            var remaining = new List<Task>(tasks);

            if (remaining.Count == 0)
            {
                yield break;
            }

            while (remaining.Count > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var completed = await Task.WhenAny(remaining).ConfigureAwait(false);
                remaining.Remove(completed);
                yield return completed;
            }
        }

        /// <summary>
        /// Creates an async-enumerable that yields completed tasks from the provided collection as they complete.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.wheneach?view=net-11.0#system-threading-tasks-task-wheneach-1(system-collections-generic-ienumerable((system-threading-tasks-task((-0)))))
        public static async IAsyncEnumerable<Task<TResult>> WhenEach<TResult>(
            IEnumerable<Task<TResult>> tasks,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            var remaining = new List<Task<TResult>>(tasks);

            if (remaining.Count == 0)
            {
                yield break;
            }

            while (remaining.Count > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var completed = await Task.WhenAny(remaining).ConfigureAwait(false);
                remaining.Remove(completed);
                yield return completed;
            }
        }
    }
}

#endif
