partial class PolyfillTests
{
    static bool IsCompletedSuccessfully(Task task) =>
#if NETFRAMEWORK || NETSTANDARD
        task.Status == TaskStatus.RanToCompletion;
#else
        task.IsCompletedSuccessfully;
#endif

    // [Test]
    // [Ignore("This test is taken directly from the .NET repo but we can't match the real CancelAsync logic exactly, so differ slightly and can't pass this test")]
    // public static void CancelSource_CancelAsync_NoRegistrations_CallbackCompletesImmediately()
    // {
    //     var cancelSource = new CancelSource();
    //     Assert.True(IsCompletedSuccessfully(cancelSource.CancelAsync()));
    //     Assert.True(cancelSource.IsCancellationRequested);
    //
    //     cancelSource = new();
    //     cancelSource.Token
    //         .Register(
    //             () =>
    //             {
    //             })
    //         .Dispose();
    //     Assert.True(IsCompletedSuccessfully(cancelSource.CancelAsync()));
    //     Assert.True(cancelSource.IsCancellationRequested);
    // }

    [Test]
    public async Task CancelSource_CancelAsync_CallbacksInvokedAsynchronously()
    {
        var cancelSource = new CancelSource();

        var resetEventSlim = new ManualResetEventSlim();
        cancelSource.Token.Register(resetEventSlim.Wait);

        var t = cancelSource.CancelAsync();
        await Assert.That(t.IsCompleted).IsFalse();
        await Assert.That(cancelSource.IsCancellationRequested).IsTrue();

        // secondary call completes immediately
        await Assert.That(IsCompletedSuccessfully(cancelSource.CancelAsync())).IsTrue();

        resetEventSlim.Set();
        await t;
    }

    [Test]
    public async Task CancelSource_CancelAsync_AllCallbacksInvoked()
    {
        const int iterations = 1000;

        var sum = 0;

        var cancelSource = new CancelSource();
        for (var i = 1; i <= iterations; i++)
        {
            cancelSource.Token.Register(
                s =>
                {
                    sum += (int) s!;
                },
                i);
        }

        var t = cancelSource.CancelAsync();
        await Assert.That(cancelSource.IsCancellationRequested).IsTrue();

        // synchronously block without inlining to ensure this thread isn't reused
        ((IAsyncResult) t).AsyncWaitHandle.WaitOne();
        // propagate any exceptions
        t.Wait(cancelSource.Token);

        await Assert.That(sum).IsEqualTo(iterations * (iterations + 1) / 2);
    }
}