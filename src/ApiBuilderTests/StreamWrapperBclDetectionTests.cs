using System.Text;

/// <summary>
/// The Stream wrapper types from dotnet/runtime#126669 (StringStream, ReadOnlyMemoryStream,
/// WritableMemoryStream, ReadOnlySequenceStream) were approved/merged for net11 but are NOT in the
/// current net11 preview/RC. Polyfill therefore ships them as ACTIVE on net11 too.
///
/// This project (ApiBuilderTests) targets net11.0 and does NOT compile the polyfill source into
/// itself, so it keeps building once the BCL gains these types. This test runs on the net11 runtime
/// and fails the moment the real types appear, telling you exactly which guards to flip.
/// </summary>
public class StreamWrapperBclDetectionTests
{
    static readonly (string FullName, string File)[] pending =
    [
        ("System.IO.StringStream", "StringStream.cs"),
        ("System.IO.ReadOnlyMemoryStream", "ReadOnlyMemoryStream.cs"),
        ("System.IO.WritableMemoryStream", "WritableMemoryStream.cs"),
        ("System.Buffers.ReadOnlySequenceStream", "ReadOnlySequenceStream.cs"),
    ];

    [Test]
    public void StreamWrapperPolyfillsStillNeededOnNet11()
    {
        var present = new List<(string FullName, string File, string Assembly)>();
        foreach (var (fullName, file) in pending)
        {
            var type = FindBclType(fullName);
            if (type != null)
            {
                present.Add((fullName, file, type.Assembly.GetName().Name!));
            }
        }

        if (present.Count == 0)
        {
            return;
        }

        var builder = new StringBuilder();
        builder.AppendLine($"{present.Count} Stream wrapper type(s) from dotnet/runtime#126669 are now provided by the BCL");
        builder.AppendLine($"on this runtime ({System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}).");
        builder.AppendLine("The matching Polyfill source now collides with the real type and must be retired for net11.");
        builder.AppendLine("For EACH type below, edit the listed file under src/Polyfill/:");
        builder.AppendLine();

        foreach (var (fullName, file, assembly) in present)
        {
            builder.AppendLine($"  {fullName}  (now in {assembly})");
            builder.AppendLine($"    file: src/Polyfill/{file}");
            builder.AppendLine("    1. change `#if FeatureMemory` to `#if FeatureMemory && !NET11_0_OR_GREATER`");
            builder.AppendLine("    2. append after the final #endif:");
            builder.AppendLine("         #if NET11_0_OR_GREATER");
            builder.AppendLine($"         [assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof({fullName}))]");
            builder.AppendLine("         #endif");
            builder.AppendLine();
        }

        builder.AppendLine("Then re-run ApiBuilderTests in Debug to regenerate Split + api_list, and drop any now-redundant");
        builder.AppendLine("usage from src/Consume/Consume.cs.");

        throw new(builder.ToString());
    }

    static System.Type? FindBclType(string fullName)
    {
        foreach (var assembly in new[]
                 {
                     "System.Private.CoreLib",
                     "System.Runtime",
                     "System.Memory",
                     "mscorlib"
                 })
        {
            var type = System.Type.GetType($"{fullName}, {assembly}", throwOnError: false);
            if (type is { IsPublic: true })
            {
                return type;
            }
        }

        return null;
    }
}
