#if !NET5_0_OR_GREATER

namespace Polyfills;

using System.Text;

static partial class Polyfill
{
    extension(Encoding)
    {
        /// <summary>
        /// Gets an encoding for the Latin1 character set (ISO-8859-1).
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.latin1?view=net-11.0
        public static Encoding Latin1 => Encoding.GetEncoding(28591);
    }
}
#endif
