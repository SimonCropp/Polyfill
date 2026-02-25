namespace Polyfills;

// ReSharper disable once RedundantUsingDirective
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

#if PolyPublic
public
#endif
static partial class Guard
{
    public static T NotNull<T>(
        [NotNull] T? argument,
        [CallerArgumentExpression("argument")] string? name = null)
        where T : class =>
        Ensure.NotNull(argument, name);

    public static string NotNull(
        [NotNull] string? argument,
        [CallerArgumentExpression("argument")] string? name = null) =>
        Ensure.NotNull(argument, name);
}