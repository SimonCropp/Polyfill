#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER && FeatureMemory

namespace Polyfills;

using System;
using System.Text;

static partial class Polyfill
{
    /// <summary>
    /// Inserts the sequence of characters into this instance at the specified character position.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.insert?view=net-11.0#system-text-stringbuilder-insert(system-int32-system-readonlyspan((system-char)))
    public static StringBuilder Insert(this StringBuilder target, int index, ReadOnlySpan<char> value) =>
        target.Insert(index, value.ToString());
}
#endif
