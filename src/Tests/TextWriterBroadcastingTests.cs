using System.IO;
using System.Text;
using System.Threading;

// Runs on every target, so net9.0 and later validate the BCL and everything below the polyfill.
public class TextWriterBroadcastingTests
{
    // The point of this test: the broadcast must forward each overload to the *same* overload on
    // every underlying writer. Leaving the base class to decompose Write(int) into Write(string)
    // would still produce the right characters, but would bypass a writer that overrides the
    // composite overload, which is the normal shape of a logging writer.
    [Test]
    public async Task ForwardsEachOverloadVerbatim()
    {
        var log = new List<string>();
        var first = new Recorder("A", log);
        var second = new Recorder("B", log);
        var broadcast = TextWriter.CreateBroadcasting(first, second);

        var expected = new List<string>();

        void Expect(string call)
        {
            expected.Add("A." + call);
            expected.Add("B." + call);
        }

        broadcast.Write(true);
        Expect("Write(bool)");
        broadcast.Write('c');
        Expect("Write(char)");
        broadcast.Write(new[] { 'a' });
        Expect("Write(char[])");
        broadcast.Write(new[] { 'a', 'b' }, 0, 1);
        Expect("Write(char[],int,int)");
        broadcast.Write(1.5m);
        Expect("Write(decimal)");
        broadcast.Write(1.5d);
        Expect("Write(double)");
        broadcast.Write(1.5f);
        Expect("Write(float)");
        broadcast.Write(1);
        Expect("Write(int)");
        broadcast.Write(1L);
        Expect("Write(long)");
        broadcast.Write((object) "o");
        Expect("Write(object)");
        broadcast.Write("s");
        Expect("Write(string)");
        broadcast.Write(1u);
        Expect("Write(uint)");
        broadcast.Write(1ul);
        Expect("Write(ulong)");
        broadcast.Write("{0}", 1);
        Expect("Write(format,arg0)");
        broadcast.Write("{0}{1}", 1, 2);
        Expect("Write(format,arg0,arg1)");
        broadcast.Write("{0}{1}{2}", 1, 2, 3);
        Expect("Write(format,arg0,arg1,arg2)");
        // an explicit array, since net9.0 added a params ReadOnlySpan<object> overload that a
        // loose argument list would bind to there but not below
        broadcast.Write("{0}{1}{2}{3}", new object[] { 1, 2, 3, 4 });
        Expect("Write(format,args)");

        broadcast.WriteLine();
        Expect("WriteLine()");
        broadcast.WriteLine(true);
        Expect("WriteLine(bool)");
        broadcast.WriteLine('c');
        Expect("WriteLine(char)");
        broadcast.WriteLine(new[] { 'a' });
        Expect("WriteLine(char[])");
        broadcast.WriteLine(new[] { 'a', 'b' }, 0, 1);
        Expect("WriteLine(char[],int,int)");
        broadcast.WriteLine(1.5m);
        Expect("WriteLine(decimal)");
        broadcast.WriteLine(1.5d);
        Expect("WriteLine(double)");
        broadcast.WriteLine(1.5f);
        Expect("WriteLine(float)");
        broadcast.WriteLine(1);
        Expect("WriteLine(int)");
        broadcast.WriteLine(1L);
        Expect("WriteLine(long)");
        broadcast.WriteLine((object) "o");
        Expect("WriteLine(object)");
        broadcast.WriteLine("s");
        Expect("WriteLine(string)");
        broadcast.WriteLine(1u);
        Expect("WriteLine(uint)");
        broadcast.WriteLine(1ul);
        Expect("WriteLine(ulong)");
        broadcast.WriteLine("{0}", 1);
        Expect("WriteLine(format,arg0)");
        broadcast.WriteLine("{0}{1}", 1, 2);
        Expect("WriteLine(format,arg0,arg1)");
        broadcast.WriteLine("{0}{1}{2}", 1, 2, 3);
        Expect("WriteLine(format,arg0,arg1,arg2)");
        broadcast.WriteLine("{0}{1}{2}{3}", new object[] { 1, 2, 3, 4 });
        Expect("WriteLine(format,args)");

        broadcast.Flush();
        Expect("Flush()");
        await broadcast.FlushAsync();
        Expect("FlushAsync()");
        await broadcast.WriteAsync('c');
        Expect("WriteAsync(char)");
        await broadcast.WriteAsync(new[] { 'a', 'b' }, 0, 1);
        Expect("WriteAsync(char[],int,int)");
        await broadcast.WriteAsync("s");
        Expect("WriteAsync(string)");
        await broadcast.WriteLineAsync();
        Expect("WriteLineAsync()");
        await broadcast.WriteLineAsync('c');
        Expect("WriteLineAsync(char)");
        await broadcast.WriteLineAsync(new[] { 'a', 'b' }, 0, 1);
        Expect("WriteLineAsync(char[],int,int)");
        await broadcast.WriteLineAsync("s");
        Expect("WriteLineAsync(string)");

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        broadcast.Write("ab".AsSpan());
        Expect("Write(span)");
        broadcast.WriteLine("ab".AsSpan());
        Expect("WriteLine(span)");
        await broadcast.WriteAsync("ab".AsMemory());
        Expect("WriteAsync(memory)");
        await broadcast.WriteLineAsync("ab".AsMemory());
        Expect("WriteLineAsync(memory)");
#endif

#if NETCOREAPP3_0_OR_GREATER
        broadcast.Write(new StringBuilder("sb"));
        Expect("Write(StringBuilder)");
        broadcast.WriteLine(new StringBuilder("sb"));
        Expect("WriteLine(StringBuilder)");
        await broadcast.WriteAsync(new StringBuilder("sb"));
        Expect("WriteAsync(StringBuilder)");
        await broadcast.WriteLineAsync(new StringBuilder("sb"));
        Expect("WriteLineAsync(StringBuilder)");
#endif

#if NET8_0_OR_GREATER
        await broadcast.FlushAsync(CancellationToken.None);
        Expect("FlushAsync(token)");
#endif

        await Assert.That(string.Join("\n", log)).IsEqualTo(string.Join("\n", expected));
    }

    [Test]
    public async Task Properties()
    {
        var log = new List<string>();
        var first = new Recorder("A", log) { EncodingOverride = Encoding.ASCII, ProviderOverride = System.Globalization.CultureInfo.GetCultureInfo("fr-FR") };
        var second = new Recorder("B", log) { EncodingOverride = Encoding.UTF32 };
        var broadcast = TextWriter.CreateBroadcasting(first, second);

        // both come from the first writer
        await Assert.That(broadcast.Encoding).IsSameReferenceAs(first.Encoding);
        await Assert.That(broadcast.FormatProvider).IsSameReferenceAs(first.FormatProvider);

        var empty = TextWriter.CreateBroadcasting();
        await Assert.That(empty.Encoding).IsSameReferenceAs(Encoding.Unicode);
        await Assert.That(empty.FormatProvider).IsSameReferenceAs(System.Globalization.CultureInfo.InvariantCulture);
    }

    [Test]
    public async Task NewLine_PropagatesToEveryWriter()
    {
        var log = new List<string>();
        var first = new Recorder("A", log);
        var second = new Recorder("B", log);
        first.NewLine = "<1>";
        second.NewLine = "<2>";
        var broadcast = TextWriter.CreateBroadcasting(first, second);

        // the broadcast keeps its own default rather than adopting a child's
        await Assert.That(broadcast.NewLine).IsEqualTo(Environment.NewLine);

        broadcast.NewLine = "<B>";
        await Assert.That(broadcast.NewLine).IsEqualTo("<B>");
        await Assert.That(first.NewLine).IsEqualTo("<B>");
        await Assert.That(second.NewLine).IsEqualTo("<B>");
    }

    [Test]
    public async Task ArgumentValidation()
    {
        var log = new List<string>();
        await Assert.That(() => TextWriter.CreateBroadcasting(null!)).Throws<ArgumentNullException>();
        await Assert.That(() => TextWriter.CreateBroadcasting(new Recorder("A", log), null!)).Throws<ArgumentNullException>();

        // an empty broadcast is legal and simply drops everything
        var empty = TextWriter.CreateBroadcasting();
        empty.Write("dropped");
        empty.Flush();
        await Assert.That(log.Count).IsEqualTo(0);
    }

    [Test]
    public async Task CopiesTheArray()
    {
        var log = new List<string>();
        var writers = new TextWriter[] { new Recorder("A", log), new Recorder("B", log) };
        var broadcast = TextWriter.CreateBroadcasting(writers);

        writers[0] = new Recorder("Z", log);
        broadcast.Write("x");

        await Assert.That(log).IsEquivalentTo(new List<string> { "A.Write(string)", "B.Write(string)" });
    }

    [Test]
    public async Task DisposeReachesEveryWriter()
    {
        var log = new List<string>();
        var broadcast = TextWriter.CreateBroadcasting(new Recorder("A", log), new Recorder("B", log));
        broadcast.Dispose();
        await Assert.That(log).IsEquivalentTo(new List<string> { "A.Dispose(True)", "B.Dispose(True)" });

        // Close routes through Dispose rather than the children's Close
        log.Clear();
        TextWriter.CreateBroadcasting(new Recorder("C", log), new Recorder("D", log)).Close();
        await Assert.That(log).IsEquivalentTo(new List<string> { "C.Dispose(True)", "D.Dispose(True)" });
    }

#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    [Test]
    public async Task DisposeAsyncReachesEveryWriter()
    {
        var log = new List<string>();
        await TextWriter.CreateBroadcasting(new Recorder("A", log), new Recorder("B", log)).DisposeAsync();
        await Assert.That(log).IsEquivalentTo(new List<string> { "A.DisposeAsync()", "B.DisposeAsync()" });
    }
#endif

    [Test]
    public async Task AWriterThatThrowsStopsTheRest()
    {
        var log = new List<string>();
        var broadcast = TextWriter.CreateBroadcasting(new Throwing(), new Recorder("B", log));

        await Assert.That(() => broadcast.Write("x")).Throws<InvalidOperationException>();
        // the writers after the failing one are not reached
        await Assert.That(log.Count).IsEqualTo(0);
    }

    class Throwing : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(string? value) =>
            throw new InvalidOperationException("boom");
    }

    class Recorder(string tag, List<string> log) : TextWriter
    {
        public Encoding? EncodingOverride { get; set; }
        public IFormatProvider? ProviderOverride { get; set; }

        public override Encoding Encoding => EncodingOverride ?? Encoding.UTF8;
        public override IFormatProvider FormatProvider => ProviderOverride ?? base.FormatProvider;

        void Add(string call) => log.Add(tag + "." + call);

        public override void Write(bool value) => Add("Write(bool)");
        public override void Write(char value) => Add("Write(char)");
        public override void Write(char[]? buffer) => Add("Write(char[])");
        public override void Write(char[] buffer, int index, int count) => Add("Write(char[],int,int)");
        public override void Write(decimal value) => Add("Write(decimal)");
        public override void Write(double value) => Add("Write(double)");
        public override void Write(float value) => Add("Write(float)");
        public override void Write(int value) => Add("Write(int)");
        public override void Write(long value) => Add("Write(long)");
        public override void Write(object? value) => Add("Write(object)");
        public override void Write(string? value) => Add("Write(string)");
        public override void Write(uint value) => Add("Write(uint)");
        public override void Write(ulong value) => Add("Write(ulong)");
        public override void Write(string format, object? arg0) => Add("Write(format,arg0)");
        public override void Write(string format, object? arg0, object? arg1) => Add("Write(format,arg0,arg1)");
        public override void Write(string format, object? arg0, object? arg1, object? arg2) => Add("Write(format,arg0,arg1,arg2)");
        public override void Write(string format, params object?[] arg) => Add("Write(format,args)");

        public override void WriteLine() => Add("WriteLine()");
        public override void WriteLine(bool value) => Add("WriteLine(bool)");
        public override void WriteLine(char value) => Add("WriteLine(char)");
        public override void WriteLine(char[]? buffer) => Add("WriteLine(char[])");
        public override void WriteLine(char[] buffer, int index, int count) => Add("WriteLine(char[],int,int)");
        public override void WriteLine(decimal value) => Add("WriteLine(decimal)");
        public override void WriteLine(double value) => Add("WriteLine(double)");
        public override void WriteLine(float value) => Add("WriteLine(float)");
        public override void WriteLine(int value) => Add("WriteLine(int)");
        public override void WriteLine(long value) => Add("WriteLine(long)");
        public override void WriteLine(object? value) => Add("WriteLine(object)");
        public override void WriteLine(string? value) => Add("WriteLine(string)");
        public override void WriteLine(uint value) => Add("WriteLine(uint)");
        public override void WriteLine(ulong value) => Add("WriteLine(ulong)");
        public override void WriteLine(string format, object? arg0) => Add("WriteLine(format,arg0)");
        public override void WriteLine(string format, object? arg0, object? arg1) => Add("WriteLine(format,arg0,arg1)");
        public override void WriteLine(string format, object? arg0, object? arg1, object? arg2) => Add("WriteLine(format,arg0,arg1,arg2)");
        public override void WriteLine(string format, params object?[] arg) => Add("WriteLine(format,args)");

        public override void Flush() => Add("Flush()");

        public override Task FlushAsync()
        {
            Add("FlushAsync()");
            return Task.CompletedTask;
        }

        public override Task WriteAsync(char value)
        {
            Add("WriteAsync(char)");
            return Task.CompletedTask;
        }

        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            Add("WriteAsync(char[],int,int)");
            return Task.CompletedTask;
        }

        public override Task WriteAsync(string? value)
        {
            Add("WriteAsync(string)");
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync()
        {
            Add("WriteLineAsync()");
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(char value)
        {
            Add("WriteLineAsync(char)");
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(char[] buffer, int index, int count)
        {
            Add("WriteLineAsync(char[],int,int)");
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(string? value)
        {
            Add("WriteLineAsync(string)");
            return Task.CompletedTask;
        }

        protected override void Dispose(bool disposing)
        {
            Add($"Dispose({disposing})");
            base.Dispose(disposing);
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        public override void Write(ReadOnlySpan<char> buffer) => Add("Write(span)");
        public override void WriteLine(ReadOnlySpan<char> buffer) => Add("WriteLine(span)");

        public override Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default)
        {
            Add("WriteAsync(memory)");
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default)
        {
            Add("WriteLineAsync(memory)");
            return Task.CompletedTask;
        }
#endif

#if NETCOREAPP3_0_OR_GREATER
        public override void Write(StringBuilder? value) => Add("Write(StringBuilder)");
        public override void WriteLine(StringBuilder? value) => Add("WriteLine(StringBuilder)");

        public override Task WriteAsync(StringBuilder? value, CancellationToken cancellationToken = default)
        {
            Add("WriteAsync(StringBuilder)");
            return Task.CompletedTask;
        }

        public override Task WriteLineAsync(StringBuilder? value, CancellationToken cancellationToken = default)
        {
            Add("WriteLineAsync(StringBuilder)");
            return Task.CompletedTask;
        }
#endif

#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        public override ValueTask DisposeAsync()
        {
            Add("DisposeAsync()");
            return default;
        }
#endif

#if NET8_0_OR_GREATER
        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            Add("FlushAsync(token)");
            return Task.CompletedTask;
        }
#endif
    }
}
