#nullable disable
/// <summary>
/// Processes conditional compilation directives in source code.
/// Evaluates known framework symbols and preserves unknown symbols.
/// </summary>
public class ConditionalProcessor
{
    HashSet<string> definedSymbols;
    List<string> lines;
    List<string> outputLines;

    public ConditionalProcessor(string sourceCode, HashSet<string> definedSymbols)
    {
        this.definedSymbols = definedSymbols;
        lines = sourceCode.Split('\n').Select(l => l.TrimEnd('\r')).ToList();
        outputLines = [];
    }

    public List<string> Process()
    {
        var state = new Stack<ConditionalState>();
        var i = 0;

        // Helper to check if we're inside an outer False branch
        bool IsInsideFalseBranch()
        {
            foreach (var s in state)
            {
                if (!s.BranchTaken)
                {
                    return true;
                }
            }
            return false;
        }

        while (i < lines.Count)
        {
            var line = lines[i];
            var trimmed = line.TrimStart();

            if (trimmed.StartsWith("#if ") || trimmed.StartsWith("#if("))
            {
                var expression = ExtractExpression(trimmed, "#if");
                var evalResult = EvaluateExpression(expression);

                // Check if we're inside a False branch BEFORE pushing the new state
                var insideFalseBranch = IsInsideFalseBranch();

                state.Push(new()
                {
                    EvalResult = evalResult,
                    BranchTaken = evalResult is TriState.True or TriState.Unknown,
                    HasElse = false,
                    ElseEvalResult = null,
                    InsideExcludedBranch = insideFalseBranch
                });

                if (evalResult == TriState.Unknown && !insideFalseBranch)
                {
                    // Keep the directive but with simplified expression
                    var simplified = SimplifyExpression(expression);
                    var indent = line[..^trimmed.Length];
                    outputLines.Add($"{indent}#if {simplified}");
                }
                // If evalResult is True or False, we don't output the #if
            }
            else if (trimmed.StartsWith("#elif ") || trimmed.StartsWith("#elif("))
            {
                if (state.Count == 0)
                {
                    throw new InvalidOperationException($"Unexpected #elif at line {i + 1}");
                }

                var current = state.Peek();
                var expression = ExtractExpression(trimmed, "#elif");
                var evalResult = EvaluateExpression(expression);

                // Handle elif: if previous branches were true/taken, skip this one
                if (current.EvalResult == TriState.True || current.BranchTaken && current.EvalResult == TriState.Unknown)
                {
                    // Previous branch was taken, this elif becomes else-false
                    current.BranchTaken = false;
                    if (current.EvalResult == TriState.Unknown && evalResult == TriState.Unknown && !current.InsideExcludedBranch)
                    {
                        // Only output elif if it has unknown symbols and we're not inside an excluded branch
                        var simplified = SimplifyExpression(expression);
                        var indent = line[..^trimmed.Length];
                        outputLines.Add($"{indent}#elif {simplified}");
                    }
                    // If elif condition is definitely true or false, don't output it
                }
                else if (current.EvalResult == TriState.False)
                {
                    // Previous branch was not taken, check this one
                    current.EvalResult = evalResult;
                    current.BranchTaken = evalResult is TriState.True or TriState.Unknown;

                    if (evalResult == TriState.Unknown && !current.InsideExcludedBranch)
                    {
                        var simplified = SimplifyExpression(expression);
                        var indent = line[..^trimmed.Length];
                        outputLines.Add($"{indent}#elif {simplified}");
                    }
                }
                else
                {
                    // Unknown state - keep the elif only if it has unknown symbols
                    current.BranchTaken = evalResult is TriState.True or TriState.Unknown;
                    if (evalResult == TriState.Unknown && !current.InsideExcludedBranch)
                    {
                        var simplified = SimplifyExpression(expression);
                        var indent = line.Substring(0, line.Length - trimmed.Length);
                        outputLines.Add($"{indent}#elif {simplified}");
                    }
                }
            }
            else if (trimmed == "#else")
            {
                if (state.Count == 0)
                {
                    throw new($"Unexpected #else at line {i + 1}");
                }

                var current = state.Peek();
                current.HasElse = true;

                if (current.EvalResult == TriState.True)
                {
                    // If was true, else is false - don't take this branch
                    current.ElseEvalResult = TriState.False;
                    current.BranchTaken = false;
                }
                else if (current.EvalResult == TriState.False)
                {
                    // If was false, else is true - take this branch
                    current.ElseEvalResult = TriState.True;
                    current.BranchTaken = true;
                }
                else
                {
                    // Unknown - keep the else only if not inside an excluded branch
                    current.ElseEvalResult = TriState.Unknown;
                    current.BranchTaken = true;
                    if (!current.InsideExcludedBranch)
                    {
                        outputLines.Add(line);
                    }
                }
            }
            else if (trimmed == "#endif")
            {
                if (state.Count == 0)
                {
                    throw new($"Unexpected #endif at line {i + 1}");
                }

                var current = state.Pop();

                // Only output #endif if we had an unknown condition and not inside an excluded branch
                if (!current.InsideExcludedBranch &&
                    (current.EvalResult == TriState.Unknown ||
                     (current.HasElse && current.ElseEvalResult == TriState.Unknown)))
                {
                    outputLines.Add(line);
                }
            }
            else
            {
                // Regular line - check if we should include it
                var shouldInclude = true;
                foreach (var s in state)
                {
                    if (!s.BranchTaken)
                    {
                        shouldInclude = false;
                        break;
                    }
                }

                if (shouldInclude)
                {
                    outputLines.Add(line);
                }
            }

            i++;
        }

        return outputLines;
    }

    static string ExtractExpression(string line, string directive)
    {
        var startIndex = line.IndexOf(directive, StringComparison.Ordinal) + directive.Length;
        return line[startIndex..].Trim();
    }

    /// <summary>
    /// Evaluates a preprocessor expression.
    /// Returns True if the condition is definitely true, False if definitely false, Unknown if it depends on unknown symbols.
    /// </summary>
    TriState EvaluateExpression(string expression)
    {
        var parser = new ExpressionParser(expression, definedSymbols, Splitter.AllKnownFrameworkSymbols);
        return parser.Evaluate();
    }

    /// <summary>
    /// Simplifies an expression by replacing known symbols with their values and simplifying boolean logic.
    /// </summary>
    string SimplifyExpression(string expression)
    {
        var parser = new ExpressionParser(expression, definedSymbols, Splitter.AllKnownFrameworkSymbols);
        return parser.Simplify();
    }

    class ConditionalState
    {
        public TriState EvalResult { get; set; }
        public bool BranchTaken { get; set; }
        public bool HasElse { get; set; }
        public TriState? ElseEvalResult { get; set; }
        public bool InsideExcludedBranch { get; init; }
    }
}