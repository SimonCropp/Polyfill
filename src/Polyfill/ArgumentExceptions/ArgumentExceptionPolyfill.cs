namespace Polyfills;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

static partial class Polyfill
{
    extension(ArgumentException)
    {
#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception.throwifnullorempty?view=net-11.0#system-argumentexception-throwifnullorempty(system-string-system-string)
        public static void ThrowIfNullOrEmpty([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (argument.Length == 0)
            {
                throw new ArgumentException("The value cannot be an empty string.", paramName);
            }
        }
#endif

#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception.throwifnullorwhitespace?view=net-11.0#system-argumentexception-throwifnullorwhitespace(system-string-system-string)
        public static void ThrowIfNullOrWhiteSpace([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException("The value cannot be an empty string or composed entirely of whitespace.", paramName);
            }
        }
#endif
    }
}
