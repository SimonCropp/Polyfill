#if FeatureMemory

using System;
using System.Buffers.Binary;

public class BinaryPrimitivesPolyfillTests
{
    // ReadDoubleBigEndian / ReadDoubleLittleEndian

    [Test]
    public async Task ReadDoubleBigEndian_KnownValue()
    {
        // 1.0 in IEEE 754 = 0x3FF0000000000000
        var bytes = new byte[] { 0x3F, 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        var result = BinaryPrimitives.ReadDoubleBigEndian(bytes);
        await Assert.That(result).IsEqualTo(1.0);
    }

    [Test]
    public async Task ReadDoubleLittleEndian_KnownValue()
    {
        // 1.0 in IEEE 754 = 0x3FF0000000000000, little endian bytes reversed
        var bytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0x3F };
        var result = BinaryPrimitives.ReadDoubleLittleEndian(bytes);
        await Assert.That(result).IsEqualTo(1.0);
    }

    // ReadSingleBigEndian / ReadSingleLittleEndian

    [Test]
    public async Task ReadSingleBigEndian_KnownValue()
    {
        // 1.0f in IEEE 754 = 0x3F800000
        var bytes = new byte[] { 0x3F, 0x80, 0x00, 0x00 };
        var result = BinaryPrimitives.ReadSingleBigEndian(bytes);
        await Assert.That(result).IsEqualTo(1.0f);
    }

    [Test]
    public async Task ReadSingleLittleEndian_KnownValue()
    {
        // 1.0f in IEEE 754 = 0x3F800000, little endian bytes reversed
        var bytes = new byte[] { 0x00, 0x00, 0x80, 0x3F };
        var result = BinaryPrimitives.ReadSingleLittleEndian(bytes);
        await Assert.That(result).IsEqualTo(1.0f);
    }

    // Write + Read roundtrip (Double)

    [Test]
    public async Task WriteDoubleBigEndian_ReadDoubleBigEndian_RoundTrip()
    {
        var original = 3.141592653589793;
        var buffer = new byte[8];
        BinaryPrimitives.WriteDoubleBigEndian(buffer, original);
        var result = BinaryPrimitives.ReadDoubleBigEndian(buffer);
        await Assert.That(result).IsEqualTo(original);
    }

    [Test]
    public async Task WriteDoubleLittleEndian_ReadDoubleLittleEndian_RoundTrip()
    {
        var original = -2.718281828459045;
        var buffer = new byte[8];
        BinaryPrimitives.WriteDoubleLittleEndian(buffer, original);
        var result = BinaryPrimitives.ReadDoubleLittleEndian(buffer);
        await Assert.That(result).IsEqualTo(original);
    }

    // Write + Read roundtrip (Single)

    [Test]
    public async Task WriteSingleBigEndian_ReadSingleBigEndian_RoundTrip()
    {
        var original = 3.14159f;
        var buffer = new byte[4];
        BinaryPrimitives.WriteSingleBigEndian(buffer, original);
        var result = BinaryPrimitives.ReadSingleBigEndian(buffer);
        await Assert.That(result).IsEqualTo(original);
    }

    [Test]
    public async Task WriteSingleLittleEndian_ReadSingleLittleEndian_RoundTrip()
    {
        var original = -1.5f;
        var buffer = new byte[4];
        BinaryPrimitives.WriteSingleLittleEndian(buffer, original);
        var result = BinaryPrimitives.ReadSingleLittleEndian(buffer);
        await Assert.That(result).IsEqualTo(original);
    }

    // TryRead success

    [Test]
    public async Task TryReadDoubleBigEndian_Success()
    {
        var bytes = new byte[] { 0x3F, 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        var success = BinaryPrimitives.TryReadDoubleBigEndian(bytes, out var value);
        await Assert.That(success).IsTrue();
        await Assert.That(value).IsEqualTo(1.0);
    }

    [Test]
    public async Task TryReadDoubleLittleEndian_Success()
    {
        var bytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0x3F };
        var success = BinaryPrimitives.TryReadDoubleLittleEndian(bytes, out var value);
        await Assert.That(success).IsTrue();
        await Assert.That(value).IsEqualTo(1.0);
    }

    [Test]
    public async Task TryReadSingleBigEndian_Success()
    {
        var bytes = new byte[] { 0x3F, 0x80, 0x00, 0x00 };
        var success = BinaryPrimitives.TryReadSingleBigEndian(bytes, out var value);
        await Assert.That(success).IsTrue();
        await Assert.That(value).IsEqualTo(1.0f);
    }

    [Test]
    public async Task TryReadSingleLittleEndian_Success()
    {
        var bytes = new byte[] { 0x00, 0x00, 0x80, 0x3F };
        var success = BinaryPrimitives.TryReadSingleLittleEndian(bytes, out var value);
        await Assert.That(success).IsTrue();
        await Assert.That(value).IsEqualTo(1.0f);
    }

    // TryRead failure (buffer too short)

    [Test]
    public async Task TryReadDoubleBigEndian_BufferTooShort()
    {
        var bytes = new byte[7];
        var success = BinaryPrimitives.TryReadDoubleBigEndian(bytes, out var value);
        await Assert.That(success).IsFalse();
        await Assert.That(value).IsEqualTo(0.0);
    }

    [Test]
    public async Task TryReadDoubleLittleEndian_BufferTooShort()
    {
        var bytes = new byte[7];
        var success = BinaryPrimitives.TryReadDoubleLittleEndian(bytes, out var value);
        await Assert.That(success).IsFalse();
        await Assert.That(value).IsEqualTo(0.0);
    }

    [Test]
    public async Task TryReadSingleBigEndian_BufferTooShort()
    {
        var bytes = new byte[3];
        var success = BinaryPrimitives.TryReadSingleBigEndian(bytes, out var value);
        await Assert.That(success).IsFalse();
        await Assert.That(value).IsEqualTo(0.0f);
    }

    [Test]
    public async Task TryReadSingleLittleEndian_BufferTooShort()
    {
        var bytes = new byte[3];
        var success = BinaryPrimitives.TryReadSingleLittleEndian(bytes, out var value);
        await Assert.That(success).IsFalse();
        await Assert.That(value).IsEqualTo(0.0f);
    }

    // TryWrite success + roundtrip

    [Test]
    public async Task TryWriteDoubleBigEndian_Success()
    {
        var buffer = new byte[8];
        var success = BinaryPrimitives.TryWriteDoubleBigEndian(buffer, 1.0);
        await Assert.That(success).IsTrue();
        await Assert.That(BinaryPrimitives.ReadDoubleBigEndian(buffer)).IsEqualTo(1.0);
    }

    [Test]
    public async Task TryWriteDoubleLittleEndian_Success()
    {
        var buffer = new byte[8];
        var success = BinaryPrimitives.TryWriteDoubleLittleEndian(buffer, 1.0);
        await Assert.That(success).IsTrue();
        await Assert.That(BinaryPrimitives.ReadDoubleLittleEndian(buffer)).IsEqualTo(1.0);
    }

    [Test]
    public async Task TryWriteSingleBigEndian_Success()
    {
        var buffer = new byte[4];
        var success = BinaryPrimitives.TryWriteSingleBigEndian(buffer, 1.0f);
        await Assert.That(success).IsTrue();
        await Assert.That(BinaryPrimitives.ReadSingleBigEndian(buffer)).IsEqualTo(1.0f);
    }

    [Test]
    public async Task TryWriteSingleLittleEndian_Success()
    {
        var buffer = new byte[4];
        var success = BinaryPrimitives.TryWriteSingleLittleEndian(buffer, 1.0f);
        await Assert.That(success).IsTrue();
        await Assert.That(BinaryPrimitives.ReadSingleLittleEndian(buffer)).IsEqualTo(1.0f);
    }

    // TryWrite failure (buffer too short)

    [Test]
    public async Task TryWriteDoubleBigEndian_BufferTooShort()
    {
        var buffer = new byte[7];
        var success = BinaryPrimitives.TryWriteDoubleBigEndian(buffer, 1.0);
        await Assert.That(success).IsFalse();
    }

    [Test]
    public async Task TryWriteDoubleLittleEndian_BufferTooShort()
    {
        var buffer = new byte[7];
        var success = BinaryPrimitives.TryWriteDoubleLittleEndian(buffer, 1.0);
        await Assert.That(success).IsFalse();
    }

    [Test]
    public async Task TryWriteSingleBigEndian_BufferTooShort()
    {
        var buffer = new byte[3];
        var success = BinaryPrimitives.TryWriteSingleBigEndian(buffer, 1.0f);
        await Assert.That(success).IsFalse();
    }

    [Test]
    public async Task TryWriteSingleLittleEndian_BufferTooShort()
    {
        var buffer = new byte[3];
        var success = BinaryPrimitives.TryWriteSingleLittleEndian(buffer, 1.0f);
        await Assert.That(success).IsFalse();
    }
}

#endif
