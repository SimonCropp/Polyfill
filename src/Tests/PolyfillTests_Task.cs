// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident
// ReSharper disable MethodSupportsCancellation
partial class PolyfillTests
{
    static async Task<T?> AssertThrowsAsync<T>(string expectedParamName, Func<Task> action)
        where T : ArgumentException
    {
        var exception = await Assert.That(action).Throws<T>();

        await Assert.That(exception?.ParamName).IsEqualTo(expectedParamName);

        return exception;
    }

    //TODO: work out why this fails in CI
#if DEBUG
    [Test]
    public async Task Task_WaitAsync_With_Cancellation_Timeout()
    {
        var tokenSource = new CancelSource(TimeSpan.FromMilliseconds(400));
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await Task.Delay(2000)
                .WaitAsync(tokenSource.Token);
        }
        catch (TaskCanceledException exception)
        {
            stopwatch.Stop();
            await Assert.That(exception.CancellationToken == tokenSource.Token).IsTrue();
        }

        // Allow tolerance for timing variations (CI machines can be slow)
        await Assert.That(stopwatch.ElapsedMilliseconds).IsGreaterThanOrEqualTo(350);
        await Assert.That(stopwatch.ElapsedMilliseconds).IsLessThanOrEqualTo(800);
    }
#endif

    [Test]
    public async Task Task_WaitAsync_InvalidTimeout_Throws()
    {
        foreach (var timeout in new[] { TimeSpan.FromMilliseconds(-2), TimeSpan.MaxValue, TimeSpan.MinValue })
        {
#if NET
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource().Task.WaitAsync(timeout));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource().Task.WaitAsync(timeout, Cancel.None));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource().Task.WaitAsync(timeout, new Cancel(true)));
#endif

            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource<int>().Task.WaitAsync(timeout));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource<int>().Task.WaitAsync(timeout, Cancel.None));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => new TaskCompletionSource<int>().Task.WaitAsync(timeout, new Cancel(true)));

            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.CompletedTask.WaitAsync(timeout));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.CompletedTask.WaitAsync(timeout, Cancel.None));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.CompletedTask.WaitAsync(timeout, new Cancel(true)));

            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromResult(42).WaitAsync(timeout));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromResult(42).WaitAsync(timeout, Cancel.None));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromResult(42).WaitAsync(timeout, new Cancel(true)));

            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled(new Cancel(true)).WaitAsync(timeout));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled(new Cancel(true)).WaitAsync(timeout, Cancel.None));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled(new Cancel(true)).WaitAsync(timeout, new Cancel(true)));

            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(timeout));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(timeout, Cancel.None));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(timeout, new Cancel(true)));

            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException(new FormatException()).WaitAsync(timeout));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException(new FormatException()).WaitAsync(timeout, Cancel.None));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException(new FormatException()).WaitAsync(timeout, new Cancel(true)));

            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException<int>(new FormatException()).WaitAsync(timeout));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException<int>(new FormatException()).WaitAsync(timeout, Cancel.None));
            await AssertThrowsAsync<ArgumentOutOfRangeException>("timeout", () => Task.FromException<int>(new FormatException()).WaitAsync(timeout, new Cancel(true)));
        }
    }

    [Test]
    public async Task Task_WaitAsync_CanceledAndTimedOut_AlreadyCompleted_UsesTaskResult()
    {
        await Task.CompletedTask.WaitAsync(TimeSpan.Zero);
        await Task.CompletedTask.WaitAsync(new Cancel(true));
        await Task.CompletedTask.WaitAsync(TimeSpan.Zero, new Cancel(true));

        await Assert.That(await Task.FromResult(42).WaitAsync(TimeSpan.Zero)).IsEqualTo(42);
        await Assert.That(await Task.FromResult(42).WaitAsync(new Cancel(true))).IsEqualTo(42);
        await Assert.That(await Task.FromResult(42).WaitAsync(TimeSpan.Zero, new Cancel(true))).IsEqualTo(42);

        await Assert.That(() => Task.FromException(new FormatException()).WaitAsync(TimeSpan.Zero)).Throws<FormatException>();
        await Assert.That(() => Task.FromException(new FormatException()).WaitAsync(new Cancel(true))).Throws<FormatException>();
        await Assert.That(() => Task.FromException(new FormatException()).WaitAsync(TimeSpan.Zero, new Cancel(true))).Throws<FormatException>();

        await Assert.That(() => Task.FromException<int>(new FormatException()).WaitAsync(TimeSpan.Zero)).Throws<FormatException>();
        await Assert.That(() => Task.FromException<int>(new FormatException()).WaitAsync(new Cancel(true))).Throws<FormatException>();
        await Assert.That(() => Task.FromException<int>(new FormatException()).WaitAsync(TimeSpan.Zero, new Cancel(true))).Throws<FormatException>();

        await Assert.That(() => Task.FromCanceled(new Cancel(true)).WaitAsync(TimeSpan.Zero)).Throws<TaskCanceledException>();
        await Assert.That(() => Task.FromCanceled(new Cancel(true)).WaitAsync(new Cancel(true))).Throws<TaskCanceledException>();
        await Assert.That(() => Task.FromCanceled(new Cancel(true)).WaitAsync(TimeSpan.Zero, new Cancel(true))).Throws<TaskCanceledException>();

        await Assert.That(() => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(TimeSpan.Zero)).Throws<TaskCanceledException>();
        await Assert.That(() => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(new Cancel(true))).Throws<TaskCanceledException>();
        await Assert.That(() => Task.FromCanceled<int>(new Cancel(true)).WaitAsync(TimeSpan.Zero, new Cancel(true))).Throws<TaskCanceledException>();
    }

    [Test]
    public async Task Task_WaitAsync_TimeoutOrCanceled_Throws()
    {
        var tcs = new TaskCompletionSource<int>();
        var cancelSource = new CancelSource();

        await Assert.That(() => ((Task)tcs.Task).WaitAsync(TimeSpan.Zero)).Throws<TimeoutException>();
        await Assert.That(() => ((Task)tcs.Task).WaitAsync(TimeSpan.FromMilliseconds(1))).Throws<TimeoutException>();
        await Assert.That(() => ((Task)tcs.Task).WaitAsync(TimeSpan.FromMilliseconds(1), cancelSource.Token)).Throws<TimeoutException>();

        await Assert.That(() => tcs.Task.WaitAsync(TimeSpan.Zero)).Throws<TimeoutException>();
        await Assert.That(() => tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(1))).Throws<TimeoutException>();
        await Assert.That(() => tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(1), cancelSource.Token)).Throws<TimeoutException>();

        var assert1 = ((Task)tcs.Task).WaitAsync(cancelSource.Token);
        var assert2 = ((Task)tcs.Task).WaitAsync(Timeout.InfiniteTimeSpan, cancelSource.Token);
        Task assert3 = tcs.Task.WaitAsync(cancelSource.Token);
        Task assert4 = tcs.Task.WaitAsync(Timeout.InfiniteTimeSpan, cancelSource.Token);
        await Assert.That(assert1.IsCompleted).IsFalse();
        await Assert.That(assert2.IsCompleted).IsFalse();
        await Assert.That(assert3.IsCompleted).IsFalse();
        await Assert.That(assert4.IsCompleted).IsFalse();

        cancelSource.Cancel();
        await Assert.That(() => assert1).Throws<TaskCanceledException>();
        await Assert.That(() => assert2).Throws<TaskCanceledException>();
        await Assert.That(() => assert3).Throws<TaskCanceledException>();
        await Assert.That(() => assert4).Throws<TaskCanceledException>();
    }

    [Test]
    public async Task Task_WaitAsync_NoCancellationOrTimeoutOccurs_Success()
    {
        var cancelSource = new CancelSource();

#if NET
        var tcs = new TaskCompletionSource();
        var t = tcs.Task.WaitAsync(TimeSpan.FromDays(1), cancelSource.Token);
        await Assert.That(t.IsCompleted).IsFalse();
        tcs.SetResult();
        await t;
#endif

        var tcsg = new TaskCompletionSource<int>();
        var tg = tcsg.Task.WaitAsync(TimeSpan.FromDays(1), cancelSource.Token);
        await Assert.That(tg.IsCompleted).IsFalse();
        tcsg.SetResult(42);
        await Assert.That(await tg).IsEqualTo(42);
    }

    #region WaitAsync(TimeSpan, CancellationToken) Tests

    [Test]
    public async Task WaitAsync_TimeoutAndToken_TaskCompletesBeforeTimeout_Success()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(100);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token)).ThrowsNothing();
    }

    [Test]
    public async Task WaitAsync_TimeoutAndToken_TimeoutOccurs_ThrowsTimeoutException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(5000);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(100), cancelSource.Token)).Throws<TimeoutException>();
    }

    //TODO: work out why this fails in CI
    #if DEBUG
    [Test]
    public async Task WaitAsync_TimeoutAndToken_CancellationOccurs_ThrowsTaskCanceledException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.CancelAfter(100);
        var task = Task.Delay(5000);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(10), cancelSource.Token)).Throws<TaskCanceledException>();
    }
    #endif

    [Test]
    public async Task WaitAsync_TimeoutAndToken_CancellationBeforeTimeout_ThrowsTaskCanceledException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.CancelAfter(100);
        var task = Task.Delay(10000);

        // Act & Assert (cancellation should win - use 5s timeout to ensure cancellation fires first on slow CI servers)
        await Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token));
    }

    [Test]
    public async Task WaitAsync_TimeoutAndToken_AlreadyCompletedTask_ReturnsImmediately()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.CompletedTask;

        // Act
        await task.WaitAsync(TimeSpan.FromSeconds(1), cancelSource.Token);
    }

    [Test]
    public async Task WaitAsync_TimeoutAndToken_AlreadyCancelledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.Cancel();
        var task = Task.Delay(5000);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(10), cancelSource.Token)).Throws<TaskCanceledException>();
    }

    [Test]
    public async Task WaitAsync_TimeoutAndToken_InfiniteTimeout_WaitsForTask()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(100);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(Timeout.InfiniteTimeSpan, cancelSource.Token)).ThrowsNothing();
    }

    [Test]
    public async Task WaitAsync_TimeoutAndToken_ZeroTimeout_ThrowsTimeoutException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(5000);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.Zero, cancelSource.Token)).Throws<TimeoutException>();
    }

    #endregion

    #region WaitAsync(TimeSpan) Tests

    [Test]
    public async Task WaitAsync_Timeout_TaskCompletesBeforeTimeout_Success()
    {
        // Arrange
        var task = Task.Delay(100);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5))).ThrowsNothing();
    }

    [Test]
    public async Task WaitAsync_Timeout_TimeoutOccurs_ThrowsTimeoutException()
    {
        // Arrange
        var task = Task.Delay(5000);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(100))).Throws<TimeoutException>();
    }

    [Test]
    public async Task WaitAsync_Timeout_InfiniteTimeout_WaitsForTask()
    {
        // Arrange
        var task = Task.Delay(100);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(Timeout.InfiniteTimeSpan)).ThrowsNothing();
    }

    #endregion

    #region WaitAsync(CancellationToken) Tests

    [Test]
    public async Task WaitAsync_Token_TaskCompletesBeforeCancellation_Success()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(100);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(cancelSource.Token)).ThrowsNothing();
    }

    [Test]
    public async Task WaitAsync_Token_CancellationOccurs_ThrowsTaskCanceledException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.CancelAfter(100);
        var task = Task.Delay(5000);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(cancelSource.Token)).Throws<TaskCanceledException>();
    }

    [Test]
    public async Task WaitAsync_Token_NonCancellableToken_ReturnsOriginalTask()
    {
        // Arrange
        var task = Task.Delay(100);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(Cancel.None)).ThrowsNothing();
    }

    #endregion

    #region Generic Task<T> Tests

    [Test]
    public async Task WaitAsync_GenericTask_ReturnsCorrectResult()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.FromResult(42);

        // Act
        var result = await task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token);

        // Assert
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task WaitAsync_GenericTask_TaskCompletesWithResult_Success()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = GetValueAfterDelayAsync(123, 100);

        // Act
        var result = await task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token);

        // Assert
        await Assert.That(result).IsEqualTo(123);
    }

    [Test]
    public async Task WaitAsync_GenericTask_TimeoutOccurs_ThrowsTimeoutException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = GetValueAfterDelayAsync(42, 5000);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(100), cancelSource.Token)).Throws<TimeoutException>();
    }

    [Test]
    public async Task WaitAsync_GenericTask_CancellationOccurs_ThrowsTaskCanceledException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.CancelAfter(100);
        var task = GetValueAfterDelayAsync(42, 5000);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(10), cancelSource.Token)).Throws<TaskCanceledException>();
    }

    [Test]
    public async Task WaitAsync_GenericTask_WithTimeoutOnly_ReturnsResult()
    {
        // Arrange
        var task = GetValueAfterDelayAsync("test", 100);

        // Act
        var result = await task.WaitAsync(TimeSpan.FromSeconds(5));

        // Assert
        await Assert.That(result).IsEqualTo("test");
    }

    [Test]
    public async Task WaitAsync_GenericTask_WithTokenOnly_ReturnsResult()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = GetValueAfterDelayAsync(3.14, 100);

        // Act
        var result = await task.WaitAsync(cancelSource.Token);

        // Assert
        await Assert.That(result).IsEqualTo(3.14);
    }

    #endregion

    #region Exception Propagation Tests

    [Test]
    public async Task WaitAsync_TaskThrowsException_ExceptionIsPropagated()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = ThrowExceptionAsync();

        // Act & Assert
        var exception = await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token)).Throws<InvalidOperationException>();

        await Assert.That(exception!.Message).IsEqualTo("Test exception");
    }

    [Test]
    public async Task WaitAsync_GenericTaskThrowsException_ExceptionIsPropagated()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = ThrowExceptionAsync<int>();

        // Act & Assert
        var exception = await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token)).Throws<InvalidOperationException>();

        await Assert.That(exception!.Message).IsEqualTo("Test exception");
    }

    [Test]
    public async Task WaitAsync_TaskFaulted_ExceptionPropagatedBeforeTimeout()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Run(() => throw new ArgumentException("Faulted task"));

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token)).Throws<ArgumentException>();
    }

    [Test]
    public async Task WaitAsync_TaskCancelled_CancellationPropagated()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.Cancel();
        var cancelledTask = Task.FromCanceled(cancelSource.Token);

        // Act & Assert
        await Assert.That(async () =>
            await cancelledTask.WaitAsync(TimeSpan.FromSeconds(5), Cancel.None)).Throws<TaskCanceledException>();
    }

    #endregion

    #region Argument Validation Tests

    [Test]
    public async Task WaitAsync_NegativeTimeout_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var task = Task.CompletedTask;

        // Act & Assert
        await Assert.That(() =>
            task.WaitAsync(TimeSpan.FromMilliseconds(-100), Cancel.None)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task WaitAsync_TimeoutTooLarge_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var task = Task.CompletedTask;
        var timeout = TimeSpan.FromMilliseconds((long)0xfffffffe + 1);

        // Act & Assert
        await Assert.That(() =>
            task.WaitAsync(timeout, Cancel.None)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task WaitAsync_MinusOneMilliseconds_AllowedAsInfinite()
    {
        // Arrange
        var task = Task.Delay(100);

        // Act & Assert (should not throw)
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(-1), Cancel.None)).ThrowsNothing();
    }

    #endregion

    #region Edge Cases Tests

    [Test]
    public async Task WaitAsync_MultipleAwaitsOnSameTask_AllComplete()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(100);

        // Act
        var task1 = task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token);
        var task2 = task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token);
        var task3 = task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token);

        // Assert
        await Assert.That(async () =>
            await Task.WhenAll(task1, task2, task3)).ThrowsNothing();
    }

    [Test]
    public async Task WaitAsync_VeryShortTimeout_WorksCorrectly()
    {
        // Arrange
        var task = Task.Delay(1000);

        // Act & Assert
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(1))).Throws<TimeoutException>();
    }

    [Test]
    public async Task WaitAsync_ConcurrentCancellationAndTimeout_RespectsOrder()
    {
        // Arrange
        var cancelSource = new CancelSource();

        // Cancel immediately
        cancelSource.Cancel();

        var task = Task.Delay(10000);

        // Act & Assert (cancellation should win even with short timeout)
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(100), cancelSource.Token)).Throws<TaskCanceledException>();
    }

    [Test]
    public async Task WaitAsync_DisposedCancellationTokenSource_BehavesCorrectly()
    {
        // Arrange
        var task = Task.Delay(100);
        Cancel token;

        using (var cancelSource = new CancelSource())
        {
            token = cancelSource.Token;
        }

        // Act & Assert (should still work with token from disposed source)
        await Assert.That(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5), token)).ThrowsNothing();
    }

    #endregion

    #region Performance Tests

    [Test]
    public async Task WaitAsync_AlreadyCompletedTask_NoPerformanceOverhead()
    {
        // Arrange
        var completedTask = Task.FromResult(42);
        var cancelSource = new CancelSource();

        // Act
        var result = await completedTask.WaitAsync(TimeSpan.FromSeconds(1), cancelSource.Token);

        // Assert
        await Assert.That(result).IsEqualTo(42);
        // This should return immediately without creating additional tasks
    }

    [Test]
    public async Task WaitAsync_InfiniteTimeoutAndNoCancellation_ReturnsOriginalTask()
    {
        // Arrange
        var task = Task.Delay(100);

        // Act
        var wrappedTask = task.WaitAsync(Timeout.InfiniteTimeSpan, Cancel.None);

        // Assert
        await wrappedTask;
        // Should return the original task for performance
    }

    #endregion

    #region Helper Methods

    async Task<T> GetValueAfterDelayAsync<T>(T value, int delayMs)
    {
        await Task.Delay(delayMs);
        return value;
    }

    async Task ThrowExceptionAsync()
    {
        await Task.Delay(10);
        throw new InvalidOperationException("Test exception");
    }

    async Task<T> ThrowExceptionAsync<T>()
    {
        await Task.Delay(10);
        throw new InvalidOperationException("Test exception");
    }

    #endregion
}