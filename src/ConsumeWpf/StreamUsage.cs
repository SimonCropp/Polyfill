namespace ConsumeWpf;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Polyfills;

static class StreamUsage
{
    public static int Read(Stream stream, Span<byte> buffer) => stream.Read(buffer);

    public static ValueTask<int> ReadAsync(Stream stream, Memory<byte> buffer) =>
        stream.ReadAsync(buffer, CancellationToken.None);
}
