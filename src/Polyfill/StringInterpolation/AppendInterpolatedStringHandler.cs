﻿// <auto-generated />
#pragma warning disable

#if FeatureMemory && !NET6_0_OR_GREATER

#nullable enable

namespace System.Text;

using ComponentModel;
using Diagnostics;
using Diagnostics.CodeAnalysis;
using Runtime.CompilerServices;

//https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Text/StringBuilder.cs
/// <summary>Provides a handler used by the language compiler to append interpolated strings into <see cref="StringBuilder"/> instances.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[InterpolatedStringHandler]
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyPublic
public
#endif
struct AppendInterpolatedStringHandler
{
    const int StackallocCharBufferSizeLimit = 256;

    StringBuilder _stringBuilder;

    IFormatProvider? _provider;

    bool _hasCustomFormatter;

    /// <summary>Creates a handler used to append an interpolated string into a <see cref="StringBuilder"/>.</summary>
    public AppendInterpolatedStringHandler(int literalLength, int formattedCount, StringBuilder stringBuilder)
    {
        _stringBuilder = stringBuilder;
        _provider = null;
        _hasCustomFormatter = false;
    }

    /// <summary>Creates a handler used to translate an interpolated string into a <see cref="string"/>.</summary>
    public AppendInterpolatedStringHandler(int literalLength, int formattedCount, StringBuilder stringBuilder, IFormatProvider? provider)
    {
        _stringBuilder = stringBuilder;
        _provider = provider;
        _hasCustomFormatter = provider is not null && DefaultInterpolatedStringHandler.HasCustomFormatter(provider);
    }

    /// <summary>Writes the specified string to the handler.</summary>
    public void AppendLiteral(string value) => _stringBuilder.Append(value);

    #region AppendFormatted

    #region AppendFormatted T

    /// <summary>Writes the specified value to the handler.</summary>
    public void AppendFormatted<T>(T value)
    {
        if (_hasCustomFormatter)
        {
            AppendCustomFormatter(value, format: null);
        }
        else if (value is IFormattable fValue)
        {
            if (typeof(T).IsEnum || HasTryFormatExtension(typeof(T)))
            {
                AppendFormattedWithTempSpace(value, 0, format: null);
            }
#if NET6_0_OR_GREATER
            else if (fValue is ISpanFormattable)
            {
                AppendFormattedWithTempSpace(value, 0, format: null);
            }
#endif
            else
            {
                _stringBuilder.Append(fValue.ToString(format: null, _provider));
            }
        }
        else if (value is not null)
        {
            _stringBuilder.Append(value.ToString());
        }
    }

    /// <summary>Writes the specified value to the handler.</summary>
    public void AppendFormatted<T>(T value, string? format)
    {
        if (_hasCustomFormatter)
        {
            AppendCustomFormatter(value, format);
        }
        else if (value is IFormattable fValue)
        {
            if (typeof(T).IsEnum || HasTryFormatExtension(typeof(T)))
            {
                AppendFormattedWithTempSpace(value, 0, format);
            }
#if NET6_0_OR_GREATER
            else if (fValue is ISpanFormattable)
            {
                AppendFormattedWithTempSpace(value, 0, format);
            }
#endif
            else
            {
                _stringBuilder.Append(fValue.ToString(format, _provider));
            }
        }
        else if (value is not null)
        {
            _stringBuilder.Append(value.ToString());
        }
    }

    /// <summary>Writes the specified value to the handler.</summary>
    public void AppendFormatted<T>(T value, int alignment) =>
        AppendFormatted(value, alignment, format: null);

    /// <summary>Writes the specified value to the handler.</summary>
    public void AppendFormatted<T>(T value, int alignment, string? format)
    {
        if (alignment == 0)
        {
            AppendFormatted(value, format);
        }
        else if (alignment < 0)
        {
            var start = _stringBuilder.Length;
            AppendFormatted(value, format);
            var paddingRequired = -alignment - (_stringBuilder.Length - start);
            if (paddingRequired > 0)
            {
                _stringBuilder.Append(' ', paddingRequired);
            }
        }
        else
        {
            AppendFormattedWithTempSpace(value, alignment, format);
        }
    }

    void AppendFormattedWithTempSpace<T>(T value, int alignment, string? format)
    {
        var handler = new DefaultInterpolatedStringHandler(0, 0, _provider, stackalloc char[StackallocCharBufferSizeLimit]);
        handler.AppendFormatted(value, format);
        AppendFormatted(handler.Text, alignment);
        handler.Clear();
    }

    #endregion

    #region AppendFormatted ReadOnlySpan<char>

    /// <summary>Writes the specified character span to the handler.</summary>
    public void AppendFormatted(ReadOnlySpan<char> value) => _stringBuilder.Append(value);

    /// <summary>Writes the specified string of chars to the handler.</summary>
    public void AppendFormatted(ReadOnlySpan<char> value, int alignment = 0, string? format = null)
    {
        if (alignment == 0)
        {
            _stringBuilder.Append(value);
        }
        else
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
                _stringBuilder.Append(value);
            }
            else if (leftAlign)
            {
                _stringBuilder.Append(value);
                _stringBuilder.Append(' ', paddingRequired);
            }
            else
            {
                _stringBuilder.Append(' ', paddingRequired);
                _stringBuilder.Append(value);
            }
        }
    }

    #endregion

    #region AppendFormatted string

    /// <summary>Writes the specified value to the handler.</summary>
    public void AppendFormatted(string? value)
    {
        if (!_hasCustomFormatter)
        {
            _stringBuilder.Append(value);
        }
        else
        {
            AppendFormatted<string?>(value);
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

    /// <summary>Formats the value using the custom formatter from the provider.</summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    void AppendCustomFormatter<T>(T value, string? format)
    {
        var formatter = (ICustomFormatter?)_provider!.GetFormat(typeof(ICustomFormatter));

        if (formatter is not null)
        {
            _stringBuilder.Append(formatter.Format(format, value, _provider));
        }
    }

    static bool HasTryFormatExtension(Type type) =>
        type == typeof(int) || type == typeof(bool) || type == typeof(byte) || type == typeof(float) ||
        type == typeof(double) || type == typeof(DateTime) || type == typeof(DateTimeOffset) ||
        type == typeof(decimal) || type == typeof(long) || type == typeof(short) || type == typeof(ushort) ||
        type == typeof(uint) || type == typeof(ulong) || type == typeof(sbyte);
}
#endif