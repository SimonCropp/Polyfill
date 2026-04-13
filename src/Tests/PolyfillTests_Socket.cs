using System.Net;
using System.Net.Sockets;

partial class PolyfillTests
{
#if FeatureValueTask
    [Test]
    public async Task SocketConnectAsync_EndPoint_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await socket.ConnectAsync(endpoint, cancel));
    }

    [Test]
    public async Task SocketDisconnectAsync_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await socket.DisconnectAsync(false, cancel));
    }

#if FeatureMemory
    [Test]
    public async Task SocketReceiveFromAsync_Memory_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
        Memory<byte> buffer = new byte[3];
        EndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await socket.ReceiveFromAsync(buffer, SocketFlags.None, endpoint, cancel));
    }

    [Test]
    public async Task SocketSendToAsync_ReadOnlyMemory_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ReadOnlyMemory<byte> buffer = new byte[] { 1, 2, 3 };
        EndPoint endpoint = new IPEndPoint(IPAddress.Loopback, 12345);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await socket.SendToAsync(buffer, SocketFlags.None, endpoint, cancel));
    }
#endif
#endif
}
