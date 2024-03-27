partial class PolyfillTests
{
    [Test]
    public void ReadOnlySpan_ZeroLengthContains()
    {
        var span = new ReadOnlySpan<int>([]);

        var found = span.Contains(0);
        Assert.False(found);
    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3X || NET5_0_OR_GREATER
    [Test]
    public void CharSpan_Split()
    {
        var span = "Hello,World,This,Is,A,Test".AsSpan();

        Span<Range> ranges = stackalloc Range[10];

        int partsCount = span.Split(ranges, ',', StringSplitOptions.None);

        Assert.Equals(6, partsCount);
        Assert.Equals("Hello", ranges[0]);
    }
#endif

    [Test]
    public void ReadOnlySpan_TestContains()
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
                Assert.True(found);
            }
        }
    }

    [Test]
    public void ReadOnlySpan_TestMultipleContains()
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
            Assert.True(found);
        }
    }

    [Test]
    public void ReadOnlySpan_ZeroLengthContains_String()
    {
        var span = new ReadOnlySpan<string>([]);
        var found = span.Contains("a");
        Assert.False(found);
    }

    [Test]
    public void ReadOnlySpan_TestMatchContains_String()
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
                Assert.True(found);
            }
        }
    }

    [Test]
    public void ReadOnlySpan_TestNoMatchContains_String()
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
            Assert.False(found);
        }
    }

    [Test]
    public void ReadOnlySpan_TestMultipleMatchContains_String()
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
            Assert.True(found);
        }
    }

    [Test]
    public void Span_ZeroLengthContains()
    {
        var span = new Span<int>([]);

        var found = span.Contains(0);
        Assert.False(found);
    }

    [Test]
    public void Span_TestContains()
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
                Assert.True(found);
            }
        }
    }

    [Test]
    public void Span_TestMultipleContains()
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
            Assert.True(found);
        }
    }

    [Test]
    public void Span_ZeroLengthContains_String()
    {
        var span = new Span<string>([]);
        var found = span.Contains("a");
        Assert.False(found);
    }

    [Test]
    public void Span_TestMatchContains_String()
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
                Assert.True(found);
            }
        }
    }

    [Test]
    public void Span_TestNoMatchContains_String()
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
            Assert.False(found);
        }
    }

    [Test]
    public void Span_TestMultipleMatchContains_String()
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
            Assert.True(found);
        }
    }

    [Test]
    public void Span_SequenceEqual()
    {
        Assert.True("value".AsSpan().SequenceEqual("value"));
        Assert.False("value".AsSpan().SequenceEqual("value2"));
        Assert.False("value".AsSpan().SequenceEqual("v"));
        var span = new Span<char>("value".ToCharArray());
        Assert.True(span.SequenceEqual("value"));
        Assert.False(span.SequenceEqual("value2"));
        Assert.False(span.SequenceEqual("v"));
    }

    [Test]
    public void Span_StartsWith()
    {
        Assert.True("value".AsSpan().StartsWith("value"));
        Assert.False("value".AsSpan().StartsWith("value2"));
        Assert.True("value".AsSpan().StartsWith("v"));
        var span = new Span<char>("value".ToCharArray());
        Assert.True(span.StartsWith("value"));
        Assert.False(span.StartsWith("value2"));
        Assert.True(span.StartsWith("val"));
    }

    [Test]
    public void Span_EndsWith()
    {
        Assert.True("value".AsSpan().EndsWith("value"));
        Assert.False("value".AsSpan().EndsWith("value2"));
        Assert.True("value".AsSpan().EndsWith("e"));
        var span = new Span<char>("value".ToCharArray());
        Assert.True(span.EndsWith("value"));
        Assert.False(span.EndsWith("value2"));
        Assert.True(span.EndsWith("lue"));
    }

    [Test]
    public void SpanStringBuilderAppend()
    {
        var builder = new StringBuilder();
        builder.Append("value".AsSpan());
        Assert.AreEqual("value", builder.ToString());
    }

    [Test]
    public void StringEqualsSpan()
    {
        var builder = new StringBuilder("value");
        Assert.IsTrue(builder.Equals("value".AsSpan()));
    }
}
