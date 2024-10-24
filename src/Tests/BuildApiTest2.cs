#if NET9_0 && DEBUG
using Microsoft.CodeAnalysis.CSharp;

[TestFixture]
class BuildApiTest2
{
    static string solutionDirectory = SolutionDirectoryFinder.Find();

    [Test]
    public void RunWithRoslyn()
    {
        var polyfillDir = Path.Combine(solutionDirectory, "Polyfill");
        var types = new Dictionary<string, List<MethodDeclarationSyntax>>();
        foreach (var file in Directory.EnumerateFiles(polyfillDir, "*.cs", SearchOption.AllDirectories))
        {
            var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file));
            var typeDeclarations = tree.GetRoot().DescendantNodes().OfType<TypeDeclarationSyntax>();

            foreach (var typeDeclaration in typeDeclarations)
            {
                var identifier = typeDeclaration.Identifier.Text;
                if (!types.TryGetValue(identifier, out var methods))
                {
                    methods = new();
                    types.Add(identifier, methods);
                }

                foreach (var method in typeDeclaration
                             .DescendantNodes()
                             .OfType<MethodDeclarationSyntax>()
                             .Where(_ => _.IsPublic()))
                {
                    methods.Add(method);
                }
            }
        }

        var md = Path.Combine(solutionDirectory, "..", "api_list2.include.md");
        File.Delete(md);
        using var writer = File.CreateText(md);
        var count = 0;

        var extensions = types.Single(_ => _.Key == "Polyfill").Value;
        writer.WriteLine("### Extension methods");
        writer.WriteLine();
        foreach (var extension in extensions
                     .GroupBy(_ => _.ParameterList.Parameters[0].Type!.ToString())
                     .OrderBy(_ => _.Key))
        {
            writer.WriteLine($"#### {extension.Key}");
            writer.WriteLine();

            foreach (var method in extension)
            {
                count++;
                WriteSignature(method, writer);
            }

            writer.WriteLine();
            writer.WriteLine();
        }
        writer.Flush();
    }

    static void WriteSignature(MethodDeclarationSyntax method, StreamWriter writer)
    {
        var parameters = BuildParameters(method);
        var typeArgs = BuildTypeArgs(method);
        var signature = new StringBuilder($"{method.ReturnType.ToString()} {method.Identifier.Text}{typeArgs}({parameters})");

        if (TryGetReference(method, out var reference))
        {
            writer.WriteLine($" * `{signature}` [reference]({reference})");
        }
        else
        {
            writer.WriteLine($" * `{signature}`");
        }
    }

    static string BuildTypeArgs(MethodDeclarationSyntax method)
    {
        if (method.TypeParameterList == null || method.TypeParameterList.Parameters.Count == 0)
        {
            return string.Empty;
        }

        return $"<{string.Join(", ", method.TypeParameterList.Parameters.Select(p => p.Identifier.Text))}>";
    }

    static string BuildParameters(MethodDeclarationSyntax method)
    {
        if (method.ParameterList.Parameters.Count == 0)
        {
            return "";
        }

        List<ParameterSyntax> parameters;
        if (method.IsExtensionMethod())
        {
            parameters = method.ParameterList.Parameters.Skip(1).ToList();
            if (parameters.Count == 0)
            {
                return "";
            }
        }
        else
        {
            parameters = method.ParameterList.Parameters.ToList();
        }

        var last = parameters.Last();
        if (last.IsCaller())
        {
            parameters.Remove(last);
        }

        return string.Join(", ", parameters.Select(_ => _.Type!.ToString()));
    }
    
    static bool TryGetReference(MethodDeclarationSyntax method, [NotNullWhen(true)] out string? reference)
    {
        var descriptionAttribute = method.Attributes()
            .SingleOrDefault(_ => _.Name.ToString() == "Link");
        if (descriptionAttribute == null)
        {
            reference = null;
            return false;
        }

        reference = descriptionAttribute.ArgumentList!.Arguments.Single().Value();
        return true;
    }
}
#endif