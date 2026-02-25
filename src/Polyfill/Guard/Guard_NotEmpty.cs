namespace Polyfills;

using System.Runtime.CompilerServices;
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
// ReSharper restore RedundantUsingDirective
using System.Collections;
using System.Diagnostics.CodeAnalysis;

#if PolyPublic
public
#endif
static partial class Guard
{
    [return: NotNullIfNotNull(nameof(value))]
    public static string? NotEmpty(string? value, [CallerArgumentExpression("value")] string name = "") =>
        Ensure.NotEmpty(value, name);

#if FeatureMemory

    public static ReadOnlySpan<T> NotEmpty<T>(ReadOnlySpan<T> value, [CallerArgumentExpression("value")] string name = "") =>
        Ensure.NotEmpty(value, name);

    public static Span<T> NotEmpty<T>(Span<T> value, [CallerArgumentExpression("value")] string name = "") =>
        Ensure.NotEmpty(value, name);

    [return: NotNullIfNotNull(nameof(value))]
    public static Memory<T>? NotEmpty<T>(Memory<T>? value, [CallerArgumentExpression("value")] string name = "") =>
        Ensure.NotEmpty(value, name);

    public static Memory<T> NotEmpty<T>(Memory<T> value, [CallerArgumentExpression("value")] string name = "") =>
        Ensure.NotEmpty(value, name);

    [return: NotNullIfNotNull(nameof(value))]
    public static ReadOnlyMemory<T>? NotEmpty<T>(ReadOnlyMemory<T>? value, [CallerArgumentExpression("value")] string name = "") =>
        Ensure.NotEmpty(value, name);

    public static ReadOnlyMemory<T> NotEmpty<T>(ReadOnlyMemory<T> value, [CallerArgumentExpression("value")] string name = "") =>
        Ensure.NotEmpty(value, name);

#endif

    [return: NotNullIfNotNull(nameof(value))]
    public static T? NotEmpty<T>(
        T? value,
        [CallerArgumentExpression("value")] string name = "")
        where T : IEnumerable =>
        Ensure.NotEmpty(value, name);
}