using System.Net.Http;
using VerifyTests;

[TestFixture]
public class NullabilitySync
{
    static string solutionDir = AttributeReader.GetSolutionDirectory();
    static string sourceOnlyDir = Path.Combine(solutionDir, "PolyFill", "Nullability");

    [Test]
    public async Task Run()
    {
        var client = new HttpClient();
        var infoContext = await client.GetStringAsync("https://raw.githubusercontent.com/dotnet/runtime/main/src/libraries/System.Private.CoreLib/src/System/Reflection/NullabilityInfoContext.cs");

        infoContext = infoContext
            .Replace(".IsGenericMethodParameter", ".IsGenericMethodParameter()")
            .Replace("SR.NullabilityInfoContext_NotSupported", "\"NullabilityInfoContext is not supported\"");

        var lines = infoContext.Split('\r', '\n');
        infoContext = string.Join(Environment.NewLine, lines.Where(_ => !_.Contains("ArgumentNullException.ThrowIfNull")));
        infoContext = infoContext.Replace("!!", "");

        var info = await client.GetStringAsync("https://raw.githubusercontent.com/dotnet/runtime/main/src/libraries/System.Private.CoreLib/src/System/Reflection/NullabilityInfo.cs");

        var prefix = @"#nullable enable

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global
// ReSharper disable ArrangeNamespaceBody
// ReSharper disable MergeIntoPattern
// ReSharper disable RedundantSuppressNullableWarningExpression
// ReSharper disable RedundantIfElseBlock
// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

using System.Linq;
";
        infoContext = prefix + infoContext;
        infoContext = MakeInternal(infoContext);
        OverWrite(infoContext, "NullabilityInfoContext.cs");

        info = prefix + info;
        info = MakeInternal(info);
        OverWrite(info, "NullabilityInfo.cs");
    }

    static string MakeInternal(string source) =>
        source
            .Replace("public enum", "enum")
            .Replace("public sealed class", "sealed class");

    static void OverWrite(string? content, string file)
    {
        var path = Path.Combine(sourceOnlyDir, file);
        File.Delete(path);
        File.WriteAllText(path, content);
    }
}