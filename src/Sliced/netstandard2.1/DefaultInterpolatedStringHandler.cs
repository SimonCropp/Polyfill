
#pragma warning disable

#if FeatureMemory && !NET6_0_OR_GREATER

#nullable enable

namespace System.Runtime.CompilerServices;

using System;
using Buffers;
using Diagnostics;
using Diagnostics.CodeAnalysis;
using Globalization;
using Link = ComponentModel.DescriptionAttribute;

/// <summary>Provides a handler used by the language compiler to process interpolated strings into <see cref="string"/> instances.</summary>
[InterpolatedStringHandler]
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[Link("https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.defaultinterpolatedstringhandler")]
#if PolyPublic
public
#endif
ref struct DefaultInterpolatedStringHandler
{
    
    
    
    

    /// <summary>Expected average length of formatted data used for an individual interpolation expression result.</summary>
    /// <remarks>
    /// This is inherited from string.Format, and could be changed based on further data.
    /// string.Format actually uses `format.Length + args.Length * 8`, but format.Length
    /// includes the format items themselves, e.g. "{0}", and since it's rare to have double-digit
    /// numbers of items, we bump the 8 up to 11 to account for the three extra characters in "{d}",
    /// since the compiler-provided base length won't include the equivalent character count.
    /// </remarks>
    const int GuessedLengthPerHole = 11;
    /// <summary>Minimum size array to rent from the pool.</summary>
    /// <remarks>Same as stack-allocation size used today by string.Format.</remarks>
    const int MinimumArrayPoolLength = 256;

    /// <summary>Maximum length allowed for a string.</summary>
    /// <remarks>Keep in sync with AllocateString in gchelpers.cpp.</remarks>
    const int StringMaxLength = 0x3FFFFFDF;

    /// <summary>Optional provider to pass to IFormattable.ToString or ISpanFormattable.TryFormat calls.</summary>
    readonly IFormatProvider? _provider;
    /// <summary>Array rented from the array pool and used to back <see cref="_chars"/>.</summary>
    char[]? _arrayToReturnToPool;
    /// <summary>The span to write into.</summary>
    Span<char> _chars;
    /// <summary>Position at which to write the next character.</summary>
    int _pos;
    /// <summary>Whether <see cref="_provider"/> provides an ICustomFormatter.</summary>
    /// <remarks>
    /// Custom formatters are very rare.  We want to support them, but it's ok if we make them more expensive
    /// in order to make them as pay-for-play as possible.  So, we avoid adding another reference type field
    /// to reduce the size of the handler and to reduce required zero'ing, by only storing whether the provider
    /// provides a formatter, rather than actually storing the formatter.  This in turn means, if there is a
    /// formatter, we pay for the extra interface call on each AppendFormatted that needs it.
    /// </remarks>
    readonly bool _hasCustomFormatter;

    /// <summary>Creates a handler used to translate an interpolated string into a <see cref="string"/>.</summary>
    /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
    /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
    /// <remarks>This is intended to be called only by compiler-generated code. Arguments are not validated as they'd otherwise be for members intended to be used directly.</remarks>
    public DefaultInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        _provider = null;
        _chars = _arrayToReturnToPool = ArrayPool<char>.Shared.Rent(GetDefaultLength(literalLength, formattedCount));
        _pos = 0;
        _hasCustomFormatter = false;
    }

    /// <summary>Creates a handler used to translate an interpolated string into a <see cref="string"/>.</summary>
    /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
    /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <remarks>This is intended to be called only by compiler-generated code. Arguments are not validated as they'd otherwise be for members intended to be used directly.</remarks>
    public DefaultInterpolatedStringHandler(int literalLength, int formattedCount, IFormatProvider? provider)
    {
        _provider = provider;
        _chars = _arrayToReturnToPool = ArrayPool<char>.Shared.Rent(GetDefaultLength(literalLength, formattedCount));
        _pos = 0;
        _hasCustomFormatter = provider is not null && HasCustomFormatter(provider);
    }

    /// <summary>Creates a handler used to translate an interpolated string into a <see cref="string"/>.</summary>
    /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
    /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="initialBuffer">A buffer temporarily transferred to the handler for use as part of its formatting.  Contents may be overwritten.</param>
    /// <remarks>This is intended to be called only by compiler-generated code. Arguments are not validated as they'd otherwise be for members intended to be used directly.</remarks>
    public DefaultInterpolatedStringHandler(int literalLength, int formattedCount, IFormatProvider? provider, Span<char> initialBuffer)
    {
        _provider = provider;
        _chars = initialBuffer;
        _arrayToReturnToPool = null;
        _pos = 0;
        _hasCustomFormatter = provider is not null && HasCustomFormatter(provider);
    }

    /// <summary>Derives a default length with which to seed the handler.</summary>
    /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
    /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    internal static int GetDefaultLength(int literalLength, int formattedCount) =>
        Math.Max(MinimumArrayPoolLength, literalLength + formattedCount * GuessedLengthPerHole);

    /// <summary>Gets the built <see cref="string"/>.</summary>
    /// <returns>The built string.</returns>
    public override string ToString() => Text.ToString();

    /// <summary>Gets the built <see cref="string"/> and clears the handler.</summary>
    /// <returns>The built string.</returns>
    /// <remarks>
    /// This releases any resources used by the handler. The method should be invoked only
    /// once and as the last thing performed on the handler. Subsequent use is erroneous, ill-defined,
    /// and may destabilize the process, as may using any other copies of the handler after ToStringAndClear
    /// is called on any one of them.
    /// </remarks>
    public string ToStringAndClear()
    {
        var result = Text.ToString();
        Clear();
        return result;
    }

    /// <summary>Clears the handler, returning any rented array to the pool.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    internal void Clear()
    {
        var toReturn = _arrayToReturnToPool;
        this = default; 
        if (toReturn is not null)
        {
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }

    /// <summary>Gets a span of the written characters thus far.</summary>
    internal ReadOnlySpan<char> Text => _chars.Slice(0, _pos);

    /// <summary>Writes the specified string to the handler.</summary>
    /// <param name="value">The string to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLiteral(string value)
    {
        if (value.TryCopyTo(_chars.Slice(_pos)))
        {
            _pos += value.Length;
        }
        else
        {
            GrowThenCopyString(value);
        }
    }

    #region AppendFormatted
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    #region AppendFormatted T
    /// <summary>Writes the specified value to the handler.</summary>
    /// <param name="value">The value to write.</param>
    /// <typeparam name="T">The type of the value to write.</typeparam>
    public void AppendFormatted<T>(T value)
    {
        
        
        

        
        if (_hasCustomFormatter)
        {
            AppendCustomFormatter(value, format: null);
            return;
        }

        
        
        
        
        
        
        
        string? s;
        if (value is IFormattable fValue)
        {
            

            if (TryFormatWithExtensions(value, default))
            {
                return;
            }
            else if (fValue is ISpanFormattable sfValue)
            {
                int charsWritten;
                
                while (!sfValue.TryFormat(_chars.Slice(_pos), out charsWritten, default, _provider))
                {
                    Grow();
                }

                _pos += charsWritten;
                return;
            }

            s = fValue.ToString(format: null, _provider); 
        }
        else
        {
            s = value?.ToString();
        }

        if (s is not null)
        {
            AppendLiteral(s);
        }
    }

    /// <summary>Writes the specified value to the handler.</summary>
    /// <param name="value">The value to write.</param>
    /// <param name="format">The format string.</param>
    /// <typeparam name="T">The type of the value to write.</typeparam>
    public void AppendFormatted<T>(T value, string? format)
    {
        
        if (_hasCustomFormatter)
        {
            AppendCustomFormatter(value, format);
            return;
        }

        
        
        
        
        
        
        
        string? s;
        if (value is IFormattable fValue)
        {
            

            if (TryFormatWithExtensions(value, format.AsSpan()))
            {
                return;
            }
            else if (fValue is ISpanFormattable sfValue)
            {
                int charsWritten;
                
                while (!sfValue.TryFormat(_chars.Slice(_pos), out charsWritten, format.AsSpan(), _provider))
                {
                    Grow();
                }

                _pos += charsWritten;
                return;
            }

            s = fValue.ToString(format, _provider); 
        }
        else
        {
            s = value?.ToString();
        }

        if (s is not null)
        {
            AppendLiteral(s);
        }
    }

    /// <summary>Writes the specified value to the handler.</summary>
    /// <param name="value">The value to write.</param>
    /// <param name="alignment">
    /// Minimum number of characters that should be written for this value.  If the value is negative, it indicates
    /// left-aligned and the required minimum is the absolute value.
    /// </param>
    /// <typeparam name="T">The type of the value to write.</typeparam>
    public void AppendFormatted<T>(T value, int alignment)
    {
        var startingPos = _pos;
        AppendFormatted(value);
        if (alignment != 0)
        {
            AppendOrInsertAlignmentIfNeeded(startingPos, alignment);
        }
    }

    /// <summary>Writes the specified value to the handler.</summary>
    /// <param name="value">The value to write.</param>
    /// <param name="format">The format string.</param>
    /// <param name="alignment">
    /// Minimum number of characters that should be written for this value.  If the value is negative, it indicates
    /// left-aligned and the required minimum is the absolute value.
    /// </param>
    /// <typeparam name="T">The type of the value to write.</typeparam>
    public void AppendFormatted<T>(T value, int alignment, string? format)
    {
        var startingPos = _pos;
        AppendFormatted(value, format);
        if (alignment != 0)
        {
            AppendOrInsertAlignmentIfNeeded(startingPos, alignment);
        }
    }
    #endregion

    #region AppendFormatted ReadOnlySpan<char>
    /// <summary>Writes the specified character span to the handler.</summary>
    /// <param name="value">The span to write.</param>
    public void AppendFormatted(scoped ReadOnlySpan<char> value)
    {
        
        if (value.TryCopyTo(_chars.Slice(_pos)))
        {
            _pos += value.Length;
        }
        else
        {
            GrowThenCopySpan(value);
        }
    }

    /// <summary>Writes the specified string of chars to the handler.</summary>
    /// <param name="value">The span to write.</param>
    /// <param name="alignment">
    /// Minimum number of characters that should be written for this value.  If the value is negative, it indicates
    /// left-aligned and the required minimum is the absolute value.
    /// </param>
    /// <param name="format">The format string.</param>
    public void AppendFormatted(scoped ReadOnlySpan<char> value, int alignment = 0, string? format = null)
    {
        var leftAlign = false;
        if (alignment < 0)
        {
            leftAlign = true;
            alignment = -alignment;
        }

        var paddingRequired = alignment - value.Length;
        if (paddingRequired <= 0)
        {
            
            
            AppendFormatted(value);
            return;
        }

        
        EnsureCapacityForAdditionalChars(value.Length + paddingRequired);
        if (leftAlign)
        {
            value.CopyTo(_chars.Slice(_pos));
            _pos += value.Length;
            _chars.Slice(_pos, paddingRequired).Fill(' ');
            _pos += paddingRequired;
        }
        else
        {
            _chars.Slice(_pos, paddingRequired).Fill(' ');
            _pos += paddingRequired;
            value.CopyTo(_chars.Slice(_pos));
            _pos += value.Length;
        }
    }
    #endregion

    #region AppendFormatted string
    /// <summary>Writes the specified value to the handler.</summary>
    /// <param name="value">The value to write.</param>
    public void AppendFormatted(string? value)
    {
        
        if (!_hasCustomFormatter &&
            value is not null &&
            value.TryCopyTo(_chars.Slice(_pos)))
        {
            _pos += value.Length;
        }
        else
        {
            AppendFormattedSlow(value);
        }
    }

    /// <summary>Writes the specified value to the handler.</summary>
    /// <param name="value">The value to write.</param>
    /// <remarks>
    /// Slow path to handle a custom formatter, potentially null value,
    /// or a string that doesn't fit in the current buffer.
    /// </remarks>
    [MethodImpl(MethodImplOptions.NoInlining)]
    void AppendFormattedSlow(string? value)
    {
        if (_hasCustomFormatter)
        {
            AppendCustomFormatter(value, format: null);
        }
        else if (value is not null)
        {
            EnsureCapacityForAdditionalChars(value.Length);
            value.CopyTo(_chars.Slice(_pos));
            _pos += value.Length;
        }
    }

    /// <summary>Writes the specified value to the handler.</summary>
    /// <param name="value">The value to write.</param>
    /// <param name="alignment">
    /// Minimum number of characters that should be written for this value.  If the value is negative, it indicates
    /// left-aligned and the required minimum is the absolute value.
    /// </param>
    /// <param name="format">The format string.</param>
    public void AppendFormatted(string? value, int alignment = 0, string? format = null) =>
        
        
        
        AppendFormatted<string?>(value, alignment, format);
    #endregion

    #region AppendFormatted object
    /// <summary>Writes the specified value to the handler.</summary>
    /// <param name="value">The value to write.</param>
    /// <param name="alignment">
    /// Minimum number of characters that should be written for this value.  If the value is negative, it indicates
    /// left-aligned and the required minimum is the absolute value.
    /// </param>
    /// <param name="format">The format string.</param>
    public void AppendFormatted(object? value, int alignment = 0, string? format = null) =>
        
        
        
        AppendFormatted<object?>(value, alignment, format);
    #endregion
    #endregion

    /// <summary>Gets whether the provider provides a custom formatter.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    internal static bool HasCustomFormatter(IFormatProvider provider)
    {
        Debug.Assert(provider is not null);
        Debug.Assert(
            provider is not CultureInfo || provider.GetFormat(typeof(ICustomFormatter)) is null,
            "Expected CultureInfo to not provide a custom formatter");

        return
            provider!.GetType() != typeof(CultureInfo) && 
            provider.GetFormat(typeof(ICustomFormatter)) != null;
    }

    /// <summary>Formats the value using the custom formatter from the provider.</summary>
    /// <param name="value">The value to write.</param>
    /// <param name="format">The format string.</param>
    /// <typeparam name="T">The type of the value to write.</typeparam>
    [MethodImpl(MethodImplOptions.NoInlining)]
    void AppendCustomFormatter<T>(T value, string? format)
    {
        
        
        
        
        Debug.Assert(_hasCustomFormatter);
        Debug.Assert(_provider != null);

        var formatter = (ICustomFormatter?)_provider!.GetFormat(typeof(ICustomFormatter));
        Debug.Assert(
            formatter != null,
            "An incorrectly written provider said it implemented ICustomFormatter, and then didn't");

        if (formatter?.Format(format, value, _provider) is { } customFormatted)
        {
            AppendLiteral(customFormatted);
        }
    }

    /// <summary>Handles adding any padding required for aligning a formatted value in an interpolation expression.</summary>
    /// <param name="startingPos">The position at which the written value started.</param>
    /// <param name="alignment">
    /// Non-zero minimum number of characters that should be written for this value.  If the value is negative, it
    /// indicates left-aligned and the required minimum is the absolute value.
    /// </param>
    void AppendOrInsertAlignmentIfNeeded(int startingPos, int alignment)
    {
        Debug.Assert(startingPos >= 0 && startingPos <= _pos);
        Debug.Assert(alignment != 0);

        var charsWritten = _pos - startingPos;

        var leftAlign = false;
        if (alignment < 0)
        {
            leftAlign = true;
            alignment = -alignment;
        }

        var paddingNeeded = alignment - charsWritten;
        if (paddingNeeded > 0)
        {
            EnsureCapacityForAdditionalChars(paddingNeeded);

            if (leftAlign)
            {
                _chars.Slice(_pos, paddingNeeded).Fill(' ');
            }
            else
            {
                _chars.Slice(startingPos, charsWritten).CopyTo(_chars.Slice(startingPos + paddingNeeded));
                _chars.Slice(startingPos, paddingNeeded).Fill(' ');
            }

            _pos += paddingNeeded;
        }
    }

    /// <summary>
    /// Ensures <see cref="_chars"/> has the capacity to store <paramref name="additionalChars"/> beyond <see cref="_pos"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void EnsureCapacityForAdditionalChars(int additionalChars)
    {
        if (_chars.Length - _pos < additionalChars)
        {
            Grow(additionalChars);
        }
    }

    /// <summary>
    /// Fallback for fast path in <see cref="AppendLiteral(string)"/> when there's not enough space in the destination.
    /// </summary>
    /// <param name="value">The string to write.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    void GrowThenCopyString(string value)
    {
        Grow(value.Length);
        value.CopyTo(_chars.Slice(_pos));
        _pos += value.Length;
    }

    /// <summary>
    /// Fallback for <see cref="AppendFormatted(ReadOnlySpan{char})"/> for when not enough space exists in the current buffer.
    /// </summary>
    /// <param name="value">The span to write.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    void GrowThenCopySpan(scoped ReadOnlySpan<char> value)
    {
        Grow(value.Length);
        value.CopyTo(_chars.Slice(_pos));
        _pos += value.Length;
    }

    /// <summary>
    /// Grows <see cref="_chars"/> to have the capacity to store at least <paramref name="additionalChars"/>
    /// beyond <see cref="_pos"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)] 
    void Grow(int additionalChars)
    {
        
        
        
        
        Debug.Assert(additionalChars > _chars.Length - _pos);
        GrowCore((uint)_pos + (uint)additionalChars);
    }

    /// <summary>Grows the size of <see cref="_chars"/>.</summary>
    [MethodImpl(MethodImplOptions.NoInlining)] 
    void Grow() =>
        
        
        
        GrowCore((uint)_chars.Length + 1);

    /// <summary>
    /// Grow the size of <see cref="_chars"/> to at least the specified <paramref name="requiredMinCapacity"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    void GrowCore(uint requiredMinCapacity)
    {
        
        
        
        
        

        var newCapacity = Math.Max(requiredMinCapacity, Math.Min((uint)_chars.Length * 2, StringMaxLength));
        var arraySize = (int)InternalMath.Clamp(newCapacity, MinimumArrayPoolLength, int.MaxValue);

        var newArray = ArrayPool<char>.Shared.Rent(arraySize);
        _chars.Slice(0, _pos).CopyTo(newArray);

        var toReturn = _arrayToReturnToPool;
        _chars = _arrayToReturnToPool = newArray;

        if (toReturn is not null)
        {
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }

    bool TryFormatWithExtensions<T>(T value, ReadOnlySpan<char> format)
    {
        int charsWritten;
        switch (value)
        {
            case int cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case bool cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten))
                {
                    Grow();
                }
                break;
            case byte cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case float cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case double cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case DateTime cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case DateTimeOffset cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case decimal cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case long cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case short cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case ushort cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case uint cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case ulong cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            case sbyte cval:
                while (!cval.TryFormat(_chars.Slice(_pos), out charsWritten, format, _provider))
                {
                    Grow();
                }
                break;
            default:
                return false;
        }

        _pos += charsWritten;
        return true;
    }
}

[ExcludeFromCodeCoverage]
static file class InternalMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Clamp(uint value, uint min, uint max)
    {
        if (min > max)
        {
            ThrowMinMaxException(min, max);
        }

        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }

        return value;
    }

    [DoesNotReturn]
    static void ThrowMinMaxException<T>(T min, T max) =>
        throw new ArgumentException(string.Format(SR.Argument_MinMaxValue, min, max));
}

[ExcludeFromCodeCoverage]
static file class SR
{
    public const string Argument_MinMaxValue = "'{0}' cannot be greater than {1}.";
}

#endif