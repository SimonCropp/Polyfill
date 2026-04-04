#if FeatureMemory

public class TaskPolyfillTests
{
    [Test]
    public async Task WhenAll_Span()
    {
        var taskArray = new[] { Task.CompletedTask, Task.CompletedTask };
        var result = Task.WhenAll(new ReadOnlySpan<Task>(taskArray));
        await result;
    }

    [Test]
    public async Task WhenAll_Generic_Span()
    {
        var taskArray = new[] { Task.FromResult(1), Task.FromResult(2) };
        var result = Task.WhenAll(new ReadOnlySpan<Task<int>>(taskArray));
        var results = await result;
        await Assert.That(results).Count().IsEqualTo(2);
        await Assert.That(results[0]).IsEqualTo(1);
        await Assert.That(results[1]).IsEqualTo(2);
    }

    [Test]
    public async Task WhenAny_Span()
    {
        var taskArray = new[] { Task.CompletedTask, Task.CompletedTask };
        var result = Task.WhenAny(new ReadOnlySpan<Task>(taskArray));
        var completed = await result;
        await Assert.That(completed.IsCompleted).IsTrue();
    }

    [Test]
    public async Task WhenAny_Generic_Span()
    {
        var taskArray = new[] { Task.FromResult(42), Task.FromResult(99) };
        var result = Task.WhenAny(new ReadOnlySpan<Task<int>>(taskArray));
        var completed = await result;
        await Assert.That(completed.IsCompleted).IsTrue();
    }
}

#endif
