// Rune ships from netcoreapp3.0. Write(Rune) and WriteLine(Rune) aren't polyfilled: the instance Write(object) and
// WriteLine(object) already accept a Rune and write the same text, and an instance method always wins over an
// extension, so the polyfill would never be called.
#if NETCOREAPP3_0_OR_GREATER && !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    /// <summary>
    /// Writes a rune to the text stream asynchronously.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-11.0#system-io-textwriter-writeasync(system-text-rune)
    public static Task WriteAsync(this TextWriter target, Rune value)
    {
        // matches the BCL's TextWriter.WriteAsync(Rune), which nothing in the BCL overrides apart from TextWriter.Null
        if (value.IsBmp)
        {
            return target.WriteAsync((char) value.Value);
        }

        Span<char> chars = stackalloc char[2];
        value.EncodeToUtf16(chars);
        return Task.Factory.StartNew(
            static state =>
            {
                var (writer, high, low) = ((TextWriter, char, char)) state!;
                writer.Write(high);
                writer.Write(low);
            },
            (target, chars[0], chars[1]),
            CancellationToken.None,
            TaskCreationOptions.DenyChildAttach,
            TaskScheduler.Default);
    }

    /// <summary>
    /// Writes a rune followed by a line terminator to the text stream asynchronously.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-11.0#system-io-textwriter-writelineasync(system-text-rune)
    public static Task WriteLineAsync(this TextWriter target, Rune value)
    {
        // matches the BCL's TextWriter.WriteLineAsync(Rune), which nothing in the BCL overrides apart from TextWriter.Null
        if (value.IsBmp)
        {
            return target.WriteLineAsync((char) value.Value);
        }

        Span<char> chars = stackalloc char[2];
        value.EncodeToUtf16(chars);
        return Task.Factory.StartNew(
            static state =>
            {
                var (writer, high, low) = ((TextWriter, char, char)) state!;
                writer.Write(high);
                writer.WriteLine(low);
            },
            (target, chars[0], chars[1]),
            CancellationToken.None,
            TaskCreationOptions.DenyChildAttach,
            TaskScheduler.Default);
    }
}

#endif
