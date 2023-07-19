
// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantAttributeSuffix

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class PolyfillExtensions
{
#if !NET8_0_OR_GREATER

    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync")]
    public static Task CancelAsync(this CancellationTokenSource target)
    {
         target.Cancel();
         return Task.CompletedTask;
    }

#endif
}
