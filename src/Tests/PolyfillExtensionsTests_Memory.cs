partial class PolyfillExtensionsTests
{
    [Test]
    public void ReadOnlySpan_ZeroLengthContains()
    {
        ReadOnlySpan<int> span = new ReadOnlySpan<int>(Array.Empty<int>());

        bool found = span.Contains(0);
        Assert.False(found);
    }

    [Test]
    public void ReadOnlySpan_TestContains()
    {
        for (int length = 0; length < 32; length++)
        {
            int[] a = new int[length];
            for (int i = 0; i < length; i++)
            {
                a[i] = 10 * (i + 1);
            }
            ReadOnlySpan<int> span = new ReadOnlySpan<int>(a);

            for (int targetIndex = 0; targetIndex < length; targetIndex++)
            {
                int target = a[targetIndex];
                bool found = span.Contains(target);
                Assert.True(found);
            }
        }
    }

    [Test]
    public void ReadOnlySpan_TestMultipleContains()
    {
        for (int length = 2; length < 32; length++)
        {
            int[] a = new int[length];
            for (int i = 0; i < length; i++)
            {
                a[i] = 10 * (i + 1);
            }

            a[length - 1] = 5555;
            a[length - 2] = 5555;

            ReadOnlySpan<int> span = new ReadOnlySpan<int>(a);
            bool found = span.Contains(5555);
            Assert.True(found);
        }
    }

    [Test]
    public void ReadOnlySpan_ZeroLengthContains_String()
    {
        ReadOnlySpan<string> span = new ReadOnlySpan<string>(Array.Empty<string>());
        bool found = span.Contains("a");
        Assert.False(found);
    }

    [Test]
    public void ReadOnlySpan_TestMatchContains_String()
    {
        for (int length = 0; length < 32; length++)
        {
            string[] a = new string[length];
            for (int i = 0; i < length; i++)
            {
                a[i] = (10 * (i + 1)).ToString();
            }
            ReadOnlySpan<string> span = new ReadOnlySpan<string>(a);

            for (int targetIndex = 0; targetIndex < length; targetIndex++)
            {
                string target = a[targetIndex];
                bool found = span.Contains(target);
                Assert.True(found);
            }
        }
    }

    [Test]
    public void ReadOnlySpan_TestNoMatchContains_String()
    {
        var rnd = new Random(42);
        for (int length = 0; length <= byte.MaxValue; length++)
        {
            string[] a = new string[length];
            string target = (rnd.Next(0, 256)).ToString();
            for (int i = 0; i < length; i++)
            {
                string val = (i + 1).ToString();
                a[i] = val == target ? (target + 1) : val;
            }
            ReadOnlySpan<string> span = new ReadOnlySpan<string>(a);

            bool found = span.Contains(target);
            Assert.False(found);
        }
    }

    [Test]
    public void ReadOnlySpan_TestMultipleMatchContains_String()
    {
        for (int length = 2; length < 32; length++)
        {
            string[] a = new string[length];
            for (int i = 0; i < length; i++)
            {
                a[i] = (10 * (i + 1)).ToString();
            }

            a[length - 1] = "5555";
            a[length - 2] = "5555";

            ReadOnlySpan<string> span = new ReadOnlySpan<string>(a);
            bool found = span.Contains("5555");
            Assert.True(found);
        }
    }

    [Test]
    public void Span_ZeroLengthContains()
    {
        Span<int> span = new Span<int>(Array.Empty<int>());

        bool found = span.Contains(0);
        Assert.False(found);
    }

    [Test]
    public void Span_TestContains()
    {
        for (int length = 0; length < 32; length++)
        {
            int[] a = new int[length];
            for (int i = 0; i < length; i++)
            {
                a[i] = 10 * (i + 1);
            }
            Span<int> span = new Span<int>(a);

            for (int targetIndex = 0; targetIndex < length; targetIndex++)
            {
                int target = a[targetIndex];
                bool found = span.Contains(target);
                Assert.True(found);
            }
        }
    }

    [Test]
    public void Span_TestMultipleContains()
    {
        for (int length = 2; length < 32; length++)
        {
            int[] a = new int[length];
            for (int i = 0; i < length; i++)
            {
                a[i] = 10 * (i + 1);
            }

            a[length - 1] = 5555;
            a[length - 2] = 5555;

            Span<int> span = new Span<int>(a);
            bool found = span.Contains(5555);
            Assert.True(found);
        }
    }

    [Test]
    public void Span_ZeroLengthContains_String()
    {
        Span<string> span = new Span<string>(Array.Empty<string>());
        bool found = span.Contains("a");
        Assert.False(found);
    }

    [Test]
    public void Span_TestMatchContains_String()
    {
        for (int length = 0; length < 32; length++)
        {
            string[] a = new string[length];
            for (int i = 0; i < length; i++)
            {
                a[i] = (10 * (i + 1)).ToString();
            }
            Span<string> span = new Span<string>(a);

            for (int targetIndex = 0; targetIndex < length; targetIndex++)
            {
                string target = a[targetIndex];
                bool found = span.Contains(target);
                Assert.True(found);
            }
        }
    }

    [Test]
    public void Span_TestNoMatchContains_String()
    {
        var rnd = new Random(42);
        for (int length = 0; length <= byte.MaxValue; length++)
        {
            string[] a = new string[length];
            string target = (rnd.Next(0, 256)).ToString();
            for (int i = 0; i < length; i++)
            {
                string val = (i + 1).ToString();
                a[i] = val == target ? (target + 1) : val;
            }
            Span<string> span = new Span<string>(a);

            bool found = span.Contains(target);
            Assert.False(found);
        }
    }

    [Test]
    public void Span_TestMultipleMatchContains_String()
    {
        for (int length = 2; length < 32; length++)
        {
            string[] a = new string[length];
            for (int i = 0; i < length; i++)
            {
                a[i] = (10 * (i + 1)).ToString();
            }

            a[length - 1] = "5555";
            a[length - 2] = "5555";

            Span<string> span = new Span<string>(a);
            bool found = span.Contains("5555");
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
