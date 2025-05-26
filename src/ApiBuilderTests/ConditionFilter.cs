public class ConditionalCompilationFilterTests
{
    [Fact]
    public Task FilterCodeByMultipleTargetFrameworkOr()
    {
        var sourceCode =
            """
            #if NET6_0 || FeatureMemory
            Console.WriteLine("NET6_0");
            #else
            Console.WriteLine("Exclude");
            #endif
            """;
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        return Verify(result);
    }
    [Fact]
    public Task FilterCodeByMultipleTargetFrameworkAnd()
    {
        var sourceCode =
            """
            #if NET6_0 && FeatureMemory
            Console.WriteLine("Exclude");
            #else
            Console.WriteLine("Include");
            #endif

            """;
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        return Verify(result);
    }
    [Fact]
    public Task FilterCodeByTargetFramework_ShouldKeepMatchingCode()
    {
        var sourceCode =
            """

            #if NET6_0
            Console.WriteLine("NET6_0");
            #endif

            """;
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        return Verify(result);
    }

    [Fact]
    public Task FilterCodeByTargetFramework_ShouldRemoveNonMatchingCode()
    {
        var sourceCode =
            """

            #if NET5_0
            Console.WriteLine("NET5_0");
            #endif

            """;
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        return Verify(result);
    }

    [Fact]
    public Task FilterCodeByTargetFramework_ShouldHandleElseBlocks()
    {
        var sourceCode =
            """

            #if NET6_0
            Console.WriteLine("NET6_0");
            #else
            Console.WriteLine("Other");
            #endif

            """;
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        return Verify(result);
    }

    [Fact]
    public Task FilterCodeByTargetFramework_ShouldHandleNestedDirectives()
    {
        var sourceCode =
            """

            #if NET6_0
            Console.WriteLine("NET6_0");
            #if DEBUG
            Console.WriteLine("Debug");
            #endif
            #endif

            """;
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        return Verify(result);
    }

    [Fact]
    public void FilterCodeByTargetFramework_ShouldHandleEmptySourceCode()
    {
        var sourceCode = "";
        var targetFramework = "NET6_0";

        var result = ConditionalCompilationFilter.FilterCodeByTargetFramework(sourceCode, targetFramework);

        Assert.Empty(result);
    }
}