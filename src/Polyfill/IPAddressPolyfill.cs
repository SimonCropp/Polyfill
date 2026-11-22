#if FeatureMemory

namespace Polyfills;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

// A class of its own because IPAddress.Parse(ReadOnlySpan<byte>) would otherwise share a signature with
// Guid.Parse(ReadOnlySpan<byte>) in Polyfill, and two static extension members with one signature cannot
// coexist on a class.
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static class IPAddressPolyfill
{
    extension(IPAddress)
    {
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

        /// <summary>
        /// Converts an IP address represented as a character span to an <see cref="IPAddress"/> instance.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress.parse?view=net-11.0#system-net-ipaddress-parse(system-readonlyspan((system-char)))
        //Note: Copies the span to a string first, so this allocates where the BCL works straight from the span.
        public static IPAddress Parse(ReadOnlySpan<char> ipString) =>
            IPAddress.Parse(ipString.ToString());

        /// <summary>
        /// Determines whether a span of characters is a valid IP address.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress.tryparse?view=net-11.0#system-net-ipaddress-tryparse(system-readonlyspan((system-char))-system-net-ipaddress@)
        //Note: Copies the span to a string first, so this allocates where the BCL works straight from the span.
        public static bool TryParse(ReadOnlySpan<char> ipString, [NotNullWhen(true)] out IPAddress? address) =>
            IPAddress.TryParse(ipString.ToString(), out address);

#endif

#if !NET10_0_OR_GREATER

        /// <summary>
        /// Parses a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress.parse?view=net-11.0#system-net-ipaddress-parse(system-readonlyspan((system-byte)))
        //Note: Decodes the bytes to a string first, so this allocates where the BCL works straight from the UTF-8.
        public static IPAddress Parse(ReadOnlySpan<byte> utf8Text) =>
            IPAddress.Parse(Encoding.UTF8.GetString(utf8Text));

        /// <summary>
        /// Tries to parse a span of UTF-8 characters into a value.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress.tryparse?view=net-11.0#system-net-ipaddress-tryparse(system-readonlyspan((system-byte))-system-net-ipaddress@)
        //Note: Decodes the bytes to a string first, so this allocates where the BCL works straight from the UTF-8.
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, [NotNullWhen(true)] out IPAddress? result) =>
            IPAddress.TryParse(Encoding.UTF8.GetString(utf8Text), out result);

        /// <summary>
        /// Determines whether the specified character span represents a valid IP address.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress.isvalid?view=net-11.0
        //Note: Copies the span to a string first, so this allocates where the BCL works straight from the span.
        public static bool IsValid(ReadOnlySpan<char> ipSpan) =>
            IPAddress.TryParse(ipSpan.ToString(), out _);

        /// <summary>
        /// Determines whether the specified UTF-8 span represents a valid IP address.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress.isvalidutf8?view=net-11.0
        //Note: Decodes the bytes to a string first, so this allocates where the BCL works straight from the UTF-8.
        public static bool IsValidUtf8(ReadOnlySpan<byte> utf8Text) =>
            IPAddress.TryParse(Encoding.UTF8.GetString(utf8Text), out _);

#endif
    }
}

#endif
