partial class PolyfillTests
{
    [Test]
    public async Task ReadOnlySpan_ZeroLengthContains()
    {
        var span = new ReadOnlySpan<int>([]);

        var found = span.Contains(0);
        await Assert.That(found).IsFalse();
    }

    [Test]
    public Task ReadOnlySpan_TestContains()
    {
        for (var length = 0; length < 32; length++)
        {
            var a = new int[length];
            for (var i = 0; i < length; i++)
            {
                a[i] = 10 * (i + 1);
            }
            var span = new ReadOnlySpan<int>(a);

            for (var targetIndex = 0; targetIndex < length; targetIndex++)
            {
                var target = a[targetIndex];
                var found = span.Contains(target);
                if (!found)
                    throw new Exception($"Expected to find {target} in span");
            }
        }
        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_TestMultipleContains()
    {
        for (var length = 2; length < 32; length++)
        {
            var a = new int[length];
            for (var i = 0; i < length; i++)
            {
                a[i] = 10 * (i + 1);
            }

            a[length - 1] = 5555;
            a[length - 2] = 5555;

            var span = new ReadOnlySpan<int>(a);
            var found = span.Contains(5555);
            if (!found)
                throw new Exception("Expected to find 5555 in span");
        }
        return Task.CompletedTask;
    }

    [Test]
    public async Task ReadOnlySpan_ZeroLengthContains_String()
    {
        var span = new ReadOnlySpan<string>([]);
        var found = span.Contains("a");
        await Assert.That(found).IsFalse();
    }

    [Test]
    public async Task ReadOnlySpan_EnumerateLines()
    {
        var list = new List<string>();
        var input = """
                    a
                    b
                    """;
        foreach (var line in input.AsSpan().EnumerateLines())
        {
            list.Add(line.ToString());
        }

        await Assert.That(list[0]).IsEqualTo("a");
        await Assert.That(list[1]).IsEqualTo("b");
    }

    [Test]
    public Task ReadOnlySpan_TestMatchContains_String()
    {
        for (var length = 0; length < 32; length++)
        {
            var a = new string[length];
            for (var i = 0; i < length; i++)
            {
                a[i] = (10 * (i + 1)).ToString();
            }

            var span = new ReadOnlySpan<string>(a);

            for (var targetIndex = 0; targetIndex < length; targetIndex++)
            {
                var target = a[targetIndex];
                var found = span.Contains(target);
                if (!found)
                    throw new Exception($"Expected to find {target} in span");
            }
        }
        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_TestNoMatchContains_String()
    {
        var rnd = new Random(42);
        for (var length = 0; length <= byte.MaxValue; length++)
        {
            var a = new string[length];
            var target = rnd.Next(0, 256).ToString();
            for (var i = 0; i < length; i++)
            {
                var val = (i + 1).ToString();
                a[i] = val == target ? target + 1 : val;
            }
            var span = new ReadOnlySpan<string>(a);

            var found = span.Contains(target);
            if (found)
                throw new Exception($"Expected not to find {target} in span");
        }
        return Task.CompletedTask;
    }

    [Test]
    public Task ReadOnlySpan_TestMultipleMatchContains_String()
    {
        for (var length = 2; length < 32; length++)
        {
            var a = new string[length];
            for (var i = 0; i < length; i++)
            {
                a[i] = (10 * (i + 1)).ToString();
            }

            a[length - 1] = "5555";
            a[length - 2] = "5555";

            var span = new ReadOnlySpan<string>(a);
            var found = span.Contains("5555");
            if (!found)
                throw new Exception("Expected to find '5555' in span");
        }
        return Task.CompletedTask;
    }

    [Test]
    public async Task Span_ZeroLengthContains()
    {
        var span = new Span<int>([]);

        var found = span.Contains(0);
        await Assert.That(found).IsFalse();
    }

    [Test]
    public Task Span_TestContains()
    {
        for (var length = 0; length < 32; length++)
        {
            var a = new int[length];
            for (var i = 0; i < length; i++)
            {
                a[i] = 10 * (i + 1);
            }
            var span = new Span<int>(a);

            for (var targetIndex = 0; targetIndex < length; targetIndex++)
            {
                var target = a[targetIndex];
                var found = span.Contains(target);
                if (!found)
                    throw new Exception($"Expected to find {target} in span");
            }
        }
        return Task.CompletedTask;
    }

    [Test]
    public Task Span_TestMultipleContains()
    {
        for (var length = 2; length < 32; length++)
        {
            var a = new int[length];
            for (var i = 0; i < length; i++)
            {
                a[i] = 10 * (i + 1);
            }

            a[length - 1] = 5555;
            a[length - 2] = 5555;

            var span = new Span<int>(a);
            var found = span.Contains(5555);
            if (!found)
                throw new Exception("Expected to find 5555 in span");
        }
        return Task.CompletedTask;
    }

    [Test]
    public async Task Span_ZeroLengthContains_String()
    {
        var span = new Span<string>([]);
        var found = span.Contains("a");
        await Assert.That(found).IsFalse();
    }

    [Test]
    public Task Span_TestMatchContains_String()
    {
        for (var length = 0; length < 32; length++)
        {
            var a = new string[length];
            for (var i = 0; i < length; i++)
            {
                a[i] = (10 * (i + 1)).ToString();
            }
            var span = new Span<string>(a);

            for (var targetIndex = 0; targetIndex < length; targetIndex++)
            {
                var target = a[targetIndex];
                var found = span.Contains(target);
                if (!found)
                    throw new Exception($"Expected to find {target} in span");
            }
        }
        return Task.CompletedTask;
    }

    [Test]
    public Task Span_TestNoMatchContains_String()
    {
        var rnd = new Random(42);
        for (var length = 0; length <= byte.MaxValue; length++)
        {
            var a = new string[length];
            var target = rnd.Next(0, 256).ToString();
            for (var i = 0; i < length; i++)
            {
                var val = (i + 1).ToString();
                a[i] = val == target ? target + 1 : val;
            }
            var span = new Span<string>(a);

            var found = span.Contains(target);
            if (found)
                throw new Exception($"Expected not to find {target} in span");
        }
        return Task.CompletedTask;
    }

    [Test]
    public Task Span_TestMultipleMatchContains_String()
    {
        for (var length = 2; length < 32; length++)
        {
            var a = new string[length];
            for (var i = 0; i < length; i++)
            {
                a[i] = (10 * (i + 1)).ToString();
            }

            a[length - 1] = "5555";
            a[length - 2] = "5555";

            var span = new Span<string>(a);
            var found = span.Contains("5555");
            if (!found)
                throw new Exception("Expected to find '5555' in span");
        }
        return Task.CompletedTask;
    }

    [Test]
    public Task Span_SequenceEqual()
    {
        if (!"value".AsSpan().SequenceEqual("value"))
            throw new Exception("Expected SequenceEqual to be true");
        if ("value".AsSpan().SequenceEqual("value2"))
            throw new Exception("Expected SequenceEqual to be false");
        if ("value".AsSpan().SequenceEqual("v"))
            throw new Exception("Expected SequenceEqual to be false");
        var span = new Span<char>("value".ToCharArray());
        if (!span.SequenceEqual("value"))
            throw new Exception("Expected SequenceEqual to be true");
        if (span.SequenceEqual("value2"))
            throw new Exception("Expected SequenceEqual to be false");
        if (span.SequenceEqual("v"))
            throw new Exception("Expected SequenceEqual to be false");
        return Task.CompletedTask;
    }

    [Test]
    public Task Span_StartsWith()
    {
        if (!"value".AsSpan().StartsWith("value"))
            throw new Exception("Expected StartsWith to be true");
        if ("value".AsSpan().StartsWith("value2"))
            throw new Exception("Expected StartsWith to be false");
        if (!"value".AsSpan().StartsWith("v"))
            throw new Exception("Expected StartsWith to be true");
        if (!"value".AsSpan().StartsWith('v'))
            throw new Exception("Expected StartsWith char to be true");
        var span = new Span<char>("value".ToCharArray());
        if (!span.StartsWith("value"))
            throw new Exception("Expected StartsWith to be true");
        if (!span.StartsWith('v'))
            throw new Exception("Expected StartsWith char to be true");
        if (span.StartsWith('a'))
            throw new Exception("Expected StartsWith char to be false");
        if (span.StartsWith("value2"))
            throw new Exception("Expected StartsWith to be false");
        if (!span.StartsWith("val"))
            throw new Exception("Expected StartsWith to be true");
        return Task.CompletedTask;
    }

    [Test]
    public Task Span_EndsWith()
    {
        if (!"value".AsSpan().EndsWith("value"))
            throw new Exception("Expected EndsWith to be true");
        if ("value".AsSpan().EndsWith("value2"))
            throw new Exception("Expected EndsWith to be false");
        if (!"value".AsSpan().EndsWith("e"))
            throw new Exception("Expected EndsWith to be true");
        if (!"value".AsSpan().EndsWith('e'))
            throw new Exception("Expected EndsWith char to be true");
        var span = new Span<char>("value".ToCharArray());
        if (!span.EndsWith("value"))
            throw new Exception("Expected EndsWith to be true");
        if (span.EndsWith("value2"))
            throw new Exception("Expected EndsWith to be false");
        if (!span.EndsWith("lue"))
            throw new Exception("Expected EndsWith to be true");
        if (!span.EndsWith('e'))
            throw new Exception("Expected EndsWith char to be true");
        if (span.EndsWith('w'))
            throw new Exception("Expected EndsWith char to be false");
        return Task.CompletedTask;
    }

    [Test]
    public async Task SpanStringBuilderAppend()
    {
        var builder = new StringBuilder();
        builder.Append("value".AsSpan());
        await Assert.That(builder.ToString()).IsEqualTo("value");
    }

    [Test]
    public async Task StringEqualsSpan()
    {
        var builder = new StringBuilder("value");
        await Assert.That(builder.Equals("value".AsSpan())).IsTrue();
    }
}
