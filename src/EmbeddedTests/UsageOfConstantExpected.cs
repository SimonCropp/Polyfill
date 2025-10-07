namespace EmbeddedTests;

internal class UsageOfConstantExpected
{
    // Only ensures that this does compile.
    // 1. The typeof expression ensures that ConsumeEmbeddedLib is referenced and is exposing IVT to EmbeddedTests.
    // 2. ConsumeEmbeddedLib.UsageOfConstantExpected is expected to be using ConstantExpected from Polyfill.
    // 3. Because EmbeddedAttribute is used, there is no clash between EmbeddedAttribute from Polyfill in ConsumeEmbeddedLib and
    //    the one in System.Runtime when EmbeddedTests is compiled against .NET 7 or later.
    public void M([ConstantExpected] int x)
        => _ = typeof(ConsumeEmbeddedLib.UsageOfConstantExpected);
}
