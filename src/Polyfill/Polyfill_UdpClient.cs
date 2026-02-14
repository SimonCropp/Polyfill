#if !NET6_0_OR_GREATER && FeatureValueTask

namespace Polyfills;

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    /// <summary>
    /// Returns a UDP datagram asynchronously that was sent by a remote host.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.receiveasync?view=net-11.0#system-net-sockets-udpclient-receiveasync(system-threading-cancellationtoken)
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

    static async Task<UdpReceiveResult> ReceiveWithCancellationAsync(
        UdpClient target,
        CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            return await target.ReceiveAsync();
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
}
#endif
