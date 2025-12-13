// ReSharper disable UnusedParameter.Global
#if FeatureMemory

#region OverloadResolutionPriority

public class OverloadResolutionPriorityAttributeTests
{
    [Test]
    public Task Run()
    {
        int[] arr = [1, 2, 3];
        //Prints "Span" because resolution priority is higher
        Method(arr);
        return Task.CompletedTask;
    }

    [OverloadResolutionPriority(2)]
    static void Method(ReadOnlySpan<int> list) =>
        Console.WriteLine("Span");

    [OverloadResolutionPriority(1)]
    static void Method(int[] list) =>
        Console.WriteLine("Array");
}

#endregion
#endif
