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
        public static Task ForEachAsync<T>(
            IEnumerable<T> source,
            ParallelOptions parallelOptions,
            Func<T, CancellationToken, ValueTask> body)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (parallelOptions is null)
            {
                throw new ArgumentNullException(nameof(parallelOptions));
            }

            if (body is null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            if (parallelOptions.CancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled(parallelOptions.CancellationToken);
            }

            var enumerator = source.GetEnumerator();
            var cancelSource = CancellationTokenSource.CreateLinkedTokenSource(parallelOptions.CancellationToken);
            var gate = new object();

            return RunForEachAsyncWorkers(
                parallelOptions,
                cancelSource,
                Worker,
                () =>
                {
                    enumerator.Dispose();
                    return Task.CompletedTask;
                });

            // The awaits here deliberately omit ConfigureAwait(false): workers are started on the requested
            // TaskScheduler (never on the context of the caller), and should keep running the bodies on it.
            async Task Worker(Action launchNext)
            {
                var launchedNext = false;
                while (true)
                {
                    T item;
                    lock (gate)
                    {
                        if (cancelSource.IsCancellationRequested ||
                            !enumerator.MoveNext())
                        {
                            return;
                        }

                        item = enumerator.Current;
                    }

                    if (!launchedNext)
                    {
                        launchedNext = true;
                        launchNext();
                    }

                    await body(item, cancelSource.Token);
                }
            }
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
        public static Task ForEachAsync<T>(
            IAsyncEnumerable<T> source,
            ParallelOptions parallelOptions,
            Func<T, CancellationToken, ValueTask> body)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (parallelOptions is null)
            {
                throw new ArgumentNullException(nameof(parallelOptions));
            }

            if (body is null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            if (parallelOptions.CancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled(parallelOptions.CancellationToken);
            }

            var cancelSource = CancellationTokenSource.CreateLinkedTokenSource(parallelOptions.CancellationToken);
            var enumerator = source.GetAsyncEnumerator(cancelSource.Token);
            var gate = new SemaphoreSlim(1, 1);

            return RunForEachAsyncWorkers(
                parallelOptions,
                cancelSource,
                Worker,
                () => enumerator.DisposeAsync().AsTask());

            // The awaits here deliberately omit ConfigureAwait(false): workers are started on the requested
            // TaskScheduler (never on the context of the caller), and should keep running the bodies on it.
            async Task Worker(Action launchNext)
            {
                var launchedNext = false;
                while (true)
                {
                    T item;
                    await gate.WaitAsync();
                    try
                    {
                        if (cancelSource.IsCancellationRequested ||
                            !await enumerator.MoveNextAsync())
                        {
                            return;
                        }

                        item = enumerator.Current;
                    }
                    finally
                    {
                        gate.Release();
                    }

                    if (!launchedNext)
                    {
                        launchedNext = true;
                        launchNext();
                    }

                    await body(item, cancelSource.Token);
                }
            }
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

    // Items are pulled lazily: a worker takes one item, runs the body, then takes the next. A worker that gets
    // an item launches one more worker until MaxDegreeOfParallelism is reached. The first failure cancels
    // cancelSource, which stops workers pulling and is the token handed to the bodies.
    static Task RunForEachAsyncWorkers(
        ParallelOptions parallelOptions,
        CancellationTokenSource cancelSource,
        Func<Action, Task> worker,
        Func<Task> dispose)
    {
        var scheduler = parallelOptions.TaskScheduler ?? TaskScheduler.Default;
        var availableWorkers = Environment.ProcessorCount;
        if (parallelOptions.MaxDegreeOfParallelism > 0)
        {
            availableWorkers = parallelOptions.MaxDegreeOfParallelism;
        }

        var runningWorkers = 0;
        var exceptions = new List<Exception>();
        var completion = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        Launch();

        return completion.Task;

        void Launch()
        {
            if (Interlocked.Decrement(ref availableWorkers) < 0)
            {
                return;
            }

            Interlocked.Increment(ref runningWorkers);
            Task.Factory.StartNew(
                RunWorker,
                CancellationToken.None,
                TaskCreationOptions.DenyChildAttach,
                scheduler);
        }

        async Task RunWorker()
        {
            try
            {
                await worker(Launch);
            }
            catch (Exception exception)
            {
                lock (exceptions)
                {
                    exceptions.Add(exception);
                }

                cancelSource.Cancel();
            }
            finally
            {
                if (Interlocked.Decrement(ref runningWorkers) == 0)
                {
                    await Complete().ConfigureAwait(false);
                }
            }
        }

        async Task Complete()
        {
            try
            {
                await dispose().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                exceptions.Add(exception);
            }

            var externalToken = parallelOptions.CancellationToken;
            cancelSource.Dispose();

            if (externalToken.IsCancellationRequested)
            {
                completion.TrySetCanceled(externalToken);
                return;
            }

            if (exceptions.Count == 0)
            {
                completion.TrySetResult(true);
                return;
            }

            completion.TrySetException(exceptions);
        }
    }
}

#endif
