public class TypeForwardedToTests
{
    [Test]
    public void AllTypeForwardedToAttributesShouldBeFullyQualified()
    {
        var polyfillDir = Path.Combine(ProjectFiles.SolutionDirectory, "Polyfill");
        var splitDir = Path.Combine(ProjectFiles.SolutionDirectory, "Split");

        var errors = new List<string>();

        // Find all .cs files in both Polyfill and Split directories
        var files = Directory.EnumerateFiles(polyfillDir, "*.cs", SearchOption.AllDirectories)
            .Concat(Directory.EnumerateFiles(splitDir, "*.cs", SearchOption.AllDirectories))
            .ToList();

        foreach (var file in files)
        {
            var content = File.ReadAllText(file);

            // Skip files without TypeForwardedTo
            if (!content.Contains("TypeForwardedTo"))
            {
                continue;
            }

            var tree = CSharpSyntaxTree.ParseText(content);
            var root = tree.GetRoot();

            // Find all TypeForwardedTo attributes
            var attributes = root.DescendantNodes()
                .OfType<AttributeSyntax>()
                .Where(_ => _.Name.ToString().Contains("TypeForwardedTo"))
                .ToList();

            foreach (var attribute in attributes)
            {
                var attributeName = attribute.Name.ToString();

                // Check if the attribute name is fully qualified
                if (!attributeName.StartsWith("System.Runtime.CompilerServices.TypeForwardedTo"))
                {
                    errors.Add($"{Path.GetFileName(file)}: TypeForwardedTo attribute is not fully qualified: {attributeName}");
                }

                // Check if the typeof argument is fully qualified (contains at least one dot)
                if (attribute.ArgumentList?.Arguments.Count > 0)
                {
                    var argument = attribute.ArgumentList.Arguments[0];

                    if (argument.Expression is TypeOfExpressionSyntax typeofExpression)
                    {
                        var typeName = typeofExpression.Type.ToString();

                        // Check if type name contains a dot (is qualified)
                        if (!typeName.Contains('.'))
                        {
                            errors.Add($"{Path.GetFileName(file)}: Type in TypeForwardedTo is not fully qualified: {typeName}");
                        }
                    }
                }
            }
        }

        if (errors.Count <= 0)
        {
            return;
        }

        throw new($"Found {errors.Count} TypeForwardedTo attributes that are not fully qualified:\n" + string.Join('\n', errors));
    }
}
