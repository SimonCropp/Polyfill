using System.Collections.Generic;
using System.IO;
using System.Linq;

[TestFixture]
public class FrameworkSplitterTest
{
    static string solutionDirectory;
    static string polyfillSourceDir;
    static string splitOutputDir;

    static FrameworkSplitterTest()
    {
        solutionDirectory = SolutionDirectoryFinder.Find();
        polyfillSourceDir = Path.Combine(solutionDirectory, "Polyfill");
        splitOutputDir = Path.Combine(solutionDirectory, "Split");
    }

    [Test]
    public void SplitPolyfillsByTargetFramework()
    {
        var frameworks = GetAllTargetFrameworks();

        // Get all .cs files in the Polyfill directory
        var sourceFiles = Directory.GetFiles(polyfillSourceDir, "*.cs", SearchOption.AllDirectories)
            .Where(f => !f.Contains("\\bin\\") && !f.Contains("\\obj\\"))
            .ToList();

        Console.WriteLine($"Processing {sourceFiles.Count} source files for {frameworks.Count} frameworks");

        foreach (var framework in frameworks)
        {
            Console.WriteLine($"\nProcessing framework: {framework.Moniker}");
            ProcessFramework(framework, sourceFiles);
        }

        Console.WriteLine($"\nCompleted! Output written to: {splitOutputDir}");
    }

    void ProcessFramework(FrameworkIdentifier framework, List<string> sourceFiles)
    {
        var frameworkOutputDir = Path.Combine(splitOutputDir, framework.Moniker);

        // Create framework output directory
        if (Directory.Exists(frameworkOutputDir))
        {
            Directory.Delete(frameworkOutputDir, recursive: true);
        }
        Directory.CreateDirectory(frameworkOutputDir);

        foreach (var sourceFile in sourceFiles)
        {
            ProcessFile(sourceFile, framework, frameworkOutputDir);
        }
    }

    void ProcessFile(string sourceFile, FrameworkIdentifier framework, string frameworkOutputDir)
    {
        var code = File.ReadAllText(sourceFile);

        var frameworkSymbols = framework.Directives;
        var sharedSymbols = GetSharedSymbols();

        // Parse with ALL symbols (framework + shared)
        // This makes all code active (both framework and feature code)
        var allSymbols = frameworkSymbols.Concat(sharedSymbols).ToList();
        var parseOptions = CSharpParseOptions.Default.WithPreprocessorSymbols(allSymbols);
        var syntaxTree = CSharpSyntaxTree.ParseText(code, parseOptions);

        // Get the root and remove only framework-related directives
        // Feature directives will be kept
        var root = syntaxTree.GetRoot();
        var processedRoot = RemoveFrameworkDirectives(root, frameworkSymbols);

        // Check if the file has any meaningful code
        if (!HasMeaningfulCode(processedRoot))
        {
            // Skip files with no actual code
            var relativePath = Path.GetRelativePath(polyfillSourceDir, sourceFile);
            Console.WriteLine($"  Skipped (no code): {relativePath}");
            return;
        }

        // Get the relative path and recreate directory structure
        var relativePath2 = Path.GetRelativePath(polyfillSourceDir, sourceFile);
        var outputPath = Path.Combine(frameworkOutputDir, relativePath2);

        // Create subdirectories if needed
        var outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Write the processed code
        var processedCode = processedRoot.ToFullString();
        File.WriteAllText(outputPath, processedCode);

        Console.WriteLine($"  Processed: {relativePath2}");
    }

    bool HasMeaningfulCode(SyntaxNode root)
    {
        // Check if the file has any actual code elements (not just directives/comments)
        var hasMembers = root.DescendantNodes()
            .Any(node => node is MemberDeclarationSyntax or StatementSyntax or UsingDirectiveSyntax);

        return hasMembers;
    }

    SyntaxNode RemoveFrameworkDirectives(SyntaxNode root, List<string> frameworkSymbols)
    {
        // Remove only framework-related preprocessor directives while keeping feature directives
        var rewriter = new DirectiveRemovalRewriter(frameworkSymbols);
        return rewriter.Visit(root);
    }

    List<string> GetSharedSymbols() =>
        [
            "FeatureMemory",
            "FeatureRuntimeInformation",
            "PolyEnsure",
            "PolyArgumentExceptions",
            "PolyPublic",
            "FeatureHttp",
            "PolyNullability",
            "AllowUnsafeBlocks",
            "FeatureValueTask",
            "FeatureValueTuple",
            "FeatureCompression"
        ];

    List<FrameworkIdentifier> GetAllTargetFrameworks() =>
        [
            // .NET Framework
            new()
            {
                Moniker = "net461",
                Directives =
                [
                    "NET461",
                    "NET461_OR_GREATER",
                    "NET46X",
                    "NETFRAMEWORK",
                ]
            },
            new()
            {
                Moniker = "net462",
                Directives =
                [
                    "NET462",
                    "NET462_OR_GREATER",
                    "NET46X",
                    "NET461_OR_GREATER",
                    "NETFRAMEWORK",
                ]
            },
            new()
            {
                Moniker = "net47",
                Directives =
                [
                    "NET47",
                    "NET47_OR_GREATER",
                    "NET47X",
                    "NET462_OR_GREATER",
                    "NET461_OR_GREATER",
                    "NETFRAMEWORK",
                ]
            },
            new()
            {
                Moniker = "net471",
                Directives =
                [
                    "NET471",
                    "NET471_OR_GREATER",
                    "NET47_OR_GREATER",
                    "NET47X",
                    "NET462_OR_GREATER",
                    "NET461_OR_GREATER",
                    "NETFRAMEWORK",
                ]
            },
            new()
            {
                Moniker = "net472",
                Directives =
                [
                    "NET472",
                    "NET472_OR_GREATER",
                    "NET471_OR_GREATER",
                    "NET47_OR_GREATER",
                    "NET47X",
                    "NET462_OR_GREATER",
                    "NET461_OR_GREATER",
                    "NETFRAMEWORK",
                ]
            },
            new()
            {
                Moniker = "net48",
                Directives =
                [
                    "NET48",
                    "NET48_OR_GREATER",
                    "NET472_OR_GREATER",
                    "NET471_OR_GREATER",
                    "NET47_OR_GREATER",
                    "NET47X",
                    "NET462_OR_GREATER",
                    "NET461_OR_GREATER",
                    "NETFRAMEWORK",
                ]
            },
            new()
            {
                Moniker = "net481",
                Directives =
                [
                    "NET481",
                    "NET48X",
                    "NET481_OR_GREATER",
                    "NET48_OR_GREATER",
                    "NET472_OR_GREATER",
                    "NET471_OR_GREATER",
                    "NET47_OR_GREATER",
                    "NET462_OR_GREATER",
                    "NET461_OR_GREATER",
                    "NETFRAMEWORK",
                ]
            },

            // .NET Standard
            new()
            {
                Moniker = "netstandard2.0",
                Directives =
                [
                    "NETSTANDARD2_0",
                    "NETSTANDARD",
                    "NETSTANDARD2_0_OR_GREATER"
                ]
            },
            new()
            {
                Moniker = "netstandard2.1",
                Directives =
                [
                    "NETSTANDARD2_1",
                    "NETSTANDARD",
                    "NETSTANDARD2_1_OR_GREATER",
                    "NETSTANDARD2_0_OR_GREATER"
                ]
            },

            // .NET Core
            new()
            {
                Moniker = "netcoreapp2.0",
                Directives =
                [
                    "NETCOREAPP2_0",
                    "NETCOREAPP2X",
                    "NETCOREAPP",
                    "NETCOREAPP2_0_OR_GREATER"
                ]
            },
            new()
            {
                Moniker = "netcoreapp2.1",
                Directives =
                [
                    "NETCOREAPP2_1",
                    "NETCOREAPP2X",
                    "NETCOREAPP",
                    "NETCOREAPP2_1_OR_GREATER",
                    "NETCOREAPP2_0_OR_GREATER"
                ]
            },
            new()
            {
                Moniker = "netcoreapp2.2",
                Directives =
                [
                    "NETCOREAPP2_2",
                    "NETCOREAPP2X",
                    "NETCOREAPP",
                    "NETCOREAPP2_2_OR_GREATER",
                    "NETCOREAPP2_1_OR_GREATER",
                    "NETCOREAPP2_0_OR_GREATER"
                ]
            },
            new()
            {
                Moniker = "netcoreapp3.0",
                Directives =
                [
                    "NETCOREAPP3_0",
                    "NETCOREAPP3X",
                    "NETCOREAPP",
                    "NETCOREAPP3_0_OR_GREATER",
                    "NETCOREAPP2_2_OR_GREATER",
                    "NETCOREAPP2_1_OR_GREATER",
                    "NETCOREAPP2_0_OR_GREATER"
                ]
            },
            new()
            {
                Moniker = "netcoreapp3.1",
                Directives =
                [
                    "NETCOREAPP3_1",
                    "NETCOREAPP3X",
                    "NETCOREAPP",
                    "NETCOREAPP3_1_OR_GREATER",
                    "NETCOREAPP3_0_OR_GREATER",
                    "NETCOREAPP2_2_OR_GREATER",
                    "NETCOREAPP2_1_OR_GREATER",
                    "NETCOREAPP2_0_OR_GREATER"
                ]
            },

            // .NET 5+
            new()
            {
                Moniker = "net5.0",
                Directives =
                [
                    "NET",
                    "NET5_0",
                    "NET5_0_OR_GREATER"
                ]
            },
            new()
            {
                Moniker = "net6.0",
                Directives =
                [
                    "NET",
                    "NET6_0",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER"
                ]
            },
            new()
            {
                Moniker = "net6.0-windows",
                Directives =
                [
                    "NET",
                    "NET6_0",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER",
                    "WINDOWS"
                ]
            },
            new()
            {
                Moniker = "net7.0",
                Directives =
                [
                    "NET",
                    "NET7_0",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER"
                ]
            },
            new()
            {
                Moniker = "net8.0",
                Directives =
                [
                    "NET",
                    "NET8_0",
                    "NET8_0_OR_GREATER",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER"
                ]
            },
            new()
            {
                Moniker = "net9.0",
                Directives =
                [
                    "NET",
                    "NET9_0",
                    "NET9_0_OR_GREATER",
                    "NET8_0_OR_GREATER",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER"
                ]
            },
            new()
            {
                Moniker = "net10.0",
                Directives =
                [
                    "NET",
                    "NET10_0",
                    "NET10_0_OR_GREATER",
                    "NET9_0_OR_GREATER",
                    "NET8_0_OR_GREATER",
                    "NET7_0_OR_GREATER",
                    "NET6_0_OR_GREATER",
                    "NET5_0_OR_GREATER"
                ]
            }
        ];

    class FrameworkIdentifier
    {
        public required string Moniker { get; init; }
        public required List<string> Directives { get; init; }
    }

    class DirectiveRemovalRewriter : CSharpSyntaxRewriter
    {
        readonly HashSet<string> frameworkSymbols;
        readonly Stack<bool> directiveStack = new();
        readonly HashSet<string> frameworkRelatedKeywords;

        public DirectiveRemovalRewriter(List<string> frameworkSymbols) : base(visitIntoStructuredTrivia: false)
        {
            this.frameworkSymbols = frameworkSymbols.ToHashSet();

            // Keywords that indicate framework-specific conditional compilation
            // These are the primary framework identifiers
            frameworkRelatedKeywords =
            [
                "NETFRAMEWORK",
                "NETSTANDARD",
                "NETCOREAPP",
                "NET461", "NET462", "NET46X", "NET47", "NET471", "NET47X", "NET472", "NET48", "NET48X", "NET481",
                "NET5_0", "NET6_0", "NET7_0", "NET8_0", "NET9_0", "NET10_0",
                "NETCOREAPP2_0", "NETCOREAPP2_1", "NETCOREAPP2_2", "NETCOREAPP2X",
                "NETCOREAPP3_0", "NETCOREAPP3_1", "NETCOREAPP3X",
                "NETSTANDARD2_0", "NETSTANDARD2_1",
                "NET5_0_OR_GREATER", "NET6_0_OR_GREATER", "NET7_0_OR_GREATER",
                "NET8_0_OR_GREATER", "NET9_0_OR_GREATER", "NET10_0_OR_GREATER",
                "NET461_OR_GREATER", "NET462_OR_GREATER", "NET47_OR_GREATER", "NET471_OR_GREATER",
                "NET472_OR_GREATER", "NET48_OR_GREATER", "NET481_OR_GREATER",
                "NETCOREAPP2_0_OR_GREATER", "NETCOREAPP2_1_OR_GREATER", "NETCOREAPP2_2_OR_GREATER",
                "NETCOREAPP3_0_OR_GREATER", "NETCOREAPP3_1_OR_GREATER",
                "NETSTANDARD2_0_OR_GREATER", "NETSTANDARD2_1_OR_GREATER",
                "WINDOWS"
            ];
        }

        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            // Process leading trivia
            var newLeadingTrivia = ProcessTrivia(token.LeadingTrivia);

            // Process trailing trivia
            var newTrailingTrivia = ProcessTrivia(token.TrailingTrivia);

            return token
                .WithLeadingTrivia(newLeadingTrivia)
                .WithTrailingTrivia(newTrailingTrivia);
        }

        SyntaxTriviaList ProcessTrivia(SyntaxTriviaList triviaList)
        {
            var newTrivia = new List<SyntaxTrivia>();

            foreach (var trivia in triviaList)
            {
                // Remove //Link: comment lines
                if (trivia.IsKind(SyntaxKind.SingleLineCommentTrivia))
                {
                    var commentText = trivia.ToString();
                    if (commentText.StartsWith("//Link:", StringComparison.OrdinalIgnoreCase))
                    {
                        continue; // Remove Link comments
                    }
                }

                // Remove disabled text and skipped tokens from framework conditionals
                // Since we parsed with all symbols, disabled text only exists where symbols aren't defined
                // This happens for the #else branches of framework conditionals
                if (trivia.IsKind(SyntaxKind.DisabledTextTrivia) || trivia.IsKind(SyntaxKind.SkippedTokensTrivia))
                {
                    // Check if this disabled text is part of a framework conditional
                    // Only check the INNERMOST directive (Peek), not any outer ones
                    var insideFrameworkBlock = directiveStack.Count > 0 && directiveStack.Peek();
                    if (insideFrameworkBlock)
                    {
                        continue; // Remove disabled text from framework blocks
                    }
                    // Keep disabled text from feature blocks (though there shouldn't be any since we parse with all symbols)
                }

                var shouldRemove = ShouldRemoveDirective(trivia);

                if (shouldRemove)
                {
                    // Skip this trivia (remove it)
                    continue;
                }

                newTrivia.Add(trivia);
            }

            return SyntaxFactory.TriviaList(newTrivia);
        }

        bool ShouldRemoveDirective(SyntaxTrivia trivia)
        {
            if (!trivia.HasStructure)
            {
                return false;
            }

            var structure = trivia.GetStructure();

            // Remove any inactive directives (these are inside disabled blocks)
            if (structure is DirectiveTriviaSyntax directive && !directive.IsActive)
            {
                return true;
            }

            // Check if it's an #if directive
            if (structure is IfDirectiveTriviaSyntax ifDirective)
            {
                var isFrameworkRelated = IsFrameworkRelated(ifDirective.Condition.ToString());
                directiveStack.Push(isFrameworkRelated);
                return isFrameworkRelated;
            }

            // Check if it's an #elif directive
            if (structure is ElifDirectiveTriviaSyntax elifDirective)
            {
                // #elif is part of the current directive block
                if (directiveStack.Count > 0 && directiveStack.Peek())
                {
                    return true;
                }
                return false;
            }

            // Check if it's an #else directive
            if (structure is ElseDirectiveTriviaSyntax)
            {
                // #else is part of the current directive block
                if (directiveStack.Count > 0 && directiveStack.Peek())
                {
                    return true;
                }
                return false;
            }

            // Check if it's an #endif directive
            if (structure is EndIfDirectiveTriviaSyntax)
            {
                // #endif closes the current directive block
                if (directiveStack.Count > 0)
                {
                    var wasFrameworkRelated = directiveStack.Pop();
                    return wasFrameworkRelated;
                }
                return false;
            }

            return false;
        }

        bool IsFrameworkRelated(string condition)
        {
            // Normalize the condition
            var normalizedCondition = condition.Replace(" ", "").ToUpperInvariant();

            // Check if the condition contains any framework-related keywords
            // We need to check for exact matches or with operators
            foreach (var keyword in frameworkRelatedKeywords)
            {
                if (normalizedCondition.Contains(keyword.ToUpperInvariant(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            // Also check if it references "NET" without other qualifiers (like in "#if !NET")
            // Be careful not to match NETSTANDARD, NETFRAMEWORK, etc.
            if (System.Text.RegularExpressions.Regex.IsMatch(normalizedCondition, @"\bNET\b") &&
                !normalizedCondition.Contains("NETSTANDARD") &&
                !normalizedCondition.Contains("NETFRAMEWORK") &&
                !normalizedCondition.Contains("NETCOREAPP"))
            {
                return true;
            }

            return false;
        }
    }
}
