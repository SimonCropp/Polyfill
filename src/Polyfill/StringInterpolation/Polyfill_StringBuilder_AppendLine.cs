namespace Polyfills;

using System;
using System.Runtime.CompilerServices;
using System.Text;

static partial class Polyfill
{
#if FeatureMemory && !NET6_0_OR_GREATER

    /// <summary>Appends the specified interpolated string followed by the default line terminator to the end of the current StringBuilder object.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-11.0#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@)
    public static StringBuilder AppendLine(
        StringBuilder target,
        [InterpolatedStringHandlerArgument(nameof(target))]
        ref AppendInterpolatedStringHandler handler) =>
        target.AppendLine();

    /// <summary>Appends the specified interpolated string followed by the default line terminator to the end of the current StringBuilder object.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-11.0#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@)
    public static StringBuilder AppendLine(
        StringBuilder target,
        IFormatProvider? provider,
        [InterpolatedStringHandlerArgument(nameof(target), nameof(provider))]
        ref AppendInterpolatedStringHandler handler) =>
        target.AppendLine();

#elif NET6_0_OR_GREATER

    /// <summary>Appends the specified interpolated string followed by the default line terminator to the end of the current StringBuilder object.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-11.0#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@)
    public static StringBuilder AppendLine(
        StringBuilder target,
        [InterpolatedStringHandlerArgument(nameof(target))]
        ref StringBuilder.AppendInterpolatedStringHandler handler) =>
        target.AppendLine(ref handler);

    /// <summary>Appends the specified interpolated string followed by the default line terminator to the end of the current StringBuilder object.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-11.0#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@)
    public static StringBuilder AppendLine(
        StringBuilder target,
        IFormatProvider? provider,
        [InterpolatedStringHandlerArgument(nameof(target), nameof(provider))]
        ref StringBuilder.AppendInterpolatedStringHandler handler) =>
        target.AppendLine(provider, ref handler);
#endif

}
