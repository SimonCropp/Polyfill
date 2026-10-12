// ReSharper disable RedundantCast
// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

#if FeatureMemory

#if NET9_0_OR_GREATER
using MemoryExtensions = System.MemoryExtensions;
#else
using MemoryExtensions = Polyfills.Polyfill;
#endif

#if NET8_0
using System.Buffers;
#endif

partial class PolyfillTests
{
    [Test]
    public Task DefaultSpanSplitEnumeratorBehaviour()
    {
        var charSpanEnumerator = new MemoryExtensions.SpanSplitEnumerator<char>();
        var stringSpanEnumerator = new MemoryExtensions.SpanSplitEnumerator<string>();
        AssertRangeEquals(charSpanEnumerator.Current!, new Range(0, 0));
        if (charSpanEnumerator.MoveNext())
        {
            throw new Exception("Expected MoveNext to return false");
        }

        // Implicit DoesNotThrow assertion
        // ReSharper disable once GenericEnumeratorNotDisposed
        charSpanEnumerator.GetEnumerator();

        AssertRangeEquals(stringSpanEnumerator.Current!, new Range(0, 0));
        if (stringSpanEnumerator.MoveNext())
        {
            throw new Exception("Expected MoveNext to return false");
        }
        // ReSharper disable once GenericEnumeratorNotDisposed
        stringSpanEnumerator.GetEnumerator();
        return Task.CompletedTask;
    }

    static void AssertRangeEquals(Range actual, Range expected)
    {
        if (actual.Start.Value != expected.Start.Value ||
            actual.End.Value != expected.End.Value)
        {
            throw new Exception($"Expected Range({expected.Start}..{expected.End}) but got Range({actual.Start}..{actual.End})");
        }
    }

    [Test]
    public Task Split_SingleElementSeparator()
    {
        // Split on default
        Test((char[]) ['a', ' ', 'b'], default, (Range[]) [0..3]);
        Test((int[]) [1, 2, 3], default, (Range[]) [0..3]);
        Test((long[]) [1, 2, 3], default, (Range[]) [0..3]);
        Test((byte[]) [1, 2, 3], default, (Range[]) [0..3]);
        Test((CustomStruct[]) [new(1), new(2), new(3)], default, (Range[]) [0..3]);
        Test((CustomClass[]) [new(1), new(2), new(3)], default, (Range[]) [0..3]);

        // Split no matching element
        Test((char[]) ['a', ' ', 'b'], ',', (Range[]) [0..3]);
        Test((int[]) [1, 2, 3], (int) 4, (Range[]) [0..3]);
        Test((long[]) [1, 2, 3], (long) 4, (Range[]) [0..3]);
        Test((byte[]) [1, 2, 3], (byte) 4, (Range[]) [0..3]);
        Test((CustomStruct[]) [new(1), new(2), new(3)], new CustomStruct(4), (Range[]) [0..3]);
        Test((CustomClass[]) [new(1), new(2), new(3)], new CustomClass(4), (Range[]) [0..3]);

        // Split on sequence containing only a separator
        Test((char[]) [','], ',', (Range[]) [0..0, 1..1]);
        Test((int[]) [1], (int) 1, (Range[]) [0..0, 1..1]);
        Test((long[]) [1], (long) 1, (Range[]) [0..0, 1..1]);
        Test((byte[]) [1], (byte) 1, (Range[]) [0..0, 1..1]);
        Test((CustomStruct[]) [new(1)], new CustomStruct(1), (Range[]) [0..0, 1..1]);
        Test((CustomClass[]) [new(1)], new CustomClass(1), (Range[]) [0..0, 1..1]);

        // Split on empty sequence with default separator
        Test((char[]) [], default, (Range[]) [0..0]);
        Test((int[]) [], default, (Range[]) [0..0]);
        Test((long[]) [], default, (Range[]) [0..0]);
        Test((byte[]) [], default, (Range[]) [0..0]);
        Test((CustomStruct[]) [], default, (Range[]) [0..0]);
        Test((CustomClass[]) [], default, (Range[]) [0..0]);

        Test((char[]) ['a', ',', 'b'], ',', (Range[]) [0..1, 2..3]);
        Test((int[]) [1, 2, 3], (int) 2, (Range[]) [0..1, 2..3]);
        Test((long[]) [1, 2, 3], (long) 2, (Range[]) [0..1, 2..3]);
        Test((byte[]) [1, 2, 3], (byte) 2, (Range[]) [0..1, 2..3]);
        Test((CustomStruct[]) [new(1), new(2), new(3)], new CustomStruct(2), (Range[]) [0..1, 2..3]);
        Test((CustomClass[]) [new(1), new(2), new(3)], new CustomClass(2), (Range[]) [0..1, 2..3]);

        Test((char[]) ['a', 'b', ',', ','], ',', (Range[]) [0..2, 3..3, 4..4]);
        Test((int[]) [1, 3, 2, 2], (int) 2, (Range[]) [0..2, 3..3, 4..4]);
        Test((long[]) [1, 3, 2, 2], (long) 2, (Range[]) [0..2, 3..3, 4..4]);
        Test((byte[]) [1, 3, 2, 2], (byte) 2, (Range[]) [0..2, 3..3, 4..4]);
        Test((CustomStruct[]) [new(1), new(3), new(2), new(2)], new CustomStruct(2), (Range[]) [0..2, 3..3, 4..4]);
        Test((CustomClass[]) [new(1), new(3), new(2), new(2)], new CustomClass(2), (Range[]) [0..2, 3..3, 4..4]);

        return Task.CompletedTask;

        static void Test<T>(T[] value, T separator, Range[] result)
            where T : IEquatable<T> =>
            AssertEnsureCorrectEnumeration(new ReadOnlySpan<T>(value).Split(separator), result);
    }

    [Test]
    public Task Split_SequenceSeparator()
    {
        // Split no separators
        Test((char[]) ['a', ' ', 'b'], (char[]) [], (Range[]) [0..3]);
        Test((int[]) [1, 2, 3], (int[]) [], (Range[]) [0..3]);
        Test((long[]) [1, 2, 3], (long[]) [], (Range[]) [0..3]);
        Test((byte[]) [1, 2, 3], (byte[]) [], (Range[]) [0..3]);
        Test((CustomStruct[]) [new(1), new(2), new(3)], (CustomStruct[]) [], (Range[]) [0..3]);
        Test((CustomClass[]) [new(1), new(2), new(3)], (CustomClass[]) [], (Range[]) [0..3]);

        // Split no matching elements
        Test((char[]) ['a', ' ', 'b'], (char[]) [',', '.'], (Range[]) [0..3]);
        Test((int[]) [1, 2, 3], (int[]) [4, 3], (Range[]) [0..3]);
        Test((long[]) [1, 2, 3], (long[]) [4, 3], (Range[]) [0..3]);
        Test((byte[]) [1, 2, 3], (byte[]) [4, 3], (Range[]) [0..3]);
        Test((CustomStruct[]) [new(1), new(2), new(3)], (CustomStruct[]) [new(4), new(3)], (Range[]) [0..3]);
        Test((CustomClass[]) [new(1), new(2), new(3)], (CustomClass[]) [new(4), new(3)], (Range[]) [0..3]);

        // Split on input span with only a single sequence separator
        Test((char[]) [',', '.'], (char[]) [',', '.'], (Range[]) [0..0, 2..2]);
        Test((int[]) [4, 3], (int[]) [4, 3], (Range[]) [0..0, 2..2]);
        Test((long[]) [4, 3], (long[]) [4, 3], (Range[]) [0..0, 2..2]);
        Test((byte[]) [4, 3], (byte[]) [4, 3], (Range[]) [0..0, 2..2]);
        Test((CustomStruct[]) [new(4), new(3)], (CustomStruct[]) [new(4), new(3)], (Range[]) [0..0, 2..2]);
        Test((CustomClass[]) [new(4), new(3)], (CustomClass[]) [new(4), new(3)], (Range[]) [0..0, 2..2]);

        // Split on empty sequence with default separator
        Test((char[]) [], (char[]) [default], (Range[]) [0..0]);
        Test((int[]) [], (int[]) [default], (Range[]) [0..0]);
        Test((long[]) [], (long[]) [default], (Range[]) [0..0]);
        Test((byte[]) [], (byte[]) [default], (Range[]) [0..0]);
        Test((CustomStruct[]) [], (CustomStruct[]) [default], (Range[]) [0..0]);
        Test((CustomClass[]) [], (CustomClass[]) [default], (Range[]) [0..0]);

        Test((char[]) ['a', ',', '-', 'b'], (char[]) [',', '-'], (Range[]) [0..1, 3..4]);
        Test((int[]) [1, 2, 4, 3], (int[]) [2, 4], (Range[]) [0..1, 3..4]);
        Test((long[]) [1, 2, 4, 3], (long[]) [2, 4], (Range[]) [0..1, 3..4]);
        Test((byte[]) [1, 2, 4, 3], (byte[]) [2, 4], (Range[]) [0..1, 3..4]);
        Test((CustomStruct[]) [new(1), new(2), new(4), new(3)], (CustomStruct[]) [new(2), new(4)], (Range[]) [0..1, 3..4]);
        Test((CustomClass[]) [new(1), new(2), new(4), new(3)], (CustomClass[]) [new(2), new(4)], (Range[]) [0..1, 3..4]);

        Test((char[]) [',', '-', 'a', ',', '-', 'b'], (char[]) [',', '-'], (Range[]) [0..0, 2..3, 5..6]);
        Test((int[]) [2, 4, 3, 2, 4, 5], (int[]) [2, 4], (Range[]) [0..0, 2..3, 5..6]);
        Test((long[]) [2, 4, 3, 2, 4, 5], (long[]) [2, 4], (Range[]) [0..0, 2..3, 5..6]);
        Test((byte[]) [2, 4, 3, 2, 4, 5], (byte[]) [2, 4], (Range[]) [0..0, 2..3, 5..6]);
        Test((CustomStruct[]) [new(2), new(4), new(3), new(2), new(4), new(5)], (CustomStruct[]) [new(2), new(4)], (Range[]) [0..0, 2..3, 5..6]);
        Test((CustomClass[]) [new(2), new(4), new(3), new(2), new(4), new(5)], (CustomClass[]) [new(2), new(4)], (Range[]) [0..0, 2..3, 5..6]);

        return Task.CompletedTask;

        static void Test<T>(T[] value, T[] separator, Range[] result)
            where T : IEquatable<T> =>
            AssertEnsureCorrectEnumeration(new ReadOnlySpan<T>(value).Split(separator), result);
    }

    [Test]
    public Task SplitAnySeparatorData()
    {
        // Split no separators
        Test((char[]) ['a', ' ', 'b'], (char[]) [], (Range[]) [0..1, 2..3]); // an empty span of separators for char is handled as all whitespace being separators
        Test((int[]) [1, 2, 3], (int[]) [], (Range[]) [0..3]);
        Test((long[]) [1, 2, 3], (long[]) [], (Range[]) [0..3]);
        Test((byte[]) [1, 2, 3], (byte[]) [], (Range[]) [0..3]);
        Test((CustomStruct[]) [new(1), new(2), new(3)], (CustomStruct[]) [], (Range[]) [0..3]);
        Test((CustomClass[]) [new(1), new(2), new(3)], (CustomClass[]) [], (Range[]) [0..3]);

        // Split non-matching separators
        Test((char[]) ['a', ' ', 'b'], (char[]) [',', '.'], (Range[]) [0..3]);
        Test((int[]) [1, 2, 3], (int[]) [4, 5], (Range[]) [0..3]);
        Test((long[]) [1, 2, 3], (long[]) [4, 5], (Range[]) [0..3]);
        Test((byte[]) [1, 2, 3], (byte[]) [4, 5], (Range[]) [0..3]);
        Test((CustomStruct[]) [new(1), new(2), new(3)], (CustomStruct[]) [new(4), new(5)], (Range[]) [0..3]);
        Test((CustomClass[]) [new(1), new(2), new(3)], (CustomClass[]) [new(4), new(5)], (Range[]) [0..3]);

        // Split on sequence containing only a separator
        Test((char[]) [','], (char[]) [','], (Range[]) [0..0, 1..1]);
        Test((int[]) [1], (int[]) [1], (Range[]) [0..0, 1..1]);
        Test((long[]) [1], (long[]) [1], (Range[]) [0..0, 1..1]);
        Test((byte[]) [1], (byte[]) [1], (Range[]) [0..0, 1..1]);
        Test((CustomStruct[]) [new(1)], (CustomStruct[]) [new(1)], (Range[]) [0..0, 1..1]);
        Test((CustomClass[]) [new(1)], (CustomClass[]) [new(1)], (Range[]) [0..0, 1..1]);

        // Split on empty sequence with default separator
        Test((char[]) [], (char[]) [default], (Range[]) [0..0]);
        Test((int[]) [], (int[]) [default], (Range[]) [0..0]);
        Test((long[]) [], (long[]) [default], (Range[]) [0..0]);
        Test((byte[]) [], (byte[]) [default], (Range[]) [0..0]);
        Test((CustomStruct[]) [], (CustomStruct[]) [new(default)], (Range[]) [0..0]);
        Test((CustomClass[]) [], (CustomClass[]) [new(default)], (Range[]) [0..0]);

        Test((char[]) ['a', ',', '-', 'b'], (char[]) [',', '-'], (Range[]) [0..1, 2..2, 3..4]);
        Test((int[]) [1, 2, 4, 3], (int[]) [2, 4], (Range[]) [0..1, 2..2, 3..4]);
        Test((long[]) [1, 2, 4, 3], (long[]) [2, 4], (Range[]) [0..1, 2..2, 3..4]);
        Test((byte[]) [1, 2, 4, 3], (byte[]) [2, 4], (Range[]) [0..1, 2..2, 3..4]);
        Test((CustomStruct[]) [new(1), new(2), new(4), new(3)], (CustomStruct[]) [new(2), new(4)], (Range[]) [0..1, 2..2, 3..4]);
        Test((CustomClass[]) [new(1), new(2), new(4), new(3)], (CustomClass[]) [new(2), new(4)], (Range[]) [0..1, 2..2, 3..4]);

        Test((char[]) [',', '-', 'a', ',', '-', 'b'], (char[]) [',', '-'], (Range[]) [0..0, 1..1, 2..3, 4..4, 5..6]);
        Test((int[]) [2, 4, 3, 2, 4, 5], (int[]) [2, 4], (Range[]) [0..0, 1..1, 2..3, 4..4, 5..6]);
        Test((long[]) [2, 4, 3, 2, 4, 5], (long[]) [2, 4], (Range[]) [0..0, 1..1, 2..3, 4..4, 5..6]);
        Test((byte[]) [2, 4, 3, 2, 4, 5], (byte[]) [2, 4], (Range[]) [0..0, 1..1, 2..3, 4..4, 5..6]);
        Test((CustomStruct[]) [new(2), new(4), new(3), new(2), new(4), new(5)], (CustomStruct[]) [new(2), new(4)], (Range[]) [0..0, 1..1, 2..3, 4..4, 5..6]);
        Test((CustomClass[]) [new(2), new(4), new(3), new(2), new(4), new(5)], (CustomClass[]) [new(2), new(4)], (Range[]) [0..0, 1..1, 2..3, 4..4, 5..6]);

        return Task.CompletedTask;

        static void Test<T>(T[] value, T[] separator, Range[] result)
            where T : IEquatable<T>
        {
            var span = new ReadOnlySpan<T>(value);
            AssertEnsureCorrectEnumeration(span.SplitAny(separator), result);

            if (value is not char[] source ||
                // the SearchValues overload does not special-case empty
                separator is not char[] {Length: > 0} separators)
            {
                return;
            }

            var readOnlySpan = new ReadOnlySpan<char>(source);
#if NET8_0
            var searchValuesBasedEnumerator = readOnlySpan.SplitAny(SearchValues.Create(separators));
            AssertEnsureCorrectEnumeration(searchValuesBasedEnumerator, result);
#endif
            var spanBasedEnumerator = readOnlySpan.SplitAny(separators);
            AssertEnsureCorrectEnumeration(spanBasedEnumerator, result);
        }
    }

    static void AssertEnsureCorrectEnumeration<T>(MemoryExtensions.SpanSplitEnumerator<T> enumerator, Range[] result)
        where T : IEquatable<T>
    {
        var current = enumerator.Current;
        if (current.Start.Value != 0 || current.End.Value != 0)
            throw new Exception($"Expected Range(0,0) but got Range({current.Start}..{current.End})");

        for (var i = 0; i < result.Length; i++)
        {
            if (!enumerator.MoveNext())
                throw new Exception($"Expected MoveNext to return true at index {i}");
            var actual = enumerator.Current;
            var expected = result[i];
            if (actual.Start.Value != expected.Start.Value || actual.End.Value != expected.End.Value)
                throw new Exception($"Expected Range({expected.Start}..{expected.End}) but got Range({actual.Start}..{actual.End}) at index {i}");
        }

        if (enumerator.MoveNext())
            throw new Exception("Expected MoveNext to return false after all elements");
    }

    static List<Range> ExpectedSplitAny<T>(T[] value, T[] separators)
    {
        var comparer = EqualityComparer<T>.Default;
        var expected = new List<Range>();
        var start = 0;
        for (var index = 0; index < value.Length; index++)
        {
            foreach (var separator in separators)
            {
                if (comparer.Equals(value[index], separator))
                {
                    expected.Add(new(start, index));
                    start = index + 1;
                    break;
                }
            }
        }

        expected.Add(new(start, value.Length));
        return expected;
    }

    static void AssertSplitAny<T>(T[] value, T[] separators)
        where T : IEquatable<T> =>
        AssertEnsureCorrectEnumeration(
            new ReadOnlySpan<T>(value).SplitAny(separators),
            ExpectedSplitAny(value, separators).ToArray());

    [Test]
    public Task SplitAny_SeparatorCounts()
    {
        string[] inputs =
        [
            "",
            "a",
            ",",
            "/",
            "abc",
            "a,b;c|d-e+f/g",
            "a/b/c/d",
            "/a/b/",
            ",;|-+/",
            "//,,;;",
            "a,,b;;c//d",
            "no separators here",
            "trailing/",
            ",leading",
        ];
        const string allSeparators = ",;|-+/";

        // 1 to 3 separators take the fixed arity path below net7, 4 or more are matched an element at a time
        for (var count = 1; count <= allSeparators.Length; count++)
        {
            var charSeparators = allSeparators.Substring(0, count).ToCharArray();
            var lastCharSeparators = allSeparators.Substring(allSeparators.Length - count).ToCharArray();
            foreach (var input in inputs)
            {
                var chars = input.ToCharArray();
                AssertSplitAny(chars, charSeparators);
                AssertSplitAny(chars, lastCharSeparators);

                AssertSplitAny(Array.ConvertAll(chars, _ => (int) _), Array.ConvertAll(charSeparators, _ => (int) _));
                AssertSplitAny(Array.ConvertAll(chars, _ => (int) _), Array.ConvertAll(lastCharSeparators, _ => (int) _));

                AssertSplitAny(Array.ConvertAll(chars, _ => (byte) _), Array.ConvertAll(charSeparators, _ => (byte) _));

                AssertSplitAny(Array.ConvertAll(chars, _ => _.ToString()), Array.ConvertAll(charSeparators, _ => _.ToString()));
                AssertSplitAny(Array.ConvertAll(chars, _ => _.ToString()), Array.ConvertAll(lastCharSeparators, _ => _.ToString()));

                AssertSplitAny(Array.ConvertAll(chars, _ => new CustomStruct(_)), Array.ConvertAll(charSeparators, _ => new CustomStruct(_)));
                AssertSplitAny(Array.ConvertAll(chars, _ => new CustomClass(_)), Array.ConvertAll(lastCharSeparators, _ => new CustomClass(_)));
            }
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task SplitAny_DuplicateSeparators()
    {
        AssertSplitAny("a,b;c".ToCharArray(), (char[]) [',', ',']);
        AssertSplitAny("a,b;c".ToCharArray(), (char[]) [',', ';', ',']);
        AssertSplitAny("a,b;c".ToCharArray(), (char[]) [',', ';', ',', ';', ',']);
        return Task.CompletedTask;
    }

    [Test]
    public Task SplitAny_NullElements()
    {
        AssertSplitAny((string[]) ["a", null, "b"], (string[]) [null, "x"]);
        AssertSplitAny((string[]) ["a", null, "b"], (string[]) ["x", "y", "z", null]);
        AssertSplitAny((string[]) ["a", null, "b"], (string[]) ["b"]);
        AssertSplitAny((string[]) ["a", null, "b"], (string[]) ["x", "y", "z", "a"]);
        return Task.CompletedTask;
    }

    [Test]
    public Task SplitAny_ManyWords()
    {
        var builder = new StringBuilder();
        for (var index = 0; index < 4000; index++)
        {
            builder.Append("word").Append(index).Append(' ');
        }

        var chars = builder.ToString().ToCharArray();
        AssertSplitAny(chars, (char[]) ['\t', ' ']);
        AssertSplitAny(chars, (char[]) ['\t', ';', ',', '|', ' ']);
        return Task.CompletedTask;
    }

    [Test]
    public async Task SplitAny_NoSeparators_SplitsCharsOnWhiteSpace()
    {
        var splitting = new List<int>();
        for (var value = 0; value <= char.MaxValue; value++)
        {
            var input = "a" + (char) value + "b";
            var count = 0;
            foreach (var range in input.AsSpan().SplitAny(ReadOnlySpan<char>.Empty))
            {
                count++;
            }

            if (count != 1)
            {
                splitting.Add(value);
            }
        }

        int[] expected =
        [
            0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x20, 0x85, 0xA0, 0x1680,
            0x2000, 0x2001, 0x2002, 0x2003, 0x2004, 0x2005, 0x2006, 0x2007, 0x2008, 0x2009, 0x200A,
            0x2028, 0x2029, 0x202F, 0x205F, 0x3000
        ];
        await Assert.That(string.Join(",", splitting)).IsEqualTo(string.Join(",", expected));
    }

    [Test]
    public Task SplitAny_NoSeparators_WhiteSpaceRanges()
    {
        var chars = " a\tb \r\nc ".ToCharArray();
        AssertEnsureCorrectEnumeration(
            new ReadOnlySpan<char>(chars).SplitAny(ReadOnlySpan<char>.Empty),
            [0..0, 1..2, 3..4, 5..5, 6..6, 7..8, 9..9]);
        AssertEnsureCorrectEnumeration(
            new ReadOnlySpan<char>([]).SplitAny(ReadOnlySpan<char>.Empty),
            [0..0]);
        AssertEnsureCorrectEnumeration(
            new ReadOnlySpan<char>("abc".ToCharArray()).SplitAny(ReadOnlySpan<char>.Empty),
            [0..3]);
        return Task.CompletedTask;
    }

    public record struct CustomStruct(int value);

    public record CustomClass(int value);
}
#endif
