#if !NET11_0_OR_GREATER

namespace Polyfills;

// ReSharper disable RedundantUsingDirective
using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
// ReSharper restore RedundantUsingDirective
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

// A class of its own because IPEndPoint.Parse(ReadOnlySpan<char>) would otherwise share a signature with
// IPAddress.Parse(ReadOnlySpan<char>) in Polyfill, and two static extension members with one signature
// cannot coexist on a class.
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
static class IPEndPointPolyfill
{
    extension(IPEndPoint)
    {
#if !NETCOREAPP3_0_OR_GREATER

        /// <summary>
        /// Converts an IP network endpoint (address and port) represented as a string to an <see cref="IPEndPoint"/> instance.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipendpoint.parse?view=net-11.0#system-net-ipendpoint-parse(system-string)
        public static IPEndPoint Parse(string s)
        {
            if (s is null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return ParseString(s);
        }

        /// <summary>
        /// Tries to convert an IP network endpoint (address and port) represented as a string to its <see cref="IPEndPoint"/> equivalent.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipendpoint.tryparse?view=net-11.0#system-net-ipendpoint-tryparse(system-string-system-net-ipendpoint@)
        public static bool TryParse(string s, [NotNullWhen(true)] out IPEndPoint? result) =>
            TryParseString(s, out result);

#if FeatureMemory

        /// <summary>
        /// Converts an IP network endpoint (address and port) represented as a read-only span to an <see cref="IPEndPoint"/> instance.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipendpoint.parse?view=net-11.0#system-net-ipendpoint-parse(system-readonlyspan((system-char)))
        //Note: Copies the span to a string first, so this allocates where the BCL works straight from the span.
        public static IPEndPoint Parse(ReadOnlySpan<char> s) =>
            ParseString(s.ToString());

        /// <summary>
        /// Tries to convert an IP network endpoint (address and port) represented as a read-only span to its <see cref="IPEndPoint"/> equivalent.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipendpoint.tryparse?view=net-11.0#system-net-ipendpoint-tryparse(system-readonlyspan((system-char))-system-net-ipendpoint@)
        //Note: Copies the span to a string first, so this allocates where the BCL works straight from the span.
        public static bool TryParse(ReadOnlySpan<char> s, [NotNullWhen(true)] out IPEndPoint? result) =>
            TryParseString(s.ToString(), out result);

#endif

#endif

#if FeatureMemory

        /// <summary>
        /// Converts an IP network endpoint (address and port) represented as a span of UTF-8 characters to an <see cref="IPEndPoint"/> instance.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipendpoint.parse?view=net-11.0#system-net-ipendpoint-parse(system-readonlyspan((system-byte)))
        //Note: Decodes the bytes to a string first, so this allocates where the BCL works straight from the UTF-8.
        public static IPEndPoint Parse(ReadOnlySpan<byte> utf8Text)
        {
            if (TryParseUtf8(utf8Text, out var result))
            {
                return result;
            }

            throw new FormatException(badEndpointMessage);
        }

        /// <summary>
        /// Tries to convert an IP network endpoint (address and port) represented as a span of UTF-8 characters to its <see cref="IPEndPoint"/> equivalent.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipendpoint.tryparse?view=net-11.0#system-net-ipendpoint-tryparse(system-readonlyspan((system-byte))-system-net-ipendpoint@)
        //Note: Decodes the bytes to a string first, so this allocates where the BCL works straight from the UTF-8.
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, [NotNullWhen(true)] out IPEndPoint? result) =>
            TryParseUtf8(utf8Text, out result);

#endif
    }

#if FeatureMemory

    extension(IPEndPoint target)
    {
        /// <summary>
        /// Tries to format the value of the current instance as characters into the provided span of characters.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipendpoint.tryformat?view=net-11.0#system-net-ipendpoint-tryformat(system-span((system-char))-system-int32@)
        //Note: On .NET Framework, netstandard2.0 and netcoreapp2.0 the address goes through IPAddress.ToString() first, so this can allocate there.
        public bool TryFormat(Span<char> destination, out int charsWritten)
        {
            Span<char> buffer = stackalloc char[maxFormattedLength];
            var length = FormatChars(target, buffer);
            if (buffer.Slice(0, length).TryCopyTo(destination))
            {
                charsWritten = length;
                return true;
            }

            charsWritten = 0;
            return false;
        }

        /// <summary>
        /// Tries to format the value of the current instance as UTF-8 bytes into the provided span.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.ipendpoint.tryformat?view=net-11.0#system-net-ipendpoint-tryformat(system-span((system-byte))-system-int32@)
        //Note: On .NET Framework, netstandard2.0 and netcoreapp2.0 the address goes through IPAddress.ToString() first, so this can allocate there.
        public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten)
        {
            Span<char> buffer = stackalloc char[maxFormattedLength];
            var length = FormatChars(target, buffer);
            if (length <= utf8Destination.Length)
            {
                // every char of an endpoint is ASCII, so each one is a single UTF-8 byte
                for (var i = 0; i < length; i++)
                {
                    utf8Destination[i] = (byte) buffer[i];
                }

                bytesWritten = length;
                return true;
            }

            bytesWritten = 0;
            return false;
        }
    }

    // An endpoint is at most "[" + a 45 char IPv6 address + "%" + a 10 digit scope id + "]:" + a 5 digit port.
    // Formatting goes into a buffer this size before being copied out, because before .NET 8 IPAddress.TryFormat
    // returns false for an IPv4 address whenever the destination is shorter than 15 chars (the longest IPv4
    // address), even if this address would fit.
    const int maxFormattedLength = 128;

    static bool TryParseUtf8(ReadOnlySpan<byte> utf8Text, [NotNullWhen(true)] out IPEndPoint? result) =>
#if NETCOREAPP3_0_OR_GREATER
        IPEndPoint.TryParse(Encoding.UTF8.GetString(utf8Text), out result);
#else
        TryParseString(Encoding.UTF8.GetString(utf8Text), out result);
#endif

    // Writes what IPEndPoint.ToString() returns, "[address]:port" for IPv6 and "address:port" otherwise, into a
    // buffer of maxFormattedLength chars and returns the length. It reads Address and Port rather than calling
    // ToString(), matching the BCL, which formats from its fields, so a ToString() override has no effect.
    static int FormatChars(IPEndPoint endpoint, Span<char> buffer)
    {
        var address = endpoint.Address;
        var isIPv6 = address.AddressFamily == AddressFamily.InterNetworkV6;
        var position = 0;

        if (isIPv6)
        {
            buffer[position++] = '[';
        }

        position += FormatAddress(address, buffer.Slice(position));

        if (isIPv6)
        {
            buffer[position++] = ']';
        }

        buffer[position++] = ':';
        position += FormatPort(endpoint.Port, buffer.Slice(position));
        return position;
    }

    static int FormatAddress(IPAddress address, Span<char> buffer)
    {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        if (address.TryFormat(buffer, out var written))
        {
            return written;
        }
#endif

        var text = address.ToString();
        text.AsSpan().CopyTo(buffer);
        return text.Length;
    }

    // IPEndPoint keeps Port within 0 to 65535, so it is at most 5 digits and never negative.
    static int FormatPort(int port, Span<char> buffer)
    {
        var length = port < 10 ? 1 : port < 100 ? 2 : port < 1000 ? 3 : port < 10000 ? 4 : 5;
        for (var i = length - 1; i >= 0; i--)
        {
            buffer[i] = (char) ('0' + port % 10);
            port /= 10;
        }

        return length;
    }

#endif

    const string badEndpointMessage = "An invalid IPEndPoint was specified.";

#if !NETCOREAPP3_0_OR_GREATER

    static IPEndPoint ParseString(string s)
    {
        if (TryParseString(s, out var result))
        {
            return result;
        }

        throw new FormatException(badEndpointMessage);
    }

    // Port of the BCL's IPEndPoint.InternalTryParse, with IPAddress.TryParse standing in for the internal
    // IPAddressParser it calls.
    static bool TryParseString(string? s, [NotNullWhen(true)] out IPEndPoint? result)
    {
        if (s is null)
        {
            result = null;
            return false;
        }

        // with no port, the whole string goes to the address parser
        var addressLength = s.Length;
        var lastColon = s.LastIndexOf(':');

        if (lastColon > 0)
        {
            // an IPv6 address with a port
            if (s[lastColon - 1] == ']')
            {
                addressLength = lastColon;
            }
            // an IPv4 address with a port (an IPv6 address has another colon)
            else if (s.LastIndexOf(':', lastColon - 1) == -1)
            {
                addressLength = lastColon;
            }
        }

        if (IPAddress.TryParse(s.Substring(0, addressLength), out var address))
        {
            if (addressLength == s.Length)
            {
                result = new(address, 0);
                return true;
            }

            if (uint.TryParse(s.Substring(addressLength + 1), NumberStyles.None, CultureInfo.InvariantCulture, out var port) &&
                port <= IPEndPoint.MaxPort)
            {
                result = new(address, (int) port);
                return true;
            }
        }

        result = null;
        return false;
    }

#endif
}

#endif
