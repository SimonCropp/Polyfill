[TestFixture]
public class AssemblySizeTest
{
    static readonly string[] TargetFrameworks =
    [
        "netstandard2.0",
        "netstandard2.1",
        "net461",
        "net462",
        "net47",
        "net471",
        "net472",
        "net48",
        "net481",
        "netcoreapp2.0",
        "netcoreapp2.1",
        "netcoreapp2.2",
        "netcoreapp3.0",
        "netcoreapp3.1",
        "net5.0",
        "net6.0",
        "net7.0",
        "net8.0",
        "net9.0",
        "net10.0"
    ];

    [Test]
    [Explicit]
    public void MeasureAssemblySizes()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), $"PolyfillSizeTest_{Guid.NewGuid():N}");
        Directory.CreateDirectory(tempDir);

        try
        {
            var results = new List<SizeResult>();
            var resultsWithEmbed = new List<SizeResult>();

            Console.WriteLine("Building without EmbedUntrackedSources...");
            var sizesWithoutEmbed = MeasureAllVariants(tempDir, "no_embed", embedUntrackedSources: false);
            results = ConvertToSizeResults(sizesWithoutEmbed);

            Console.WriteLine("Building with EmbedUntrackedSources...");
            var sizesWithEmbed = MeasureAllVariants(tempDir, "with_embed", embedUntrackedSources: true);
            resultsWithEmbed = ConvertToSizeResults(sizesWithEmbed);

            // Generate markdown
            var mdPath = Path.Combine(ProjectFiles.SolutionDirectory, "..", "assemblySize.include.md");
            using var writer = File.CreateText(mdPath);

            WriteTable(writer, results, "Assembly Sizes");
            writer.WriteLine();
            writer.WriteLine();
            WriteTable(writer, resultsWithEmbed, "Assembly Sizes with EmbedUntrackedSources");

            Console.WriteLine($"Results written to {mdPath}");
        }
        finally
        {
            Directory.Delete(tempDir, recursive: true);
        }
    }

    static Dictionary<string, Dictionary<string, long>> MeasureAllVariants(string baseDir, string suffix, bool embedUntrackedSources)
    {
        var projectDir = Path.Combine(baseDir, suffix);
        Directory.CreateDirectory(projectDir);

        var embedProperty = embedUntrackedSources ? "<EmbedUntrackedSources>true</EmbedUntrackedSources>" : "";

        // Build all variants and collect sizes
        var allSizes = new Dictionary<string, Dictionary<string, long>>();

        Console.WriteLine("  Building without polyfill.. .");
        allSizes["without"] = BuildAllFrameworksAndMeasure(projectDir, "without", embedProperty, polyfillImport: false, polyOptions: "");

        Console.WriteLine("  Building with polyfill.. .");
        allSizes["with"] = BuildAllFrameworksAndMeasure(projectDir, "with", embedProperty, polyfillImport: true, polyOptions: "<PolyEnsure>false</PolyEnsure><PolyArgumentExceptions>false</PolyArgumentExceptions><PolyStringInterpolation>false</PolyStringInterpolation><PolyNullability>false</PolyNullability>");

        Console.WriteLine("  Building with PolyEnsure...");
        allSizes["ensure"] = BuildAllFrameworksAndMeasure(projectDir, "ensure", embedProperty, polyfillImport: true, polyOptions: "<PolyEnsure>true</PolyEnsure>");

        Console.WriteLine("  Building with PolyArgumentExceptions...");
        allSizes["argex"] = BuildAllFrameworksAndMeasure(projectDir, "argex", embedProperty, polyfillImport: true, polyOptions: "<PolyArgumentExceptions>true</PolyArgumentExceptions>");

        Console.WriteLine("  Building with PolyStringInterpolation...");
        allSizes["stringinterp"] = BuildAllFrameworksAndMeasure(projectDir, "stringinterp", embedProperty, polyfillImport: true, polyOptions: "<PolyStringInterpolation>true</PolyStringInterpolation>");

        Console.WriteLine("  Building with PolyNullability...");
        allSizes["nullability"] = BuildAllFrameworksAndMeasure(projectDir, "nullability", embedProperty, polyfillImport: true, polyOptions: "<PolyNullability>true</PolyNullability>");

        return allSizes;
    }

    static Dictionary<string, long> BuildAllFrameworksAndMeasure(string projectDir, string variant, string embedProperty, bool polyfillImport, string polyOptions)
    {
        var variantDir = Path.Combine(projectDir, variant);
        Directory.CreateDirectory(variantDir);

        // Navigate from assembly location to find Polyfill directory
        var assemblyDir = Path.GetDirectoryName(typeof(AssemblySizeTest).Assembly.Location)!;
        // Go up from bin/Debug/net10.0 to src, then into Polyfill
        var polyfillDir = Path.GetFullPath(Path.Combine(assemblyDir, "..", "..", "..", "..", "Polyfill"));
        var polyfillTargetsPath = Path.Combine(polyfillDir, "Polyfill.targets");

        // Include Polyfill source files before the targets (which use Remove to exclude based on options)
        var polyfillSourceIncludes = polyfillImport
            ? $"""
                 <ItemGroup>
                   <Compile Include="{polyfillDir}\**\*.cs" Exclude="{polyfillDir}\obj\**;{polyfillDir}\bin\**" />
                 </ItemGroup>
               """
            : "";
        var polyfillImportLines = polyfillImport
            ? $"""
                 <Import Project="{polyfillTargetsPath}" />
               """
            : "";

        var packageReferences = polyfillImport
            ? """
                <ItemGroup>
                  <PackageReference Include="System.Memory" Condition="'$(TargetFrameworkIdentifier)' == '.NETStandard' or '$(TargetFrameworkIdentifier)' == '.NETFramework' or $(TargetFramework.StartsWith('netcoreapp'))" Version="4.5.5" />
                  <PackageReference Include="System.ValueTuple" Condition="$(TargetFramework.StartsWith('net46'))" Version="4.5.0" />
                  <PackageReference Include="System.Net.Http" Condition="$(TargetFramework.StartsWith('net4'))" Version="4.3.4" />
                  <PackageReference Include="System.Threading.Tasks.Extensions" Condition="'$(TargetFramework)' == 'netstandard2.0' or '$(TargetFramework)' == 'netcoreapp2.0' or '$(TargetFrameworkIdentifier)' == '.NETFramework'" Version="4.5.4" />
                  <PackageReference Include="System.Runtime.InteropServices.RuntimeInformation" Condition="$(TargetFramework.StartsWith('net4'))" Version="4.3.0" />
                  <PackageReference Include="System.IO.Compression" Condition="'$(TargetFrameworkIdentifier)' == '.NETFramework'" Version="4.3.0" />
                </ItemGroup>
              """
            : "";

        var allFrameworks = string.Join(";", TargetFrameworks);

        var csproj = $"""
                      <Project Sdk="Microsoft.NET.Sdk">
                        <PropertyGroup>
                          <TargetFrameworks>{allFrameworks}</TargetFrameworks>
                          <OutputType>Library</OutputType>
                          <GenerateAssemblyInfo>false</GenerateAssemblyInfo>
                          <EnableDefaultItems>false</EnableDefaultItems>
                          <NoWarn>$(NoWarn);PolyfillTargetsForNuget</NoWarn>
                          <LangVersion>preview</LangVersion>
                          <DebugType>embedded</DebugType>
                          <DebugSymbols>true</DebugSymbols>
                          {embedProperty}
                          {polyOptions}
                        </PropertyGroup>
                        {packageReferences}
                        {polyfillSourceIncludes}
                        {polyfillImportLines}
                        <ItemGroup>
                          <Compile Include="Class1.cs" />
                        </ItemGroup>
                      </Project>
                      """;

        var csprojPath = Path.Combine(variantDir, "TestProject.csproj");
        File.WriteAllText(csprojPath, csproj);

        var classFile = """
                        public class Class1
                        {
                            public void Method1() { }
                        }
                        """;
        File.WriteAllText(Path.Combine(variantDir, "Class1.cs"), classFile);

        // Build the project once for all frameworks
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "build -c Release",
            WorkingDirectory = variantDir,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(startInfo)!;
        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        process.WaitForExit(120000);

        if (process.ExitCode != 0)
        {
            throw new(
                $"""
                 Build failed for {variant}:
                 {output}
                 {error}
                 """);
        }

        // Collect sizes from all framework DLLs
        var sizes = new Dictionary<string, long>();
        var binPath = Path.Combine(variantDir, "bin", "Release");

        foreach (var framework in TargetFrameworks)
        {
            var dllPath = Path.Combine(binPath, framework, "TestProject.dll");
            if (File.Exists(dllPath))
            {
                var fileInfo = new FileInfo(dllPath);
                sizes[framework] = fileInfo.Length;
            }
            else
            {
                Console.WriteLine($"    Warning: DLL not found for {framework} at {dllPath}");
                sizes[framework] = -1; // Mark as unavailable
            }
        }

        return sizes;
    }

    static List<SizeResult> ConvertToSizeResults(Dictionary<string, Dictionary<string, long>> allSizes)
    {
        var results = new List<SizeResult>();

        foreach (var framework in TargetFrameworks)
        {
            results.Add(new SizeResult
            {
                TargetFramework = framework,
                SizeWithoutPolyfill = allSizes["without"].GetValueOrDefault(framework, -1),
                SizeWithPolyfill = allSizes["with"].GetValueOrDefault(framework, -1),
                SizeWithEnsure = allSizes["ensure"].GetValueOrDefault(framework, -1),
                SizeWithArgumentExceptions = allSizes["argex"].GetValueOrDefault(framework, -1),
                SizeWithStringInterpolation = allSizes["stringinterp"].GetValueOrDefault(framework, -1),
                SizeWithNullability = allSizes["nullability"].GetValueOrDefault(framework, -1)
            });
        }

        return results;
    }

    static void WriteTable(StreamWriter writer, List<SizeResult> results, string title)
    {
        writer.WriteLine($"### {title}");
        writer.WriteLine();
        writer.WriteLine("|                | without polyfill | with polyfill | difference | PolyEnsure | PolyArgumentExceptions | PolyStringInterpolation | PolyNullability |");
        writer.WriteLine("|----------------|------------------|---------------|------------|------------|------------------------|-------------------------|-----------------|");

        foreach (var result in results)
        {
            var sizeDiff = result.SizeWithPolyfill - result.SizeWithoutPolyfill;
            var sizeDiffEnsure = result.SizeWithEnsure - result.SizeWithPolyfill;
            var sizeDiffArgEx = result.SizeWithArgumentExceptions - result.SizeWithPolyfill;
            var sizeDiffStringInterp = result.SizeWithStringInterpolation - result.SizeWithPolyfill;
            var sizeDiffNullability = result.SizeWithNullability - result.SizeWithPolyfill;

            Debug.Assert(sizeDiffEnsure > 0, $"sizeDiffEnsure should be positive for {result.TargetFramework}, but was {sizeDiffEnsure}");
            Debug.Assert(sizeDiffNullability > 0, $"sizeDiffNullability should be positive for {result.TargetFramework}, but was {sizeDiffNullability}");

            writer.WriteLine($"| {result.TargetFramework,-14} | {FormatSize(result.SizeWithoutPolyfill),16} | {FormatSize(result.SizeWithPolyfill),13} | {FormatSizeDiff(sizeDiff),10} | {FormatSizeDiff(sizeDiffEnsure),10} | {FormatSizeDiff(sizeDiffArgEx),22} | {FormatSizeDiff(sizeDiffStringInterp),23} | {FormatSizeDiff(sizeDiffNullability),15} |");
        }
    }

    static string FormatSize(long bytes)
    {
        if (bytes < 1024)
        {
            return $"{bytes:N0} bytes";
        }

        var kb = bytes / 1024.0;
        return $"{kb:N} KB";
    }

    static string FormatSizeDiff(long bytes)
    {
        if (bytes == 0)
        {
            return "";
        }

        return $"+{FormatSize(bytes)}";
    }

    class SizeResult
    {
        public string TargetFramework { get; set; } = "";
        public long SizeWithoutPolyfill { get; set; }
        public long SizeWithPolyfill { get; set; }
        public long SizeWithEnsure { get; set; }
        public long SizeWithArgumentExceptions { get; set; }
        public long SizeWithStringInterpolation { get; set; }
        public long SizeWithNullability { get; set; }
    }
}