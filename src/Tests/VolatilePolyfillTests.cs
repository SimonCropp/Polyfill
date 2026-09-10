using System.Diagnostics;
using System.Threading;

// Memory ordering cannot be asserted deterministically from a unit test: on x86 and x64 the
// message passing case below succeeds even with no barrier at all. These exercise the calls in the
// pattern they exist for, which catches a barrier that throws or fails to compile on a target, and
// leave the ordering guarantee itself to the implementation.
public class VolatilePolyfillTests
{
    [Test]
    public async Task Barriers_CanBeCalled()
    {
        Volatile.ReadBarrier();
        Volatile.WriteBarrier();

        await Assert.That(true).IsTrue();
    }

    [Test]
    public async Task Barriers_MessagePassing()
    {
        const int iterations = 200;

        var payload = 0;
        var published = 0;
        var observed = new int[iterations];

        var writer = Task.Run(
            () =>
            {
                for (var i = 1; i <= iterations; i++)
                {
                    payload = i;
                    Volatile.WriteBarrier();
                    Volatile.Write(ref published, i);
                }
            });

        var reader = Task.Run(
            () =>
            {
                var watch = Stopwatch.StartNew();
                for (var i = 1; i <= iterations; i++)
                {
                    var spin = new SpinWait();
                    while (Volatile.Read(ref published) < i)
                    {
                        if (watch.Elapsed > TimeSpan.FromSeconds(20))
                        {
                            return;
                        }

                        spin.SpinOnce();
                    }

                    Volatile.ReadBarrier();
                    observed[i - 1] = payload;
                }
            });

        var completed = Task.WaitAll(new[] { writer, reader }, TimeSpan.FromSeconds(30));

        await Assert.That(completed).IsTrue();

        // the reader never sees a payload older than the one it waited to be published
        var stale = -1;
        for (var i = 0; i < iterations; i++)
        {
            if (observed[i] < i + 1)
            {
                stale = i;
                break;
            }
        }

        await Assert.That(stale).IsEqualTo(-1);
    }
}
