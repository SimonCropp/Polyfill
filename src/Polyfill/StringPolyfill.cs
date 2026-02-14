
#if !NET9_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;

static partial class Polyfill
{
    extension(string)
    {
#if FeatureMemory

#if !NET9_0_OR_GREATER

        /// <summary>
        /// Concatenates the string representations of a span of objects, using the specified separator between each member.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-char-system-readonlyspan((system-object)))
        public static string Join(char separator, scoped ReadOnlySpan<object?> values) =>
            Join(separator, values.ToArray());

        /// <summary>
        /// Concatenates a span of strings, using the specified separator between each member.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-char-system-readonlyspan((system-string)))
        public static string Join(char separator, scoped ReadOnlySpan<string?> values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            if (values.Length == 1)
            {
                return values[0] ?? string.Empty;
            }

#if AllowUnsafeBlocks
            var length = 0;

            foreach (var value in values)
            {
                length += 1;
                if (value != null)
                {
                    length += value.Length;
                }
            }

            length -= 1;

            var result = new string(separator, length);

            unsafe
            {
                fixed (char* strPtr = result)
                {
                    var span = new Span<char>(strPtr, length);

                    for (var index = 0; index < values.Length; index++)
                    {
                        if (index > 0)
                        {
                            span = span.Slice(1);
                        }

                        var value = values[index];

                        value.CopyTo(span);

                        span = span.Slice(value.Length);
                    }
                }
            }

            return result;
#else
            return Join(separator, values.ToArray());
#endif
        }

        /// <summary>
        /// Concatenates a span of strings, using the specified separator between each member.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-string-system-readonlyspan((system-string)))
        public static string Join(string? separator, scoped ReadOnlySpan<string?> values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            if (values.Length == 1)
            {
                return values[0] ?? string.Empty;
            }

#if AllowUnsafeBlocks
            separator ??= string.Empty;

            var length = 0;

            foreach (var value in values)
            {
                length += separator.Length;
                if (value != null)
                {
                    length += value.Length;
                }
            }

            length -= separator.Length;

            var result = new string('\0', length);

            unsafe
            {
                fixed (char* strPtr = result)
                {
                    var span = new Span<char>(strPtr, length);

                    for (var index = 0; index < values.Length; index++)
                    {
                        if (index > 0 &&
                            separator.Length > 0)
                        {
                            separator.CopyTo(span);

                            span = span.Slice(separator.Length);
                        }

                        var value = values[index];

                        if (value is null)
                        {
                            continue;
                        }

                        value.CopyTo(span);

                        span = span.Slice(value.Length);
                    }
                }
            }

            return result;
#else
            return string.Join(separator, values.ToArray());
#endif
        }
#endif

#if !NETCOREAPP3_0_OR_GREATER
        /// <summary>
        /// Returns the hash code for the provided read-only character span.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-11.0#system-string-gethashcode(system-readonlyspan((system-char)))
        public static int GetHashCode(ReadOnlySpan<char> value) =>
            value.ToString().GetHashCode();

        /// <summary>
        /// Returns the hash code for the provided read-only character span using the specified rules.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-11.0#system-string-gethashcode(system-readonlyspan((system-char))-system-stringcomparison)
        public static int GetHashCode(ReadOnlySpan<char> value,StringComparison comparisonType) =>
            value.ToString().GetHashCode(comparisonType);
#endif
#endif

#if !NET9_0_OR_GREATER && FeatureMemory
        /// <summary>
        /// Concatenates the string representations of a span of objects, using the specified separator between each member.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-string-system-readonlyspan((system-object)))
        public static string Join(string? separator, scoped ReadOnlySpan<object?> values) =>
            string.Join(separator, values.ToArray());
#endif

#if (NETSTANDARD2_0 || NETFRAMEWORK) && !WINDOWS_UWP

        /// <summary>
        /// Concatenates an array of strings, using the specified separator between each member.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-char-system-string())
        public static string Join(char separator, params string?[] values) =>
#if AllowUnsafeBlocks && FeatureMemory
            Join(separator, new ReadOnlySpan<string?>(values));
#else
            string.Join(new(separator, 1), values);
#endif

        /// <summary>
        /// Concatenates the string representations of an array of objects, using the specified separator between each member.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-char-system-object())
        public static string Join(char separator, params object?[] values) =>
            string.Join(new(separator, 1), values);

        /// <summary>
        /// Concatenates the specified elements of a string array, using the specified separator between each element.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join(system-char-system-string()-system-int32-system-int32)
        public static string Join(char separator, string?[] value, int startIndex, int count) =>
#if AllowUnsafeBlocks && FeatureMemory
            Join(separator, new ReadOnlySpan<string?>(value, startIndex, count));
#else
            string.Join(new(separator, 1), value, startIndex, count);
#endif

        /// <summary>
        /// Concatenates the specified elements of a string array, using the specified separator between each element.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-11.0#system-string-join-1(system-char-system-collections-generic-ienumerable((-0)))
        public static string Join<T>(char separator, IEnumerable<T> values) =>
            string.Join(new(separator, 1), values);
#endif

#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2X
#if FeatureMemory
        /// <summary>
        /// Creates a new string with a specific length and initializes it after creation by using the specified callback.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.string.create?view=net-11.0#system-string-create-1(system-int32-0-system-buffers-spanaction((system-char-0)))
        public static string Create<TState>(int length, TState state, System.Buffers.SpanAction<char, TState> action)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (length == 0)
            {
                return string.Empty;
            }

#if AllowUnsafeBlocks
            var str = new string('\0', length);

            unsafe
            {
                fixed (char* strPtr = str)
                {
                    action(new Span<char>(strPtr, length), state);
                }
            }

            return str;
#else
            var pool = System.Buffers.ArrayPool<char>.Shared;
            var chars = pool.Rent(length);

            try
            {
                var span = chars.AsSpan(0, length);
                // IMPORTANT: Clear the span to avoid garbage data from pooled buffer
                // ArrayPool doesn't clear buffers for performance
                span.Clear();
                action(span, state);

                return new string(chars, 0, length);
            }
            finally
            {
                pool.Return(chars);
            }
#endif
        }
#endif
#endif
    }
}
#endif
