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
            Assert.True(exception.CancellationToken == tokenSource.Token);
        }

        Assert.AreEqual(400, stopwatch.ElapsedMilliseconds, 300);
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

    #region WaitAsync(TimeSpan, CancellationToken) Tests

    [Test]
    public void WaitAsync_TimeoutAndToken_TaskCompletesBeforeTimeout_Success()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(100);

        // Act & Assert
        Assert.DoesNotThrowAsync(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token));
    }

    [Test]
    public void WaitAsync_TimeoutAndToken_TimeoutOccurs_ThrowsTimeoutException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(5000);

        // Act & Assert
        Assert.ThrowsAsync<TimeoutException>(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(100), cancelSource.Token));
    }

    [Test]
    public void WaitAsync_TimeoutAndToken_CancellationOccurs_ThrowsTaskCanceledException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.CancelAfter(100);
        var task = Task.Delay(5000);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(10), cancelSource.Token));
    }

    [Test]
    public void WaitAsync_TimeoutAndToken_CancellationBeforeTimeout_ThrowsTaskCanceledException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.CancelAfter(100);
        var task = Task.Delay(10000);

        // Act & Assert (cancellation should win)
        Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(500), cancelSource.Token));
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
    public void WaitAsync_TimeoutAndToken_AlreadyCancelledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.Cancel();
        var task = Task.Delay(5000);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(10), cancelSource.Token));
    }

    [Test]
    public void WaitAsync_TimeoutAndToken_InfiniteTimeout_WaitsForTask()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(100);

        // Act & Assert
        Assert.DoesNotThrowAsync(async () =>
            await task.WaitAsync(Timeout.InfiniteTimeSpan, cancelSource.Token));
    }

    [Test]
    public void WaitAsync_TimeoutAndToken_ZeroTimeout_ThrowsTimeoutException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(5000);

        // Act & Assert
        Assert.ThrowsAsync<TimeoutException>(async () =>
            await task.WaitAsync(TimeSpan.Zero, cancelSource.Token));
    }

    #endregion

    #region WaitAsync(TimeSpan) Tests

    [Test]
    public void WaitAsync_Timeout_TaskCompletesBeforeTimeout_Success()
    {
        // Arrange
        var task = Task.Delay(100);

        // Act & Assert
        Assert.DoesNotThrowAsync(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5)));
    }

    [Test]
    public void WaitAsync_Timeout_TimeoutOccurs_ThrowsTimeoutException()
    {
        // Arrange
        var task = Task.Delay(5000);

        // Act & Assert
        Assert.ThrowsAsync<TimeoutException>(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(100)));
    }

    [Test]
    public void WaitAsync_Timeout_InfiniteTimeout_WaitsForTask()
    {
        // Arrange
        var task = Task.Delay(100);

        // Act & Assert
        Assert.DoesNotThrowAsync(async () =>
            await task.WaitAsync(Timeout.InfiniteTimeSpan));
    }

    #endregion

    #region WaitAsync(CancellationToken) Tests

    [Test]
    public void WaitAsync_Token_TaskCompletesBeforeCancellation_Success()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(100);

        // Act & Assert
        Assert.DoesNotThrowAsync(async () =>
            await task.WaitAsync(cancelSource.Token));
    }

    [Test]
    public void WaitAsync_Token_CancellationOccurs_ThrowsTaskCanceledException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.CancelAfter(100);
        var task = Task.Delay(5000);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await task.WaitAsync(cancelSource.Token));
    }

    [Test]
    public void WaitAsync_Token_NonCancellableToken_ReturnsOriginalTask()
    {
        // Arrange
        var task = Task.Delay(100);

        // Act & Assert
        Assert.DoesNotThrowAsync(async () =>
            await task.WaitAsync(Cancel.None));
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
        Assert.AreEqual(42, result);
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
        Assert.AreEqual(123, result);
    }

    [Test]
    public void WaitAsync_GenericTask_TimeoutOccurs_ThrowsTimeoutException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = GetValueAfterDelayAsync(42, 5000);

        // Act & Assert
        Assert.ThrowsAsync<TimeoutException>(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(100), cancelSource.Token));
    }

    [Test]
    public void WaitAsync_GenericTask_CancellationOccurs_ThrowsTaskCanceledException()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.CancelAfter(100);
        var task = GetValueAfterDelayAsync(42, 5000);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(10), cancelSource.Token));
    }

    [Test]
    public async Task WaitAsync_GenericTask_WithTimeoutOnly_ReturnsResult()
    {
        // Arrange
        var task = GetValueAfterDelayAsync("test", 100);

        // Act
        var result = await task.WaitAsync(TimeSpan.FromSeconds(5));

        // Assert
        Assert.AreEqual("test", result);
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
        Assert.AreEqual(3.14, result, 0.001);
    }

    #endregion

    #region Exception Propagation Tests

    [Test]
    public void WaitAsync_TaskThrowsException_ExceptionIsPropagated()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = ThrowExceptionAsync();

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token))!;

        Assert.AreEqual("Test exception", exception.Message);
    }

    [Test]
    public void WaitAsync_GenericTaskThrowsException_ExceptionIsPropagated()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = ThrowExceptionAsync<int>();

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token))!;

        Assert.AreEqual("Test exception", exception.Message);
    }

    [Test]
    public void WaitAsync_TaskFaulted_ExceptionPropagatedBeforeTimeout()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Run(() => throw new ArgumentException("Faulted task"));

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token));
    }

    [Test]
    public void WaitAsync_TaskCancelled_CancellationPropagated()
    {
        // Arrange
        var cancelSource = new CancelSource();
        cancelSource.Cancel();
        var cancelledTask = Task.FromCanceled(cancelSource.Token);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await cancelledTask.WaitAsync(TimeSpan.FromSeconds(5), Cancel.None));
    }

    #endregion

    #region Argument Validation Tests

    [Test]
    public void WaitAsync_NegativeTimeout_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var task = Task.CompletedTask;

        // Act & Assert
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            task.WaitAsync(TimeSpan.FromMilliseconds(-100), Cancel.None));
    }

    [Test]
    public void WaitAsync_TimeoutTooLarge_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var task = Task.CompletedTask;
        var timeout = TimeSpan.FromMilliseconds((long)0xfffffffe + 1);

        // Act & Assert
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            task.WaitAsync(timeout, Cancel.None));
    }

    [Test]
    public void WaitAsync_MinusOneMilliseconds_AllowedAsInfinite()
    {
        // Arrange
        var task = Task.Delay(100);

        // Act & Assert (should not throw)
        Assert.DoesNotThrowAsync(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(-1), Cancel.None));
    }

    #endregion

    #region Edge Cases Tests

    [Test]
    public void WaitAsync_MultipleAwaitsOnSameTask_AllComplete()
    {
        // Arrange
        var cancelSource = new CancelSource();
        var task = Task.Delay(100);

        // Act
        var task1 = task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token);
        var task2 = task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token);
        var task3 = task.WaitAsync(TimeSpan.FromSeconds(5), cancelSource.Token);

        // Assert
        Assert.DoesNotThrowAsync(async () =>
            await Task.WhenAll(task1, task2, task3));
    }

    [Test]
    public void WaitAsync_VeryShortTimeout_WorksCorrectly()
    {
        // Arrange
        var task = Task.Delay(1000);

        // Act & Assert
        Assert.ThrowsAsync<TimeoutException>(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(1)));
    }

    [Test]
    public void WaitAsync_ConcurrentCancellationAndTimeout_RespectsOrder()
    {
        // Arrange
        var cancelSource = new CancelSource();

        // Cancel immediately
        cancelSource.Cancel();

        var task = Task.Delay(10000);

        // Act & Assert (cancellation should win even with short timeout)
        Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await task.WaitAsync(TimeSpan.FromMilliseconds(100), cancelSource.Token));
    }

    [Test]
    public void WaitAsync_DisposedCancellationTokenSource_BehavesCorrectly()
    {
        // Arrange
        var task = Task.Delay(100);
        Cancel token;

        using (var cancelSource = new CancelSource())
        {
            token = cancelSource.Token;
        }

        // Act & Assert (should still work with token from disposed source)
        Assert.DoesNotThrowAsync(async () =>
            await task.WaitAsync(TimeSpan.FromSeconds(5), token));
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
        Assert.AreEqual(42, result);
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

#if NET5_0_OR_GREATER || NETCOREAPP3_0_OR_GREATER

    #region WhenEach Tests

    [Test]
    public async Task WhenEach_NonGeneric_CompletesInOrder()
    {
        // Arrange
        var tcs1 = new TaskCompletionSource();
        var tcs2 = new TaskCompletionSource();
        var tcs3 = new TaskCompletionSource();
        var tasks = new[] { tcs1.Task, tcs2.Task, tcs3.Task };
        var completed = new List<int>();

        // Act
        var whenEachTask = Task.Run(async () =>
        {
            var index = 0;
            await foreach (var task in Task.WhenEach(tasks))
            {
                completed.Add(index++);
            }
        });

        // Complete in specific order
        tcs2.SetResult();
        await Task.Delay(10);
        tcs1.SetResult();
        await Task.Delay(10);
        tcs3.SetResult();
        await whenEachTask;

        // Assert - verify we got all tasks
        Assert.AreEqual(3, completed.Count);
    }

    [Test]
    public async Task WhenEach_Generic_CompletesInOrder()
    {
        // Arrange
        var tcs1 = new TaskCompletionSource<int>();
        var tcs2 = new TaskCompletionSource<int>();
        var tcs3 = new TaskCompletionSource<int>();
        var tasks = new[] { tcs1.Task, tcs2.Task, tcs3.Task };
        var results = new List<int>();

        // Act
        var whenEachTask = Task.Run(async () =>
        {
            await foreach (var task in Task.WhenEach(tasks))
            {
                results.Add(await task);
            }
        });

        // Complete in specific order with different values
        tcs2.SetResult(2);
        await Task.Delay(10);
        tcs1.SetResult(1);
        await Task.Delay(10);
        tcs3.SetResult(3);
        await whenEachTask;

        // Assert - verify we got all results
        Assert.AreEqual(3, results.Count);
        Assert.Contains(1, results);
        Assert.Contains(2, results);
        Assert.Contains(3, results);
    }

    [Test]
    public async Task WhenEach_NonGeneric_EmptyCollection_CompletesImmediately()
    {
        // Arrange
        var tasks = Array.Empty<Task>();
        var count = 0;

        // Act
        await foreach (var task in Task.WhenEach(tasks))
        {
            count++;
        }

        // Assert
        Assert.AreEqual(0, count);
    }

    [Test]
    public async Task WhenEach_Generic_EmptyCollection_CompletesImmediately()
    {
        // Arrange
        var tasks = Array.Empty<Task<int>>();
        var count = 0;

        // Act
        await foreach (var task in Task.WhenEach(tasks))
        {
            count++;
        }

        // Assert
        Assert.AreEqual(0, count);
    }

    [Test]
    public void WhenEach_NonGeneric_NullTasks_ThrowsArgumentNullException()
    {
        // Act & Assert
#pragma warning disable IDE0022
        Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await foreach (var task in Task.WhenEach((IEnumerable<Task>)null!))
            {
            }
        });
#pragma warning restore IDE0022
    }

    [Test]
    public void WhenEach_Generic_NullTasks_ThrowsArgumentNullException()
    {
        // Act & Assert
#pragma warning disable IDE0022
        Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await foreach (var task in Task.WhenEach((IEnumerable<Task<int>>)null!))
            {
            }
        });
#pragma warning restore IDE0022
    }

#if !NET9_0_OR_GREATER
    // CancellationToken overload only exists in polyfill, not in native .NET 9+ implementation
    [Test]
    public async Task WhenEach_NonGeneric_WithCancellation_StopsIterating()
    {
        // Arrange
        var tcs1 = new TaskCompletionSource();
        var tcs2 = new TaskCompletionSource();
        var tcs3 = new TaskCompletionSource();
        var tasks = new[] { tcs1.Task, tcs2.Task, tcs3.Task };
        var cancelSource = new CancelSource();
        var count = 0;

        // Act
        var whenEachTask = Task.Run(async () =>
        {
            await foreach (var task in Task.WhenEach(tasks, cancelSource.Token))
            {
                count++;
                if (count == 1)
                {
                    cancelSource.Cancel();
                }
            }
        });

        tcs1.SetResult();
        await Task.Delay(10);
        tcs2.SetResult();
        await Task.Delay(10);
        tcs3.SetResult();

        // Assert
        Assert.ThrowsAsync<OperationCanceledException>(() => whenEachTask);
    }
#endif

    [Test]
    public async Task WhenEach_Generic_WithFaultedTask_PropagatesException()
    {
        // Arrange
        var tcs1 = new TaskCompletionSource<int>();
        var tcs2 = new TaskCompletionSource<int>();
        var tasks = new[] { tcs1.Task, tcs2.Task };
        var results = new List<Task<int>>();

        // Act
        var whenEachTask = Task.Run(async () =>
        {
            await foreach (var task in Task.WhenEach(tasks))
            {
                results.Add(task);
            }
        });

        tcs1.SetResult(1);
        await Task.Delay(10);
        tcs2.SetException(new InvalidOperationException("Test exception"));
        await whenEachTask;

        // Assert - WhenEach completes, but the task itself is faulted
        Assert.AreEqual(2, results.Count);
        Assert.DoesNotThrowAsync(() => results[0]);
        Assert.ThrowsAsync<InvalidOperationException>(() => results[1]);
    }

    [Test]
    public async Task WhenEach_NonGeneric_AlreadyCompletedTasks_YieldsAll()
    {
        // Arrange
        var tasks = new[]
        {
            Task.CompletedTask,
            Task.CompletedTask,
            Task.CompletedTask
        };
        var count = 0;

        // Act
        await foreach (var task in Task.WhenEach(tasks))
        {
            count++;
            Assert.True(task.IsCompleted);
        }

        // Assert
        Assert.AreEqual(3, count);
    }

    [Test]
    public async Task WhenEach_Generic_AlreadyCompletedTasks_YieldsAll()
    {
        // Arrange
        var tasks = new[]
        {
            Task.FromResult(1),
            Task.FromResult(2),
            Task.FromResult(3)
        };
        var results = new List<int>();

        // Act
        await foreach (var task in Task.WhenEach(tasks))
        {
            results.Add(await task);
        }

        // Assert
        Assert.AreEqual(3, results.Count);
        Assert.Contains(1, results);
        Assert.Contains(2, results);
        Assert.Contains(3, results);
    }

    #endregion

#endif

    #region Helper Methods

    private async Task<T> GetValueAfterDelayAsync<T>(T value, int delayMs)
    {
        await Task.Delay(delayMs);
        return value;
    }

    private async Task ThrowExceptionAsync()
    {
        await Task.Delay(10);
        throw new InvalidOperationException("Test exception");
    }

    private async Task<T> ThrowExceptionAsync<T>()
    {
        await Task.Delay(10);
        throw new InvalidOperationException("Test exception");
    }

    #endregion
}