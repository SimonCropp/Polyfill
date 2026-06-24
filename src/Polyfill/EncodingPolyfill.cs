#if !NET5_0_OR_GREATER

namespace Polyfills;

using System.Text;

static partial class Polyfill
{
    static Encoding latin1 = Encoding.GetEncoding(28591);

    extension(Encoding)
    {
        /// <summary>
        /// Gets an encoding for the Latin1 character set (ISO-8859-1).
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.latin1?view=net-11.0
        public static Encoding Latin1 => latin1;
    }

#if FeatureMemory && !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
    extension(Encoding target)
    {
        /// <summary>
        /// When overridden in a derived class, returns a span containing the sequence of bytes that specifies the encoding used.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.preamble?view=net-11.0
        //Note: Allocates a new array on each access, unlike the BCL property which returns a cached span.
        public System.ReadOnlySpan<byte> Preamble => target.GetPreamble();
    }
#endif
}
#endif
