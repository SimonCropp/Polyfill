namespace Polyfills;

using System;
using System.Runtime.CompilerServices;
using System.Text;

static partial class Polyfill
{
#if FeatureMemory && !NET6_0_OR_GREATER
    /// <summary>Appends the specified interpolated string to this instance.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@)
    public static StringBuilder Append(
        StringBuilder target,
        [InterpolatedStringHandlerArgument(nameof(target))]
        ref AppendInterpolatedStringHandler handler) => target;

    /// <summary>Appends the specified interpolated string to this instance.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@)
    public static StringBuilder Append(
        StringBuilder target,
        IFormatProvider? provider,
        [InterpolatedStringHandlerArgument(nameof(target), nameof(provider))]
        ref AppendInterpolatedStringHandler handler) => target;

#elif NET6_0_OR_GREATER

    /// <summary>Appends the specified interpolated string to this instance.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@)
    public static StringBuilder Append(
        StringBuilder target,
        [InterpolatedStringHandlerArgument(nameof(target))] ref StringBuilder.AppendInterpolatedStringHandler handler) =>
        target.Append(ref handler);

    /// <summary>Appends the specified interpolated string to this instance.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-11.0#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@)
    public static StringBuilder Append(
        StringBuilder target,
        IFormatProvider? provider,
        [InterpolatedStringHandlerArgument(nameof(target), nameof(provider))] ref StringBuilder.AppendInterpolatedStringHandler handler) =>
        target.Append(provider, ref handler);

#endif
}
