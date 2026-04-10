#if !NET6_0_OR_GREATER && FeatureValueTask

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    extension(Parallel)
    {
        /// <summary>
        /// Executes a for-each operation on an <see cref="IEnumerable{T}"/> in which iterations may run in parallel.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.foreachasync?view=net-11.0#system-threading-tasks-parallel-foreachasync-1(system-collections-generic-ienumerable((-0))-system-threading-tasks-paralleloptions-system-func((-0-system-threading-cancellationtoken-system-threading-tasks-valuetask)))
        public static async Task ForEachAsync<T>(
            IEnumerable<T> source,
            ParallelOptions parallelOptions,
            Func<T, CancellationToken, ValueTask> body)
        {
            using var semaphore = new SemaphoreSlim(
                parallelOptions.MaxDegreeOfParallelism > 0
                    ? parallelOptions.MaxDegreeOfParallelism
                    : Environment.ProcessorCount);

            var tasks = new List<Task>();
            foreach (var item in source)
            {
                tasks.Add(
                    Task.Factory.StartNew(
                            async () =>
                            {
                                await semaphore
                                    .WaitAsync(parallelOptions.CancellationToken)
                                    .ConfigureAwait(false);
                                try
                                {
                                    await body(item, parallelOptions.CancellationToken)
                                        .ConfigureAwait(false);
                                }
                                finally
                                {
                                    semaphore.Release();
                                }
                            },
                            parallelOptions.CancellationToken,
                            TaskCreationOptions.None,
                            parallelOptions.TaskScheduler ?? TaskScheduler.Default)
                        .Unwrap());
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes a for-each operation on an <see cref="IEnumerable{T}"/> in which iterations may run in parallel.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.foreachasync?view=net-11.0#system-threading-tasks-parallel-foreachasync-1(system-collections-generic-ienumerable((-0))-system-threading-cancellationtoken-system-func((-0-system-threading-cancellationtoken-system-threading-tasks-valuetask)))
        public static Task ForEachAsync<T>(
            IEnumerable<T> source,
            CancellationToken cancellationToken,
            Func<T, CancellationToken, ValueTask> body) =>
            Parallel.ForEachAsync(source, new ParallelOptions { CancellationToken = cancellationToken }, body);

        /// <summary>
        /// Executes a for-each operation on an <see cref="IEnumerable{T}"/> in which iterations may run in parallel.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.foreachasync?view=net-11.0#system-threading-tasks-parallel-foreachasync-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-threading-cancellationtoken-system-threading-tasks-valuetask)))
        public static Task ForEachAsync<T>(
            IEnumerable<T> source,
            Func<T, CancellationToken, ValueTask> body) =>
            Parallel.ForEachAsync(source, CancellationToken.None, body);

#if FeatureAsyncInterfaces

        /// <summary>
        /// Executes a for-each operation on an <see cref="IAsyncEnumerable{T}"/> in which iterations may run in parallel.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.foreachasync?view=net-11.0#system-threading-tasks-parallel-foreachasync-1(system-collections-generic-iasyncenumerable((-0))-system-threading-tasks-paralleloptions-system-func((-0-system-threading-cancellationtoken-system-threading-tasks-valuetask)))
        public static async Task ForEachAsync<T>(
            IAsyncEnumerable<T> source,
            ParallelOptions parallelOptions,
            Func<T, CancellationToken, ValueTask> body)
        {
            using var semaphore = new SemaphoreSlim(
                parallelOptions.MaxDegreeOfParallelism > 0
                    ? parallelOptions.MaxDegreeOfParallelism
                    : Environment.ProcessorCount);

            var tasks = new List<Task>();

            await foreach (var item in source.WithCancellation(parallelOptions.CancellationToken))
            {
                tasks.Add(
                    Task.Factory.StartNew(
                            async () =>
                            {
                                await semaphore
                                    .WaitAsync(parallelOptions.CancellationToken)
                                    .ConfigureAwait(false);
                                try
                                {
                                    await body(item, parallelOptions.CancellationToken)
                                        .ConfigureAwait(false);
                                }
                                finally
                                {
                                    semaphore.Release();
                                }
                            },
                            parallelOptions.CancellationToken,
                            TaskCreationOptions.None,
                            parallelOptions.TaskScheduler ?? TaskScheduler.Default)
                        .Unwrap());
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes a for-each operation on an <see cref="IAsyncEnumerable{T}"/> in which iterations may run in parallel.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.foreachasync?view=net-11.0#system-threading-tasks-parallel-foreachasync-1(system-collections-generic-iasyncenumerable((-0))-system-threading-cancellationtoken-system-func((-0-system-threading-cancellationtoken-system-threading-tasks-valuetask)))
        public static Task ForEachAsync<T>(
            IAsyncEnumerable<T> source,
            CancellationToken cancellationToken,
            Func<T, CancellationToken, ValueTask> body) =>
            Parallel.ForEachAsync(source, new ParallelOptions { CancellationToken = cancellationToken }, body);

        /// <summary>
        /// Executes a for-each operation on an <see cref="IAsyncEnumerable{T}"/> in which iterations may run in parallel.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.foreachasync?view=net-11.0#system-threading-tasks-parallel-foreachasync-1(system-collections-generic-iasyncenumerable((-0))-system-func((-0-system-threading-cancellationtoken-system-threading-tasks-valuetask)))
        public static Task ForEachAsync<T>(
            IAsyncEnumerable<T> source,
            Func<T, CancellationToken, ValueTask> body) =>
            Parallel.ForEachAsync(source, CancellationToken.None, body);

#endif
    }
}

#endif
