namespace ConsumeOldStyle;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Polyfills;

static class StreamUsage
{
    /* No Feature */
    public static void ReadExactly(Stream stream, byte[] buffer, int offset, int count) =>
        stream.ReadExactly(buffer, offset, count);

    /* FeatureMemory */
    public static int Read(Stream stream, Span<byte> buffer) =>
        stream.Read(buffer);

    /* FeatureMemory && FeatureValueTask */
    public static ValueTask<int> ReadAsync(Stream stream, Memory<byte> buffer) =>
        stream.ReadAsync(buffer, CancellationToken.None);
}
