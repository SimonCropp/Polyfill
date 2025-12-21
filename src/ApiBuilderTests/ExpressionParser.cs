/// <summary>
/// Parses and evaluates preprocessor expressions, handling known and unknown symbols.
/// </summary>
public class ExpressionParser(string expression, HashSet<string> definedSymbols, HashSet<string> knownSymbols)
{
    int pos;

    public TriState Evaluate()
    {
        pos = 0;
        return ParseOr();
    }

    public string Simplify()
    {
        pos = 0;
        return SimplifyOr();
    }

    void SkipWhitespace()
    {
        while (pos < expression.Length && char.IsWhiteSpace(expression[pos]))
        {
            pos++;
        }
    }

    TriState ParseOr()
    {
        var left = ParseAnd();
        SkipWhitespace();

        while (pos < expression.Length - 1 && expression.Substring(pos, 2) == "||")
        {
            pos += 2;
            var right = ParseAnd();
            left = OrTriState(left, right);
        }

        return left;
    }

    TriState ParseAnd()
    {
        var left = ParseUnary();
        SkipWhitespace();

        while (pos < expression.Length - 1 && expression.Substring(pos, 2) == "&&")
        {
            pos += 2;
            var right = ParseUnary();
            left = AndTriState(left, right);
        }

        return left;
    }

    TriState ParseUnary()
    {
        SkipWhitespace();

        if (pos < expression.Length && expression[pos] == '!')
        {
            pos++;
            var inner = ParseUnary();
            return NotTriState(inner);
        }

        return ParsePrimary();
    }

    TriState ParsePrimary()
    {
        SkipWhitespace();

        if (pos < expression.Length && expression[pos] == '(')
        {
            pos++;
            var result = ParseOr();
            SkipWhitespace();
            if (pos < expression.Length && expression[pos] == ')')
            {
                pos++;
            }

            return result;
        }

        // Parse identifier
        var start = pos;
        while (pos < expression.Length && (char.IsLetterOrDigit(expression[pos]) || expression[pos] == '_'))
        {
            pos++;
        }

        var identifier = expression.Substring(start, pos - start);
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
        if (knownSymbols.Contains(identifier))
        {
            return definedSymbols.Contains(identifier) ? TriState.True : TriState.False;
        }

        // Unknown symbol
        return TriState.Unknown;
    }

    // Simplification methods
    string SimplifyOr()
    {
        var left = SimplifyAnd();
        SkipWhitespace();
        var parts = new List<string> {left};

        while (pos < expression.Length - 1 && expression.Substring(pos, 2) == "||")
        {
            pos += 2;
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
        var parts = new List<string> {left};

        while (pos < expression.Length - 1 && expression.Substring(pos, 2) == "&&")
        {
            pos += 2;
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

        if (pos < expression.Length && expression[pos] == '!')
        {
            pos++;
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

        if (pos < expression.Length && expression[pos] == '(')
        {
            pos++;
            var result = SimplifyOr();
            SkipWhitespace();
            if (pos < expression.Length && expression[pos] == ')')
            {
                pos++;
            }

            // Only keep parens if needed (contains || and was inside an && context)
            if (result.Contains("||") && !result.StartsWith('('))
            {
                return $"({result})";
            }

            return result;
        }

        // Parse identifier
        var start = pos;
        while (pos < expression.Length && (char.IsLetterOrDigit(expression[pos]) || expression[pos] == '_'))
        {
            pos++;
        }

        var identifier = expression[start..pos];
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
        if (knownSymbols.Contains(identifier))
        {
            return definedSymbols.Contains(identifier) ? "true" : "false";
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