using System.Threading;

// Runs on every target, so net9.0 and later validate the BCL and everything below the polyfill.
public class InterlockedNarrowTests
{
    [Test]
    public async Task Exchange_ReturnsTheOriginal()
    {
        byte unsignedByte = 5;
        await Assert.That(Interlocked.Exchange(ref unsignedByte, 9)).IsEqualTo((byte) 5);
        await Assert.That(unsignedByte).IsEqualTo((byte) 9);

        sbyte signedByte = -128;
        await Assert.That(Interlocked.Exchange(ref signedByte, 127)).IsEqualTo((sbyte) -128);
        await Assert.That(signedByte).IsEqualTo((sbyte) 127);

        short signedShort = -1;
        await Assert.That(Interlocked.Exchange(ref signedShort, 300)).IsEqualTo((short) -1);
        await Assert.That(signedShort).IsEqualTo((short) 300);

        ushort unsignedShort = ushort.MaxValue;
        await Assert.That(Interlocked.Exchange(ref unsignedShort, 1)).IsEqualTo(ushort.MaxValue);
        await Assert.That(unsignedShort).IsEqualTo((ushort) 1);
    }

    [Test]
    public async Task CompareExchange_ReplacesOnlyOnAMatch()
    {
        byte unsignedByte = 5;
        await Assert.That(Interlocked.CompareExchange(ref unsignedByte, 9, 5)).IsEqualTo((byte) 5);
        await Assert.That(unsignedByte).IsEqualTo((byte) 9);

        unsignedByte = 5;
        // the original is returned whether or not the comparand matched
        await Assert.That(Interlocked.CompareExchange(ref unsignedByte, 9, 4)).IsEqualTo((byte) 5);
        await Assert.That(unsignedByte).IsEqualTo((byte) 5);

        sbyte signedByte = -1;
        await Assert.That(Interlocked.CompareExchange(ref signedByte, 7, -1)).IsEqualTo((sbyte) -1);
        await Assert.That(signedByte).IsEqualTo((sbyte) 7);

        short signedShort = 1000;
        await Assert.That(Interlocked.CompareExchange(ref signedShort, 2000, 999)).IsEqualTo((short) 1000);
        await Assert.That(signedShort).IsEqualTo((short) 1000);

        ushort unsignedShort = 40000;
        await Assert.That(Interlocked.CompareExchange(ref unsignedShort, 50000, 40000)).IsEqualTo((ushort) 40000);
        await Assert.That(unsignedShort).IsEqualTo((ushort) 50000);
    }

    // The property that rules out implementing this by widening to a CompareExchange on the
    // containing 32 bit word: that would write the neighbouring bytes back and could lose a
    // concurrent plain write to one of them. The BCL does not do that, and neither does the
    // polyfill, which writes only the target byte.
    [Test]
    public async Task DoesNotDisturbTheAdjacentByte()
    {
        var buffer = new byte[4];
        var stop = false;
        var wrong = 0;
        var iterations = 0;

        var writer = new Thread(() =>
        {
            while (!Volatile.Read(ref stop))
            {
                buffer[1] = 0xAB;
                buffer[3] = 0xCD;
                if (Volatile.Read(ref buffer[1]) != 0xAB ||
                    Volatile.Read(ref buffer[3]) != 0xCD)
                {
                    Interlocked.Increment(ref wrong);
                }

                Interlocked.Increment(ref iterations);
            }
        });

        var exchanger = new Thread(() =>
        {
            byte flip = 0;
            while (!Volatile.Read(ref stop))
            {
                Interlocked.Exchange(ref buffer[0], flip);
                Interlocked.CompareExchange(ref buffer[2], flip, flip);
                flip = (byte) (flip ^ 0xFF);
            }
        });

        writer.Start();
        exchanger.Start();
        Thread.Sleep(500);
        Volatile.Write(ref stop, true);
        writer.Join();
        exchanger.Join();

        var observed = iterations;
        await Assert.That(observed).IsGreaterThan(0);
        await Assert.That(wrong).IsEqualTo(0);
    }

    // no update may be lost when every writer goes through Interlocked, which is the contract
    // these members actually have to keep
    [Test]
    public async Task CompareExchangeLoopLosesNoUpdates()
    {
        const int threads = 4;
        const int perThread = 2000;

        ushort counter = 0;
        var workers = new Thread[threads];
        for (var i = 0; i < threads; i++)
        {
            workers[i] = new Thread(() =>
            {
                for (var n = 0; n < perThread; n++)
                {
                    while (true)
                    {
                        var current = Interlocked.CompareExchange(ref counter, 0, 0);
                        var next = (ushort) (current + 1);
                        if (Interlocked.CompareExchange(ref counter, next, current) == current)
                        {
                            break;
                        }
                    }
                }
            });
        }

        foreach (var worker in workers)
        {
            worker.Start();
        }

        foreach (var worker in workers)
        {
            worker.Join();
        }

        var final = counter;
        await Assert.That(final).IsEqualTo((ushort) (threads * perThread));
    }
}
