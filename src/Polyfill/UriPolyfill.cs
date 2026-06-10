#if !NET11_0_OR_GREATER

namespace Polyfills;

using System;

static partial class Polyfill
{
    extension(Uri)
    {
        /// <summary>
        /// Provides the scheme name for the <c>data</c> URI scheme (RFC 2397). This field is read-only.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.uri.urischemedata?view=net-11.0
        public static string UriSchemeData => "data";
    }
}

#endif
