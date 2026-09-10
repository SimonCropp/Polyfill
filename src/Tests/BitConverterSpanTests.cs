#if FeatureMemory

// Runs on every target, so netstandard2.1/netcoreapp2.1 and later validate the BCL and
// netstandard2.0, netcoreapp2.0 and .NET Framework validate the polyfill.
public class BitConverterSpanTests
{
    static byte[] source = BuildSource();

    static byte[] BuildSource()
    {
        var bytes = new byte[24];
        for (var i = 0; i < bytes.Length; i++)
        {
            bytes[i] = (byte) (i + 1);
        }

        return bytes;
    }

    [Test]
    public async Task ReadsMatchTheArrayOverloads()
    {
        // an odd offset, so an implementation that assumes alignment would fault or misread
        foreach (var offset in new[] { 0, 1, 3, 5, 8 })
        {
            await Assert.That(ReadBoolean(offset)).IsEqualTo(BitConverter.ToBoolean(source, offset));
            await Assert.That(ReadChar(offset)).IsEqualTo(BitConverter.ToChar(source, offset));
            await Assert.That(ReadInt16(offset)).IsEqualTo(BitConverter.ToInt16(source, offset));
            await Assert.That(ReadInt32(offset)).IsEqualTo(BitConverter.ToInt32(source, offset));
            await Assert.That(ReadInt64(offset)).IsEqualTo(BitConverter.ToInt64(source, offset));
            await Assert.That(ReadSingle(offset)).IsEqualTo(BitConverter.ToSingle(source, offset));
            await Assert.That(ReadDouble(offset)).IsEqualTo(BitConverter.ToDouble(source, offset));
            await Assert.That(ReadUInt16(offset)).IsEqualTo(BitConverter.ToUInt16(source, offset));
            await Assert.That(ReadUInt32(offset)).IsEqualTo(BitConverter.ToUInt32(source, offset));
            await Assert.That(ReadUInt64(offset)).IsEqualTo(BitConverter.ToUInt64(source, offset));
        }
    }

    [Test]
    public async Task ToBoolean_TreatsAnyNonZeroAsTrue()
    {
        await Assert.That(ReadBooleanOf(0)).IsFalse();
        await Assert.That(ReadBooleanOf(1)).IsTrue();
        await Assert.That(ReadBooleanOf(2)).IsTrue();
        await Assert.That(ReadBooleanOf(255)).IsTrue();
    }

    [Test]
    public async Task ShortSpanThrows()
    {
        await Assert.That(Capture(() => ReadBoolean(source, 0))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => ReadChar(source, 1))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => ReadInt16(source, 1))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => ReadInt32(source, 3))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => ReadInt64(source, 7))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => ReadSingle(source, 3))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => ReadDouble(source, 7))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => ReadUInt16(source, 1))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => ReadUInt32(source, 3))).IsEqualTo("ArgumentOutOfRangeException:value");
        await Assert.That(Capture(() => ReadUInt64(source, 7))).IsEqualTo("ArgumentOutOfRangeException:value");

        // exactly enough is accepted
        await Assert.That(Capture(() => ReadInt32(source, 4))).IsEqualTo("none");
        await Assert.That(Capture(() => ReadInt64(source, 8))).IsEqualTo("none");
    }

    [Test]
    public async Task TryWriteBytes_MatchesGetBytesAndSizing()
    {
        await Check(1, BitConverter.GetBytes(true), (buffer, _) => BitConverter.TryWriteBytes(buffer, true));
        await Check(2, BitConverter.GetBytes('A'), (buffer, _) => BitConverter.TryWriteBytes(buffer, 'A'));
        await Check(2, BitConverter.GetBytes((short) 0x1234), (buffer, _) => BitConverter.TryWriteBytes(buffer, (short) 0x1234));
        await Check(4, BitConverter.GetBytes(0x12345678), (buffer, _) => BitConverter.TryWriteBytes(buffer, 0x12345678));
        await Check(8, BitConverter.GetBytes(0x1122334455667788L), (buffer, _) => BitConverter.TryWriteBytes(buffer, 0x1122334455667788L));
        await Check(4, BitConverter.GetBytes(1.5f), (buffer, _) => BitConverter.TryWriteBytes(buffer, 1.5f));
        await Check(8, BitConverter.GetBytes(1.5d), (buffer, _) => BitConverter.TryWriteBytes(buffer, 1.5d));
        await Check(2, BitConverter.GetBytes((ushort) 0xABCD), (buffer, _) => BitConverter.TryWriteBytes(buffer, (ushort) 0xABCD));
        await Check(4, BitConverter.GetBytes(0xDEADBEEFu), (buffer, _) => BitConverter.TryWriteBytes(buffer, 0xDEADBEEFu));
        await Check(8, BitConverter.GetBytes(0xFEEDFACECAFEBEEFuL), (buffer, _) => BitConverter.TryWriteBytes(buffer, 0xFEEDFACECAFEBEEFuL));
    }

    static async Task Check(int width, byte[] expected, Func<byte[], int, bool> write)
    {
        // one byte short: refused, and the destination is left alone
        var small = Filled(width - 1);
        var smallOk = write(small, 0);
        await Assert.That(smallOk).IsFalse();
        await Assert.That(AllFill(small)).IsTrue();

        var exact = Filled(width);
        var exactOk = write(exact, 0);
        await Assert.That(exactOk).IsTrue();
        await Assert.That(Hex(exact)).IsEqualTo(Hex(expected));

        // a larger destination is written only up to the width of the value
        var larger = Filled(width + 1);
        var largerOk = write(larger, 0);
        await Assert.That(largerOk).IsTrue();
        await Assert.That(Hex(larger)).IsEqualTo(Hex(expected) + "aa");
    }

    static byte[] Filled(int size)
    {
        var buffer = new byte[size];
        for (var i = 0; i < size; i++)
        {
            buffer[i] = 0xAA;
        }

        return buffer;
    }

    static bool AllFill(byte[] buffer)
    {
        foreach (var b in buffer)
        {
            if (b != 0xAA)
            {
                return false;
            }
        }

        return true;
    }

    // the spans stay inside these helpers, so nothing ref struct shaped lives across an await
    static bool ReadBoolean(int offset) => BitConverter.ToBoolean(source.AsSpan(offset));
    static char ReadChar(int offset) => BitConverter.ToChar(source.AsSpan(offset));
    static short ReadInt16(int offset) => BitConverter.ToInt16(source.AsSpan(offset));
    static int ReadInt32(int offset) => BitConverter.ToInt32(source.AsSpan(offset));
    static long ReadInt64(int offset) => BitConverter.ToInt64(source.AsSpan(offset));
    static float ReadSingle(int offset) => BitConverter.ToSingle(source.AsSpan(offset));
    static double ReadDouble(int offset) => BitConverter.ToDouble(source.AsSpan(offset));
    static ushort ReadUInt16(int offset) => BitConverter.ToUInt16(source.AsSpan(offset));
    static uint ReadUInt32(int offset) => BitConverter.ToUInt32(source.AsSpan(offset));
    static ulong ReadUInt64(int offset) => BitConverter.ToUInt64(source.AsSpan(offset));

    // read from the tail of the buffer so that only `length` bytes remain
    static bool ReadBoolean(byte[] buffer, int length) => BitConverter.ToBoolean(buffer.AsSpan(buffer.Length - length, length));
    static char ReadChar(byte[] buffer, int length) => BitConverter.ToChar(buffer.AsSpan(buffer.Length - length, length));
    static short ReadInt16(byte[] buffer, int length) => BitConverter.ToInt16(buffer.AsSpan(buffer.Length - length, length));
    static int ReadInt32(byte[] buffer, int length) => BitConverter.ToInt32(buffer.AsSpan(buffer.Length - length, length));
    static long ReadInt64(byte[] buffer, int length) => BitConverter.ToInt64(buffer.AsSpan(buffer.Length - length, length));
    static float ReadSingle(byte[] buffer, int length) => BitConverter.ToSingle(buffer.AsSpan(buffer.Length - length, length));
    static double ReadDouble(byte[] buffer, int length) => BitConverter.ToDouble(buffer.AsSpan(buffer.Length - length, length));
    static ushort ReadUInt16(byte[] buffer, int length) => BitConverter.ToUInt16(buffer.AsSpan(buffer.Length - length, length));
    static uint ReadUInt32(byte[] buffer, int length) => BitConverter.ToUInt32(buffer.AsSpan(buffer.Length - length, length));
    static ulong ReadUInt64(byte[] buffer, int length) => BitConverter.ToUInt64(buffer.AsSpan(buffer.Length - length, length));

    static bool ReadBooleanOf(byte value) => BitConverter.ToBoolean(new[] { value }.AsSpan());

    static string Hex(byte[] bytes)
    {
        var builder = new System.Text.StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }

    static string Capture(Action action)
    {
        try
        {
            action();
            return "none";
        }
        catch (ArgumentException exception)
        {
            return $"{exception.GetType().Name}:{exception.ParamName}";
        }
    }
}

#endif
