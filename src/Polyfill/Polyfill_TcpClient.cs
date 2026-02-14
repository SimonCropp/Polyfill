#if FeatureValueTask

namespace Polyfills;

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if !NET5_0_OR_GREATER

    /// <summary>
    /// Connects the client to a remote TCP host using the specified IP address and port number as an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.connectasync?view=net-11.0#system-net-sockets-tcpclient-connectasync(system-net-ipaddress-system-int32-system-threading-cancellationtoken)
    public static ValueTask ConnectAsync(
        this TcpClient target,
        IPAddress address,
        int port,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask(Task.FromCanceled(cancellationToken));
        }

        return new ValueTask(ConnectWithCancellationAsync(target, address, port, cancellationToken));
    }

    /// <summary>
    /// Connects the client to a remote TCP host using the specified IP addresses and port number as an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.connectasync?view=net-11.0#system-net-sockets-tcpclient-connectasync(system-net-ipaddress()-system-int32-system-threading-cancellationtoken)
    public static ValueTask ConnectAsync(
        this TcpClient target,
        IPAddress[] addresses,
        int port,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask(Task.FromCanceled(cancellationToken));
        }

        return new ValueTask(ConnectWithCancellationAsync(target, addresses, port, cancellationToken));
    }

    /// <summary>
    /// Connects the client to a remote TCP host using the specified host and port number as an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.connectasync?view=net-11.0#system-net-sockets-tcpclient-connectasync(system-string-system-int32-system-threading-cancellationtoken)
    public static ValueTask ConnectAsync(
        this TcpClient target,
        string host,
        int port,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask(Task.FromCanceled(cancellationToken));
        }

        return new ValueTask(ConnectWithCancellationAsync(target, host, port, cancellationToken));
    }

    static async Task ConnectWithCancellationAsync(
        TcpClient target,
        IPAddress address,
        int port,
        CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            await target.ConnectAsync(address, port);
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

    static async Task ConnectWithCancellationAsync(
        TcpClient target,
        IPAddress[] addresses,
        int port,
        CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            await target.ConnectAsync(addresses, port);
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

    static async Task ConnectWithCancellationAsync(
        TcpClient target,
        string host,
        int port,
        CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            await target.ConnectAsync(host, port);
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

#if !NET6_0_OR_GREATER

    /// <summary>
    /// Connects the client to a remote TCP host using the specified endpoint as an asynchronous operation.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient.connectasync?view=net-11.0#system-net-sockets-tcpclient-connectasync(system-net-ipendpoint-system-threading-cancellationtoken)
    public static ValueTask ConnectAsync(
        this TcpClient target,
        IPEndPoint remoteEP,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ValueTask(Task.FromCanceled(cancellationToken));
        }

        return new ValueTask(ConnectWithCancellationIPEndPointAsync(target, remoteEP, cancellationToken));
    }

    static async Task ConnectWithCancellationIPEndPointAsync(
        TcpClient target,
        IPEndPoint remoteEP,
        CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(() => target.Close());
        try
        {
            await target.ConnectAsync(remoteEP.Address, remoteEP.Port);
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
