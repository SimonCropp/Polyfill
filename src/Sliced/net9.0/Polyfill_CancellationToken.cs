
#pragma warning disable

#nullable enable

namespace Polyfills;

using System;
using System.Threading;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
#if !NETCOREAPP3_0_OR_GREATER

    /// <summary>
    /// Registers a delegate that will be called when this
    /// <see cref="CancellationToken">CancellationToken</see> is canceled.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If this token is already in the canceled state, the delegate will be run immediately and synchronously.
    /// Any exception the delegate generates will be propagated out of this method call.
    /// </para>
    /// <para>
    /// <see cref="ExecutionContext">ExecutionContext</see> is not captured nor flowed
    /// to the callback's invocation.
    /// </para>
    /// </remarks>
    /// <param name="callback">The delegate to be executed when the <see cref="CancellationToken">CancellationToken</see> is canceled.</param>
    /// <param name="state">The state to pass to the <paramref name="callback"/> when the delegate is invoked.  This may be null.</param>
    /// <returns>The <see cref="CancellationTokenRegistration"/> instance that can
    /// be used to unregister the callback.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister#system-threading-cancellationtoken-unsaferegister(system-action((system-object))-system-object)")]
    public static CancellationTokenRegistration UnsafeRegister(this CancellationToken target, Action<object?> callback, object? state)
    {
        if (callback is null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        
        
        
        

        var restoreFlow = false;
        if (!ExecutionContext.IsFlowSuppressed())
        {
            ExecutionContext.SuppressFlow();
            restoreFlow = true;
        }

        try
        {
            return target.Register(callback, state, false);
        }
        finally
        {
            if (restoreFlow)
            {
                ExecutionContext.RestoreFlow();
            }
        }
    }

#endif

#if !NET6_0_OR_GREATER
#endif
}
