using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public static class Extensions
{
    public static bool IsPublic(this MemberDeclarationSyntax member) =>
        member.Modifiers.Any(_ => _.ValueText == "public");

    public static bool IsExtensionMethod(this MethodDeclarationSyntax method) =>
        method.Modifiers.Any(_ => _.IsKind(SyntaxKind.StaticKeyword)) &&
        method.ParameterList.Parameters.Count > 0 &&
        method.Attributes()
            .Any(_ => _.Name.ToString() == "Extension");

 public   static IEnumerable<MethodDeclarationSyntax> PublicMethods(this TypeDeclarationSyntax typeDeclaration) =>
        typeDeclaration
            .DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(_ => _.IsPublic() && !_.IsConstructor());

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
}