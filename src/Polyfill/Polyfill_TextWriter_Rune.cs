#if !NET11_0_OR_GREATER && NETCOREAPP3_0_OR_GREATER

namespace Polyfills;

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    /// <summary>
    /// Writes a <see cref="Rune"/> to the text stream.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-10.0
    public static void Write(this TextWriter target, Rune value) =>
        target.Write(value.ToString());

    /// <summary>
    /// Writes a <see cref="Rune"/> followed by a line terminator to the text stream.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline?view=net-10.0
    public static void WriteLine(this TextWriter target, Rune value) =>
        target.WriteLine(value.ToString());

    /// <summary>
    /// Writes a <see cref="Rune"/> to the text stream asynchronously.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-10.0
    public static Task WriteAsync(this TextWriter target, Rune value, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.WriteAsync(value.ToString());
    }

    /// <summary>
    /// Writes a <see cref="Rune"/> followed by a line terminator to the text stream asynchronously.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-10.0
    public static Task WriteLineAsync(this TextWriter target, Rune value, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return target.WriteLineAsync(value.ToString());
    }
}

#endif
