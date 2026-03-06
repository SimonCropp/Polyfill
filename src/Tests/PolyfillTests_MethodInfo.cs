using System.Reflection;

partial class PolyfillTests
{
    [Test]
    public async Task MethodInfo_CreateDelegate_Generic()
    {
        var method = typeof(PolyfillTests).GetMethod(nameof(SampleStaticMethod), BindingFlags.NonPublic | BindingFlags.Static)!;
        var del = method.CreateDelegate<Func<int, int>>();
        await Assert.That(del(5)).IsEqualTo(10);
    }

    [Test]
    public async Task MethodInfo_CreateDelegate_Generic_WithTarget()
    {
        var method = typeof(PolyfillTests).GetMethod(nameof(SampleInstanceMethod), BindingFlags.NonPublic | BindingFlags.Instance)!;
        var target = new PolyfillTests();
        var del = method.CreateDelegate<Func<int, int>>(target);
        await Assert.That(del(3)).IsEqualTo(6);
    }

    static int SampleStaticMethod(int x) => x * 2;
    int SampleInstanceMethod(int x) => x * 2;
}
