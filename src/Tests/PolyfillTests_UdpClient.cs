using System.Net;
using System.Net.Sockets;

partial class PolyfillTests
{
    [Test]
    public async Task UdpClientReceiveAsync_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new UdpClient(0);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.ReceiveAsync(cancel));
    }

    [Test]
    public async Task UdpClientSendAsync_ReadOnlyMemory_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new UdpClient("localhost", 12345);
        var data = new ReadOnlyMemory<byte>([1, 2, 3]);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.SendAsync(data, cancel));
    }

    [Test]
    public async Task UdpClientSendAsync_ReadOnlyMemoryWithEndPoint_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new UdpClient(0);
        var data = new ReadOnlyMemory<byte>([1, 2, 3]);
        var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.SendAsync(data, endpoint, cancel));
    }

    [Test]
    public async Task UdpClientSendAsync_ReadOnlyMemoryWithHostPort_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new UdpClient(0);
        var data = new ReadOnlyMemory<byte>([1, 2, 3]);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.SendAsync(data, "localhost", 12345, cancel));
    }
}
