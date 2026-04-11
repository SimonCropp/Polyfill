partial class PolyfillTests
{
    [Test]
    public async Task Task_ConfigureAwait_None() => await Task.CompletedTask.ConfigureAwait(ConfigureAwaitOptions.None);

    [Test]
    public async Task Task_ConfigureAwait_ContinueOnCapturedContext() => await Task.CompletedTask.ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);

    [Test]
    public async Task Task_ConfigureAwait_ForceYielding()
    {
        // Task.CompletedTask is already completed
        await Assert.That(Task.CompletedTask.IsCompleted).IsTrue();

        // But with ForceYielding, the awaiter should not be synchronously completed
        var awaiter = Task.CompletedTask
            .ConfigureAwait(ConfigureAwaitOptions.ForceYielding)
            .GetAwaiter();
        await Assert.That(awaiter.IsCompleted).IsFalse();

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
        var awaiter = completedTask
            .ConfigureAwait(ConfigureAwaitOptions.ForceYielding)
            .GetAwaiter();
        await Assert.That(awaiter.IsCompleted).IsFalse();

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
}
