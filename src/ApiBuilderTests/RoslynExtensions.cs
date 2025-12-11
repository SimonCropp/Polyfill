public static class RoslynExtensions
{
    public static bool IsPublic(this Member member) =>
        member.Modifiers.Any(_ => _.ValueText == "public");

    public static bool IsNested(this Type type) =>
        type.Parent is Type or Class;

    public static bool IsStatic(this Method method) =>
        method.Modifiers.Any(_ => _.IsKind(SyntaxKind.StaticKeyword));

    public static bool IsThis(this Parameter parameter) =>
        parameter.Modifiers.Any(_ => _.IsKind(SyntaxKind.ThisKeyword));

    public static IEnumerable<Method> PublicMethods(this Type type) =>
        type
            .DescendantNodes()
            .OfType<Method>()
            .Where(_ => !_.Ancestors().OfType<Type>().Any(ancestor => ancestor != type && ancestor.GetType().Name != "ExtensionBlockDeclarationSyntax") &&
                        _.IsPublic() &&
                        !_.IsConstructor());

    public static IEnumerable<Property> PublicProperties(this Type type) =>
        type
            .DescendantNodes()
            .OfType<Property>()
            .Where(_ => !_.Ancestors().OfType<Type>().Any(ancestor => ancestor != type && ancestor.GetType().Name != "ExtensionBlockDeclarationSyntax") &&
                        _.IsPublic());

    public static bool IsConstructor(this Method method)
    {
        if (method.Parent is Type type)
        {
            return method.Identifier.Text == type.Identifier.Text;
        }

        return false;
    }

    public static bool IsCaller(this Parameter parameter) =>
        parameter.Attributes()
            .Any(_ => _.Name.ToString().StartsWith("Caller"));

    public static IEnumerable<AttributeSyntax> Attributes(this Method method) =>
        method.AttributeLists
            .SelectMany(_ => _.Attributes);

    public static IEnumerable<AttributeSyntax> Attributes(this Parameter method) =>
        method.AttributeLists
            .SelectMany(_ => _.Attributes);

    public static string Value(this AttributeArgumentSyntax argument) =>
        argument.Expression.ToString();
}