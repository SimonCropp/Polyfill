partial class PolyfillTests
{
    [Test]
    public void Stack_TryPeek()
    {
        var stack = new Stack<int>();

        // Test when stack is empty
        var result = stack.TryPeek(out var value);
        Assert.IsFalse(result);
        Assert.AreEqual(0, value);

        // Test when stack has elements
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        result = stack.TryPeek(out value);
        Assert.IsTrue(result);
        // The top element should be 3
        Assert.AreEqual(3, value);

        // Ensure the stack is not modified
        Assert.AreEqual(3, stack.Count);
    }

    [Test]
    public void Stack_TryPop()
    {
        var stack = new Stack<int>();

        // Test when stack is empty
        var result = stack.TryPop(out var value);
        Assert.IsFalse(result);
        Assert.AreEqual(0, value);

        // Test when stack has elements
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        result = stack.TryPop(out value);
        Assert.IsTrue(result);
        // The top element should be 3
        Assert.AreEqual(3, value);

        // Ensure the stack is modified
        Assert.AreEqual(2, stack.Count);
        Assert.AreEqual(2, stack.Peek());
    }
}