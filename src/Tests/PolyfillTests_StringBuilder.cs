partial class PolyfillTests
{
    [Test]
    public async Task StringBuilderCopyTo()
    {
        var builder = new StringBuilder("value");

        var span = new Span<char>(new char[1]);
        builder.CopyTo(0, span, 1);
        await Assert.That(span is "v").IsTrue();

        span = new(new char[1]);
        builder.CopyTo(1, span, 1);
        await Assert.That(span is "a").IsTrue();

        span = new(new char[2]);
        builder.CopyTo(1, span, 2);
        await Assert.That(span is "al").IsTrue();

        span = new(new char[5]);
        builder.CopyTo(0, span, 5);
        await Assert.That(span is "value").IsTrue();
    }

    static StringBuilder BuildMultiChunkBuilder(int length)
    {
        // a small initial capacity and many appends leave the builder holding many chunks
        var builder = new StringBuilder(16);
        var index = 0;
        while (builder.Length < length)
        {
            builder.Append((char) ('a' + index % 26));
            if (index % 7 == 0)
            {
                builder.Append("0123456789", 0, Math.Min(10, length - builder.Length));
            }

            index++;
        }

        return builder;
    }

    [Test]
    [Arguments(0, 50000)]
    [Arguments(0, 1)]
    [Arguments(0, 16383)]
    [Arguments(0, 16384)]
    [Arguments(0, 16385)]
    [Arguments(1, 16384)]
    [Arguments(1, 32768)]
    [Arguments(1, 32769)]
    [Arguments(17, 40000)]
    [Arguments(16383, 2)]
    [Arguments(33000, 17000)]
    [Arguments(49999, 1)]
    [Arguments(25000, 0)]
    [Arguments(50000, 0)]
    public async Task StringBuilderCopyTo_MultiChunk(int sourceIndex, int count)
    {
        var builder = BuildMultiChunkBuilder(50000);
        var expected = builder.ToString();
        var destination = new char[count + 3];
#if NET6_0_OR_GREATER
        Array.Fill(destination, '#');
#else
        for (var index = 0; index < destination.Length; index++)
        {
            destination[index] = '#';
        }
#endif

        builder.CopyTo(sourceIndex, destination.AsSpan(), count);

        await Assert.That(builder.Length).IsEqualTo(50000);
        await Assert.That(new string(destination, 0, count)).IsEqualTo(expected.Substring(sourceIndex, count));
        // nothing past count is written
        await Assert.That(new string(destination, count, 3)).IsEqualTo("###");
        // the builder itself is untouched
        await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task StringBuilderCopyTo_ExactDestination()
    {
        var builder = BuildMultiChunkBuilder(40000);
        var destination = new char[40000];

        builder.CopyTo(0, destination.AsSpan(), 40000);

        await Assert.That(new string(destination)).IsEqualTo(builder.ToString());
    }

    [Test]
    public async Task StringBuilderCopyTo_Empty()
    {
        var destination = new[] {'#', '#'};

        new StringBuilder().CopyTo(0, destination.AsSpan(), 0);
        new StringBuilder("abc").CopyTo(3, Span<char>.Empty, 0);

        await Assert.That(new string(destination)).IsEqualTo("##");
    }

    [Test]
    public async Task StringBuilderCopyTo_InvalidArgs()
    {
        var builder = new StringBuilder("value");

        // count < 0
        await Assert.That(() => builder.CopyTo(0, new Span<char>(new char[2]), -1)).Throws<ArgumentOutOfRangeException>();
        // sourceIndex past the end
        await Assert.That(() => builder.CopyTo(6, new Span<char>(new char[2]), 0)).Throws<ArgumentOutOfRangeException>();
        // not enough source characters from sourceIndex onward (silent truncation previously)
        await Assert.That(() => builder.CopyTo(3, new Span<char>(new char[5]), 5)).Throws<ArgumentException>();
        // destination too short
        await Assert.That(() => builder.CopyTo(0, new Span<char>(new char[2]), 5)).Throws<ArgumentException>();
    }

    [Test]
    public async Task Replace()
    {
        var builder = new StringBuilder("a");

        builder.Replace("a".AsSpan(), "b".AsSpan());
        await Assert.That(builder.ToString()).IsEqualTo("b");
    }

#if FeatureMemory

    [Test]
    public async Task GetChunks()
    {
        var builder = new StringBuilder("a", 1);
        builder.Append("bb");
        var list = new List<string>();
        foreach (var chunk in builder.GetChunks())
        {
            list.Add(chunk.ToString());
        }

        await Assert.That(list[0]).IsEqualTo("a");
        await Assert.That(list[1]).IsEqualTo("bb");
    }

#endif

    [Test]
    public async Task Append()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.Append(builder, $"value{x}");
        await Assert.That(builder.ToString()).IsEqualTo("value10");
    }

    [Test]
    public async Task AppendLine()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.AppendLine(builder, $"value{x}");
        await Assert.That(builder.ToString()).IsEqualTo("value10" + Environment.NewLine);
    }

    [Test]
    public async Task AppendWithFormat()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.Append(builder, null, $"value{x}");
        await Assert.That(builder.ToString()).IsEqualTo("value10");
    }

    [Test]
    public async Task AppendLineWithFormat()
    {
        var builder = new StringBuilder();

        var x = 10;
        Polyfill.AppendLine(builder, null, $"value{x}");
        await Assert.That(builder.ToString()).IsEqualTo("value10" + Environment.NewLine);
    }

    [Test]
    public async Task MoveChunks()
    {
        var source = new StringBuilder("hello", 10);
        var maxCapacity = source.MaxCapacity;

        var destination = StringBuilder.MoveChunks(source);

        await Assert.That(destination.ToString()).IsEqualTo("hello");
        await Assert.That(destination.MaxCapacity).IsEqualTo(maxCapacity);

        await Assert.That(source.Length).IsEqualTo(0);
        await Assert.That(source.Capacity).IsEqualTo(0);
        await Assert.That(source.MaxCapacity).IsEqualTo(maxCapacity);

        source.Append('x');
        await Assert.That(source.ToString()).IsEqualTo("x");
    }

    [Test]
    public async Task MoveChunks_Null_Throws() =>
        await Assert.That(() => StringBuilder.MoveChunks(null!)).Throws<ArgumentNullException>();

    // Where the BCL declares StringBuilder.MoveChunks the polyfill must not be compiled: a static
    // member of the type always wins over a static extension member, so it would be dead code.
    [Test]
    public async Task MoveChunks_NotPolyfilledWhereBclHasIt()
    {
        var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        Type[] parameters = [typeof(StringBuilder)];
        var inBcl = typeof(StringBuilder).GetMethod("MoveChunks", flags, null, parameters, null) != null;
        var inPolyfill = typeof(Polyfills.Polyfill).GetMethod("MoveChunks", flags, null, parameters, null) != null;

        await Assert.That(inBcl && inPolyfill).IsFalse();
    }

    [Test]
    public async Task StringBuilder_Insert_ReadOnlySpan()
    {
        var builder = new StringBuilder("ac");
        builder.Insert(1, "b".AsSpan());
        await Assert.That(builder.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task StringBuilder_Append_StringBuilder()
    {
        var source = new StringBuilder("hello");
        var target = new StringBuilder();
        target.Append(source, 1, 3);
        await Assert.That(target.ToString()).IsEqualTo("ell");
    }

    [Test]
    [Arguments(0, 5)]
    [Arguments(0, 1)]
    [Arguments(4, 1)]
    [Arguments(2, 3)]
    [Arguments(1, 3)]
    [Arguments(0, 0)]
    [Arguments(5, 0)]
    public async Task StringBuilder_Append_StringBuilder_Ranges(int startIndex, int count)
    {
        var source = new StringBuilder("hello");
        var target = new StringBuilder("<");

        var result = target.Append(source, startIndex, count);

        await Assert.That(ReferenceEquals(result, target)).IsTrue();
        await Assert.That(target.ToString()).IsEqualTo("<" + "hello".Substring(startIndex, count));
        await Assert.That(source.ToString()).IsEqualTo("hello");
    }

    [Test]
    [Arguments(0, 30000)]
    [Arguments(0, 1)]
    [Arguments(1, 29999)]
    [Arguments(7999, 2)]
    [Arguments(12345, 6789)]
    [Arguments(29999, 1)]
    public async Task StringBuilder_Append_StringBuilder_MultiChunkSource(int startIndex, int count)
    {
        // many small appends leave the source holding many chunks
        var source = new StringBuilder(16);
        for (var index = 0; source.Length < 30000; index++)
        {
            source.Append((char) ('a' + index % 26));
        }

        var expected = source.ToString();
        var target = new StringBuilder("prefix");

        target.Append(source, startIndex, count);
        target.Append(source, startIndex, count);

        var range = expected.Substring(startIndex, count);
        await Assert.That(target.ToString()).IsEqualTo("prefix" + range + range);
        await Assert.That(source.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task StringBuilder_Append_StringBuilder_Self()
    {
        var builder = new StringBuilder("hello");

        builder.Append(builder, 1, 3);

        await Assert.That(builder.ToString()).IsEqualTo("helloell");
    }

    [Test]
    public async Task StringBuilder_Append_StringBuilder_OutOfRange()
    {
        var value = new StringBuilder("world");

        await Assert.That(() => new StringBuilder().Append(value, 0, 6)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => new StringBuilder().Append(value, 3, 3)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => new StringBuilder().Append(value, 5, 1)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => new StringBuilder().Append(value, 6, 1)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => new StringBuilder().Append(value, int.MaxValue, 1)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => new StringBuilder().Append(value, 1, int.MaxValue)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task StringBuilder_Append_StringBuilder_Null()
    {
        var target = new StringBuilder("a");

        target.Append((StringBuilder?) null, 0, 0);

        await Assert.That(target.ToString()).IsEqualTo("a");
        await Assert.That(() => new StringBuilder().Append((StringBuilder?) null, 0, 1)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task StringBuilder_Append_StringBuilder_InvalidArgs()
    {
        var value = new StringBuilder("world");

        // negative startIndex/count throw even when count is 0
        await Assert.That(() => new StringBuilder().Append(value, -1, 0)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => new StringBuilder().Append(value, 0, -1)).Throws<ArgumentOutOfRangeException>();
        // count == 0 with a non-negative startIndex is a no-op (matches the BCL)
        await Assert.That(new StringBuilder().Append(value, 999, 0).ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendJoin()
    {
        var builder = new StringBuilder();

        string?[] span = ["value1", "value2"];
        builder.AppendJoin(",", span);
        await AssertAsync();
        builder.AppendJoin(",", new object[] {"value1", "value2"});
        await AssertAsync();
        builder.AppendJoin(',', span);
        await AssertAsync();
        builder.AppendJoin(',', new object[] {"value1", "value2"});
        await AssertAsync();
        builder.AppendJoin(",", new object[] {"value1", "value2"}.Select(_ => _));
        await AssertAsync();
        builder.AppendJoin(',', new object[] {"value1", "value2"}.Select(_ => _));
        await AssertAsync();
        builder.AppendJoin<string>(',', ["value1", "value2"]);
        await AssertAsync();

        // A non-array collection must bind to the IEnumerable<T> overload, not be
        // captured as a single element of a params array.
        List<string> list = ["value1", "value2"];
        builder.AppendJoin(',', list);
        await AssertAsync();
        builder.AppendJoin(",", list);
        await AssertAsync();

        async Task AssertAsync()
        {
            await Assert.That(builder.ToString()).IsEqualTo("value1,value2");
            builder.Clear();
        }
    }

    // The BCL has no AppendJoin overload taking a params array of an open generic type.
    // Adding one makes it beat AppendJoin<T>(separator, IEnumerable<T>) for any non-array
    // collection: the collection is captured as a single element, so AppendJoin(',', list)
    // silently appends "System.Collections.Generic.List`1[System.String]" instead of its
    // contents. It compiles, so only the output reveals it, and only on the frameworks
    // where the polyfill is active.
    [Test]
    public async Task AppendJoinHasNoGenericParamsOverload()
    {
        var offenders = typeof(Polyfills.Polyfill)
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            .Where(_ => _.Name == "AppendJoin")
            .Select(_ => _.GetParameters())
            .Where(_ => _.Length > 0 && IsGenericParamsArray(_[^1]))
            .ToList();

        await Assert.That(offenders).IsEmpty();

        static bool IsGenericParamsArray(ParameterInfo parameter)
        {
            if (parameter.ParameterType.GetElementType() is not {IsGenericParameter: true})
            {
                return false;
            }

            return parameter
                .GetCustomAttributes(inherit: false)
                .Any(_ => _.GetType().Name is
                    nameof(ParamArrayAttribute) or
                    "ParamCollectionAttribute");
        }
    }
}
