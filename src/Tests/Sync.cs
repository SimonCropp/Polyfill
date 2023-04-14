using System.Net.Http;
using VerifyTests;

[TestFixture]
public class Sync
{
    static string solutionDir = AttributeReader.GetSolutionDirectory();
    static string sourceOnlyDir = Path.Combine(solutionDir, "PolyFill", "Nullability");

    [Test]
    public async Task Run()
    {
        var client = new HttpClient();
        var infoContext = await client.GetStringAsync("https://raw.githubusercontent.com/dotnet/runtime/main/src/libraries/System.Private.CoreLib/src/System/Reflection/NullabilityInfoContext.cs");

        infoContext = infoContext
            .Replace("[^1]", ".Last()")
            .Replace(".IsGenericMethodParameter", ".IsGenericMethodParameter()")
            .Replace("SR.NullabilityInfoContext_NotSupported", "\"NullabilityInfoContext is not supported\"")
            .Replace("ArgumentNullException.ThrowIfNull(propertyInfo);", "")
            .Replace("ArgumentNullException.ThrowIfNull(eventInfo);", "")
            .Replace("ArgumentNullException.ThrowIfNull(fieldInfo);", "")
            .Replace("ArgumentNullException.ThrowIfNull(parameterInfo);", "");

        infoContext = infoContext.Replace("!!", "");

        var info = await client.GetStringAsync("https://raw.githubusercontent.com/dotnet/runtime/main/src/libraries/System.Private.CoreLib/src/System/Reflection/NullabilityInfo.cs");
        info = info.Replace("!!", "");

        infoContext = $@"#nullable enable
using System.Linq;
{infoContext}";
        infoContext = MakeInternal(infoContext);
        OverWrite(infoContext, "NullabilityInfoContext.cs");

        info = $@"#nullable enable
using System.Linq;
{info}";
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