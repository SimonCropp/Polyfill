// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident
// ReSharper disable MethodSupportsCancellation
partial class PolyfillTests
{
    static T? AssertThrowsAsync<T>(string expectedParamName, AsyncTestDelegate action)
        where T : ArgumentException
    {
        var exception = Assert.ThrowsAsync<T>(action);

        Assert.AreEqual(expectedParamName, exception?.ParamName);

        return exception;
    }

    [Test]
    public void Task_WaitAsync_InvalidTimeout_Throws()
    {
        foreach (var timeout in new[] { TimeSpan.FromMilliseconds(-2), TimeSpan.MaxValue, TimeSpan.MinValue })
        {
#if NET
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource().Task.WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource().Task.WaitAsync(timeout, Cancel.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource().Task.WaitAsync(timeout, new Cancel(true)));
#endif

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource<int>().Task.WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource<int>().Task.WaitAsync(timeout, Cancel.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource<int>().Task.WaitAsync(timeout, new Cancel(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.CompletedTask.WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.CompletedTask.WaitAsync(timeout, Cancel.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.CompletedTask.WaitAsync(timeout, new Cancel(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromResult(42).WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromResult(42).WaitAsync(timeout, Cancel.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromResult(42).WaitAsync(timeout, new Cancel(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled(new Cancel(true)).WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled(new Cancel(true)).WaitAsync(timeout, Cancel.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled(new Cancel(true)).WaitAsync(timeout, new Cancel(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(timeout, Cancel.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(timeout, new Cancel(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException(new FormatException()).WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException(new FormatException()).WaitAsync(timeout, Cancel.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException(new FormatException()).WaitAsync(timeout, new Cancel(true)));

            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException<int>(new FormatException()).WaitAsync(timeout));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException<int>(new FormatException()).WaitAsync(timeout, Cancel.None));
            AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException<int>(new FormatException()).WaitAsync(timeout, new Cancel(true)));
        }
    }

    [Test]
    public async Task Task_WaitAsync_CanceledAndTimedOut_AlreadyCompleted_UsesTaskResult()
    {
        await Task.CompletedTask.WaitAsync(TimeSpan.Zero);
        await Task.CompletedTask.WaitAsync(new Cancel(true));
        await Task.CompletedTask.WaitAsync(TimeSpan.Zero, new Cancel(true));

        Assert.AreEqual(42, await Task.FromResult(42).WaitAsync(TimeSpan.Zero));
        Assert.AreEqual(42, await Task.FromResult(42).WaitAsync(new Cancel(true)));
        Assert.AreEqual(42, await Task.FromResult(42).WaitAsync(TimeSpan.Zero, new Cancel(true)));

        Assert.ThrowsAsync<FormatException>(() => Task.FromException(new FormatException()).WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<FormatException>(() => Task.FromException(new FormatException()).WaitAsync(new Cancel(true)));
        Assert.ThrowsAsync<FormatException>(() => Task.FromException(new FormatException()).WaitAsync(TimeSpan.Zero, new Cancel(true)));

        Assert.ThrowsAsync<FormatException>(() => Task.FromException<int>(new FormatException()).WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<FormatException>(() => Task.FromException<int>(new FormatException()).WaitAsync(new Cancel(true)));
        Assert.ThrowsAsync<FormatException>(() => Task.FromException<int>(new FormatException()).WaitAsync(TimeSpan.Zero, new Cancel(true)));

        Assert.ThrowsAsync<TaskCanceledException>(() => Task.FromCanceled(new Cancel(true)).WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<TaskCanceledException>(() => Task.FromCanceled(new Cancel(true)).WaitAsync(new Cancel(true)));
        Assert.ThrowsAsync<TaskCanceledException>(() => Task.FromCanceled(new Cancel(true)).WaitAsync(TimeSpan.Zero, new Cancel(true)));

        Assert.ThrowsAsync<TaskCanceledException>(() => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<TaskCanceledException>(() => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(new Cancel(true)));
        Assert.ThrowsAsync<TaskCanceledException>(() => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(TimeSpan.Zero, new Cancel(true)));
    }

    [Test]
    public void Task_WaitAsync_TimeoutOrCanceled_Throws()
    {
        var tcs = new TaskCompletionSource<int>();
        var cancelSource = new CancelSource();

        Assert.ThrowsAsync<TimeoutException>(() => ((Task)tcs.Task).WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<TimeoutException>(() => ((Task)tcs.Task).WaitAsync(TimeSpan.FromMilliseconds(1)));
        Assert.ThrowsAsync<TimeoutException>(() => ((Task)tcs.Task).WaitAsync(TimeSpan.FromMilliseconds(1), cancelSource.Token));

        Assert.ThrowsAsync<TimeoutException>(() => tcs.Task.WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<TimeoutException>(() => tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(1)));
        Assert.ThrowsAsync<TimeoutException>(() => tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(1), cancelSource.Token));

        var assert1 = ((Task)tcs.Task).WaitAsync(cancelSource.Token);
        var assert2 = ((Task)tcs.Task).WaitAsync(Timeout.InfiniteTimeSpan, cancelSource.Token);
        Task assert3 = tcs.Task.WaitAsync(cancelSource.Token);
        Task assert4 = tcs.Task.WaitAsync(Timeout.InfiniteTimeSpan, cancelSource.Token);
        Assert.False(assert1.IsCompleted);
        Assert.False(assert2.IsCompleted);
        Assert.False(assert3.IsCompleted);
        Assert.False(assert4.IsCompleted);

        cancelSource.Cancel();
        Assert.ThrowsAsync<TaskCanceledException>(() => assert1);
        Assert.ThrowsAsync<TaskCanceledException>(() => assert2);
        Assert.ThrowsAsync<TaskCanceledException>(() => assert3);
        Assert.ThrowsAsync<TaskCanceledException>(() => assert4);
    }

    [Test]
    public async Task Task_WaitAsync_NoCancellationOrTimeoutOccurs_Success()
    {
        var cancelSource = new CancelSource();

#if NET
        var tcs = new TaskCompletionSource();
        var t = tcs.Task.WaitAsync(TimeSpan.FromDays(1), cancelSource.Token);
        Assert.False(t.IsCompleted);
        tcs.SetResult();
        await t;
#endif

        var tcsg = new TaskCompletionSource<int>();
        var tg = tcsg.Task.WaitAsync(TimeSpan.FromDays(1), cancelSource.Token);
        Assert.False(tg.IsCompleted);
        tcsg.SetResult(42);
        Assert.AreEqual(42, await tg);
    }
}
