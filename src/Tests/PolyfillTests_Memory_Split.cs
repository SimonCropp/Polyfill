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

    public record struct CustomStruct(int value);

    public record CustomClass(int value);
}
#endif
