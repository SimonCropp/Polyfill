#if NETCOREAPP3_0_OR_GREATER && !NET11_0_OR_GREATER && !RefsBclMemory

namespace Polyfills;

using System;
using System.Text.Unicode;

static partial class Polyfill
{
    extension(Utf8)
    {
        /// <summary>
        /// Returns the index in <paramref name="value"/> where the first ill-formed UTF-8 subsequence begins, or -1 if <paramref name="value"/> is well-formed.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.unicode.utf8.indexofinvalidsubsequence?view=net-11.0
        public static int IndexOfInvalidSubsequence(ReadOnlySpan<byte> value)
        {
            // Validation follows the Unicode well-formed UTF-8 byte sequence table (Table 3-7).
            var i = 0;
            while (i < value.Length)
            {
                var b0 = value[i];

                // single-byte ASCII
                if (b0 <= 0x7F)
                {
                    i++;
                    continue;
                }

                int trailing;
                byte lowerMin;
                byte lowerMax;
                if (b0 is >= 0xC2 and <= 0xDF)
                {
                    trailing = 1;
                    lowerMin = 0x80;
                    lowerMax = 0xBF;
                }
                else if (b0 == 0xE0)
                {
                    trailing = 2;
                    lowerMin = 0xA0;
                    lowerMax = 0xBF;
                }
                else if (b0 is >= 0xE1 and <= 0xEC or >= 0xEE and <= 0xEF)
                {
                    trailing = 2;
                    lowerMin = 0x80;
                    lowerMax = 0xBF;
                }
                else if (b0 == 0xED)
                {
                    trailing = 2;
                    lowerMin = 0x80;
                    lowerMax = 0x9F;
                }
                else if (b0 == 0xF0)
                {
                    trailing = 3;
                    lowerMin = 0x90;
                    lowerMax = 0xBF;
                }
                else if (b0 is >= 0xF1 and <= 0xF3)
                {
                    trailing = 3;
                    lowerMin = 0x80;
                    lowerMax = 0xBF;
                }
                else if (b0 == 0xF4)
                {
                    trailing = 3;
                    lowerMin = 0x80;
                    lowerMax = 0x8F;
                }
                else
                {
                    // invalid lead byte: 0x80-0xC1 (continuation/overlong) or 0xF5-0xFF
                    return i;
                }

                // first continuation byte has a tighter valid range than the rest
                if (i + 1 >= value.Length ||
                    value[i + 1] < lowerMin ||
                    value[i + 1] > lowerMax)
                {
                    return i;
                }

                // remaining continuation bytes must each be 0x80-0xBF
                for (var j = 2; j <= trailing; j++)
                {
                    if (i + j >= value.Length ||
                        value[i + j] < 0x80 ||
                        value[i + j] > 0xBF)
                    {
                        return i;
                    }
                }

                i += trailing + 1;
            }

            return -1;
        }
    }
}

#endif
