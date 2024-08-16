// ReSharper disable UnusedParameter.Global
// ReSharper disable once MemberCanBePrivate.Global
#if FeatureMemory

#region OverloadResolutionPriority

[TestFixture]
public class OverloadResolutionPriorityAttributeTests
{
    [Test]
    public void Run()
    {
        int[] arr = [1, 2, 3];
        //Prints "Span" because resolution priority is higher
        Method(arr);
    }

    [OverloadResolutionPriority(2)]
    public static void Method(ReadOnlySpan<int> list) =>
        Console.WriteLine("Span");

    [OverloadResolutionPriority(1)]
    public static void Method(int[] list) =>
        Console.WriteLine("Array");
}

#endregion
#endif