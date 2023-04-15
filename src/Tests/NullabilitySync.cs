#if NET8_0
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
        using var client = new HttpClient();
        var infoContext = await client.GetStringAsync("https://raw.githubusercontent.com/dotnet/runtime/main/src/libraries/System.Private.CoreLib/src/System/Reflection/NullabilityInfoContext.cs");

        infoContext = infoContext
            .Replace(".IsGenericMethodParameter", ".IsGenericMethodParameter()")
            .Replace("SR.NullabilityInfoContext_NotSupported", "\"NullabilityInfoContext is not supported\"");

        var lines = infoContext.Split('\r', '\n');
        infoContext = string.Join(Environment.NewLine, lines.Where(_ => !_.Contains("ArgumentNullException.ThrowIfNull")));
        infoContext = infoContext.Replace("!!", "");

        var info = await client.GetStringAsync("https://raw.githubusercontent.com/dotnet/runtime/main/src/libraries/System.Private.CoreLib/src/System/Reflection/NullabilityInfo.cs");

        var prefix = @"
#if !NET6_0_OR_GREATER

#nullable enable

// ReSharper disable All

using System.Linq;
";

        var suffix = @"
#endif
";

        infoContext = prefix + infoContext + suffix;
        infoContext = MakeInternal(infoContext);
        OverWrite(infoContext, "NullabilityInfoContext.cs");

        info = prefix + info + suffix;
        info = MakeInternal(info);
        OverWrite(info, "NullabilityInfo.cs");
    }

    static string MakeInternal(string source) =>
        source
            .Replace(
                "public enum",
                """
                    #if PolyPublic
                    public
                    #endif
                    enum
                    """)
            .Replace(
                "public sealed class",
                """
                    #if PolyPublic
                    public
                    #endif
                    sealed class
                    """);

    static void OverWrite(string content, string file)
    {
        var path = Path.Combine(sourceOnlyDir, file);
        File.Delete(path);
        File.WriteAllText(path, content);
    }
}
#endif