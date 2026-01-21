namespace ConsumeEmbeddedLib;

using System.Diagnostics.CodeAnalysis;

internal class UsageOfConstantExpected
{
    // Only ensures that this does compile.
    public void M([ConstantExpected] int _)
    {
    }
}
