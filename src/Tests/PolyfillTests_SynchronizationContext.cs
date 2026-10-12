using System.IO;
using System.Threading;

partial class PolyfillTests
{
    // A context that counts the continuations posted to it, then runs them on the thread pool.
    // An await that captures the context posts its continuation here; one that uses
    // ConfigureAwait(false) never does. Counting, instead of blocking the context and waiting
    // for a timeout, keeps the tests independent of machine load and guarantees the operation
    // has finished (and released any file handle) before the test continues.
    class CountingContext :
        SynchronizationContext
    {
        int posts;

        public int Posts => Volatile.Read(ref posts);

        public override void Post(SendOrPostCallback callback, object? state)
        {
            Interlocked.Increment(ref posts);
            ThreadPool.QueueUserWorkItem(_ => callback(state));
        }

        public override void Send(SendOrPostCallback callback, object? state)
        {
            Interlocked.Increment(ref posts);
            callback(state);
        }

        public override SynchronizationContext CreateCopy() => this;
    }

    class YieldingStream :
        MemoryStream
    {
        public YieldingStream(byte[] buffer) :
            base(buffer)
        {
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await Task.Delay(20, cancellationToken).ConfigureAwait(false);
            return Read(buffer, offset, Math.Min(count, 1));
        }
    }

    // Runs func on a thread that has a CountingContext installed, waits for the returned task
    // to finish, and returns how many continuations were posted back to that context.
    static Task<int> PostsToCapturedContext(Func<Task> func)
    {
        var completion = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
        var thread = new Thread(() =>
        {
            var context = new CountingContext();
            SynchronizationContext.SetSynchronizationContext(context);
            try
            {
                func().GetAwaiter().GetResult();
                completion.SetResult(context.Posts);
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
    public async Task File_ReadAllBytesAsync_DoesNotCaptureContext()
    {
        var path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.bin");
        File.WriteAllBytes(path, new byte[1024 * 1024]);
        try
        {
            var posts = await PostsToCapturedContext(() => File.ReadAllBytesAsync(path));
            await Assert.That(posts).IsEqualTo(0);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Test]
    public async Task File_WriteAllTextAsync_DoesNotCaptureContext()
    {
        var path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.txt");
        try
        {
            var posts = await PostsToCapturedContext(() => File.WriteAllTextAsync(path, new string('a', 1024 * 1024)));
            await Assert.That(posts).IsEqualTo(0);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Test]
    public async Task Stream_ReadExactlyAsync_DoesNotCaptureContext()
    {
        var buffer = new byte[4];
        var posts = await PostsToCapturedContext(
            async () =>
            {
                using var stream = new YieldingStream([1, 2, 3, 4]);
                await stream.ReadExactlyAsync(buffer, 0, 4).ConfigureAwait(false);
            });
        await Assert.That(posts).IsEqualTo(0);
        await Assert.That(buffer).IsEquivalentTo(new byte[] {1, 2, 3, 4});
    }

    [Test]
    public async Task TaskT_WaitAsync_DoesNotCaptureContext()
    {
        var posts = await PostsToCapturedContext(
            () => Task.Delay(50)
                .ContinueWith(_ => 1, TaskScheduler.Default)
                .WaitAsync(TimeSpan.FromSeconds(30)));
        await Assert.That(posts).IsEqualTo(0);
    }

    [Test]
    public async Task Task_WaitAsync_DoesNotCaptureContext()
    {
        var posts = await PostsToCapturedContext(
            () => Task.Delay(50)
                .WaitAsync(TimeSpan.FromSeconds(30)));
        await Assert.That(posts).IsEqualTo(0);
    }
}
