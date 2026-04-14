using System.Net;
using System.Net.Sockets;

partial class PolyfillTests
{
#if FeatureValueTask
    [Test]
    public async Task SocketConnectAsync_EndPoint_PreCancelled_ThrowsOperationCanceledException()
    {
        var cancel = new Cancel(true);
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);

        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await socket.ConnectAsync(endpoint, cancel));
    }

    [Test]
    public async Task SocketConnectAsync_EndPoint_HappyPath()
    {
        using var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(new IPEndPoint(IPAddress.Loopback, 0));
        listener.Listen(1);
        var listenEndpoint = (IPEndPoint) listener.LocalEndPoint!;

        using var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        await client.ConnectAsync(listenEndpoint, default);

        await Assert.That(client.Connected).IsTrue();
    }

    [Test]
    public async Task SocketDisconnectAsync_PreCancelled_ThrowsOperationCanceledException()
    {
        var cancel = new Cancel(true);
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await socket.DisconnectAsync(false, cancel));
    }

    [Test]
    public async Task SocketDisconnectAsync_HappyPath()
    {
        using var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(new IPEndPoint(IPAddress.Loopback, 0));
        listener.Listen(1);
        var listenEndpoint = (IPEndPoint) listener.LocalEndPoint!;

        using var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        await client.ConnectAsync(listenEndpoint, default);
        await client.DisconnectAsync(false, default);

        await Assert.That(client.Connected).IsFalse();
    }

#if FeatureMemory
    [Test]
    public async Task SocketReceiveFromAsync_Memory_PreCancelled_ThrowsOperationCanceledException()
    {
        var cancel = new Cancel(true);
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
        Memory<byte> buffer = new byte[3];
        EndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);

        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await socket.ReceiveFromAsync(buffer, SocketFlags.None, endpoint, cancel));
    }

    [Test]
    public async Task SocketSendToAsync_ReadOnlyMemory_PreCancelled_ThrowsOperationCanceledException()
    {
        var cancel = new Cancel(true);
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ReadOnlyMemory<byte> buffer = new byte[] { 1, 2, 3 };
        EndPoint endpoint = new IPEndPoint(IPAddress.Loopback, 12345);

        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await socket.SendToAsync(buffer, SocketFlags.None, endpoint, cancel));
    }

    [Test]
    public async Task SocketSendToAsync_ReceiveFromAsync_HappyPath_RoundTripsDatagram()
    {
        using var receiver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        receiver.Bind(new IPEndPoint(IPAddress.Loopback, 0));
        var receiverEndpoint = (IPEndPoint) receiver.LocalEndPoint!;

        using var sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ReadOnlyMemory<byte> sendBuffer = new byte[] { 1, 2, 3, 4, 5 };
        var sent = await sender.SendToAsync(sendBuffer, SocketFlags.None, receiverEndpoint, default);
        await Assert.That(sent).IsEqualTo(5);

        Memory<byte> receiveBuffer = new byte[16];
        var result = await receiver.ReceiveFromAsync(
            receiveBuffer,
            SocketFlags.None,
            new IPEndPoint(IPAddress.Any, 0),
            default);

        await Assert.That(result.ReceivedBytes).IsEqualTo(5);
        await Assert.That(receiveBuffer.Slice(0, 5).ToArray()).IsEquivalentTo(new byte[] { 1, 2, 3, 4, 5 });
        await Assert.That(result.RemoteEndPoint).IsNotNull();
    }
#endif
#endif
}
