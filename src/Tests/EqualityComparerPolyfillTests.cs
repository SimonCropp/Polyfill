using System;
using System.Collections.Generic;

public class EqualityComparerPolyfillTests
{
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
}
