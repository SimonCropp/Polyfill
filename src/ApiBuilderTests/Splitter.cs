/// <summary>
/// Splits Polyfill source files by target framework, evaluating conditional compilations.
/// Unknown symbols (like Feature*) are preserved in the output.
/// </summary>
public class Splitter
{
    static string polyfillDir = Path.Combine(ProjectFiles.SolutionDirectory, "Polyfill");

    public static string SplitOutputDir = Path.Combine(ProjectFiles.SolutionDirectory, "Split");

    public static readonly string[] Frameworks =
    [
        "net461",
        "net462",
        "net47",
        "net471",
        "net472",
        "net48",
        "net481",
        "netstandard2.0",
        "netstandard2.1",
        "netcoreapp2.0",
        "netcoreapp2.1",
        "netcoreapp2.2",
        "netcoreapp3.0",
        "netcoreapp3.1",
        "net5.0",
        "net6.0",
        "net7.0",
        "net8.0",
        "net9.0",
        "net10.0",
        "net11.0",
        "uap10.0"
    ];

    /// <summary>
    /// All known framework monikers that should be evaluated (not left in output).
    /// This includes all the _OR_GREATER variants and X-suffixed monikers.
    /// </summary>
    public static readonly HashSet<string> AllKnownFrameworkSymbols = BuildAllKnownSymbols();

    static HashSet<string> BuildAllKnownSymbols()
    {
        var symbols = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "NET",
            "NETCOREAPP2X",
            "NETCOREAPP3X",
            "NETCOREAPPX",
            "NET46X",
            "NET47X",
            "NET48X",
            "NETCOREAPP",
            "NETFRAMEWORK",
            "NETSTANDARD",
            "WINDOWS_UWP"
        };

        // Add all specific version symbols
        foreach (var framework in Frameworks)
        {
            var symbolsForTfm = GetPreprocessorSymbolsForFramework(framework);
            foreach (var sym in symbolsForTfm)
            {
                symbols.Add(sym);
            }
        }

        return symbols;
    }

    /// <summary>
    /// Gets the preprocessor symbols defined for a given target framework.
    /// </summary>
    public static HashSet<string> GetPreprocessorSymbolsForFramework(string framework)
    {
        var symbols = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // uap10.0 defines WINDOWS_UWP and NETSTANDARD2_0
        if (framework.StartsWith("uap"))
        {
            symbols.Add("NETSTANDARD2_0");
            symbols.Add("NETSTANDARD2_0_OR_GREATER");
            symbols.Add("NETSTANDARD");
            symbols.Add("WINDOWS_UWP");
            return symbols;
        }

        // NET Framework
        if (framework.StartsWith("net4"))
        {
            symbols.Add("NETFRAMEWORK");

            if (framework.StartsWith("net46"))
            {
                symbols.Add("NET46X");
                if (framework == "net461")
                {
                    symbols.Add("NET461");
                    symbols.Add("NET461_OR_GREATER");
                }
                else if (framework == "net462")
                {
                    symbols.Add("NET462");
                    symbols.Add("NET462_OR_GREATER");
                    symbols.Add("NET461_OR_GREATER");
                }
            }
            else if (framework.StartsWith("net47"))
            {
                symbols.Add("NET47X");
                symbols.Add("NET461_OR_GREATER");
                symbols.Add("NET462_OR_GREATER");

                if (framework == "net47")
                {
                    symbols.Add("NET47");
                    symbols.Add("NET47_OR_GREATER");
                }
                else if (framework == "net471")
                {
                    symbols.Add("NET471");
                    symbols.Add("NET471_OR_GREATER");
                    symbols.Add("NET47_OR_GREATER");
                }
                else if (framework == "net472")
                {
                    symbols.Add("NET472");
                    symbols.Add("NET472_OR_GREATER");
                    symbols.Add("NET471_OR_GREATER");
                    symbols.Add("NET47_OR_GREATER");
                }
            }
            else if (framework.StartsWith("net48"))
            {
                symbols.Add("NET48X");
                symbols.Add("NET461_OR_GREATER");
                symbols.Add("NET462_OR_GREATER");
                symbols.Add("NET47_OR_GREATER");
                symbols.Add("NET471_OR_GREATER");
                symbols.Add("NET472_OR_GREATER");

                if (framework == "net48")
                {
                    symbols.Add("NET48");
                    symbols.Add("NET48_OR_GREATER");
                }
                else if (framework == "net481")
                {
                    symbols.Add("NET481");
                    symbols.Add("NET481_OR_GREATER");
                    symbols.Add("NET48_OR_GREATER");
                }
            }
        }
        // .NET Standard
        else if (framework.StartsWith("netstandard"))
        {
            symbols.Add("NETSTANDARD");

            if (framework == "netstandard2.0")
            {
                symbols.Add("NETSTANDARD2_0");
                symbols.Add("NETSTANDARD2_0_OR_GREATER");
            }
            else if (framework == "netstandard2.1")
            {
                symbols.Add("NETSTANDARD2_1");
                symbols.Add("NETSTANDARD2_1_OR_GREATER");
                symbols.Add("NETSTANDARD2_0_OR_GREATER");
            }
        }
        // .NET Core
        else if (framework.StartsWith("netcoreapp"))
        {
            symbols.Add("NETCOREAPP");
            symbols.Add("NETCOREAPPX");

            if (framework.StartsWith("netcoreapp2"))
            {
                symbols.Add("NETCOREAPP2X");

                if (framework == "netcoreapp2.0")
                {
                    symbols.Add("NETCOREAPP2_0");
                    symbols.Add("NETCOREAPP2_0_OR_GREATER");
                }
                else if (framework == "netcoreapp2.1")
                {
                    symbols.Add("NETCOREAPP2_1");
                    symbols.Add("NETCOREAPP2_1_OR_GREATER");
                    symbols.Add("NETCOREAPP2_0_OR_GREATER");
                }
                else if (framework == "netcoreapp2.2")
                {
                    symbols.Add("NETCOREAPP2_2");
                    symbols.Add("NETCOREAPP2_2_OR_GREATER");
                    symbols.Add("NETCOREAPP2_1_OR_GREATER");
                    symbols.Add("NETCOREAPP2_0_OR_GREATER");
                }
            }
            else if (framework.StartsWith("netcoreapp3"))
            {
                symbols.Add("NETCOREAPP3X");
                symbols.Add("NETCOREAPP2_0_OR_GREATER");
                symbols.Add("NETCOREAPP2_1_OR_GREATER");
                symbols.Add("NETCOREAPP2_2_OR_GREATER");

                if (framework == "netcoreapp3.0")
                {
                    symbols.Add("NETCOREAPP3_0");
                    symbols.Add("NETCOREAPP3_0_OR_GREATER");
                }
                else if (framework == "netcoreapp3.1")
                {
                    symbols.Add("NETCOREAPP3_1");
                    symbols.Add("NETCOREAPP3_1_OR_GREATER");
                    symbols.Add("NETCOREAPP3_0_OR_GREATER");
                }
            }
        }
        // .NET 5+
        else if (framework.StartsWith("net"))
        {
            // Parse version number
            var versionStr = framework.Substring(3).Replace(".", "_");
            var match = Regex.Match(versionStr, @"^(\d+)");
            if (match.Success && int.TryParse(match.Groups[1].Value, out var majorVersion) && majorVersion >= 5)
            {
                symbols.Add("NET");

                var netSymbol = $"NET{versionStr.Replace("_0", "_0")}";
                if (!netSymbol.Contains("_"))
                {
                    netSymbol += "_0";
                }
                symbols.Add(netSymbol);
                symbols.Add($"{netSymbol}_OR_GREATER");

                // Add all previous _OR_GREATER symbols
                for (var v = 5; v < majorVersion; v++)
                {
                    symbols.Add($"NET{v}_0_OR_GREATER");
                }

                // .NET 5+ also defines legacy NETCOREAPP symbols for backwards compatibility
                symbols.Add("NETCOREAPP");
                symbols.Add("NETCOREAPP2_0_OR_GREATER");
                symbols.Add("NETCOREAPP2_1_OR_GREATER");
                symbols.Add("NETCOREAPP3_0_OR_GREATER");
                symbols.Add("NETCOREAPP3_1_OR_GREATER");
            }
        }

        return symbols;
    }

    /// <summary>
    /// Processes all .cs files in the Polyfill directory and writes split versions for each target framework.
    /// </summary>
    public static async Task Run()
    {
        // Clean up existing split directory
        if (Directory.Exists(SplitOutputDir))
        {
            Directory.Delete(SplitOutputDir, true);
        }

        var csFiles = Directory.EnumerateFiles(polyfillDir, "*.cs", SearchOption.AllDirectories)
            .Where(_ => !_.Contains(@"\obj\") &&
                        !_.Contains("/obj/"))
            .ToList();

        foreach (var framework in Frameworks)
        {
            var outputDir = Path.Combine(SplitOutputDir, framework);

            var definedSymbols = GetPreprocessorSymbolsForFramework(framework);
            var typeForwardedToLines = new List<string>();

            foreach (var csFile in csFiles)
            {
                var relativePath = Path.GetRelativePath(polyfillDir, csFile);
                var outputPath = Path.Combine(outputDir, relativePath);
                var outputFileDir = Path.GetDirectoryName(outputPath)!;

                var sourceCode = await File.ReadAllTextAsync(csFile);

                // Process file and clean up - all operations work on List<string> to avoid repeated split/join
                var lines = ProcessFile(sourceCode, definedSymbols);
                lines = RemoveEmptyConditionalBlocks(lines);
                lines = RemoveLinkComments(lines);
                lines = RemoveEmptyLines(lines);

                // Extract ALL TypeForwardedTo lines first, before checking if file should be skipped
                var forwardedToLines = lines.Where(l => l.TrimStart().Contains("TypeForwardedTo")).ToList();
                if (forwardedToLines.Count > 0)
                {
                    typeForwardedToLines.AddRange(forwardedToLines);
                    // Remove TypeForwardedTo lines from the file
                    lines = lines.Where(l => !l.TrimStart().Contains("TypeForwardedTo")).ToList();

                    // Clean up again after removing TypeForwardedTo lines
                    lines = RemoveEmptyConditionalBlocks(lines);
                    lines = RemoveEmptyLines(lines);
                }

                // If file is now empty or only contains empty Polyfill class, skip it
                if (lines.Count == 0 || !ContainsTypes(lines) || IsEmptyPolyfillClass(lines))
                {
                    continue;
                }

                lines.Insert(0,"#pragma warning disable");
                lines.Insert(0,"// <auto-generated />");
                lines = ConvertSpacesToTabs(lines);
                var content = string.Join("\n", lines) + "\n";
                Directory.CreateDirectory(outputFileDir);
                await File.WriteAllTextAsync(outputPath, content);
            }

            // Write consolidated TypeForwardeds.cs file if we have any TypeForwardedTo attributes
            if (typeForwardedToLines.Count > 0)
            {
                var typeForwardedsPath = Path.Combine(outputDir, "TypeForwardeds.cs");
                var consolidatedLines = new List<string>
                {
                    "// <auto-generated />",
                    "#pragma warning disable",
                    "using System.Runtime.CompilerServices;"
                };
                // Simplify TypeForwardedTo references by removing the namespace prefix
                var simplifiedLines = typeForwardedToLines
                    .Select(line => line.Replace("System.Runtime.CompilerServices.TypeForwardedTo", "TypeForwardedTo"))
                    .ToList();
                consolidatedLines.AddRange(simplifiedLines);
                var consolidatedContent = string.Join("\n", ConvertSpacesToTabs(consolidatedLines)) + "\n";
                Directory.CreateDirectory(outputDir);
                await File.WriteAllTextAsync(typeForwardedsPath, consolidatedContent);
            }
        }
    }

    /// <summary>
    /// Removes empty and whitespace-only lines from the list.
    /// </summary>
    public static List<string> RemoveEmptyLines(List<string> lines) =>
        lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

    /// <summary>
    /// Removes lines that are //Link: comments.
    /// </summary>
    public static List<string> RemoveLinkComments(List<string> lines) =>
        lines.Where(l => !l.TrimStart().StartsWith("//Link:")).ToList();

    /// <summary>
    /// Converts leading spaces to tabs (4 spaces = 1 tab).
    /// </summary>
    public static List<string> ConvertSpacesToTabs(List<string> lines) =>
        lines.Select(line =>
        {
            var leadingSpaces = line.Length - line.TrimStart().Length;
            var tabs = leadingSpaces / 4;
            var remainder = leadingSpaces % 4;
            return new string('\t', tabs) + new string(' ', remainder) + line.TrimStart();
        }).ToList();

    /// <summary>
    /// Checks if the lines contain any type declarations (class, struct, record, interface, enum, delegate).
    /// </summary>
    public static bool ContainsTypes(List<string> lines) =>
        lines.Any(line =>
        {
            var trimmed = line.TrimStart();
            return trimmed.Contains("class ") ||
                   trimmed.Contains("struct ") ||
                   trimmed.Contains("record ") ||
                   trimmed.Contains("interface ") ||
                   trimmed.Contains("enum ") ||
                   trimmed.Contains("delegate ");
        });

    /// <summary>
    /// Checks if the lines contain only TypeForwardedTo attributes and no actual type definitions.
    /// </summary>
    public static bool IsOnlyTypeForwardedTo(List<string> lines)
    {
        var hasTypeForwardedTo = false;

        foreach (var line in lines)
        {
            var trimmed = line.TrimStart();

            // Skip empty lines and preprocessor directives
            if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith('#'))
            {
                continue;
            }

            // Check for TypeForwardedTo
            if (trimmed.Contains("TypeForwardedTo"))
            {
                hasTypeForwardedTo = true;
                continue;
            }

            // Check for actual type definitions
            if (trimmed.Contains("class ") ||
                trimmed.Contains("struct ") ||
                trimmed.Contains("record ") ||
                trimmed.Contains("interface ") ||
                trimmed.Contains("enum ") ||
                trimmed.Contains("delegate "))
            {
                return false; // Has actual types, not just TypeForwardedTo
            }
        }

        return hasTypeForwardedTo;
    }

    /// <summary>
    /// Checks if the file is an empty Polyfill class (only contains namespace, usings, and an empty static partial class Polyfill).
    /// </summary>
    public static bool IsEmptyPolyfillClass(List<string> lines)
    {
        foreach (var line in lines)
        {
            var trimmed = line.TrimStart();

            // Skip namespace declarations, using statements, braces, and preprocessor directives
            if (trimmed.StartsWith("namespace ") ||
                trimmed.StartsWith("using ") ||
                trimmed is "{" or "}" ||
                trimmed.StartsWith('#'))
            {
                continue;
            }

            // Skip the Polyfill class declaration itself
            if (trimmed.StartsWith("static partial class Polyfill") ||
                trimmed.StartsWith("partial class Polyfill"))
            {
                continue;
            }

            // Skip empty extension blocks (e.g., "extension(Math)")
            if (trimmed.StartsWith("extension("))
            {
                continue;
            }

            // Any other content means it's not empty
            return false;
        }

        return true;
    }

    /// <summary>
    /// Removes empty conditional blocks (e.g., #if X followed directly by #endif with no content).
    /// Handles nested cases by repeatedly removing until no more empty blocks exist.
    /// </summary>
    public static List<string> RemoveEmptyConditionalBlocks(List<string> lines)
    {
        bool changed;

        do
        {
            changed = false;
            var result = new List<string>();

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var trimmed = line.AsSpan().TrimStart();

                // Check if this is an #if or #elif that is immediately followed by #endif, #else, or #elif
                if (trimmed.StartsWith("#if ") || trimmed.StartsWith("#if(") ||
                    trimmed.StartsWith("#elif ") || trimmed.StartsWith("#elif("))
                {
                    // Look ahead to find the matching #endif, #else, or #elif
                    if (i + 1 < lines.Count)
                    {
                        var nextTrimmed = lines[i + 1].AsSpan().TrimStart();
                        if (nextTrimmed is "#endif" || nextTrimmed is "#else" ||
                            nextTrimmed.StartsWith("#elif ") || nextTrimmed.StartsWith("#elif("))
                        {
                            // This #if/#elif has no content, skip it
                            // If next is #endif, skip both
                            if (nextTrimmed is "#endif")
                            {
                                i++; // Skip the #endif too
                                changed = true;
                                continue;
                            }
                            // If next is #else or #elif, just skip this #if/#elif line
                            changed = true;
                            continue;
                        }
                    }
                }

                // Check if this is an #else immediately followed by #endif
                if (trimmed is "#else")
                {
                    if (i + 1 < lines.Count)
                    {
                        var nextTrimmed = lines[i + 1].AsSpan().TrimStart();
                        if (nextTrimmed is "#endif")
                        {
                            // Empty #else block, skip just the #else (keep the #endif to close the #if)
                            changed = true;
                            continue;
                        }
                    }
                }

                result.Add(line);
            }

            lines = result;
        } while (changed);

        return lines;
    }

    /// <summary>
    /// Processes a single source file, evaluating conditional compilations based on defined symbols.
    /// Unknown symbols are preserved in the output.
    /// </summary>
    public static List<string> ProcessFile(string sourceCode, HashSet<string> definedSymbols)
    {
        var processor = new ConditionalProcessor(sourceCode, definedSymbols);
        return processor.Process();
    }
}
