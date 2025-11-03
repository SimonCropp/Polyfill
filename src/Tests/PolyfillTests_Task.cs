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
        foreach (var timeout in new[] {TimeSpan.FromMilliseconds(-2), TimeSpan.MaxValue, TimeSpan.MinValue})
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

        Assert.ThrowsAsync<TimeoutException>(() => ((Task) tcs.Task).WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<TimeoutException>(() => ((Task) tcs.Task).WaitAsync(TimeSpan.FromMilliseconds(1)));
        Assert.ThrowsAsync<TimeoutException>(() => ((Task) tcs.Task).WaitAsync(TimeSpan.FromMilliseconds(1), cancelSource.Token));

        Assert.ThrowsAsync<TimeoutException>(() => tcs.Task.WaitAsync(TimeSpan.Zero));
        Assert.ThrowsAsync<TimeoutException>(() => tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(1)));
        Assert.ThrowsAsync<TimeoutException>(() => tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(1), cancelSource.Token));

        var assert1 = ((Task) tcs.Task).WaitAsync(cancelSource.Token);
        var assert2 = ((Task) tcs.Task).WaitAsync(Timeout.InfiniteTimeSpan, cancelSource.Token);
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

    #region Non-Generic Task Tests

    [Test]
    public async Task WaitAsync_TaskCompletesBeforeCancellation_CompletesSuccessfully()
    {
        // Arrange
        var tcs = new TaskCompletionSource<bool>();
        var cts = new CancelSource();

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);
        tcs.SetResult(true);

        // Assert
        await waitTask;
        Assert.IsTrue(waitTask.IsCompleted);
        Assert.IsFalse(waitTask.IsCanceled);
        Assert.IsFalse(waitTask.IsFaulted);
    }

    [Test]
    public void WaitAsync_CancellationBeforeTaskCompletes_ThrowsOperationCanceledException()
    {
        // Arrange
        var tcs = new TaskCompletionSource<bool>();
        var cts = new CancelSource();

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);
        cts.Cancel();

        // Assert
        Assert.ThrowsAsync<OperationCanceledException>(() => waitTask);
        Assert.IsTrue(waitTask.IsCanceled);
    }

    [Test]
    public void WaitAsync_TaskFaultsBeforeCancellation_PropagatesException()
    {
        // Arrange
        var tcs = new TaskCompletionSource<bool>();
        var cts = new CancelSource();
        var expectedException = new InvalidOperationException("Test exception");

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);
        tcs.SetException(expectedException);

        // Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => waitTask)!;
        Assert.AreEqual(expectedException.Message, ex.Message);
        Assert.IsTrue(waitTask.IsFaulted);
    }

    [Test]
    public async Task WaitAsync_AlreadyCompletedTask_ReturnsImmediately()
    {
        // Arrange
        var completedTask = Task.FromResult(true);
        var cts = new CancelSource();

        // Act
        var waitTask = completedTask.WaitAsync(cts.Token);

        // Assert
        await waitTask;
        Assert.IsTrue(waitTask.IsCompleted);
        Assert.AreSame(completedTask, waitTask); // Should return the same task instance
    }

    [Test]
    public void WaitAsync_AlreadyCancelledToken_ThrowsOperationCanceledException()
    {
        // Arrange
        var tcs = new TaskCompletionSource<bool>();
        var cts = new CancelSource();
        cts.Cancel();

        // Act & Assert
        Assert.ThrowsAsync<OperationCanceledException>(() => tcs.Task.WaitAsync(cts.Token));
    }

    [Test]
    public async Task WaitAsync_NonCancellableToken_ReturnsOriginalTask()
    {
        // Arrange
        var tcs = new TaskCompletionSource<bool>();

        // Act
        var waitTask = tcs.Task.WaitAsync(Cancel.None);
        tcs.SetResult(true);

        // Assert
        await waitTask;
        Assert.AreSame(tcs.Task, waitTask); // Should return the same task instance
    }

    [Test]
    public async Task WaitAsync_CancellationDoesNotStopOriginalTask()
    {
        // Arrange
        var taskStarted = new TaskCompletionSource<bool>();
        var taskCompleted = new TaskCompletionSource<bool>();
        var cts = new CancelSource();

        var task = Task.Run(async () =>
        {
            taskStarted.SetResult(true);
            await Task.Delay(2000);
            taskCompleted.SetResult(true);
        });

        // Wait for task to start
        await taskStarted.Task;

        // Act - Cancel the WaitAsync
        var waitTask = task.WaitAsync(cts.Token);
        cts.Cancel();

        // Assert - WaitAsync throws but original task continues
        Assert.ThrowsAsync<OperationCanceledException>(() => waitTask);

        // Wait a bit more to ensure original task can complete
        await Task.Delay(2500);
        Assert.IsTrue(taskCompleted.Task.IsCompleted);
    }

    [Test]
    public async Task WaitAsync_MultipleCallsOnSameTask_WorkIndependently()
    {
        // Arrange
        var tcs = new TaskCompletionSource<bool>();
        var cts1 = new CancelSource();
        var cts2 = new CancelSource();

        // Act
        var wait1 = tcs.Task.WaitAsync(cts1.Token);
        var wait2 = tcs.Task.WaitAsync(cts2.Token);

        cts1.Cancel(); // Cancel only first one
        tcs.SetResult(true); // Complete the original task

        // Assert
        Assert.ThrowsAsync<OperationCanceledException>(() => wait1);
        await wait2; // Should complete successfully
        Assert.IsTrue(wait2.IsCompleted);
        Assert.IsFalse(wait2.IsCanceled);
    }

    [Test]
    public async Task WaitAsync_RaceCondition_CancellationAndCompletionSimultaneous()
    {
        // This test verifies behavior when cancellation and completion happen very close together
        // Arrange
        var tcs = new TaskCompletionSource<bool>();
        var cts = new CancelSource();

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);

        // Complete and cancel nearly simultaneously
        var t1 = Task.Run(() => tcs.SetResult(true));
        var t2 = Task.Run(() => cts.Cancel());

        await Task.WhenAll(t1, t2);

        // Assert - Either outcome is valid
        try
        {
            await waitTask;
            Assert.IsTrue(waitTask.IsCompleted);
        }
        catch (OperationCanceledException)
        {
            Assert.IsTrue(waitTask.IsCanceled);
        }
    }

    #endregion

    #region Generic Task<TResult> Tests

    [Test]
    public async Task WaitAsync_Generic_TaskCompletesBeforeCancellation_ReturnsResult()
    {
        // Arrange
        var tcs = new TaskCompletionSource<int>();
        var cts = new CancelSource();
        const int expectedResult = 42;

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);
        tcs.SetResult(expectedResult);

        // Assert
        var result = await waitTask;
        Assert.AreEqual(expectedResult, result);
        Assert.IsTrue(waitTask.IsCompleted);
        Assert.IsFalse(waitTask.IsCanceled);
        Assert.IsFalse(waitTask.IsFaulted);
    }

    [Test]
    public void WaitAsync_Generic_CancellationBeforeTaskCompletes_ThrowsOperationCanceledException()
    {
        // Arrange
        var tcs = new TaskCompletionSource<int>();
        var cts = new CancelSource();

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);
        cts.Cancel();

        // Assert
        Assert.ThrowsAsync<OperationCanceledException>(() => waitTask);
        Assert.IsTrue(waitTask.IsCanceled);
    }

    [Test]
    public void WaitAsync_Generic_TaskFaultsBeforeCancellation_PropagatesException()
    {
        // Arrange
        var tcs = new TaskCompletionSource<string>();
        var cts = new CancelSource();
        var expectedException = new DivideByZeroException("Cannot divide by zero");

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);
        tcs.SetException(expectedException);

        // Assert
        var ex = Assert.ThrowsAsync<DivideByZeroException>(() => waitTask)!;
        Assert.AreEqual(expectedException.Message, ex.Message);
        Assert.IsTrue(waitTask.IsFaulted);
    }

    [Test]
    public async Task WaitAsync_Generic_AlreadyCompletedTask_ReturnsImmediately()
    {
        // Arrange
        var completedTask = Task.FromResult(123);
        var cts = new CancelSource();

        // Act
        var waitTask = completedTask.WaitAsync(cts.Token);

        // Assert
        var result = await waitTask;
        Assert.AreEqual(123, result);
        Assert.AreSame(completedTask, waitTask); // Should return the same task instance
    }

    [Test]
    public void WaitAsync_Generic_AlreadyCancelledToken_ThrowsOperationCanceledException()
    {
        // Arrange
        var tcs = new TaskCompletionSource<double>();
        var cts = new CancelSource();
        cts.Cancel();

        // Act & Assert
        Assert.ThrowsAsync<OperationCanceledException>(() => tcs.Task.WaitAsync(cts.Token));
    }

    [Test]
    public async Task WaitAsync_Generic_NonCancellableToken_ReturnsOriginalTask()
    {
        // Arrange
        var tcs = new TaskCompletionSource<bool>();

        // Act
        var waitTask = tcs.Task.WaitAsync(Cancel.None);
        tcs.SetResult(true);

        // Assert
        await waitTask;
        Assert.AreSame(tcs.Task, waitTask); // Should return the same task instance
    }

    [Test]
    public async Task WaitAsync_Generic_ComplexType_WorksCorrectly()
    {
        // Arrange
        var tcs = new TaskCompletionSource<ComplexObject>();
        var cts = new CancelSource();
        var expectedObject = new ComplexObject {Id = 1, Name = "Test", Values = new[] {1, 2, 3}};

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);
        tcs.SetResult(expectedObject);

        // Assert
        var result = await waitTask;
        Assert.AreEqual(expectedObject.Id, result.Id);
        Assert.AreEqual(expectedObject.Name, result.Name);
        Assert.AreEqual(expectedObject.Values, result.Values);
    }

    [Test]
    public async Task WaitAsync_Generic_NullResult_WorksCorrectly()
    {
        // Arrange
        var tcs = new TaskCompletionSource<string?>();
        var cts = new CancelSource();

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);
        tcs.SetResult(null);

        // Assert
        var result = await waitTask;
        Assert.IsNull(result);
    }

    #endregion

    #region Performance and Timing Tests

    [Test]
    public async Task WaitAsync_CancellationOccursQuickly()
    {
        // Arrange
        var tcs = new TaskCompletionSource<bool>();
        var cts = new CancelSource();
        var stopwatch = Stopwatch.StartNew();

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);
        cts.Cancel();

        try
        {
            await waitTask;
        }
        catch (OperationCanceledException)
        {
            stopwatch.Stop();
        }

        // Assert - Cancellation should be fast (under 100ms)
        Assert.IsTrue(stopwatch.ElapsedMilliseconds < 100,
            $"Cancellation took {stopwatch.ElapsedMilliseconds}ms, expected < 100ms");
    }

    [Test]
    public async Task WaitAsync_WithRealDelay_CancelsCorrectly()
    {
        // Arrange
        var cts = new CancelSource();
        var task = Task.Delay(10000); // 10 second delay
        var stopwatch = Stopwatch.StartNew();

        // Act
        var waitTask = task.WaitAsync(cts.Token);
        await Task.Delay(100); // Let it start
        cts.Cancel();

        // Assert
        Assert.ThrowsAsync<OperationCanceledException>(() => waitTask);
        stopwatch.Stop();

        // Should cancel quickly, not wait for the 10 second delay
        Assert.IsTrue(stopwatch.ElapsedMilliseconds < 1000,
            $"Cancellation took {stopwatch.ElapsedMilliseconds}ms, expected < 1000ms");
    }

    #endregion

    #region Edge Cases

    [Test]
    public void WaitAsync_AggregateException_UnwrapsCorrectly()
    {
        // Arrange
        var tcs = new TaskCompletionSource<int>();
        var cts = new CancelSource();
        var innerException = new InvalidOperationException("Inner exception");

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);
        tcs.SetException(innerException);

        // Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => waitTask)!;
        Assert.AreEqual(innerException.Message, ex.Message);
    }

    [Test]
    public async Task WaitAsync_MultipleExceptions_PropagatesFirst()
    {
        // Arrange
        var tcs = new TaskCompletionSource<int>();
        var cts = new CancelSource();
        var exceptions = new Exception[]
        {
            new InvalidOperationException("First"),
            new ArgumentException("Second")
        };

        // Act
        var waitTask = tcs.Task.WaitAsync(cts.Token);
        tcs.SetException(exceptions);

        // Assert
        try
        {
            await waitTask;
            Assert.IsTrue(false, "Should have thrown");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("First", ex.Message);
        }
        catch (AggregateException)
        {
            // Also acceptable behavior
        }
    }

    [Test]
    public async Task WaitAsync_DisposedCancellationTokenSource_HandlesGracefully()
    {
        // Arrange
        var tcs = new TaskCompletionSource<bool>();
        var cts = new CancelSource();
        var token = cts.Token;

        // Act
        var waitTask = tcs.Task.WaitAsync(token);
        cts.Dispose();
        tcs.SetResult(true);

        // Assert - Should still complete if task finishes
        await waitTask;
        Assert.IsTrue(waitTask.IsCompleted);
    }

    #endregion

    // Helper class for complex type testing
    private class ComplexObject
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required int[] Values { get; init; }
    }

}
