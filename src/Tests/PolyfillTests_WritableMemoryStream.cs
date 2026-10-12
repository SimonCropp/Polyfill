#if FeatureMemory
using System.IO;

partial class PolyfillTests
{
    [Test]
    public async Task WritableMemoryStream_IsAStream()
    {
        using var stream = new WritableMemoryStream(new byte[4]);

        await Assert.That(stream).IsAssignableTo<Stream>();
    }

    [Test]
    public async Task WritableMemoryStream_LengthStartsAtCapacity()
    {
        using var stream = new WritableMemoryStream(new byte[8]);

        await Assert.That(stream.Length).IsEqualTo(8L);
        await Assert.That(stream.Position).IsEqualTo(0L);
        await Assert.That(stream.CanWrite).IsTrue();
        await Assert.That(stream.CanRead).IsTrue();
        await Assert.That(stream.CanSeek).IsTrue();
    }

    [Test]
    public async Task WritableMemoryStream_WritesIntoBackingArray()
    {
        var backing = new byte[5];
        using var stream = new WritableMemoryStream(backing);

        stream.Write(new byte[] { 1, 2, 3 }, 0, 3);

        await Assert.That(stream.Position).IsEqualTo(3L);
        await Assert.That(stream.Length).IsEqualTo(5L);
        await Assert.That(backing).IsEquivalentTo(new byte[] { 1, 2, 3, 0, 0 });
    }

    [Test]
    public async Task WritableMemoryStream_WritesIntoSlicedBackingMemory()
    {
        var backing = new byte[8];
        using var stream = new WritableMemoryStream(backing.AsMemory(2, 3));

        stream.WriteByte(7);
        stream.WriteByte(8);

        await Assert.That(stream.Length).IsEqualTo(3L);
        await Assert.That(backing).IsEquivalentTo(new byte[] { 0, 0, 7, 8, 0, 0, 0, 0 });
    }

    [Test]
    public async Task WritableMemoryStream_ReadBackWhatWasWritten()
    {
        using var stream = new WritableMemoryStream(new byte[16]);
        stream.Write(new byte[] { 4, 5, 6 }, 0, 3);

        stream.Position = 0;
        var buffer = new byte[3];
        var read = stream.Read(buffer, 0, 3);

        await Assert.That(read).IsEqualTo(3);
        await Assert.That(buffer).IsEquivalentTo(new byte[] { 4, 5, 6 });
    }

    [Test]
    public async Task WritableMemoryStream_WritingBeyondCapacityThrows()
    {
        using var stream = new WritableMemoryStream(new byte[3]);

        await Assert.That(() => stream.Write(new byte[] { 1, 2, 3, 4 }, 0, 4)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task WritableMemoryStream_SetLengthShrinks()
    {
        using var stream = new WritableMemoryStream(new byte[4]);

        stream.SetLength(2);

        await Assert.That(stream.Length).IsEqualTo(2L);
        await Assert.That(stream.Read(new byte[4], 0, 4)).IsEqualTo(2);
    }

    [Test]
    public async Task WritableMemoryStream_SetLengthClampsPosition()
    {
        using var stream = new WritableMemoryStream(new byte[8]);
        stream.Position = 5;

        stream.SetLength(2);

        await Assert.That(stream.Position).IsEqualTo(2L);
    }

    [Test]
    public async Task WritableMemoryStream_SetLengthBeyondCapacityThrows()
    {
        using var stream = new WritableMemoryStream(new byte[4]);

        await Assert.That(() => stream.SetLength(5)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task WritableMemoryStream_GrowingBackClearsStaleContent()
    {
        var backing = new byte[] { 9, 9, 9, 9 };
        using var stream = new WritableMemoryStream(backing);

        stream.SetLength(1);
        stream.SetLength(4);

        await Assert.That(backing).IsEquivalentTo(new byte[] { 9, 0, 0, 0 });
    }

    [Test]
    public async Task WritableMemoryStream_WriteByteBeyondCapacityThrows()
    {
        using var stream = new WritableMemoryStream(new byte[3]);

        stream.WriteByte(1);
        stream.WriteByte(2);
        stream.WriteByte(3);

        await Assert.That(() => stream.WriteByte(4)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task WritableMemoryStream_WriteUpToExactCapacitySucceeds()
    {
        using var stream = new WritableMemoryStream(new byte[10]);
        var data = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        stream.Write(data, 0, data.Length);

        await Assert.That(stream.Position).IsEqualTo(10L);
        await Assert.That(stream.Length).IsEqualTo(10L);

        stream.Position = 0;
        var readBack = new byte[10];
        var read = stream.Read(readBack, 0, 10);

        await Assert.That(read).IsEqualTo(10);
        await Assert.That(readBack).IsEquivalentTo(data);
    }

    [Test]
    public async Task WritableMemoryStream_WritePastCapacityLeavesPositionUnchanged()
    {
        using var stream = new WritableMemoryStream(new byte[10]);
        stream.Write(new byte[8], 0, 8);

        await Assert.That(stream.Position).IsEqualTo(8L);
        await Assert.That(() => stream.Write(new byte[5], 0, 5)).Throws<NotSupportedException>();
        await Assert.That(stream.Position).IsEqualTo(8L);
    }

    [Test]
    public async Task WritableMemoryStream_SeekPastCapacity()
    {
        using var stream = new WritableMemoryStream(new byte[10]);

        stream.Seek(100, SeekOrigin.Begin);

        await Assert.That(stream.Position).IsEqualTo(100L);
        await Assert.That(stream.ReadByte()).IsEqualTo(-1);
        await Assert.That(() => stream.WriteByte(42)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task WritableMemoryStream_WriteOverExistingDataReplacesData()
    {
        var backing = new byte[10];
        using var stream = new WritableMemoryStream(backing);
        stream.Write(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 0, 10);

        stream.Position = 3;
        stream.Write(new byte[] { 100, 101, 102 }, 0, 3);

        stream.Position = 0;
        var result = new byte[10];
        var read = stream.Read(result, 0, 10);

        await Assert.That(read).IsEqualTo(10);
        await Assert.That(result).IsEquivalentTo(new byte[] { 1, 2, 3, 100, 101, 102, 7, 8, 9, 10 });
    }

    [Test]
    public async Task WritableMemoryStream_WriteAfterGapClearsSkippedBytes()
    {
        var backing = new byte[] { 1, 2, 3, 4, 5, 6 };
        using var stream = new WritableMemoryStream(backing);
        stream.SetLength(1);

        stream.Position = 3;
        stream.WriteByte(99);

        await Assert.That(stream.Length).IsEqualTo(4L);
        await Assert.That(backing).IsEquivalentTo(new byte[] { 1, 0, 0, 99, 5, 6 });
    }

    [Test]
    public async Task WritableMemoryStream_EmptyWritePastCapacityIsNoOp()
    {
        using var stream = new WritableMemoryStream(new byte[2]);
        stream.Position = 100;

        stream.Write(new byte[0], 0, 0);

        await Assert.That(stream.Length).IsEqualTo(2L);
        await Assert.That(stream.Position).IsEqualTo(100L);
    }

    [Test]
    public async Task WritableMemoryStream_WriteAsync()
    {
        var backing = new byte[4];
        using var stream = new WritableMemoryStream(backing);

        await stream.WriteAsync(new byte[] { 1, 2, 3 }, 0, 3);

        await Assert.That(backing).IsEquivalentTo(new byte[] { 1, 2, 3, 0 });
    }

    [Test]
    public async Task WritableMemoryStream_WriteByte_WritesAndAdvances()
    {
        var backing = new byte[] { 9, 9, 9, 9 };
        using var stream = new WritableMemoryStream(backing);

        stream.WriteByte(1);
        stream.WriteByte(2);

        await Assert.That(stream.Position).IsEqualTo(2L);
        await Assert.That(stream.Length).IsEqualTo(4L);
        await Assert.That(backing).IsEquivalentTo(new byte[] { 1, 2, 9, 9 });
    }

    [Test]
    public async Task WritableMemoryStream_WriteByte_GrowsLengthAfterShrink()
    {
        var backing = new byte[] { 9, 9, 9, 9 };
        using var stream = new WritableMemoryStream(backing);
        stream.SetLength(0);

        stream.WriteByte(1);
        await Assert.That(stream.Length).IsEqualTo(1L);
        stream.WriteByte(2);

        await Assert.That(stream.Length).IsEqualTo(2L);
        await Assert.That(stream.Position).IsEqualTo(2L);
        await Assert.That(backing).IsEquivalentTo(new byte[] { 1, 2, 9, 9 });
    }

    [Test]
    public async Task WritableMemoryStream_WriteByte_AfterGapClearsSkippedBytes()
    {
        var backing = new byte[] { 9, 9, 9, 9, 9, 9 };
        using var stream = new WritableMemoryStream(backing);
        stream.SetLength(1);
        stream.Position = 4;

        stream.WriteByte(7);

        await Assert.That(stream.Length).IsEqualTo(5L);
        await Assert.That(stream.Position).IsEqualTo(5L);
        await Assert.That(backing).IsEquivalentTo(new byte[] { 9, 0, 0, 0, 7, 9 });
    }

    [Test]
    public async Task WritableMemoryStream_WriteByte_LastByteOfCapacity()
    {
        var backing = new byte[3];
        using var stream = new WritableMemoryStream(backing);
        stream.Position = 2;

        stream.WriteByte(5);

        await Assert.That(backing).IsEquivalentTo(new byte[] { 0, 0, 5 });
        await Assert.That(stream.Position).IsEqualTo(3L);
        await Assert.That(() => stream.WriteByte(6)).Throws<NotSupportedException>();
        await Assert.That(stream.Position).IsEqualTo(3L);
        await Assert.That(stream.Length).IsEqualTo(3L);
        await Assert.That(backing).IsEquivalentTo(new byte[] { 0, 0, 5 });
    }

    [Test]
    public async Task WritableMemoryStream_WriteByte_EmptyStreamThrows()
    {
        using var stream = new WritableMemoryStream(Memory<byte>.Empty);

        await Assert.That(() => stream.WriteByte(1)).Throws<NotSupportedException>();
        await Assert.That(stream.Position).IsEqualTo(0L);
    }

    [Test]
    public async Task WritableMemoryStream_WriteByte_PositionAtIntMaxValueThrows()
    {
        var backing = new byte[] { 9, 9 };
        using var stream = new WritableMemoryStream(backing);
        stream.Position = int.MaxValue;

        await Assert.That(() => stream.WriteByte(1)).Throws<NotSupportedException>();
        await Assert.That(backing).IsEquivalentTo(new byte[] { 9, 9 });
    }

    [Test]
    public async Task WritableMemoryStream_WriteByte_Disposed_Throws()
    {
        var stream = new WritableMemoryStream(new byte[4]);
        stream.Dispose();

        await Assert.That(() => stream.WriteByte(1)).Throws<ObjectDisposedException>();
    }

    // WriteByte has to behave exactly like a one byte Write. Drive two streams through the same
    // sequence of seeks, truncations and writes and compare everything observable after each step.
    [Test]
    public async Task WritableMemoryStream_WriteByte_MatchesSingleByteWrite()
    {
        var random = new Random(12345);
        for (var round = 0; round < 50; round++)
        {
            var capacity = random.Next(0, 12);
            var viaWriteByteBacking = new byte[capacity + 4];
            var viaWriteBacking = new byte[capacity + 4];
            random.NextBytes(viaWriteByteBacking);
            Array.Copy(viaWriteByteBacking, viaWriteBacking, viaWriteBacking.Length);
            using var viaWriteByte = new WritableMemoryStream(viaWriteByteBacking.AsMemory(2, capacity));
            using var viaWrite = new WritableMemoryStream(viaWriteBacking.AsMemory(2, capacity));

            for (var step = 0; step < 60; step++)
            {
                switch (random.Next(0, 4))
                {
                    case 0:
                        var position = random.Next(0, capacity + 3);
                        viaWriteByte.Position = position;
                        viaWrite.Position = position;
                        break;
                    case 1:
                        var length = random.Next(0, capacity + 1);
                        viaWriteByte.SetLength(length);
                        viaWrite.SetLength(length);
                        break;
                    default:
                        var value = (byte) random.Next(1, 256);
                        Exception? writeByteException = null;
                        Exception? writeException = null;
                        try
                        {
                            viaWriteByte.WriteByte(value);
                        }
                        catch (Exception exception)
                        {
                            writeByteException = exception;
                        }

                        try
                        {
                            viaWrite.Write([value], 0, 1);
                        }
                        catch (Exception exception)
                        {
                            writeException = exception;
                        }

                        await Assert.That(writeByteException?.GetType()).IsEqualTo(writeException?.GetType());
                        break;
                }

                await Assert.That(viaWriteByte.Position).IsEqualTo(viaWrite.Position);
                await Assert.That(viaWriteByte.Length).IsEqualTo(viaWrite.Length);
                await Assert.That(viaWriteByteBacking.AsSpan().SequenceEqual(viaWriteBacking)).IsTrue();
            }
        }
    }

    [Test]
    public async Task WritableMemoryStream_DisposedThrows()
    {
        var stream = new WritableMemoryStream(new byte[4]);
        stream.Dispose();

        await Assert.That(stream.CanRead).IsFalse();
        await Assert.That(stream.CanSeek).IsFalse();
        await Assert.That(stream.CanWrite).IsFalse();
        await Assert.That(() => stream.Length).Throws<ObjectDisposedException>();
    }
}
#endif
