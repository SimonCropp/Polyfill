
#if FeatureCompression && (NETFRAMEWORK && !NET472_OR_GREATER || NETSTANDARD2_0)
namespace Polyfills;

using System.IO.Compression;
using System;

static partial class Polyfill
{
    extension(ZipArchiveEntry target)
    {
        /// <summary>
        /// OS and application specific file attributes.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchiveentry.externalattributes?view=net-11.0
        public int ExternalAttributes
        {
            get => 0;
            set {  }
        }
    }
}
#endif
