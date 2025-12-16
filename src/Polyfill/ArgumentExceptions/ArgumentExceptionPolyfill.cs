namespace Polyfills;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

static partial class Polyfill
{
    extension(ArgumentException)
    {
#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception.throwifnullorempty?view=net-10.0#system-argumentexception-throwifnullorempty(system-string-system-string)
        public static void ThrowIfNullOrEmpty([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw argument is null
                    ? new ArgumentNullException(paramName)
                    : new ArgumentException("The value cannot be an empty string.", paramName);
            }
        }
#endif

#if !NET8_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception.throwifnullorwhitespace?view=net-10.0#system-argumentexception-throwifnullorwhitespace(system-string-system-string)
        public static void ThrowIfNullOrWhiteSpace([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw argument is null
                    ? new ArgumentNullException(paramName)
                    : new ArgumentException("The value cannot be an empty string or composed entirely of whitespace.", paramName);
            }
        }
#endif
    }
}
