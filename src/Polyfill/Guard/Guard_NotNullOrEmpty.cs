namespace Polyfills;

using System.Runtime.CompilerServices;
// ReSharper disable once RedundantUsingDirective
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

#if PolyPublic
public
#endif

static partial class Guard
{
    public static string NotNullOrEmpty(
        [NotNull] string? value,
        [CallerArgumentExpression("value")] string name = "") =>
        Ensure.NotNullOrEmpty(value, name);

    public static T NotNullOrEmpty<T>(
        [NotNull] T? value,
        [CallerArgumentExpression("value")] string name = "")
        where T : IEnumerable =>
        Ensure.NotNullOrEmpty(value, name);

#if FeatureMemory
    public static Memory<char> NotNullOrEmpty(
        [NotNull] Memory<char>? value,
        [CallerArgumentExpression("value")] string name = "") =>
        Ensure.NotNullOrEmpty(value, name);

    public static ReadOnlyMemory<char> NotNullOrEmpty(
        [NotNull] ReadOnlyMemory<char>? value,
        [CallerArgumentExpression("value")] string name = "") =>
        Ensure.NotNullOrEmpty(value, name);
#endif
}