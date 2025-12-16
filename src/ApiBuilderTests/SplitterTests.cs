using System.Text.RegularExpressions;

public class SplitterTests
{
    [Test]
    public async Task Run()
    {
        Splitter.Run();

        var splitDir = Splitter.SplitOutputDir;
        await Assert.That(Directory.Exists(splitDir)).IsTrue();

        // Verify all target frameworks have output directories
        foreach (var tfm in Splitter.TargetFrameworks)
        {
            var tfmDir = Path.Combine(splitDir, tfm);
            await Assert.That(Directory.Exists(tfmDir)).IsTrue();

            // Verify at least some files were written
            var files = Directory.GetFiles(tfmDir, "*.cs", SearchOption.AllDirectories);
            await Assert.That(files.Length > 0).IsTrue();

            // Verify no framework monikers remain in any file
            foreach (var file in files)
            {
                var content = File.ReadAllText(file);
                AssertNoFrameworkMonikers(content, file, tfm);
            }
        }
    }

    [Test]
    public async Task ProcessedFiles_HaveWindowsNewlines()
    {
        var source = "line1\nline2\rline3\r\nline4";
        var result = Splitter.NormalizeNewlines(source);

        await Assert.That(result).IsEqualTo("line1\r\nline2\r\nline3\r\nline4");
    }

    [Test]
    public async Task SimpleIfTrue_RemovesDirective()
    {
        var source = """
            before
            #if NET5_0_OR_GREATER
            inside
            #endif
            after
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).DoesNotContain("#if");
        await Assert.That(result).DoesNotContain("#endif");
        await Assert.That(result).Contains("inside");
        await Assert.That(result).Contains("before");
        await Assert.That(result).Contains("after");
    }

    [Test]
    public async Task SimpleIfFalse_RemovesContent()
    {
        var source = """
            before
            #if NET8_0_OR_GREATER
            inside
            #endif
            after
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).DoesNotContain("#if");
        await Assert.That(result).DoesNotContain("#endif");
        await Assert.That(result).DoesNotContain("inside");
        await Assert.That(result).Contains("before");
        await Assert.That(result).Contains("after");
    }

    [Test]
    public async Task UnknownSymbol_PreservedInOutput()
    {
        var source = """
            #if FeatureMemory
            inside
            #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).Contains("#if FeatureMemory");
        await Assert.That(result).Contains("#endif");
        await Assert.That(result).Contains("inside");
    }

    [Test]
    public async Task MixedCondition_SimplifiesExpression()
    {
        // FeatureMemory && !NET6_0_OR_GREATER
        // For net5.0: NET6_0_OR_GREATER is false, so !false = true, result is FeatureMemory && true = FeatureMemory
        var source = """
            #if FeatureMemory && !NET6_0_OR_GREATER
            inside
            #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net5.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).Contains("#if FeatureMemory");
        await Assert.That(result).DoesNotContain("NET6_0_OR_GREATER");
        await Assert.That(result).Contains("#endif");
    }

    [Test]
    public async Task MixedCondition_WhenFrameworkTrue_BecomesSimpler()
    {
        // FeatureMemory && NET5_0_OR_GREATER
        // For net6.0: NET5_0_OR_GREATER is true, so result is FeatureMemory && true = FeatureMemory
        var source = """
            #if FeatureMemory && NET5_0_OR_GREATER
            inside
            #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).Contains("#if FeatureMemory");
        await Assert.That(result).DoesNotContain("NET5_0_OR_GREATER");
    }

    [Test]
    public async Task MixedCondition_WhenFrameworkFalse_EvaluatesToFalse()
    {
        // FeatureMemory && NET8_0_OR_GREATER
        // For net6.0: NET8_0_OR_GREATER is false, so result is FeatureMemory && false = false
        var source = """
            #if FeatureMemory && NET8_0_OR_GREATER
            inside
            #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).DoesNotContain("#if");
        await Assert.That(result).DoesNotContain("inside");
    }

    [Test]
    public async Task OrCondition_WhenFrameworkTrue_IncludesContent()
    {
        // FeatureMemory || NET5_0_OR_GREATER
        // For net6.0: NET5_0_OR_GREATER is true, so result is true
        var source = """
            #if FeatureMemory || NET5_0_OR_GREATER
            inside
            #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).DoesNotContain("#if");
        await Assert.That(result).Contains("inside");
    }

    [Test]
    public async Task OrCondition_WhenFrameworkFalse_KeepsUnknown()
    {
        // FeatureMemory || NET8_0_OR_GREATER
        // For net6.0: NET8_0_OR_GREATER is false, so result is FeatureMemory || false = FeatureMemory
        var source = """
            #if FeatureMemory || NET8_0_OR_GREATER
            inside
            #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).Contains("#if FeatureMemory");
        await Assert.That(result).DoesNotContain("NET8_0_OR_GREATER");
    }

    [Test]
    public async Task IfElse_WhenTrue_IncludesIfBranch()
    {
        var source = """
            #if NET5_0_OR_GREATER
            ifbranch
            #else
            elsebranch
            #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).Contains("ifbranch");
        await Assert.That(result).DoesNotContain("elsebranch");
        await Assert.That(result).DoesNotContain("#if");
        await Assert.That(result).DoesNotContain("#else");
    }

    [Test]
    public async Task IfElse_WhenFalse_IncludesElseBranch()
    {
        var source = """
            #if NET8_0_OR_GREATER
            ifbranch
            #else
            elsebranch
            #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).DoesNotContain("ifbranch");
        await Assert.That(result).Contains("elsebranch");
        await Assert.That(result).DoesNotContain("#if");
        await Assert.That(result).DoesNotContain("#else");
    }

    [Test]
    public async Task IfElse_WhenUnknown_KeepsBoth()
    {
        var source = """
            #if FeatureMemory
            ifbranch
            #else
            elsebranch
            #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).Contains("ifbranch");
        await Assert.That(result).Contains("elsebranch");
        await Assert.That(result).Contains("#if FeatureMemory");
        await Assert.That(result).Contains("#else");
        await Assert.That(result).Contains("#endif");
    }

    [Test]
    public async Task Netcoreapp2x_DefinedForNetcoreapp2_0()
    {
        var symbols = Splitter.GetPreprocessorSymbolsForFramework("netcoreapp2.0");

        await Assert.That(symbols.Contains("NETCOREAPP2X")).IsTrue();
        await Assert.That(symbols.Contains("NETCOREAPP2_0")).IsTrue();
        await Assert.That(symbols.Contains("NETCOREAPP")).IsTrue();
    }

    [Test]
    public async Task Netcoreapp2x_DefinedForNetcoreapp2_1()
    {
        var symbols = Splitter.GetPreprocessorSymbolsForFramework("netcoreapp2.1");

        await Assert.That(symbols.Contains("NETCOREAPP2X")).IsTrue();
        await Assert.That(symbols.Contains("NETCOREAPP2_1")).IsTrue();
        await Assert.That(symbols.Contains("NETCOREAPP")).IsTrue();
    }

    [Test]
    public async Task Netcoreapp2x_DefinedForNetcoreapp2_2()
    {
        var symbols = Splitter.GetPreprocessorSymbolsForFramework("netcoreapp2.2");

        await Assert.That(symbols.Contains("NETCOREAPP2X")).IsTrue();
        await Assert.That(symbols.Contains("NETCOREAPP2_2")).IsTrue();
        await Assert.That(symbols.Contains("NETCOREAPP")).IsTrue();
    }

    [Test]
    public async Task Netcoreapp3x_DefinedForNetcoreapp3_0()
    {
        var symbols = Splitter.GetPreprocessorSymbolsForFramework("netcoreapp3.0");

        await Assert.That(symbols.Contains("NETCOREAPP3X")).IsTrue();
        await Assert.That(symbols.Contains("NETCOREAPP3_0")).IsTrue();
        await Assert.That(symbols.Contains("NETCOREAPPX")).IsTrue();
    }

    [Test]
    public async Task Netcoreappx_DefinedForAllNetcoreapp()
    {
        var symbols20 = Splitter.GetPreprocessorSymbolsForFramework("netcoreapp2.0");
        var symbols31 = Splitter.GetPreprocessorSymbolsForFramework("netcoreapp3.1");

        await Assert.That(symbols20.Contains("NETCOREAPPX")).IsTrue();
        await Assert.That(symbols31.Contains("NETCOREAPPX")).IsTrue();
    }

    [Test]
    public async Task Net46x_DefinedOnlyForNet461And462()
    {
        var symbols461 = Splitter.GetPreprocessorSymbolsForFramework("net461");
        var symbols462 = Splitter.GetPreprocessorSymbolsForFramework("net462");
        var symbols47 = Splitter.GetPreprocessorSymbolsForFramework("net47");

        await Assert.That(symbols461.Contains("NET46X")).IsTrue();
        await Assert.That(symbols462.Contains("NET46X")).IsTrue();
        // net47 should NOT have NET46X - it has NET47X instead
        await Assert.That(symbols47.Contains("NET46X")).IsFalse();
        await Assert.That(symbols47.Contains("NET47X")).IsTrue();
    }

    [Test]
    public async Task Net47x_DefinedForNet47Family()
    {
        var symbols47 = Splitter.GetPreprocessorSymbolsForFramework("net47");
        var symbols471 = Splitter.GetPreprocessorSymbolsForFramework("net471");
        var symbols472 = Splitter.GetPreprocessorSymbolsForFramework("net472");

        await Assert.That(symbols47.Contains("NET47X")).IsTrue();
        await Assert.That(symbols471.Contains("NET47X")).IsTrue();
        await Assert.That(symbols472.Contains("NET47X")).IsTrue();
    }

    [Test]
    public async Task Net48x_DefinedForNet48Family()
    {
        var symbols48 = Splitter.GetPreprocessorSymbolsForFramework("net48");
        var symbols481 = Splitter.GetPreprocessorSymbolsForFramework("net481");

        await Assert.That(symbols48.Contains("NET48X")).IsTrue();
        await Assert.That(symbols481.Contains("NET48X")).IsTrue();
    }

    [Test]
    public async Task NoFrameworkMonikers_InSimplifiedExpression()
    {
        var source = """
            #if NETFRAMEWORK || NETSTANDARD2_0
            inside
            #endif
            """;

        var definedSymbolsNet6 = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var resultNet6 = Splitter.ProcessFile(source, definedSymbolsNet6);

        // For net6.0, both are false, so content should be removed
        await Assert.That(resultNet6).DoesNotContain("NETFRAMEWORK");
        await Assert.That(resultNet6).DoesNotContain("NETSTANDARD");
        await Assert.That(resultNet6).DoesNotContain("inside");
    }

    [Test]
    public async Task NoFrameworkMonikers_InSimplifiedExpression_WithNetFramework()
    {
        var source = """
            #if NETFRAMEWORK || NETSTANDARD2_0
            inside
            #endif
            """;

        var definedSymbolsNet461 = Splitter.GetPreprocessorSymbolsForFramework("net461");
        var resultNet461 = Splitter.ProcessFile(source, definedSymbolsNet461);

        // For net461, NETFRAMEWORK is true, so content should be included without directives
        await Assert.That(resultNet461).DoesNotContain("#if");
        await Assert.That(resultNet461).Contains("inside");
    }

    [Test]
    public async Task NegatedCondition_EvaluatesCorrectly()
    {
        var source = """
            #if !NET6_0_OR_GREATER
            inside
            #endif
            """;

        var definedSymbolsNet5 = Splitter.GetPreprocessorSymbolsForFramework("net5.0");
        var definedSymbolsNet6 = Splitter.GetPreprocessorSymbolsForFramework("net6.0");

        var resultNet5 = Splitter.ProcessFile(source, definedSymbolsNet5);
        var resultNet6 = Splitter.ProcessFile(source, definedSymbolsNet6);

        // For net5.0, !NET6_0_OR_GREATER = !false = true, include content
        await Assert.That(resultNet5).Contains("inside");
        await Assert.That(resultNet5).DoesNotContain("#if");

        // For net6.0, !NET6_0_OR_GREATER = !true = false, exclude content
        await Assert.That(resultNet6).DoesNotContain("inside");
    }

    [Test]
    public async Task NegatedUnknown_PreservesNegation()
    {
        var source = """
            #if !FeatureMemory
            inside
            #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).Contains("#if !FeatureMemory");
        await Assert.That(result).Contains("inside");
    }

    [Test]
    public async Task NestedConditions_HandleCorrectly()
    {
        var source = """
            #if NET5_0_OR_GREATER
            outer_if
            #if NET6_0_OR_GREATER
            inner_if
            #endif
            after_inner
            #endif
            """;

        var definedSymbolsNet5 = Splitter.GetPreprocessorSymbolsForFramework("net5.0");
        var resultNet5 = Splitter.ProcessFile(source, definedSymbolsNet5);

        await Assert.That(resultNet5).Contains("outer_if");
        await Assert.That(resultNet5).DoesNotContain("inner_if");
        await Assert.That(resultNet5).Contains("after_inner");
    }

    [Test]
    public async Task ComplexExpression_SimplifiesCorrectly()
    {
        // (FeatureMemory || NET5_0_OR_GREATER) && !NET8_0_OR_GREATER
        // For net6.0: (FeatureMemory || true) && !false = true && true = true
        var source = """
            #if (FeatureMemory || NET5_0_OR_GREATER) && !NET8_0_OR_GREATER
            inside
            #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        // Should include the content without directives since the whole expression is true
        await Assert.That(result).Contains("inside");
        await Assert.That(result).DoesNotContain("#if");
    }

    [Test]
    public async Task AllKnownSymbols_ContainsXSuffixed()
    {
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("NETCOREAPP2X")).IsTrue();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("NETCOREAPP3X")).IsTrue();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("NETCOREAPPX")).IsTrue();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("NET46X")).IsTrue();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("NET47X")).IsTrue();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("NET48X")).IsTrue();
    }

    [Test]
    public async Task AllKnownSymbols_ContainsFrameworkCategories()
    {
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("NETFRAMEWORK")).IsTrue();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("NETSTANDARD")).IsTrue();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("NETCOREAPP")).IsTrue();
    }

    [Test]
    public async Task FeatureSymbols_NotInKnownSymbols()
    {
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("FeatureMemory")).IsFalse();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("FeatureValueTuple")).IsFalse();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("FeatureValueTask")).IsFalse();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("FeatureHttp")).IsFalse();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("FeatureCompression")).IsFalse();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("PolyPublic")).IsFalse();
        await Assert.That(Splitter.AllKnownFrameworkSymbols.Contains("PolyEnsure")).IsFalse();
    }

    [Test]
    public async Task OutputFiles_ContainNoFrameworkMonikers()
    {
        // Test that after processing, no framework monikers remain in any condition
        var source = """
            #if NETCOREAPP2X || NET47X
            code1
            #endif
            #if NETSTANDARD
            code2
            #endif
            #if NETFRAMEWORK
            code3
            #endif
            """;

        foreach (var tfm in Splitter.TargetFrameworks)
        {
            var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework(tfm);
            var result = Splitter.ProcessFile(source, definedSymbols);

            // Should never contain these framework symbols in the output
            await Assert.That(result).DoesNotContain("NETCOREAPP2X");
            await Assert.That(result).DoesNotContain("NET47X");
            await Assert.That(result).DoesNotContain("NETSTANDARD");
            await Assert.That(result).DoesNotContain("NETFRAMEWORK");
        }
    }

    [Test]
    public async Task XSuffixedSymbol_EvaluatesCorrectly()
    {
        var source = """
            #if NETCOREAPP2X
            inside
            #endif
            """;

        var symbolsNet20 = Splitter.GetPreprocessorSymbolsForFramework("netcoreapp2.0");
        var symbolsNet21 = Splitter.GetPreprocessorSymbolsForFramework("netcoreapp2.1");
        var symbolsNet22 = Splitter.GetPreprocessorSymbolsForFramework("netcoreapp2.2");
        var symbolsNet30 = Splitter.GetPreprocessorSymbolsForFramework("netcoreapp3.0");
        var symbolsNet60 = Splitter.GetPreprocessorSymbolsForFramework("net6.0");

        var resultNet20 = Splitter.ProcessFile(source, symbolsNet20);
        var resultNet21 = Splitter.ProcessFile(source, symbolsNet21);
        var resultNet22 = Splitter.ProcessFile(source, symbolsNet22);
        var resultNet30 = Splitter.ProcessFile(source, symbolsNet30);
        var resultNet60 = Splitter.ProcessFile(source, symbolsNet60);

        // NETCOREAPP2X should be true for netcoreapp2.0, 2.1, and 2.2
        await Assert.That(resultNet20).Contains("inside");
        await Assert.That(resultNet21).Contains("inside");
        await Assert.That(resultNet22).Contains("inside");

        // NETCOREAPP2X should be false for netcoreapp3.0 and net6.0
        await Assert.That(resultNet30).DoesNotContain("inside");
        await Assert.That(resultNet60).DoesNotContain("inside");
    }

    [Test]
    public async Task PreservesIndentation()
    {
        var source = """
                #if FeatureMemory
                    inside
                #endif
            """;

        var definedSymbols = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var result = Splitter.ProcessFile(source, definedSymbols);

        await Assert.That(result).Contains("    #if FeatureMemory");
        await Assert.That(result).Contains("        inside");
    }

    [Test]
    public async Task Net5Plus_DefinesLegacyNetcoreappSymbols()
    {
        // .NET 5+ should define legacy NETCOREAPP symbols for backwards compatibility
        var symbolsNet5 = Splitter.GetPreprocessorSymbolsForFramework("net5.0");
        var symbolsNet6 = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var symbolsNet7 = Splitter.GetPreprocessorSymbolsForFramework("net7.0");

        // All should have NETCOREAPP3_0_OR_GREATER
        await Assert.That(symbolsNet5.Contains("NETCOREAPP3_0_OR_GREATER")).IsTrue();
        await Assert.That(symbolsNet6.Contains("NETCOREAPP3_0_OR_GREATER")).IsTrue();
        await Assert.That(symbolsNet7.Contains("NETCOREAPP3_0_OR_GREATER")).IsTrue();

        // And also NETCOREAPP3_1_OR_GREATER
        await Assert.That(symbolsNet5.Contains("NETCOREAPP3_1_OR_GREATER")).IsTrue();
        await Assert.That(symbolsNet6.Contains("NETCOREAPP3_1_OR_GREATER")).IsTrue();
        await Assert.That(symbolsNet7.Contains("NETCOREAPP3_1_OR_GREATER")).IsTrue();

        // And NETCOREAPP
        await Assert.That(symbolsNet5.Contains("NETCOREAPP")).IsTrue();
        await Assert.That(symbolsNet6.Contains("NETCOREAPP")).IsTrue();
        await Assert.That(symbolsNet7.Contains("NETCOREAPP")).IsTrue();
    }

    [Test]
    public async Task NotNullAttributePattern_UsesTypeForwardForNet5Plus()
    {
        // This pattern is used in nullable attributes - define class for older frameworks,
        // use TypeForwardedTo for newer frameworks
        var source = """
            #if !NETCOREAPP3_0_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
            class NotNullAttribute : Attribute { }
            #else
            [assembly: TypeForwardedTo(typeof(NotNullAttribute))]
            #endif
            """;

        // For net5.0+, NETCOREAPP3_0_OR_GREATER is true, so we should get the TypeForwardedTo branch
        var definedSymbolsNet5 = Splitter.GetPreprocessorSymbolsForFramework("net5.0");
        var resultNet5 = Splitter.ProcessFile(source, definedSymbolsNet5);

        await Assert.That(resultNet5).Contains("TypeForwardedTo");
        await Assert.That(resultNet5).DoesNotContain("class NotNullAttribute");

        // For net461, both conditions are false, so we get the class definition
        var definedSymbolsNet461 = Splitter.GetPreprocessorSymbolsForFramework("net461");
        var resultNet461 = Splitter.ProcessFile(source, definedSymbolsNet461);

        await Assert.That(resultNet461).Contains("class NotNullAttribute");
        await Assert.That(resultNet461).DoesNotContain("TypeForwardedTo");

        // For netstandard2.1, NETSTANDARD2_1_OR_GREATER is true, so we get TypeForwardedTo
        var definedSymbolsNs21 = Splitter.GetPreprocessorSymbolsForFramework("netstandard2.1");
        var resultNs21 = Splitter.ProcessFile(source, definedSymbolsNs21);

        await Assert.That(resultNs21).Contains("TypeForwardedTo");
        await Assert.That(resultNs21).DoesNotContain("class NotNullAttribute");
    }

    [Test]
    public async Task FileCompletelyExcluded_HasMinimalOutput()
    {
        // If the entire file content is excluded, we should get just the header
        var source = """
            // <auto-generated />
            #pragma warning disable

            #if NET8_0_OR_GREATER
            class NewFeature { }
            #endif
            """;

        var definedSymbolsNet6 = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var resultNet6 = Splitter.ProcessFile(source, definedSymbolsNet6);

        await Assert.That(resultNet6).DoesNotContain("class NewFeature");
        await Assert.That(resultNet6).Contains("// <auto-generated />");
    }

    [Test]
    public async Task IndexRangePattern_ExcludedForNet5Plus()
    {
        // Index and Range types are defined in the framework for .NET 5+
        var source = """
            #if !NET5_0_OR_GREATER
            struct Index { }
            struct Range { }
            #endif
            """;

        var definedSymbolsNet5 = Splitter.GetPreprocessorSymbolsForFramework("net5.0");
        var resultNet5 = Splitter.ProcessFile(source, definedSymbolsNet5);

        await Assert.That(resultNet5).DoesNotContain("struct Index");
        await Assert.That(resultNet5).DoesNotContain("struct Range");

        var definedSymbolsNet461 = Splitter.GetPreprocessorSymbolsForFramework("net461");
        var resultNet461 = Splitter.ProcessFile(source, definedSymbolsNet461);

        await Assert.That(resultNet461).Contains("struct Index");
        await Assert.That(resultNet461).Contains("struct Range");
    }

    [Test]
    public async Task PolyfillMethodPattern_ExcludedWhenFrameworkHasIt()
    {
        // Methods like Append should be excluded when the framework has them
        var source = """
            #if !NET471_OR_GREATER && !NETSTANDARD2_0_OR_GREATER && !NETCOREAPP2_0_OR_GREATER
            static IEnumerable<T> Append<T>(this IEnumerable<T> source, T element) { }
            #endif
            """;

        // For net471+, this should be excluded
        var definedSymbolsNet471 = Splitter.GetPreprocessorSymbolsForFramework("net471");
        var resultNet471 = Splitter.ProcessFile(source, definedSymbolsNet471);
        await Assert.That(resultNet471).DoesNotContain("Append");

        // For net6.0 (which has NETCOREAPP2_0_OR_GREATER), this should be excluded
        var definedSymbolsNet6 = Splitter.GetPreprocessorSymbolsForFramework("net6.0");
        var resultNet6 = Splitter.ProcessFile(source, definedSymbolsNet6);
        await Assert.That(resultNet6).DoesNotContain("Append");

        // For net461, this should be included
        var definedSymbolsNet461 = Splitter.GetPreprocessorSymbolsForFramework("net461");
        var resultNet461 = Splitter.ProcessFile(source, definedSymbolsNet461);
        await Assert.That(resultNet461).Contains("Append");
    }

    [Test]
    public async Task XSuffixed_OnlyForSpecificFamily()
    {
        // NET46X should only be true for net461/net462, not net47+
        var source = """
            #if NET46X || NET47
            inside
            #endif
            """;

        // net461 - NET46X is true
        var result461 = Splitter.ProcessFile(source, Splitter.GetPreprocessorSymbolsForFramework("net461"));
        await Assert.That(result461).Contains("inside");

        // net462 - NET46X is true
        var result462 = Splitter.ProcessFile(source, Splitter.GetPreprocessorSymbolsForFramework("net462"));
        await Assert.That(result462).Contains("inside");

        // net47 - NET47 is true
        var result47 = Splitter.ProcessFile(source, Splitter.GetPreprocessorSymbolsForFramework("net47"));
        await Assert.That(result47).Contains("inside");

        // net471 - neither NET46X nor NET47 is true (NET47X is true but that's different)
        var result471 = Splitter.ProcessFile(source, Splitter.GetPreprocessorSymbolsForFramework("net471"));
        await Assert.That(result471).DoesNotContain("inside");

        // net472 - neither NET46X nor NET47 is true
        var result472 = Splitter.ProcessFile(source, Splitter.GetPreprocessorSymbolsForFramework("net472"));
        await Assert.That(result472).DoesNotContain("inside");

        // net48 - neither NET46X nor NET47 is true (NET48X is true but that's different)
        var result48 = Splitter.ProcessFile(source, Splitter.GetPreprocessorSymbolsForFramework("net48"));
        await Assert.That(result48).DoesNotContain("inside");
    }

    [Test]
    public async Task Net46x_OnlyForNet461And462()
    {
        var symbols461 = Splitter.GetPreprocessorSymbolsForFramework("net461");
        var symbols462 = Splitter.GetPreprocessorSymbolsForFramework("net462");
        var symbols47 = Splitter.GetPreprocessorSymbolsForFramework("net47");
        var symbols471 = Splitter.GetPreprocessorSymbolsForFramework("net471");
        var symbols48 = Splitter.GetPreprocessorSymbolsForFramework("net48");

        // NET46X should be true for net461 and net462
        await Assert.That(symbols461.Contains("NET46X")).IsTrue();
        await Assert.That(symbols462.Contains("NET46X")).IsTrue();

        // NET46X should be false for net47+
        await Assert.That(symbols47.Contains("NET46X")).IsFalse();
        await Assert.That(symbols471.Contains("NET46X")).IsFalse();
        await Assert.That(symbols48.Contains("NET46X")).IsFalse();
    }

    /// <summary>
    /// Asserts that no framework monikers appear in the processed content.
    /// </summary>
    static void AssertNoFrameworkMonikers(string content, string filePath, string targetFramework)
    {
        // Check for specific framework version symbols
        var monikerPatterns = new[]
        {
            @"\bNET\d+_\d+\b",
            @"\bNET\d+_\d+_OR_GREATER\b",
            @"\bNETCOREAPP\d+_\d+\b",
            @"\bNETCOREAPP\d+_\d+_OR_GREATER\b",
            @"\bNETSTANDARD\d+_\d+\b",
            @"\bNETSTANDARD\d+_\d+_OR_GREATER\b",
            @"\bNETCOREAPP\d+X\b",
            @"\bNET\d+X\b",
            @"\bNETCOREAPPX\b",
            @"\bNETFRAMEWORK\b",
            @"\bNETSTANDARD\b(?!_)",
            @"\bNETCOREAPP\b(?!_)"
        };

        foreach (var pattern in monikerPatterns)
        {
            var matches = Regex.Matches(content, pattern);
            foreach (Match match in matches)
            {
                // Check if it's inside a string literal or comment (which is OK)
                var lineStart = content.LastIndexOf('\n', match.Index) + 1;
                var lineEnd = content.IndexOf('\n', match.Index);
                if (lineEnd == -1) lineEnd = content.Length;
                var line = content.Substring(lineStart, lineEnd - lineStart);

                // Skip if it's in a comment
                if (line.TrimStart().StartsWith("//") || line.Contains("/*"))
                {
                    continue;
                }

                // Skip if it's in a string literal (basic check)
                var posInLine = match.Index - lineStart;
                var quotesBeforeMatch = line.Substring(0, posInLine).Count(c => c == '"');
                if (quotesBeforeMatch % 2 == 1)
                {
                    continue;
                }

                // This is a framework moniker in a preprocessor directive - fail
                throw new Exception(
                    $"Framework moniker '{match.Value}' found in {filePath} for target framework {targetFramework}:\n" +
                    $"Line: {line.Trim()}");
            }
        }
    }
}
