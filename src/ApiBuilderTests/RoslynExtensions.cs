public static class RoslynExtensions
{
    public static bool IsPublic(this MemberDeclarationSyntax member) =>
        member.Modifiers.Any(_ => _.ValueText == "public");

    public static bool IsNested(this TypeDeclarationSyntax type) =>
        type.Parent is TypeDeclarationSyntax or ClassDeclarationSyntax;

    public static IEnumerable<TypeDeclarationSyntax> GetTypes(this SyntaxTree tree) => tree
        .GetRoot()
        .DescendantNodes()
        .OfType<TypeDeclarationSyntax>()
        .Where(_ => !_.IsNested());

    public static bool IsStatic(this MethodDeclarationSyntax method) =>
        method.Modifiers.Any(_ => _.IsKind(SyntaxKind.StaticKeyword));

    public static IEnumerable<MethodDeclarationSyntax> PublicMethods(this TypeDeclarationSyntax type) =>
        type
            .DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(_ => _.Parent == type &&
                        _.IsPublic() &&
                        !_.IsConstructor());

    public static IEnumerable<PropertyDeclarationSyntax> PublicProperties(this TypeDeclarationSyntax type) =>
        type
            .DescendantNodes()
            .OfType<PropertyDeclarationSyntax>()
            .Where(_ => _.Parent == type &&
                        _.IsPublic());

    public static bool IsConstructor(this MethodDeclarationSyntax method)
    {
        if (method.Parent is TypeDeclarationSyntax typeDeclaration)
        {
            return method.Identifier.Text == typeDeclaration.Identifier.Text;
        }

        return false;
    }

    public static bool IsCaller(this ParameterSyntax parameter) =>
        parameter.Attributes()
            .Any(_ => _.Name.ToString().StartsWith("Caller"));

    public static IEnumerable<AttributeSyntax> Attributes(this MethodDeclarationSyntax method) =>
        method.AttributeLists
            .SelectMany(_ => _.Attributes);

    public static IEnumerable<AttributeSyntax> Attributes(this ParameterSyntax method) =>
        method.AttributeLists
            .SelectMany(_ => _.Attributes);

    public static string Value(this AttributeArgumentSyntax argument) =>
        argument.Expression.ToString();

    public static string Key(this MethodDeclarationSyntax method)
    {
        var parameters = BuildParameters(method);
        var typeArgs = BuildTypeArgs(method);

        return $"{method.Identifier.Text}{typeArgs}({parameters})";
    }
    public static string DisplayString(this MethodDeclarationSyntax method)
    {
        var signature = new StringBuilder($"{method.ReturnType} {method.Key()}");

        if (method.ConstraintClauses.Count > 0)
        {
            foreach (var constraint in method.ConstraintClauses)
            {
                signature.Append($" where {constraint.Name} : ");
                signature.Append(string.Join(", ", constraint.Constraints.Select(_ => _.ToString())));
            }
        }

        return signature.ToString();
    }

    static string BuildTypeArgs(MethodDeclarationSyntax method)
    {
        var types = method.TypeParameterList;
        if (types == null || types.Parameters.Count == 0)
        {
            return string.Empty;
        }

        return $"<{string.Join(", ", types.Parameters.Select(_ => _.Identifier.Text))}>";
    }

    static string BuildParameters(MethodDeclarationSyntax method)
    {
        var parameters = method.ParameterList.Parameters.ToList();

        if (parameters.Count > 0)
        {
            var last = parameters.Last();
            if (last.IsCaller())
            {
                parameters.Remove(last);
            }
        }

        return string.Join(", ", parameters.Select(_ => _.Type!.ToString()));
    }
}