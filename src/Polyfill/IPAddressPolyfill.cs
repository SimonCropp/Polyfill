#if FeatureMemory

namespace Polyfills;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

static partial class Polyfill
{
    extension(IPAddress)
    {
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

        /// <summary>
        /// Converts an IP address represented as a character span to an <see cref="IPAddress"/> instance.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress.parse?view=net-11.0#system-net-ipaddress-parse(system-readonlyspan((system-char)))
        public static IPAddress Parse(ReadOnlySpan<char> ipString) =>
            IPAddress.Parse(ipString.ToString());

        /// <summary>
        /// Determines whether a span of characters is a valid IP address.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress.tryparse?view=net-11.0#system-net-ipaddress-tryparse(system-readonlyspan((system-char))-system-net-ipaddress@)
        public static bool TryParse(ReadOnlySpan<char> ipString, [NotNullWhen(true)] out IPAddress? address) =>
            IPAddress.TryParse(ipString.ToString(), out address);

#endif

#if !NET10_0_OR_GREATER

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress.tryparse?view=net-11.0#system-net-ipaddress-tryparse(system-readonlyspan((system-byte))-system-net-ipaddress@)
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, [NotNullWhen(true)] out IPAddress? result) =>
            IPAddress.TryParse(Encoding.UTF8.GetString(utf8Text), out result);

#endif
    }
}

#endif
