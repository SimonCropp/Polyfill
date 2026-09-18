#nullable enable

// ArrayBufferWriter<T> ships in the BCL from netcoreapp3.0/netstandard2.1. This polyfill covers the
// earlier targets; where the runtime provides the type it is forwarded at the end of this file.
// ResetWrittenCount only arrived in net8.0, so for the targets that have the type but not that
// member it is added as an extension method in Polyfill_ArrayBufferWriter.cs.
#if FeatureMemory

#if !NETCOREAPP3_0_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

#pragma warning disable

namespace System.Buffers;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a heap-based, array-backed output sink into which <typeparamref name="T"/> data can be written.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1?view=net-11.0
#if PolyPublic
public
#endif
sealed class ArrayBufferWriter<T> :
    IBufferWriter<T>
{
    // Array.MaxLength is not exposed on the targets this type is compiled for.
    const int ArrayMaxLength = 0x7FFFFFC7;
    const int DefaultInitialBufferSize = 256;

    T[] buffer;
    int index;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayBufferWriter{T}"/> class with the default initial capacity.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.-ctor?view=net-11.0#system-buffers-arraybufferwriter-1-ctor
    public ArrayBufferWriter()
    {
        buffer = Array.Empty<T>();
        index = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayBufferWriter{T}"/> class with the specified initial capacity.
    /// </summary>
    /// <param name="initialCapacity">The minimum capacity with which to initialize the underlying buffer.</param>
    /// <exception cref="ArgumentException"><paramref name="initialCapacity"/> is less than or equal to 0.</exception>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.-ctor?view=net-11.0#system-buffers-arraybufferwriter-1-ctor(system-int32)
    public ArrayBufferWriter(int initialCapacity)
    {
        if (initialCapacity <= 0)
        {
            throw new ArgumentException(null, nameof(initialCapacity));
        }

        buffer = new T[initialCapacity];
        index = 0;
    }

    /// <summary>
    /// Gets the data written to the underlying buffer so far, as a <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.writtenmemory?view=net-11.0
    public ReadOnlyMemory<T> WrittenMemory => buffer.AsMemory(0, index);

    /// <summary>
    /// Gets the data written to the underlying buffer so far, as a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.writtenspan?view=net-11.0
    public ReadOnlySpan<T> WrittenSpan => buffer.AsSpan(0, index);

    /// <summary>
    /// Gets the amount of data written to the underlying buffer so far.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.writtencount?view=net-11.0
    public int WrittenCount => index;

    /// <summary>
    /// Gets the total amount of space within the underlying buffer.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.capacity?view=net-11.0
    public int Capacity => buffer.Length;

    /// <summary>
    /// Gets the amount of space available that can still be written into without forcing the underlying buffer to grow.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.freecapacity?view=net-11.0
    public int FreeCapacity => buffer.Length - index;

    /// <summary>
    /// Clears the data written to the underlying buffer.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.clear?view=net-11.0
    public void Clear()
    {
        buffer.AsSpan(0, index).Clear();
        index = 0;
    }

    /// <summary>
    /// Resets the data written to the underlying buffer without zeroing its contents.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.resetwrittencount?view=net-11.0
    public void ResetWrittenCount() =>
        index = 0;

    /// <summary>
    /// Notifies the <see cref="IBufferWriter{T}"/> that <paramref name="count"/> data items were written to the output <see cref="Span{T}"/> or <see cref="Memory{T}"/>.
    /// </summary>
    /// <param name="count">The number of data items written.</param>
    /// <exception cref="ArgumentException"><paramref name="count"/> is negative.</exception>
    /// <exception cref="InvalidOperationException">The method call attempts to advance past the end of the underlying buffer.</exception>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.advance?view=net-11.0
    public void Advance(int count)
    {
        if (count < 0)
        {
            throw new ArgumentException(null, nameof(count));
        }

        if (index > buffer.Length - count)
        {
            throw new InvalidOperationException($"Cannot advance past the end of the buffer, which has a size of {buffer.Length}.");
        }

        index += count;
    }

    /// <summary>
    /// Returns a <see cref="Memory{T}"/> to write to that is at least the requested size, as specified by <paramref name="sizeHint"/>.
    /// </summary>
    /// <param name="sizeHint">The minimum length of the returned <see cref="Memory{T}"/>. If 0, a non-empty buffer is returned.</param>
    /// <exception cref="ArgumentException"><paramref name="sizeHint"/> is negative.</exception>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.getmemory?view=net-11.0
    public Memory<T> GetMemory(int sizeHint = 0)
    {
        CheckAndResizeBuffer(sizeHint);
        return buffer.AsMemory(index);
    }

    /// <summary>
    /// Returns a <see cref="Span{T}"/> to write to that is at least the requested size, as specified by <paramref name="sizeHint"/>.
    /// </summary>
    /// <param name="sizeHint">The minimum length of the returned <see cref="Span{T}"/>. If 0, a non-empty buffer is returned.</param>
    /// <exception cref="ArgumentException"><paramref name="sizeHint"/> is negative.</exception>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.buffers.arraybufferwriter-1.getspan?view=net-11.0
    public Span<T> GetSpan(int sizeHint = 0)
    {
        CheckAndResizeBuffer(sizeHint);
        return buffer.AsSpan(index);
    }

    void CheckAndResizeBuffer(int sizeHint)
    {
        if (sizeHint < 0)
        {
            throw new ArgumentException(null, nameof(sizeHint));
        }

        if (sizeHint == 0)
        {
            sizeHint = 1;
        }

        if (sizeHint <= FreeCapacity)
        {
            return;
        }

        var currentLength = buffer.Length;

        // Attempt to grow by the larger of the sizeHint and double the current size.
        var growBy = Math.Max(sizeHint, currentLength);

        if (currentLength == 0)
        {
            growBy = Math.Max(growBy, DefaultInitialBufferSize);
        }

        var newSize = currentLength + growBy;

        if ((uint) newSize > ArrayMaxLength)
        {
            // Attempt to grow to the maximum array length instead.
            var needed = (uint) (currentLength - FreeCapacity + sizeHint);

            if (needed > ArrayMaxLength)
            {
                throw new OutOfMemoryException($"Cannot allocate a buffer of size {needed}.");
            }

            newSize = ArrayMaxLength;
        }

        Array.Resize(ref buffer, newSize);
    }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Buffers.ArrayBufferWriter<>))]
#endif

#endif
