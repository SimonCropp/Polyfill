#if !NET6_0_OR_GREATER

namespace Polyfills;

using System;
// ReSharper disable RedundantUsingDirective
using System.Collections.Generic;
using System.Runtime.CompilerServices;
#if FeatureMemory
using System.Buffers;
#endif
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
// ReSharper restore RedundantUsingDirective

static partial class Polyfill
{
#if FeatureValueTask

    /// <summary>
    /// Returns a UDP datagram asynchronously that was sent by a remote host.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.receiveasync?view=net-11.0#system-net-sockets-udpclient-receiveasync(system-threading-cancellationtoken)
    //Note: Cancellation abandons the wait, not the underlying receive, which stays in flight. Its datagram is returned by the next `ReceiveAsync(CancellationToken)` call, so after a cancellation do not mix in the other `Receive` overloads.
    public static ValueTask<UdpReceiveResult> ReceiveAsync(
        this UdpClient target,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask<UdpReceiveResult>(Task.FromCanceled<UdpReceiveResult>(cancellationToken));
        }

        return new ValueTask<UdpReceiveResult>(ReceiveWithCancellationAsync(target, cancellationToken));
    }

    // A pending receive can not be cancelled on these targets, other than by closing the client.
    // So a cancelled call leaves its receive in flight, and hands it to the next call. No datagram is lost, and the client stays usable.
    sealed class UdpAbandonedReceives
    {
        public readonly Queue<Task<UdpReceiveResult>> Queue = new();
    }

    static readonly ConditionalWeakTable<UdpClient, UdpAbandonedReceives> udpAbandonedReceives = new();

    static async Task<UdpReceiveResult> ReceiveWithCancellationAsync(
        UdpClient target,
        CancellationToken cancellationToken)
    {
        var abandoned = udpAbandonedReceives.GetOrCreateValue(target);

        Task<UdpReceiveResult> receive;
        lock (abandoned)
        {
            if (abandoned.Queue.Count > 0)
            {
                receive = abandoned.Queue.Dequeue();
            }
            else
            {
                receive = target.ReceiveAsync();
            }
        }

        if (!receive.IsCompleted && cancellationToken.CanBeCanceled)
        {
            var cancelled = new TaskCompletionSource<bool>();
            using var registration = cancellationToken.Register(
                state => ((TaskCompletionSource<bool>) state!).TrySetResult(true),
                cancelled);

            var completed = await Task.WhenAny(receive, cancelled.Task).ConfigureAwait(false);
            if (completed != receive)
            {
                // If the client is closed before another call takes the receive, its exception would otherwise go unobserved
                _ = receive.ContinueWith(
                    static task => _ = task.Exception,
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);

                lock (abandoned)
                {
                    abandoned.Queue.Enqueue(receive);
                }

                throw new OperationCanceledException(cancellationToken);
            }
        }

        return await receive.ConfigureAwait(false);
    }

#if FeatureMemory

    /// <summary>
    /// Sends a UDP datagram asynchronously to a remote host.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.sendasync?view=net-11.0#system-net-sockets-udpclient-sendasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken)
    public static ValueTask<int> SendAsync(
        this UdpClient target,
        ReadOnlyMemory<byte> datagram,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
        }

        return new ValueTask<int>(SendWithCancellationAsync(target, datagram, cancellationToken));
    }

    /// <summary>
    /// Sends a UDP datagram asynchronously to a remote host.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.sendasync?view=net-11.0#system-net-sockets-udpclient-sendasync(system-readonlymemory((system-byte))-system-net-ipendpoint-system-threading-cancellationtoken)
    public static ValueTask<int> SendAsync(
        this UdpClient target,
        ReadOnlyMemory<byte> datagram,
        IPEndPoint? endPoint,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
        }

        return new ValueTask<int>(SendWithCancellationAsync(target, datagram, endPoint, cancellationToken));
    }

    /// <summary>
    /// Sends a UDP datagram asynchronously to a remote host.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.sendasync?view=net-11.0#system-net-sockets-udpclient-sendasync(system-readonlymemory((system-byte))-system-string-system-int32-system-threading-cancellationtoken)
    public static ValueTask<int> SendAsync(
        this UdpClient target,
        ReadOnlyMemory<byte> datagram,
        string? hostname,
        int port,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
        }

        return new ValueTask<int>(SendWithCancellationAsync(target, datagram, hostname, port, cancellationToken));
    }

    static async Task<int> SendWithCancellationAsync(
        UdpClient target,
        ReadOnlyMemory<byte> datagram,
        CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            var bytes = datagram.ToArray();
            return await target.SendAsync(bytes, bytes.Length);
        }
        catch (ObjectDisposedException) when (cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(cancellationToken);
        }
        catch (SocketException) when (cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(cancellationToken);
        }
    }

    static async Task<int> SendWithCancellationAsync(
        UdpClient target,
        ReadOnlyMemory<byte> datagram,
        IPEndPoint? endPoint,
        CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            var bytes = datagram.ToArray();
            return await target.SendAsync(bytes, bytes.Length, endPoint);
        }
        catch (ObjectDisposedException) when (cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(cancellationToken);
        }
        catch (SocketException) when (cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(cancellationToken);
        }
    }

    static async Task<int> SendWithCancellationAsync(
        UdpClient target,
        ReadOnlyMemory<byte> datagram,
        string? hostname,
        int port,
        CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            var bytes = datagram.ToArray();
            return await target.SendAsync(bytes, bytes.Length, hostname, port);
        }
        catch (ObjectDisposedException) when (cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(cancellationToken);
        }
        catch (SocketException) when (cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException(cancellationToken);
        }
    }

#endif

#endif

#if FeatureMemory

    /// <summary>
    /// Sends a UDP datagram to a remote host.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.send?view=net-11.0#system-net-sockets-udpclient-send(system-readonlyspan((system-byte)))
    public static int Send(
        this UdpClient target,
        ReadOnlySpan<byte> datagram)
    {
        var rented = ArrayPool<byte>.Shared.Rent(datagram.Length);
        try
        {
            datagram.CopyTo(rented);
            return target.Send(rented, datagram.Length);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rented);
        }
    }

    /// <summary>
    /// Sends a UDP datagram to the host at the specified remote endpoint.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.send?view=net-11.0#system-net-sockets-udpclient-send(system-readonlyspan((system-byte))-system-net-ipendpoint)
    public static int Send(
        this UdpClient target,
        ReadOnlySpan<byte> datagram,
        IPEndPoint? endPoint)
    {
        var rented = ArrayPool<byte>.Shared.Rent(datagram.Length);
        try
        {
            datagram.CopyTo(rented);
            return target.Send(rented, datagram.Length, endPoint);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rented);
        }
    }

    /// <summary>
    /// Sends a UDP datagram to a specified port on a specified remote host.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.send?view=net-11.0#system-net-sockets-udpclient-send(system-readonlyspan((system-byte))-system-string-system-int32)
    public static int Send(
        this UdpClient target,
        ReadOnlySpan<byte> datagram,
        string? hostname,
        int port)
    {
        var rented = ArrayPool<byte>.Shared.Rent(datagram.Length);
        try
        {
            datagram.CopyTo(rented);
            return target.Send(rented, datagram.Length, hostname, port);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rented);
        }
    }

#endif
}
#endif
