#nullable enable

namespace Polyfills;

using System;
using System.Threading;

static partial class Polyfill
{
#if !NETCOREAPP3_0_OR_GREATER

    /// <summary>
    /// Registers a delegate that will be called when this
    /// <see cref="CancellationToken">CancellationToken</see> is canceled.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister?view=net-11.0#system-threading-cancellationtoken-unsaferegister(system-action((system-object))-system-object)
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

    /// <summary>Registers a delegate that will be called when this <see cref="CancellationToken">CancellationToken</see> is canceled.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.register?view=net-11.0#system-threading-cancellationtoken-register(system-action((system-object-system-threading-cancellationtoken))-system-object)
    public static CancellationTokenRegistration Register(this CancellationToken target, Action<object?, CancellationToken> callback, object? state)
    {
        if (callback is null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        return target.Register(data => callback(data, target), state, useSynchronizationContext: false);
    }

    /// <summary>Registers a delegate that will be called when this <see cref="CancellationToken">CancellationToken</see> is canceled.</summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister?view=net-11.0#system-threading-cancellationtoken-unsaferegister(system-action((system-object-system-threading-cancellationtoken))-system-object)
    public static CancellationTokenRegistration UnsafeRegister(this CancellationToken target, Action<object?, CancellationToken> callback, object? state)
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
            Action<object> internalCallback = data => callback(data, target);
            return target.Register(internalCallback, state, false);
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
}
