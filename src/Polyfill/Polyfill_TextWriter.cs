namespace Polyfills;

using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
#if FeatureMemory
using System.Buffers;
#endif

static partial class Polyfill
{
#if !NET11_0_OR_GREATER

    /// <summary>
    /// Asynchronously writes a string to the stream, with a cancellation token.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-11.0#system-io-textwriter-writeasync(system-string-system-threading-cancellationtoken)
    public static Task WriteAsync(this TextWriter target, string? value, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.WriteAsync(value)
            .WaitAsync(cancellationToken);
    }

    /// <summary>
    /// Asynchronously writes a line terminator to the stream, with a cancellation token.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-11.0#system-io-textwriter-writelineasync(system-threading-cancellationtoken)
    public static Task WriteLineAsync(this TextWriter target, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.WriteLineAsync()
            .WaitAsync(cancellationToken);
    }

    /// <summary>
    /// Asynchronously writes a string followed by a line terminator to the stream, with a cancellation token.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-11.0#system-io-textwriter-writelineasync(system-string-system-threading-cancellationtoken)
    public static Task WriteLineAsync(this TextWriter target, string? value, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.WriteLineAsync(value)
            .WaitAsync(cancellationToken);
    }

#endif

#if !NET8_0_OR_GREATER

    //https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/IO/TextWriter.cs#L670

    /// <summary>
    /// Asynchronously clears all buffers for the current writer and causes any buffered data to
    /// be written to the underlying device.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.flushasync?view=net-11.0#system-io-textwriter-flushasync(system-threading-cancellationtoken)
    public static Task FlushAsync(this TextWriter target, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.FlushAsync()
            .WaitAsync(cancellationToken);
    }

#endif

#if !NETCOREAPP3_0_OR_GREATER
    /// <summary>
    /// Equivalent to Write(stringBuilder.ToString()) however it uses the
    /// StringBuilder.GetChunks() method to avoid creating the intermediate string
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-11.0#system-io-textwriter-write(system-text-stringbuilder)
    public static void Write(this TextWriter target, StringBuilder? value)
    {
        if (value == null)
        {
            return;
        }

#if FeatureMemory
        foreach (var chunk in value.GetChunks())
        {
            target.Write(chunk.Span);
        }
#else
        target.Write(value.ToString());
#endif
    }

    /// <summary>
    /// Equivalent to WriteAsync(stringBuilder.ToString()) however it uses the
    /// StringBuilder.GetChunks() method to avoid creating the intermediate string
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-11.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)
    public static Task WriteAsync(this TextWriter target, StringBuilder? value, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        if (value == null)
        {
            return Task.CompletedTask;
        }

#if FeatureValueTask && FeatureMemory
        return WriteAsyncCore(target, value, cancellationToken);

        static async Task WriteAsyncCore(TextWriter target, StringBuilder builder, CancellationToken cancel)
        {
            foreach (var chunk in builder.GetChunks())
            {
                await target.WriteAsync(chunk, cancel);
            }
        }
#else
        return target.WriteAsync(value.ToString());
#endif
    }
#endif

#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER && FeatureMemory
#if FeatureValueTask

    /// <summary>
    /// Asynchronously writes a character memory region to the stream.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-11.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)
    public static ValueTask WriteAsync(
        this TextWriter target,
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        var task = target.WriteAsync(segment.Array!, segment.Offset, segment.Count)
            .WaitAsync(cancellationToken);
        return new(task);
    }

    /// <summary>
    /// Asynchronously writes the text representation of a character memory region to the stream, followed by a line terminator.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-11.0#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)
    public static ValueTask WriteLineAsync(
        this TextWriter target,
        ReadOnlyMemory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        var task = target.WriteLineAsync(segment.Array!, segment.Offset, segment.Count)
            .WaitAsync(cancellationToken);
        return new(task);
    }

#endif

    /// <summary>
    /// Writes a character span to the text stream.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-11.0#system-io-textwriter-write(system-readonlyspan((system-char)))
    public static void Write(
        this TextWriter target,
        ReadOnlySpan<char> buffer)
    {
        var pool = ArrayPool<char>.Shared;
        var array = pool.Rent(buffer.Length);

        try
        {
            buffer.CopyTo(new(array));
            target.Write(array, 0, buffer.Length);
        }
        finally
        {
            pool.Return(array);
        }
    }

    /// <summary>
    /// Writes the text representation of a character span to the text stream, followed by a line terminator.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline?view=net-11.0#system-io-textwriter-writeline(system-readonlyspan((system-char)))
    public static void WriteLine(
        this TextWriter target,
        ReadOnlySpan<char> buffer)
    {
        var pool = ArrayPool<char>.Shared;
        var array = pool.Rent(buffer.Length);

        try
        {
            buffer.CopyTo(new(array));
            target.WriteLine(array, 0, buffer.Length);
        }
        finally
        {
            pool.Return(array);
        }
    }
#endif
}
