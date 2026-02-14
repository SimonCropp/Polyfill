namespace Polyfills;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

static partial class Polyfill
{
    extension(ArgumentNullException)
    {
#if !NET6_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception.throwifnull?view=net-11.0#system-argumentnullexception-throwifnull(system-object-system-string)
        public static void ThrowIfNull([NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
#endif

#if AllowUnsafeBlocks && !NET9_0_OR_GREATER
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception.throwifnull?view=net-11.0#system-argumentnullexception-throwifnull(system-void*-system-string)
        public static unsafe void ThrowIfNull(void* argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
#endif
    }
}
