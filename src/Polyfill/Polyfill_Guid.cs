#pragma warning disable

#if !NET9_0_OR_GREATER

namespace Polyfills;

using System;

static partial class Polyfill
{
    extension(Guid target)
    {
        /// <summary>
        /// Gets the variant field of the <see cref="Guid"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.variant?view=net-11.0
        public int Variant =>
            HighNibble(target, 8);

        /// <summary>
        /// Gets the version field of the <see cref="Guid"/>.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.guid.version?view=net-11.0
        public int Version =>
            HighNibble(target, 7);
    }

    // Both fields are the high nibble of a single byte of the little endian layout that
    // ToByteArray and TryWriteBytes produce: the version at index 7 and the variant at index 8.
    static int HighNibble(Guid value, int index)
    {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        Span<byte> buffer = stackalloc byte[16];
        value.TryWriteBytes(buffer);
        return buffer[index] >> 4;
#else
        return value.ToByteArray()[index] >> 4;
#endif
    }
}

#endif
