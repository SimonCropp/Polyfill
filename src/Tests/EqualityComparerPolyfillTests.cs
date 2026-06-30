using System;
using System.Collections.Generic;

public class EqualityComparerPolyfillTests
{
    [Test]
    public async Task Create_NullDelegates_Throw()
    {
        await Assert.That(() => EqualityComparer<int>.Create(null!)).Throws<ArgumentNullException>();

        // The keySelector overload's delegate signature differs in nullability between the
        // polyfill (Func<T?, TKey?>) and the .NET 11 BCL (Func<T?, TKey>); adapt accordingly.
#if NET11_0_OR_GREATER
        Func<string?, int> keySelector = null!;
#else
        Func<string?, int?> keySelector = null!;
#endif
        await Assert.That(() => EqualityComparer<string>.Create(keySelector)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithEqualsAndGetHashCode()
    {
        var comparer = EqualityComparer<int>.Create(
            (x, y) => Math.Abs(x) == Math.Abs(y),
            x => Math.Abs(x));

        await Assert.That(comparer.Equals(5, 5)).IsTrue();
        await Assert.That(comparer.Equals(5, -5)).IsTrue();
        await Assert.That(comparer.Equals(5, 4)).IsFalse();
        await Assert.That(comparer.GetHashCode(5)).IsEqualTo(comparer.GetHashCode(-5));
    }

    [Test]
    public async Task Create_WithEqualsOnly()
    {
        var comparer = EqualityComparer<int>.Create(
            (x, y) => Math.Abs(x) == Math.Abs(y));

        await Assert.That(comparer.Equals(5, -5)).IsTrue();
        await Assert.That(comparer.Equals(5, 4)).IsFalse();
        await Assert.That(() => comparer.GetHashCode(5)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task Create_WithKeySelector()
    {
        var comparer = EqualityComparer<string>.Create(value => value!.Length);

        await Assert.That(comparer.Equals("abc", "xyz")).IsTrue();
        await Assert.That(comparer.Equals("abc", "ab")).IsFalse();
        await Assert.That(comparer.GetHashCode("abc")).IsEqualTo(comparer.GetHashCode("xyz"));
    }

    [Test]
    public async Task Create_WithKeySelectorAndComparer()
    {
        var comparer = EqualityComparer<string>.Create(value => value, StringComparer.OrdinalIgnoreCase);

        await Assert.That(comparer.Equals("ABC", "abc")).IsTrue();
        await Assert.That(comparer.Equals("ABC", "xyz")).IsFalse();
    }
}
