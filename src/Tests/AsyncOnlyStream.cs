/// <summary>
/// A stream that can only be read asynchronously, a byte at a time, and observes the cancellation token on every read.
/// </summary>
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Diagnostics.DebuggerNonUserCode]
class AsyncOnlyStream(byte[] data) :
    Stream
{
    int position;

    public int ReadCount { get; private set; }

    /// <summary>
    /// Cancelled once the first read has completed.
    /// </summary>
    public CancellationTokenSource? CancelAfterFirstRead { get; set; }

    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => throw new NotSupportedException();

    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    public override int Read(byte[] buffer, int offset, int count) =>
        throw new InvalidOperationException("The stream was read synchronously.");

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, Cancel cancel)
    {
        cancel.ThrowIfCancellationRequested();
        await Task.Yield();
        ReadCount++;
        if (position == data.Length || count == 0)
        {
            return 0;
        }

        buffer[offset] = data[position];
        position++;
        if (ReadCount == 1)
        {
            CancelAfterFirstRead?.Cancel();
        }

        return 1;
    }

    public override void Flush()
    {
    }

    public override long Seek(long offset, SeekOrigin origin) =>
        throw new NotSupportedException();

    public override void SetLength(long value) =>
        throw new NotSupportedException();

    public override void Write(byte[] buffer, int offset, int count) =>
        throw new NotSupportedException();
}
