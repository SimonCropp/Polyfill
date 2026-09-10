#pragma warning disable

#if !NET9_0_OR_GREATER

namespace Polyfills;

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    extension(TextWriter)
    {
        /// <summary>
        /// Creates an instance of <see cref="TextWriter"/> that writes supplied inputs to each of the writers in <paramref name="writers"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.createbroadcasting?view=net-11.0
        public static TextWriter CreateBroadcasting(params TextWriter[] writers)
        {
            if (writers == null)
            {
                throw new ArgumentNullException(nameof(writers));
            }

            var copy = new TextWriter[writers.Length];
            for (var index = 0; index < writers.Length; index++)
            {
                copy[index] = writers[index] ?? throw new ArgumentNullException(nameof(writers));
            }

            return new BroadcastingTextWriter(copy);
        }
    }

    // Every virtual member is forwarded verbatim rather than left to the base class to
    // decompose, so a writer that overrides, say, WriteLine(string) still sees that call.
    sealed class BroadcastingTextWriter : TextWriter
    {
        TextWriter[] writers;

        internal BroadcastingTextWriter(TextWriter[] writers) =>
            this.writers = writers;

        public override Encoding Encoding =>
            writers.Length > 0 ? writers[0].Encoding : Encoding.Unicode;

        public override IFormatProvider FormatProvider =>
            writers.Length > 0 ? writers[0].FormatProvider : CultureInfo.InvariantCulture;

        public override string NewLine
        {
            get => base.NewLine;
            set
            {
                base.NewLine = value;
                foreach (var writer in writers)
                {
                    writer.NewLine = value;
                }
            }
        }

        public override void Write(bool value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(bool value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void Write(char value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(char value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void Write(char[] buffer)
        {
            foreach (var writer in writers)
            {
                writer.Write(buffer);
            }
        }

        public override void WriteLine(char[] buffer)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(buffer);
            }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            foreach (var writer in writers)
            {
                writer.Write(buffer, index, count);
            }
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(buffer, index, count);
            }
        }

        public override void Write(decimal value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(decimal value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void Write(double value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(double value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void Write(float value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(float value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void Write(int value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(int value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void Write(long value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(long value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void Write(object? value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(object? value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void Write(string? value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(string? value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void Write(uint value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(uint value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void Write(ulong value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

        public override void WriteLine(ulong value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

        public override void Write(string format, object? arg0)
        {
            foreach (var writer in writers)
            {
                writer.Write(format, arg0);
            }
        }

        public override void WriteLine(string format, object? arg0)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(format, arg0);
            }
        }

        public override void Write(string format, object? arg0, object? arg1)
        {
            foreach (var writer in writers)
            {
                writer.Write(format, arg0, arg1);
            }
        }

        public override void WriteLine(string format, object? arg0, object? arg1)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(format, arg0, arg1);
            }
        }

        public override void Write(string format, object? arg0, object? arg1, object? arg2)
        {
            foreach (var writer in writers)
            {
                writer.Write(format, arg0, arg1, arg2);
            }
        }

        public override void WriteLine(string format, object? arg0, object? arg1, object? arg2)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(format, arg0, arg1, arg2);
            }
        }

        public override void Write(string format, params object?[] arg)
        {
            foreach (var writer in writers)
            {
                writer.Write(format, arg);
            }
        }

        public override void WriteLine(string format, params object?[] arg)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(format, arg);
            }
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER

        public override void Write(ReadOnlySpan<char> buffer)
        {
            foreach (var writer in writers)
            {
                writer.Write(buffer);
            }
        }

#endif

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER

        public override void WriteLine(ReadOnlySpan<char> buffer)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(buffer);
            }
        }

#endif

#if NETCOREAPP3_0_OR_GREATER

        public override void Write(StringBuilder? value)
        {
            foreach (var writer in writers)
            {
                writer.Write(value);
            }
        }

#endif

#if NETCOREAPP3_0_OR_GREATER

        public override void WriteLine(StringBuilder? value)
        {
            foreach (var writer in writers)
            {
                writer.WriteLine(value);
            }
        }

#endif

        public override void WriteLine()
        {
            foreach (var writer in writers)
            {
                writer.WriteLine();
            }
        }

        public override void Flush()
        {
            foreach (var writer in writers)
            {
                writer.Flush();
            }
        }

        public override async Task FlushAsync()
        {
            foreach (var writer in writers)
            {
                await writer.FlushAsync().ConfigureAwait(false);
            }
        }

#if NET8_0_OR_GREATER

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            foreach (var writer in writers)
            {
                await writer.FlushAsync(cancellationToken).ConfigureAwait(false);
            }
        }

#endif

        public override async Task WriteAsync(char value)
        {
            foreach (var writer in writers)
            {
                await writer.WriteAsync(value).ConfigureAwait(false);
            }
        }

        public override async Task WriteAsync(char[] buffer, int index, int count)
        {
            foreach (var writer in writers)
            {
                await writer.WriteAsync(buffer, index, count).ConfigureAwait(false);
            }
        }

        public override async Task WriteAsync(string? value)
        {
            foreach (var writer in writers)
            {
                await writer.WriteAsync(value).ConfigureAwait(false);
            }
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER

        public override async Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default)
        {
            foreach (var writer in writers)
            {
                await writer.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
            }
        }

#endif

#if NETCOREAPP3_0_OR_GREATER

        public override async Task WriteAsync(StringBuilder? value, CancellationToken cancellationToken = default)
        {
            foreach (var writer in writers)
            {
                await writer.WriteAsync(value, cancellationToken).ConfigureAwait(false);
            }
        }

#endif

        public override async Task WriteLineAsync(char value)
        {
            foreach (var writer in writers)
            {
                await writer.WriteLineAsync(value).ConfigureAwait(false);
            }
        }

        public override async Task WriteLineAsync(char[] buffer, int index, int count)
        {
            foreach (var writer in writers)
            {
                await writer.WriteLineAsync(buffer, index, count).ConfigureAwait(false);
            }
        }

        public override async Task WriteLineAsync(string? value)
        {
            foreach (var writer in writers)
            {
                await writer.WriteLineAsync(value).ConfigureAwait(false);
            }
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER

        public override async Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default)
        {
            foreach (var writer in writers)
            {
                await writer.WriteLineAsync(buffer, cancellationToken).ConfigureAwait(false);
            }
        }

#endif

#if NETCOREAPP3_0_OR_GREATER

        public override async Task WriteLineAsync(StringBuilder? value, CancellationToken cancellationToken = default)
        {
            foreach (var writer in writers)
            {
                await writer.WriteLineAsync(value, cancellationToken).ConfigureAwait(false);
            }
        }

#endif

        public override async Task WriteLineAsync()
        {
            foreach (var writer in writers)
            {
                await writer.WriteLineAsync().ConfigureAwait(false);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            foreach (var writer in writers)
            {
                writer.Dispose();
            }
        }

#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER

        public override async ValueTask DisposeAsync()
        {
            foreach (var writer in writers)
            {
                await writer.DisposeAsync().ConfigureAwait(false);
            }
        }

#endif
    }
}

#endif
