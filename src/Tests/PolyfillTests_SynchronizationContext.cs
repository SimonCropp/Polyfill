using System.IO;
using System.Threading;

partial class PolyfillTests
{
    // A context whose Post never runs the callback. Any await that captures it never resumes,
    // which is what happens when a caller blocks the context thread on the returned task.
    class NeverPostContext :
        SynchronizationContext
    {
        public override void Post(SendOrPostCallback callback, object? state)
        {
        }

        public override void Send(SendOrPostCallback callback, object? state)
        {
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

    static Task<bool> CompletesWhenContextIsBlocked(Func<Task> func)
    {
        var completion = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var thread = new Thread(() =>
        {
            SynchronizationContext.SetSynchronizationContext(new NeverPostContext());
            try
            {
                var task = func();
                completion.SetResult(task.Wait(TimeSpan.FromSeconds(10)));
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
            var completed = await CompletesWhenContextIsBlocked(() => File.ReadAllBytesAsync(path));
            await Assert.That(completed).IsTrue();
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
            var completed = await CompletesWhenContextIsBlocked(() => File.WriteAllTextAsync(path, new string('a', 1024 * 1024)));
            await Assert.That(completed).IsTrue();
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
        var completed = await CompletesWhenContextIsBlocked(
            async () =>
            {
                using var stream = new YieldingStream([1, 2, 3, 4]);
                await stream.ReadExactlyAsync(buffer, 0, 4).ConfigureAwait(false);
            });
        await Assert.That(completed).IsTrue();
        await Assert.That(buffer).IsEquivalentTo(new byte[] {1, 2, 3, 4});
    }

    [Test]
    public async Task TaskT_WaitAsync_DoesNotCaptureContext()
    {
        var completed = await CompletesWhenContextIsBlocked(
            () => Task.Delay(50)
                .ContinueWith(_ => 1, TaskScheduler.Default)
                .WaitAsync(TimeSpan.FromSeconds(30)));
        await Assert.That(completed).IsTrue();
    }

    [Test]
    public async Task Task_WaitAsync_DoesNotCaptureContext()
    {
        var completed = await CompletesWhenContextIsBlocked(
            () => Task.Delay(50)
                .WaitAsync(TimeSpan.FromSeconds(30)));
        await Assert.That(completed).IsTrue();
    }
}
