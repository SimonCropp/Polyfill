#if FeatureMemory && NET6_0_OR_GREATER && !NET10_0_OR_GREATER
namespace Polyfills;

using System.Runtime.CompilerServices;

static partial class Polyfill
{
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.defaultinterpolatedstringhandler.clear?view=net-11.0
    public static void Clear(this DefaultInterpolatedStringHandler target) =>
        target.ToStringAndClear();
}
#endif
