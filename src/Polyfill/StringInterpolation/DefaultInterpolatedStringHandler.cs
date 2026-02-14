#if FeatureMemory
#if !NET6_0_OR_GREATER

#nullable enable

namespace System.Runtime.CompilerServices;

using System;
using Buffers;
using Diagnostics;
using Diagnostics.CodeAnalysis;
using Globalization;

/// <summary>Provides a handler used by the language compiler to process interpolated strings into <see cref="string"/> instances.</summary>
[InterpolatedStringHandler]
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
//https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Runtime/CompilerServices/DefaultInterpolatedStringHandler.cs
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.defaultinterpolatedstringhandler?view=net-11.0
#if PolyPublic
public
#endif
ref struct DefaultInterpolatedStringHandler
{
    const int GuessedLengthPerHole = 11;
    const int StringMaxLength = 0x3FFFFFDF;
    const int MinimumArrayPoolLength = 256;
    readonly IFormatProvider? _provider;
    char[]? _arrayToReturnToPool;
    Span<char> _chars;
    int _pos;
    readonly bool _hasCustomFormatter;

    /// <summary>Creates a handler used to translate an interpolated string into a <see cref="string"/>.</summary>
    public DefaultInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        _provider = null;
        _chars = _arrayToReturnToPool = ArrayPool<char>.Shared.Rent(GetDefaultLength(literalLength, formattedCount));
        _pos = 0;
        _hasCustomFormatter = false;
    }

    /// <summary>Creates a handler used to translate an interpolated string into a <see cref="string"/>.</summary>
    public DefaultInterpolatedStringHandler(int literalLength, int formattedCount, IFormatProvider? provider)
    {
        _provider = provider;
        _chars = _arrayToReturnToPool = ArrayPool<char>.Shared.Rent(GetDefaultLength(literalLength, formattedCount));
        _pos = 0;
        _hasCustomFormatter = provider is not null && HasCustomFormatter(provider);
    }

    /// <summary>Creates a handler used to translate an interpolated string into a <see cref="string"/>.</summary>
    public DefaultInterpolatedStringHandler(int literalLength, int formattedCount, IFormatProvider? provider, Span<char> initialBuffer)
    {
        _provider = provider;
        _chars = initialBuffer;
        _arrayToReturnToPool = null;
        _pos = 0;
        _hasCustomFormatter = provider is not null && HasCustomFormatter(provider);
    }

    internal static int GetDefaultLength(int literalLength, int formattedCount) =>
        Math.Max(MinimumArrayPoolLength, literalLength + (formattedCount * GuessedLengthPerHole));

    public override string ToString() => Text.ToString();

    /// <summary>Gets the built <see cref="string"/> and clears the handler.</summary>
    public string ToStringAndClear()
    {
        string result = Text.ToString();
        Clear();
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        char[]? toReturn = _arrayToReturnToPool;

        _arrayToReturnToPool = null;
        _chars = default;
        _pos = 0;

        if (toReturn is not null)
        {
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }

    /// <summary>Gets a span of the characters appended to the handler.</summary>
    public ReadOnlySpan<char> Text => _chars.Slice(0, _pos);

    /// <summary>Writes the specified string to the handler.</summary>
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
    public void AppendFormatted<T>(T value)
    {
        if (_hasCustomFormatter)
        {
            AppendCustomFormatter(value, format: null);
            return;
        }

        string? s;
        if (value is IFormattable)
        {
            if (typeof(T).IsEnum)
            {
                int charsWritten;
                while (!Enum.TryFormatUnconstrained(value, _chars.Slice(_pos), out charsWritten))
                {
                    Grow();
                }

                _pos += charsWritten;
                return;
            }

#if NET6_0_OR_GREATER
            if (value is ISpanFormattable)
            {
                int charsWritten;
                while (!((ISpanFormattable) value).TryFormat(_chars.Slice(_pos), out charsWritten, default, _provider))
                {
                    Grow();
                }

                _pos += charsWritten;
                return;
            }
#endif
            s = ((IFormattable) value).ToString(format: null, _provider);
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
    public void AppendFormatted<T>(T value, string? format)
    {
        if (_hasCustomFormatter)
        {
            AppendCustomFormatter(value, format);
            return;
        }

        string? s;
        if (value is IFormattable)
        {
            var formatSpan = format.AsSpan();
            if (typeof(T).IsEnum)
            {
                int charsWritten;
                while (!Enum.TryFormatUnconstrained(value, _chars.Slice(_pos), out charsWritten, formatSpan))
                {
                    Grow();
                }

                _pos += charsWritten;
                return;
            }

#if NET6_0_OR_GREATER
            if (value is ISpanFormattable)
            {
                int charsWritten;
                while (!((ISpanFormattable) value).TryFormat(_chars.Slice(_pos), out charsWritten, formatSpan, _provider))
                {
                    Grow();
                }

                _pos += charsWritten;
                return;
            }
#endif
            s = ((IFormattable) value).ToString(format, _provider);
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
    public void AppendFormatted<T>(T value, int alignment)
    {
        int startingPos = _pos;
        AppendFormatted(value);
        if (alignment != 0)
        {
            AppendOrInsertAlignmentIfNeeded(startingPos, alignment);
        }
    }

    /// <summary>Writes the specified value to the handler.</summary>
    public void AppendFormatted<T>(T value, int alignment, string? format)
    {
        int startingPos = _pos;
        AppendFormatted(value, format);
        if (alignment != 0)
        {
            AppendOrInsertAlignmentIfNeeded(startingPos, alignment);
        }
    }

    #endregion

    #region AppendFormatted ReadOnlySpan<char>

    /// <summary>Writes the specified character span to the handler.</summary>
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
    public void AppendFormatted(scoped ReadOnlySpan<char> value, int alignment = 0, string? format = null)
    {
        bool leftAlign = false;
        if (alignment < 0)
        {
            leftAlign = true;
            alignment = -alignment;
        }

        int paddingRequired = alignment - value.Length;
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
    public void AppendFormatted(string? value, int alignment = 0, string? format = null) =>
        AppendFormatted<string?>(value, alignment, format);

    #endregion

    #region AppendFormatted object

    /// <summary>Writes the specified value to the handler.</summary>
    public void AppendFormatted(object? value, int alignment = 0, string? format = null) =>
        AppendFormatted<object?>(value, alignment, format);

    #endregion

    #endregion

    /// <summary>Gets whether the provider provides a custom formatter.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool HasCustomFormatter(IFormatProvider provider)
    {
        return
            provider.GetType() != typeof(CultureInfo) &&
            provider.GetFormat(typeof(ICustomFormatter)) != null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    void AppendCustomFormatter<T>(T value, string? format)
    {
        var formatter = (ICustomFormatter?) _provider.GetFormat(typeof(ICustomFormatter));

        if (formatter is not null && formatter.Format(format, value, _provider) is string customFormatted)
        {
            AppendLiteral(customFormatted);
        }
    }

    void AppendOrInsertAlignmentIfNeeded(int startingPos, int alignment)
    {
        int charsWritten = _pos - startingPos;

        bool leftAlign = false;
        if (alignment < 0)
        {
            leftAlign = true;
            alignment = -alignment;
        }

        int paddingNeeded = alignment - charsWritten;
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void EnsureCapacityForAdditionalChars(int additionalChars)
    {
        if (_chars.Length - _pos < additionalChars)
        {
            Grow(additionalChars);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    void GrowThenCopyString(string value)
    {
        Grow(value.Length);
        value.CopyTo(_chars.Slice(_pos));
        _pos += value.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    void GrowThenCopySpan(scoped ReadOnlySpan<char> value)
    {
        Grow(value.Length);
        value.CopyTo(_chars.Slice(_pos));
        _pos += value.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    void Grow(int additionalChars) => GrowCore((uint) _pos + (uint) additionalChars);

    [MethodImpl(MethodImplOptions.NoInlining)]
    void Grow() => GrowCore((uint) _chars.Length + 1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void GrowCore(uint requiredMinCapacity)
    {
        uint newCapacity = Math.Max(requiredMinCapacity, Math.Min((uint) _chars.Length * 2, StringMaxLength));
        int arraySize = (int) Math.Clamp(newCapacity, MinimumArrayPoolLength, int.MaxValue);

        char[] newArray = ArrayPool<char>.Shared.Rent(arraySize);
        _chars.Slice(0, _pos).CopyTo(newArray);

        char[]? toReturn = _arrayToReturnToPool;
        _chars = _arrayToReturnToPool = newArray;

        if (toReturn is not null)
        {
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Runtime.CompilerServices.DefaultInterpolatedStringHandler))]
#endif
#endif
