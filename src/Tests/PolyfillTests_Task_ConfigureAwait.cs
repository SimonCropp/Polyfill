partial class PolyfillTests
{
    [Test]
    public async Task Task_ConfigureAwait_None() => await Task.CompletedTask.ConfigureAwait(ConfigureAwaitOptions.None);

    [Test]
    public async Task Task_ConfigureAwait_ContinueOnCapturedContext() => await Task.CompletedTask.ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);

    // The BCL awaiter always reports IsCompleted as false for ForceYielding. The polyfill has to
    // return the BCL awaiter type, so it yields by handing the task to the thread pool, and a
    // pool thread can finish that hop before IsCompleted is read. A single read is therefore a
    // race on older frameworks. An implementation that does not yield reports true every time,
    // so one false in several attempts proves the yield without depending on scheduling.
    static bool ForceYieldingYields(Func<bool> isCompleted)
    {
        for (var attempt = 0; attempt < 20; attempt++)
        {
            if (!isCompleted())
            {
                return true;
            }
        }

        return false;
    }

    [Test]
    public async Task Task_ConfigureAwait_ForceYielding()
    {
        // Task.CompletedTask is already completed
        await Assert.That(Task.CompletedTask.IsCompleted).IsTrue();

        // But with ForceYielding, the awaiter should not be synchronously completed
        var yielded = ForceYieldingYields(
            () => Task.CompletedTask
                .ConfigureAwait(ConfigureAwaitOptions.ForceYielding)
                .GetAwaiter()
                .IsCompleted);
        await Assert.That(yielded).IsTrue();

        // It should still complete successfully when awaited
        await Task.CompletedTask.ConfigureAwait(ConfigureAwaitOptions.ForceYielding);
    }

    [Test]
    public async Task Task_ConfigureAwait_SuppressThrowing_Faulted()
    {
        var task = Task.FromException(new InvalidOperationException("test"));
        await task.ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
    }

    [Test]
    public async Task Task_ConfigureAwait_SuppressThrowing_Canceled()
    {
        var cts = new CancelSource();
        cts.Cancel();
        var task = Task.FromCanceled(cts.Token);
        await task.ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
    }

    [Test]
    public async Task Task_ConfigureAwait_SuppressThrowing_Completed() => await Task.CompletedTask.ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);

    [Test]
    public async Task Task_ConfigureAwait_ForceYielding_SuppressThrowing()
    {
        var task = Task.FromException(new InvalidOperationException("test"));
        await task.ConfigureAwait(
            ConfigureAwaitOptions.ForceYielding | ConfigureAwaitOptions.SuppressThrowing);
    }

    [Test]
    public async Task TaskT_ConfigureAwait_None()
    {
        var result = await Task.FromResult(42).ConfigureAwait(ConfigureAwaitOptions.None);
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task TaskT_ConfigureAwait_ContinueOnCapturedContext()
    {
        var result = await Task.FromResult(42)
            .ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task TaskT_ConfigureAwait_ForceYielding()
    {
        var completedTask = Task.FromResult(42);

        // Task is already completed
        await Assert.That(completedTask.IsCompleted).IsTrue();

        // But with ForceYielding, the awaiter should not be synchronously completed
        var yielded = ForceYieldingYields(
            () => completedTask
                .ConfigureAwait(ConfigureAwaitOptions.ForceYielding)
                .GetAwaiter()
                .IsCompleted);
        await Assert.That(yielded).IsTrue();

        // It should still return the correct result when awaited
        var result = await completedTask.ConfigureAwait(ConfigureAwaitOptions.ForceYielding);
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task TaskT_ConfigureAwait_SuppressThrowing_Throws()
    {
        var exception = await Assert.That(() =>
            Task.FromResult(42).ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing).GetAwaiter().GetResult()
        ).Throws<ArgumentOutOfRangeException>();

        await Assert.That(exception!.ParamName).IsEqualTo("options");
    }

    [Test]
    public async Task Task_ConfigureAwait_InvalidFlags_Throws()
    {
        var exception = await Assert.That(() =>
            Task.CompletedTask.ConfigureAwait((ConfigureAwaitOptions)8)
        ).Throws<ArgumentOutOfRangeException>();

        await Assert.That(exception!.ParamName).IsEqualTo("options");
    }

    [Test]
    public async Task TaskT_ConfigureAwait_InvalidFlags_Throws()
    {
        var exception = await Assert.That(() =>
            Task.FromResult(42).ConfigureAwait((ConfigureAwaitOptions)8)
        ).Throws<ArgumentOutOfRangeException>();

        await Assert.That(exception!.ParamName).IsEqualTo("options");
    }

    // A context whose Post never runs the callback, as when the context thread is blocked.
    class ConfigureAwaitBlockedContext :
        System.Threading.SynchronizationContext
    {
        public override void Post(System.Threading.SendOrPostCallback callback, object? state)
        {
        }
    }

    static Task<bool> ConfigureAwaitCompletesOnBlockedContext(Func<Task> func)
    {
        var completion = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var thread = new System.Threading.Thread(() =>
        {
            System.Threading.SynchronizationContext.SetSynchronizationContext(new ConfigureAwaitBlockedContext());
            try
            {
                completion.SetResult(func().Wait(TimeSpan.FromSeconds(10)));
            }
            catch (Exception exception)
            {
                completion.SetException(exception);
            }
        })
        {
            IsBackground = true
        };
        thread.Start();
        return completion.Task;
    }

    [Test]
    public async Task Task_ConfigureAwait_ForceYielding_DoesNotCaptureContext()
    {
        var completed = await ConfigureAwaitCompletesOnBlockedContext(
            async () => await Task.CompletedTask.ConfigureAwait(ConfigureAwaitOptions.ForceYielding));
        await Assert.That(completed).IsTrue();
    }

    [Test]
    public async Task TaskT_ConfigureAwait_ForceYielding_DoesNotCaptureContext()
    {
        var result = 0;
        var completed = await ConfigureAwaitCompletesOnBlockedContext(
            async () => result = await Task.FromResult(42).ConfigureAwait(ConfigureAwaitOptions.ForceYielding));
        await Assert.That(completed).IsTrue();
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task Task_ConfigureAwait_SuppressThrowing_DoesNotCaptureContext()
    {
        var completed = await ConfigureAwaitCompletesOnBlockedContext(
            async () =>
            {
                var task = Task.Delay(50)
                    .ContinueWith(_ => throw new InvalidOperationException("test"), TaskScheduler.Default);
                await task.ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
            });
        await Assert.That(completed).IsTrue();
    }

    [Test]
    public async Task Task_ConfigureAwait_SuppressThrowing_Pending()
    {
        var source = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
        var awaiter = ((Task) source.Task)
            .ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing)
            .GetAwaiter();
        await Assert.That(awaiter.IsCompleted).IsFalse();

        source.SetException(new InvalidOperationException("test"));
        await ((Task) source.Task).ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
    }

    [Test]
    public async Task TaskT_ConfigureAwait_ForceYielding_Faulted()
    {
        var task = Task.FromException<int>(new InvalidOperationException("test"));
        await Assert.That(async () => await task.ConfigureAwait(ConfigureAwaitOptions.ForceYielding))
            .Throws<InvalidOperationException>();
    }
}
