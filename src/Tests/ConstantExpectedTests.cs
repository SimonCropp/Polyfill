// ReSharper disable IncorrectConstantExpectedAnnotation
#pragma warning disable CA1856
public class ConstantExpectedTests
{
    public void Method([ConstantExpected] int b) { }
    public void MethodWithMinMax([ConstantExpected(Min = -5, Max = 10)] int b) { }

    // Invalid declaration
    // public void InvalidRange([ConstantExpected(Min = 5, Max = -5)] int b) { }

    public void Test(int variableNum)
    {
        // Valid calls
        const int constNum = 10;
        Method(constNum);
        MethodWithMinMax(constNum);
        Method(1);
        MethodWithMinMax(2);

        // Invalid calls

        // CA1857: The argument should be a constant for optimal performance
        // https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca1857
        // Method(variableNum);

        // CA1857: The constant does not fit within the value bounds of '-5' to '10'
        // https://learn.microsoft.com/dotnet/fundamentals/code-analysis/quality-rules/ca1857
        // MethodWithMinMax(15);
    }
}
