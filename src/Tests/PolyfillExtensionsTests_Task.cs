partial class PolyfillExtensionsTests
{
    private static T? AssertThrowsAsync<T>(string expectedParamName, AsyncTestDelegate action)
        where T : ArgumentException
    {
        T? exception = Assert.ThrowsAsync<T>(action);

        Assert.AreEqual(expectedParamName, exception?.ParamName);

        return exception;
    }

    [Test]
    public void Task_WaitAsync_InvalidTimeout_Throws()
    {
        foreach (TimeSpan timeout in new[] { TimeSpan.FromMilliseconds(-2), TimeSpan.MaxValue, TimeSpan.MinValue })
        {
#if NET5_0_OR_GREATER
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await new TaskCompletionSource().Task.WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await new TaskCompletionSource().Task.WaitAsync(timeout, CancellationToken.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await new TaskCompletionSource().Task.WaitAsync(timeout, new CancellationToken(true)));
#endif

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await new TaskCompletionSource<int>().Task.WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await new TaskCompletionSource<int>().Task.WaitAsync(timeout, CancellationToken.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await new TaskCompletionSource<int>().Task.WaitAsync(timeout, new CancellationToken(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.CompletedTask.WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.CompletedTask.WaitAsync(timeout, CancellationToken.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.CompletedTask.WaitAsync(timeout, new CancellationToken(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromResult(42).WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromResult(42).WaitAsync(timeout, CancellationToken.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromResult(42).WaitAsync(timeout, new CancellationToken(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromCanceled(new CancellationToken(true)).WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromCanceled(new CancellationToken(true)).WaitAsync(timeout, CancellationToken.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromCanceled(new CancellationToken(true)).WaitAsync(timeout, new CancellationToken(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromCanceled<int>(new CancellationToken(true)).WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromCanceled<int>(new CancellationToken(true)).WaitAsync(timeout, CancellationToken.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromCanceled<int>(new CancellationToken(true)).WaitAsync(timeout, new CancellationToken(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromException(new FormatException()).WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromException(new FormatException()).WaitAsync(timeout, CancellationToken.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromException(new FormatException()).WaitAsync(timeout, new CancellationToken(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromException<int>(new FormatException()).WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromException<int>(new FormatException()).WaitAsync(timeout, CancellationToken.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", async () => await Task.FromException<int>(new FormatException()).WaitAsync(timeout, new CancellationToken(true)));
        }
    }

    [Test]
    public async Task Task_WaitAsync_CanceledAndTimedOut_AlreadyCompleted_UsesTaskResult()
    {
        await Task.CompletedTask.WaitAsync(TimeSpan.Zero);
        await Task.CompletedTask.WaitAsync(new CancellationToken(true));
        await Task.CompletedTask.WaitAsync(TimeSpan.Zero, new CancellationToken(true));

        Assert.AreEqual(42, await Task.FromResult(42).WaitAsync(TimeSpan.Zero));
        Assert.AreEqual(42, await Task.FromResult(42).WaitAsync(new CancellationToken(true)));
        Assert.AreEqual(42, await Task.FromResult(42).WaitAsync(TimeSpan.Zero, new CancellationToken(true)));

        Assert.ThrowsAsync<FormatException>(async () => await Task.FromException(new FormatException()).WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<FormatException>(async () => await Task.FromException(new FormatException()).WaitAsync(new CancellationToken(true)));
        Assert.ThrowsAsync<FormatException>(async () => await Task.FromException(new FormatException()).WaitAsync(TimeSpan.Zero, new CancellationToken(true)));

        Assert.ThrowsAsync<FormatException>(async () => await Task.FromException<int>(new FormatException()).WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<FormatException>(async () => await Task.FromException<int>(new FormatException()).WaitAsync(new CancellationToken(true)));
        Assert.ThrowsAsync<FormatException>(async () => await Task.FromException<int>(new FormatException()).WaitAsync(TimeSpan.Zero, new CancellationToken(true)));

        Assert.ThrowsAsync<TaskCanceledException>(async () => await Task.FromCanceled(new CancellationToken(true)).WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<TaskCanceledException>(async () => await Task.FromCanceled(new CancellationToken(true)).WaitAsync(new CancellationToken(true)));
        Assert.ThrowsAsync<TaskCanceledException>(async () => await Task.FromCanceled(new CancellationToken(true)).WaitAsync(TimeSpan.Zero, new CancellationToken(true)));

        Assert.ThrowsAsync<TaskCanceledException>(async () => await Task.FromCanceled<int>(new CancellationToken(true)).WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<TaskCanceledException>(async () => await Task.FromCanceled<int>(new CancellationToken(true)).WaitAsync(new CancellationToken(true)));
        Assert.ThrowsAsync<TaskCanceledException>(async () => await Task.FromCanceled<int>(new CancellationToken(true)).WaitAsync(TimeSpan.Zero, new CancellationToken(true)));
    }

    [Test]
    public void Task_WaitAsync_TimeoutOrCanceled_Throws()
    {
        var tcs = new TaskCompletionSource<int>();
        var cts = new CancellationTokenSource();

        Assert.ThrowsAsync<TimeoutException>(async () => await ((Task)tcs.Task).WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<TimeoutException>(async () => await ((Task)tcs.Task).WaitAsync(TimeSpan.FromMilliseconds(1)));
        Assert.ThrowsAsync<TimeoutException>(async () => await ((Task)tcs.Task).WaitAsync(TimeSpan.FromMilliseconds(1), cts.Token));

        Assert.ThrowsAsync<TimeoutException>(async () => await tcs.Task.WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<TimeoutException>(async () => await tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(1)));
        Assert.ThrowsAsync<TimeoutException>(async () => await tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(1), cts.Token));

        Task assert1 = ((Task)tcs.Task).WaitAsync(cts.Token);
        Task assert2 = ((Task)tcs.Task).WaitAsync(Timeout.InfiniteTimeSpan, cts.Token);
        Task assert3 = tcs.Task.WaitAsync(cts.Token);
        Task assert4 = tcs.Task.WaitAsync(Timeout.InfiniteTimeSpan, cts.Token);
        Assert.False(assert1.IsCompleted);
        Assert.False(assert2.IsCompleted);
        Assert.False(assert3.IsCompleted);
        Assert.False(assert4.IsCompleted);

        cts.Cancel();
        Assert.ThrowsAsync<TaskCanceledException>(async () => await assert1);
        Assert.ThrowsAsync<TaskCanceledException>(async () => await assert2);
        Assert.ThrowsAsync<TaskCanceledException>(async () => await assert3);
        Assert.ThrowsAsync<TaskCanceledException>(async () => await assert4);
    }

    [Test]
    public async Task Task_WaitAsync_NoCancellationOrTimeoutOccurs_Success()
    {
        CancellationTokenSource cts = new CancellationTokenSource();

#if NET5_0_OR_GREATER
        var tcs = new TaskCompletionSource();
        Task t = tcs.Task.WaitAsync(TimeSpan.FromDays(1), cts.Token);
        Assert.False(t.IsCompleted);
        tcs.SetResult();
        await t;
#endif

        var tcsg = new TaskCompletionSource<int>();
        Task<int> tg = tcsg.Task.WaitAsync(TimeSpan.FromDays(1), cts.Token);
        Assert.False(tg.IsCompleted);
        tcsg.SetResult(42);
        Assert.AreEqual(42, await tg);
    }
}
