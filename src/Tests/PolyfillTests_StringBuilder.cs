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
