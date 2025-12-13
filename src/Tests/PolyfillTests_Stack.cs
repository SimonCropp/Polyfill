partial class PolyfillTests
{
    [Test]
    public async Task Stack_TryPeek()
    {
        var stack = new Stack<int>();

        // Test when stack is empty
        var result = stack.TryPeek(out var value);
        await Assert.That(result).IsFalse();
        await Assert.That(value).IsEqualTo(0);

        // Test when stack has elements
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        result = stack.TryPeek(out value);
        await Assert.That(result).IsTrue();
        // The top element should be 3
        await Assert.That(value).IsEqualTo(3);

        // Ensure the stack is not modified
        await Assert.That(stack.Count).IsEqualTo(3);
    }

    [Test]
    public async Task Stack_TryPop()
    {
        var stack = new Stack<int>();

        // Test when stack is empty
        var result = stack.TryPop(out var value);
        await Assert.That(result).IsFalse();
        await Assert.That(value).IsEqualTo(0);

        // Test when stack has elements
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        result = stack.TryPop(out value);
        await Assert.That(result).IsTrue();
        // The top element should be 3
        await Assert.That(value).IsEqualTo(3);

        // Ensure the stack is modified
        await Assert.That(stack.Count).IsEqualTo(2);
        await Assert.That(stack.Peek()).IsEqualTo(2);
    }
}