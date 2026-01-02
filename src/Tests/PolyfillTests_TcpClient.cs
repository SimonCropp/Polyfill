using System.Net;
using System.Net.Sockets;

partial class PolyfillTests
{
    [Test]
    public async Task TcpClientConnectAsync_IPAddress_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new TcpClient();

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.ConnectAsync(IPAddress.Loopback, 12345, cancel));
    }

    [Test]
    public async Task TcpClientConnectAsync_IPAddressArray_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new TcpClient();
        var addresses = new[] { IPAddress.Loopback };

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.ConnectAsync(addresses, 12345, cancel));
    }

    [Test]
    public async Task TcpClientConnectAsync_Host_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new TcpClient();

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.ConnectAsync("localhost", 12345, cancel));
    }

    [Test]
    public async Task TcpClientConnectAsync_IPEndPoint_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var cancel = new Cancel(true);
        using var client = new TcpClient();
        var endpoint = new IPEndPoint(IPAddress.Loopback, 12345);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            async () => await client.ConnectAsync(endpoint, cancel));
    }
}
