#if !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.Runtime.CompilerServices;

static partial class Polyfill
{
    extension(Enum)
    {
#if !NET

        /// <summary>
        /// Retrieves an array of the values of the constants in a specified enumeration type.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvalues?view=net-11.0
        public static TEnum[] GetValues<TEnum>()
            where TEnum : struct, Enum
        {
            var values = Enum.GetValues(typeof(TEnum));
            var result = new TEnum[values.Length];
            Array.Copy(values, result, values.Length);
            return result;
        }

        /// <summary>Returns a <see cref="bool"/> telling whether a given integral value exists in a specified enumeration.</summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.isdefined?view=net-11.0#system-enum-isdefined-1(-0)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDefined<TEnum>(TEnum value)
            where TEnum : struct, Enum =>
            Enum.IsDefined(typeof(TEnum), value);

        /// <summary>
        /// Retrieves an array of the names of the constants in a specified enumeration type.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.getnames?view=net-11.0
        public static string[] GetNames<TEnum>()
            where TEnum : struct, Enum =>
            Enum.GetNames(typeof(TEnum));

#endif

#if !NET7_0_OR_GREATER

        /// <summary>
        /// Retrieves an array of the values of the constants in a specified enumeration type.
        /// The values are returned as the underlying type of the enum.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvaluesasunderlyingtype?view=net-11.0#system-enum-getvaluesasunderlyingtype(system-type)
        public static Array GetValuesAsUnderlyingType(Type enumType)
        {
            var values = Enum.GetValues(enumType);
            var underlyingType = Enum.GetUnderlyingType(enumType);
            var result = Array.CreateInstance(underlyingType, values.Length);
            for (var i = 0; i < values.Length; i++)
            {
                result.SetValue(Convert.ChangeType(values.GetValue(i)!, underlyingType), i);
            }

            return result;
        }

        /// <summary>
        /// Retrieves an array of the values of the constants in a specified enumeration type.
        /// The values are returned as the underlying type of the enum.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvaluesasunderlyingtype?view=net-11.0#system-enum-getvaluesasunderlyingtype-1
        public static Array GetValuesAsUnderlyingType<TEnum>()
            where TEnum : struct, Enum =>
            GetValuesAsUnderlyingType(typeof(TEnum));

#endif

#if NETFRAMEWORK || NETSTANDARD

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants specified by TEnum to an equivalent enumerated object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-11.0#system-enum-parse-1(system-string-system-boolean)
        public static TEnum Parse<TEnum>(string value)
            where TEnum : struct, Enum =>
            (TEnum) Enum.Parse(typeof(TEnum), value);

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants specified by TEnum to an equivalent enumerated object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-11.0#system-enum-parse-1(system-string-system-boolean)
        public static TEnum Parse<TEnum>(string value, bool ignoreCase)
            where TEnum : struct, Enum =>
            (TEnum) Enum.Parse(typeof(TEnum), value, ignoreCase);

#endif

#if FeatureMemory

#if !NET6_0_OR_GREATER

        /// <summary>
        /// Converts the span of characters representation of the name or numeric value of one or more enumerated constants specified by TEnum to an equivalent enumerated object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-11.0#system-enum-parse-1(system-readonlyspan((system-char)))
        public static TEnum Parse<TEnum>(ReadOnlySpan<char> value)
            where TEnum : struct, Enum =>
            (TEnum)Enum.Parse(typeof(TEnum), value.ToString());

        /// <summary>
        /// Converts the span of characters representation of the name or numeric value of one or more enumerated constants specified by TEnum to an equivalent enumerated object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-11.0#system-enum-parse-1(system-readonlyspan((system-char))-system-boolean)
        public static TEnum Parse<TEnum>(ReadOnlySpan<char> value, bool ignoreCase)
            where TEnum : struct, Enum =>
            (TEnum)Enum.Parse(typeof(TEnum), value.ToString(), ignoreCase);

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse?view=net-11.0#system-enum-tryparse-1(system-readonlyspan((system-char))-0@)
        public static bool TryParse<TEnum>(ReadOnlySpan<char> value, out TEnum result)
            where TEnum : struct, Enum =>
            Enum.TryParse<TEnum>(value.ToString(), out result);

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. A parameter specifies whether the operation is case-sensitive. The return value indicates whether the conversion succeeded.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse?view=net-11.0#system-enum-tryparse-1(system-readonlyspan((system-char))-system-boolean-0@)
        public static bool TryParse<TEnum>(ReadOnlySpan<char> value, bool ignoreCase, out TEnum result)
            where TEnum : struct, Enum =>
            Enum.TryParse<TEnum>(value.ToString(), ignoreCase, out result);

#endif

#if !NET8_0_OR_GREATER

        /// <summary>
        /// Tries to format the value of the enumerated type instance into the provided span of characters.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryformat?view=net-11.0
        public static bool TryFormat<TEnum>(TEnum value, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default)
            where TEnum : struct, Enum =>
            TryFormatUnconstrained(value, destination, out charsWritten, format);

        internal static bool TryFormatUnconstrained(object value, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default)
        {
            string result;
            if (format.Length == 0)
            {
                result = value.ToString()!;
            }
            else
            {
                result = Enum.Format(value.GetType(), value, format.ToString());
            }

            if (result.Length > destination.Length)
            {
                charsWritten = 0;
                return false;
            }

            charsWritten = result.Length;
            result.CopyTo(destination);
            return true;
        }

#endif
#endif
    }
}
#endif
