using System.Diagnostics.CodeAnalysis;

namespace ConsumeEmbeddedLib;

internal class UsageOfConstantExpected
{
    // Only ensures that this does compile.
    public void M([ConstantExpected] int _)
    {

    }
}
