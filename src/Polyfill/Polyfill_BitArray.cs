#if !NET11_0_OR_GREATER

namespace Polyfills;

using System.Collections;

static partial class Polyfill
{
#if !NET8_0_OR_GREATER

    /// <summary>
    /// Determines whether all bits in the <see cref="BitArray"/> are set to <see langword="true"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.bitarray.hasallset?view=net-11.0
    public static bool HasAllSet(this BitArray target)
    {
        for (var index = 0; index < target.Length; index++)
        {
            if (!target[index])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Determines whether any bit in the <see cref="BitArray"/> is set to <see langword="true"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.bitarray.hasanyset?view=net-11.0
    public static bool HasAnySet(this BitArray target)
    {
        for (var index = 0; index < target.Length; index++)
        {
            if (target[index])
            {
                return true;
            }
        }

        return false;
    }

#endif

    /// <summary>
    /// Returns the number of bits set to <see langword="true"/> in the <see cref="BitArray"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.collections.bitarray.popcount?view=net-11.0
    public static int PopCount(this BitArray target)
    {
        var count = 0;
        for (var index = 0; index < target.Length; index++)
        {
            if (target[index])
            {
                count++;
            }
        }

        return count;
    }
}

#endif
