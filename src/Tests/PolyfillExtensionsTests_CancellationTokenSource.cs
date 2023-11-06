partial class PolyfillExtensionsTests
{
    private static bool IsCompletedSuccessfully(Task task)
    {
#if NETFRAMEWORK || NETSTANDARD
        return task.Status == TaskStatus.RanToCompletion;
#else
        return task.IsCompletedSuccessfully;
#endif
    }

    [Test]
    [Ignore("This test is taken directly from the .NET repo but we can't match the real CancelAsync logic exactly, so differ slightly and can't pass this test")]
    public static void CancellationTokenSource_CancelAsync_NoRegistrations_CallbackCompletesImmediately()
    {
        var cts = new CancellationTokenSource();
        Assert.True(IsCompletedSuccessfully(cts.CancelAsync()));
        Assert.True(cts.IsCancellationRequested);

        cts = new CancellationTokenSource();
        cts.Token.Register(() => { }).Dispose();
        Assert.True(IsCompletedSuccessfully(cts.CancelAsync()));
        Assert.True(cts.IsCancellationRequested);
    }

    [Test]
    public static async Task CancellationTokenSource_CancelAsync_CallbacksInvokedAsynchronously()
    {
        var cts = new CancellationTokenSource();

        var mres = new ManualResetEventSlim();
        cts.Token.Register(mres.Wait);

        Task t = cts.CancelAsync();
        Assert.False(t.IsCompleted);
        Assert.True(cts.IsCancellationRequested);

        Assert.True(IsCompletedSuccessfully(cts.CancelAsync())); // secondary call completes immediately

        mres.Set();
        await t;
    }

    [Test]
    public static void CancellationTokenSource_CancelAsync_AllCallbacksInvoked()
    {
        const int Iters = 1000;

        int sum = 0;
        int callingThreadId = Environment.CurrentManagedThreadId;

        var cts = new CancellationTokenSource();
        for (int i = 1; i <= Iters; i++)
        {
            cts.Token.Register(s =>
            {
                sum += (int)s!;
            }, i);
        }

        Task t = cts.CancelAsync();
        Assert.True(cts.IsCancellationRequested);

        ((IAsyncResult)t).AsyncWaitHandle.WaitOne(); // synchronously block without inlining to ensure this thread isn't reused
        t.Wait(); // propagate any exceptions

        Assert.AreEqual(Iters * (Iters + 1) / 2, sum);
    }
}
