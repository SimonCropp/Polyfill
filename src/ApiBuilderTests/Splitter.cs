/// <summary>
/// Splits Polyfill source files by target framework, evaluating conditional compilations.
/// Unknown symbols (like Feature*) are preserved in the output.
/// </summary>
public class Splitter
{
    static readonly string PolyfillDir = Path.Combine(ProjectFiles.SolutionDirectory, "Polyfill");

    public static readonly string SplitOutputDir = Path.Combine(ProjectFiles.SolutionDirectory, "Split");

    /// <summary>
    /// All target frameworks to generate splits for.
    /// </summary>
    public static readonly string[] TargetFrameworks =
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
        "net10.0"
    ];

    /// <summary>
    /// All known framework monikers that should be evaluated (not left in output).
    /// This includes all the _OR_GREATER variants and X-suffixed monikers.
    /// </summary>
    public static readonly HashSet<string> AllKnownFrameworkSymbols = BuildAllKnownSymbols();

    static HashSet<string> BuildAllKnownSymbols()
    {
        var symbols = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Add all specific version symbols
        foreach (var tfm in TargetFrameworks)
        {
            var symbolsForTfm = GetPreprocessorSymbolsForFramework(tfm);
            foreach (var sym in symbolsForTfm)
            {
                symbols.Add(sym);
            }
        }

        // Add X-suffixed symbols
        symbols.Add("NETCOREAPP2X");
        symbols.Add("NETCOREAPP3X");
        symbols.Add("NETCOREAPPX");
        symbols.Add("NET46X");
        symbols.Add("NET47X");
        symbols.Add("NET48X");
        symbols.Add("NETCOREAPP");
        symbols.Add("NETFRAMEWORK");
        symbols.Add("NETSTANDARD");

        return symbols;
    }

    /// <summary>
    /// Gets the preprocessor symbols defined for a given target framework.
    /// </summary>
    public static HashSet<string> GetPreprocessorSymbolsForFramework(string targetFramework)
    {
        var symbols = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var tfm = targetFramework.ToLowerInvariant();

        // NET Framework
        if (tfm.StartsWith("net4"))
        {
            symbols.Add("NETFRAMEWORK");

            if (tfm.StartsWith("net46"))
            {
                symbols.Add("NET46X");
                if (tfm == "net461")
                {
                    symbols.Add("NET461");
                    symbols.Add("NET461_OR_GREATER");
                }
                else if (tfm == "net462")
                {
                    symbols.Add("NET462");
                    symbols.Add("NET462_OR_GREATER");
                    symbols.Add("NET461_OR_GREATER");
                }
            }
            else if (tfm.StartsWith("net47"))
            {
                symbols.Add("NET47X");
                symbols.Add("NET461_OR_GREATER");
                symbols.Add("NET462_OR_GREATER");

                if (tfm == "net47")
                {
                    symbols.Add("NET47");
                    symbols.Add("NET47_OR_GREATER");
                }
                else if (tfm == "net471")
                {
                    symbols.Add("NET471");
                    symbols.Add("NET471_OR_GREATER");
                    symbols.Add("NET47_OR_GREATER");
                }
                else if (tfm == "net472")
                {
                    symbols.Add("NET472");
                    symbols.Add("NET472_OR_GREATER");
                    symbols.Add("NET471_OR_GREATER");
                    symbols.Add("NET47_OR_GREATER");
                }
            }
            else if (tfm.StartsWith("net48"))
            {
                symbols.Add("NET48X");
                symbols.Add("NET461_OR_GREATER");
                symbols.Add("NET462_OR_GREATER");
                symbols.Add("NET47_OR_GREATER");
                symbols.Add("NET471_OR_GREATER");
                symbols.Add("NET472_OR_GREATER");

                if (tfm == "net48")
                {
                    symbols.Add("NET48");
                    symbols.Add("NET48_OR_GREATER");
                }
                else if (tfm == "net481")
                {
                    symbols.Add("NET481");
                    symbols.Add("NET481_OR_GREATER");
                    symbols.Add("NET48_OR_GREATER");
                }
            }
        }
        // .NET Standard
        else if (tfm.StartsWith("netstandard"))
        {
            symbols.Add("NETSTANDARD");

            if (tfm == "netstandard2.0")
            {
                symbols.Add("NETSTANDARD2_0");
                symbols.Add("NETSTANDARD2_0_OR_GREATER");
            }
            else if (tfm == "netstandard2.1")
            {
                symbols.Add("NETSTANDARD2_1");
                symbols.Add("NETSTANDARD2_1_OR_GREATER");
                symbols.Add("NETSTANDARD2_0_OR_GREATER");
            }
        }
        // .NET Core
        else if (tfm.StartsWith("netcoreapp"))
        {
            symbols.Add("NETCOREAPP");
            symbols.Add("NETCOREAPPX");

            if (tfm.StartsWith("netcoreapp2"))
            {
                symbols.Add("NETCOREAPP2X");

                if (tfm == "netcoreapp2.0")
                {
                    symbols.Add("NETCOREAPP2_0");
                    symbols.Add("NETCOREAPP2_0_OR_GREATER");
                }
                else if (tfm == "netcoreapp2.1")
                {
                    symbols.Add("NETCOREAPP2_1");
                    symbols.Add("NETCOREAPP2_1_OR_GREATER");
                    symbols.Add("NETCOREAPP2_0_OR_GREATER");
                }
                else if (tfm == "netcoreapp2.2")
                {
                    symbols.Add("NETCOREAPP2_2");
                    symbols.Add("NETCOREAPP2_2_OR_GREATER");
                    symbols.Add("NETCOREAPP2_1_OR_GREATER");
                    symbols.Add("NETCOREAPP2_0_OR_GREATER");
                }
            }
            else if (tfm.StartsWith("netcoreapp3"))
            {
                symbols.Add("NETCOREAPP3X");
                symbols.Add("NETCOREAPP2_0_OR_GREATER");
                symbols.Add("NETCOREAPP2_1_OR_GREATER");
                symbols.Add("NETCOREAPP2_2_OR_GREATER");

                if (tfm == "netcoreapp3.0")
                {
                    symbols.Add("NETCOREAPP3_0");
                    symbols.Add("NETCOREAPP3_0_OR_GREATER");
                }
                else if (tfm == "netcoreapp3.1")
                {
                    symbols.Add("NETCOREAPP3_1");
                    symbols.Add("NETCOREAPP3_1_OR_GREATER");
                    symbols.Add("NETCOREAPP3_0_OR_GREATER");
                }
            }
        }
        // .NET 5+
        else if (tfm.StartsWith("net"))
        {
            // Parse version number
            var versionStr = tfm.Substring(3).Replace(".", "_");
            var match = Regex.Match(versionStr, @"^(\d+)");
            if (match.Success && int.TryParse(match.Groups[1].Value, out var majorVersion) && majorVersion >= 5)
            {
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
    public static void Run()
    {
        // Clean up existing split directory
        if (Directory.Exists(SplitOutputDir))
        {
            Directory.Delete(SplitOutputDir, true);
        }

        var csFiles = Directory.EnumerateFiles(PolyfillDir, "*.cs", SearchOption.AllDirectories)
            .Where(f => !f.Contains("\\obj\\") && !f.Contains("/obj/"))
            .ToList();

        foreach (var targetFramework in TargetFrameworks)
        {
            var outputDir = Path.Combine(SplitOutputDir, targetFramework);
            Directory.CreateDirectory(outputDir);

            var definedSymbols = GetPreprocessorSymbolsForFramework(targetFramework);

            foreach (var csFile in csFiles)
            {
                var relativePath = Path.GetRelativePath(PolyfillDir, csFile);
                var outputPath = Path.Combine(outputDir, relativePath);
                var outputFileDir = Path.GetDirectoryName(outputPath)!;
                Directory.CreateDirectory(outputFileDir);

                var sourceCode = File.ReadAllText(csFile);

                // Process file and clean up - all operations work on List<string> to avoid repeated split/join
                var lines = ProcessFile(sourceCode, definedSymbols);
                lines = RemoveEmptyConditionalBlocks(lines);
                lines = RemoveEmptyLines(lines);

                File.WriteAllText(outputPath, string.Join("\r\n", lines));
            }
        }
    }

    /// <summary>
    /// Removes empty and whitespace-only lines from the list.
    /// </summary>
    public static List<string> RemoveEmptyLines(List<string> lines) =>
        lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

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
                var trimmed = line.TrimStart();

                // Check if this is an #if or #elif that is immediately followed by #endif, #else, or #elif
                if (trimmed.StartsWith("#if ") || trimmed.StartsWith("#if(") ||
                    trimmed.StartsWith("#elif ") || trimmed.StartsWith("#elif("))
                {
                    // Look ahead to find the matching #endif, #else, or #elif
                    if (i + 1 < lines.Count)
                    {
                        var nextTrimmed = lines[i + 1].TrimStart();
                        if (nextTrimmed == "#endif" || nextTrimmed == "#else" ||
                            nextTrimmed.StartsWith("#elif ") || nextTrimmed.StartsWith("#elif("))
                        {
                            // This #if/#elif has no content, skip it
                            // If next is #endif, skip both
                            if (nextTrimmed == "#endif")
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
                if (trimmed == "#else")
                {
                    if (i + 1 < lines.Count)
                    {
                        var nextTrimmed = lines[i + 1].TrimStart();
                        if (nextTrimmed == "#endif")
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
    /// Unknown symbols are preserved in the output. Returns lines (without \r).
    /// </summary>
    public static List<string> ProcessFile(string sourceCode, HashSet<string> definedSymbols)
    {
        var processor = new ConditionalProcessor(sourceCode, definedSymbols);
        return processor.Process();
    }
}

/// <summary>
/// Represents a tri-state boolean: True, False, or Unknown.
/// </summary>
public enum TriState
{
    False,
    True,
    Unknown
}

/// <summary>
/// Parses and evaluates preprocessor expressions, handling known and unknown symbols.
/// </summary>
public class ExpressionParser
{
    readonly string _expression;
    readonly HashSet<string> _definedSymbols;
    readonly HashSet<string> _knownSymbols;
    int _pos;

    public ExpressionParser(string expression, HashSet<string> definedSymbols, HashSet<string> knownSymbols)
    {
        _expression = expression;
        _definedSymbols = definedSymbols;
        _knownSymbols = knownSymbols;
        _pos = 0;
    }

    public TriState Evaluate()
    {
        _pos = 0;
        return ParseOr();
    }

    public string Simplify()
    {
        _pos = 0;
        return SimplifyOr();
    }

    void SkipWhitespace()
    {
        while (_pos < _expression.Length && char.IsWhiteSpace(_expression[_pos]))
        {
            _pos++;
        }
    }

    TriState ParseOr()
    {
        var left = ParseAnd();
        SkipWhitespace();

        while (_pos < _expression.Length - 1 && _expression.Substring(_pos, 2) == "||")
        {
            _pos += 2;
            var right = ParseAnd();
            left = OrTriState(left, right);
        }

        return left;
    }

    TriState ParseAnd()
    {
        var left = ParseUnary();
        SkipWhitespace();

        while (_pos < _expression.Length - 1 && _expression.Substring(_pos, 2) == "&&")
        {
            _pos += 2;
            var right = ParseUnary();
            left = AndTriState(left, right);
        }

        return left;
    }

    TriState ParseUnary()
    {
        SkipWhitespace();

        if (_pos < _expression.Length && _expression[_pos] == '!')
        {
            _pos++;
            var inner = ParseUnary();
            return NotTriState(inner);
        }

        return ParsePrimary();
    }

    TriState ParsePrimary()
    {
        SkipWhitespace();

        if (_pos < _expression.Length && _expression[_pos] == '(')
        {
            _pos++;
            var result = ParseOr();
            SkipWhitespace();
            if (_pos < _expression.Length && _expression[_pos] == ')')
            {
                _pos++;
            }
            return result;
        }

        // Parse identifier
        var start = _pos;
        while (_pos < _expression.Length && (char.IsLetterOrDigit(_expression[_pos]) || _expression[_pos] == '_'))
        {
            _pos++;
        }

        var identifier = _expression.Substring(start, _pos - start);
        SkipWhitespace();

        if (string.Equals(identifier, "true", StringComparison.OrdinalIgnoreCase))
        {
            return TriState.True;
        }
        if (string.Equals(identifier, "false", StringComparison.OrdinalIgnoreCase))
        {
            return TriState.False;
        }

        // Check if this is a known symbol
        if (_knownSymbols.Contains(identifier))
        {
            return _definedSymbols.Contains(identifier) ? TriState.True : TriState.False;
        }

        // Unknown symbol
        return TriState.Unknown;
    }

    // Simplification methods
    string SimplifyOr()
    {
        var left = SimplifyAnd();
        SkipWhitespace();
        var parts = new List<string> { left };

        while (_pos < _expression.Length - 1 && _expression.Substring(_pos, 2) == "||")
        {
            _pos += 2;
            var right = SimplifyAnd();
            parts.Add(right);
        }

        // Simplify: if any part is "true", result is "true"
        // If any part is "false", remove it
        var filtered = parts.Where(p => p != "false").ToList();
        if (filtered.Any(p => p == "true"))
        {
            return "true";
        }
        if (filtered.Count == 0)
        {
            return "false";
        }
        if (filtered.Count == 1)
        {
            return filtered[0];
        }
        return string.Join(" || ", filtered);
    }

    string SimplifyAnd()
    {
        var left = SimplifyUnary();
        SkipWhitespace();
        var parts = new List<string> { left };

        while (_pos < _expression.Length - 1 && _expression.Substring(_pos, 2) == "&&")
        {
            _pos += 2;
            var right = SimplifyUnary();
            parts.Add(right);
        }

        // Simplify: if any part is "false", result is "false"
        // If any part is "true", remove it
        if (parts.Any(p => p == "false"))
        {
            return "false";
        }
        var filtered = parts.Where(p => p != "true").ToList();
        if (filtered.Count == 0)
        {
            return "true";
        }
        if (filtered.Count == 1)
        {
            return filtered[0];
        }
        return string.Join(" && ", filtered);
    }

    string SimplifyUnary()
    {
        SkipWhitespace();

        if (_pos < _expression.Length && _expression[_pos] == '!')
        {
            _pos++;
            var inner = SimplifyUnary();
            if (inner == "true")
            {
                return "false";
            }
            if (inner == "false")
            {
                return "true";
            }
            // Check if inner starts with ! and simplify double negation
            if (inner.StartsWith('!'))
            {
                return inner[1..];
            }
            return $"!{inner}";
        }

        return SimplifyPrimary();
    }

    string SimplifyPrimary()
    {
        SkipWhitespace();

        if (_pos < _expression.Length && _expression[_pos] == '(')
        {
            _pos++;
            var result = SimplifyOr();
            SkipWhitespace();
            if (_pos < _expression.Length && _expression[_pos] == ')')
            {
                _pos++;
            }
            // Only keep parens if needed (contains || and was inside an && context)
            if (result.Contains("||") && !result.StartsWith('('))
            {
                return $"({result})";
            }
            return result;
        }

        // Parse identifier
        var start = _pos;
        while (_pos < _expression.Length && (char.IsLetterOrDigit(_expression[_pos]) || _expression[_pos] == '_'))
        {
            _pos++;
        }

        var identifier = _expression.Substring(start, _pos - start);
        SkipWhitespace();

        if (string.Equals(identifier, "true", StringComparison.OrdinalIgnoreCase))
        {
            return "true";
        }
        if (string.Equals(identifier, "false", StringComparison.OrdinalIgnoreCase))
        {
            return "false";
        }

        // Check if this is a known symbol - replace with true/false
        if (_knownSymbols.Contains(identifier))
        {
            return _definedSymbols.Contains(identifier) ? "true" : "false";
        }

        // Unknown symbol - keep as-is
        return identifier;
    }

    static TriState AndTriState(TriState left, TriState right)
    {
        if (left == TriState.False || right == TriState.False)
        {
            return TriState.False;
        }
        if (left == TriState.True && right == TriState.True)
        {
            return TriState.True;
        }
        return TriState.Unknown;
    }

    static TriState OrTriState(TriState left, TriState right)
    {
        if (left == TriState.True || right == TriState.True)
        {
            return TriState.True;
        }
        if (left == TriState.False && right == TriState.False)
        {
            return TriState.False;
        }
        return TriState.Unknown;
    }

    static TriState NotTriState(TriState value) =>
        value switch
        {
            TriState.True => TriState.False,
            TriState.False => TriState.True,
            _ => TriState.Unknown
        };
}
