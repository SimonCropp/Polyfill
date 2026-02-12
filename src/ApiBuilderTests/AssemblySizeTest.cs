public class AssemblySizeTest
{
    static readonly string[] targetFrameworks =
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
        "net10.0",
        "net11.0"
    ];

    [Test]
#if RELEASE
    [Explicit]
#endif
    public void MeasureAssemblySizes()
    {
        using var tempDir = new TempDirectory();

        var (sizes, sourceSizes) = MeasureAllVariants(tempDir);
        var results = ConvertToSizeResults(sizes);

        // Compute EmbedUntrackedSources results by adding source file sizes
        var resultsWithEmbed = ConvertToSizeResultsWithEmbed(sizes, sourceSizes);

        // Generate markdown
        var mdPath = Path.Combine(ProjectFiles.SolutionDirectory, "..", "assemblySize.include.md");
        using var writer = File.CreateText(mdPath);

        WriteTable(writer, results, "Assembly Sizes");
        writer.WriteLine();
        writer.WriteLine();
        WriteTable(writer, resultsWithEmbed, "Assembly Sizes with EmbedUntrackedSources");
    }

    static (Dictionary<string, Dictionary<string, long>> assemblySizes, Dictionary<string, Dictionary<string, long>> sourceSizes) MeasureAllVariants(string baseDir)
    {
        var projectDir = Path.Combine(baseDir, "build");
        Directory.CreateDirectory(projectDir);

        // Build all variants and collect sizes
        var allSizes = new Dictionary<string, Dictionary<string, long>>();
        var sourceSizes = new Dictionary<string, Dictionary<string, long>>();

        Console.WriteLine("  Building without polyfill...");
        (allSizes["without"], sourceSizes["without"]) = BuildAllFrameworksAndMeasure(projectDir, "without", polyfillImport: false, polyOptions: "");

        Console.WriteLine("  Building with polyfill...");
        (allSizes["with"], sourceSizes["with"]) = BuildAllFrameworksAndMeasure(projectDir, "with", polyfillImport: true, polyOptions: "<PolyEnsure>false</PolyEnsure><PolyArgumentExceptions>false</PolyArgumentExceptions><PolyStringInterpolation>false</PolyStringInterpolation><PolyNullability>false</PolyNullability>");

        Console.WriteLine("  Building with PolyEnsure...");
        (allSizes["ensure"], sourceSizes["ensure"]) = BuildAllFrameworksAndMeasure(projectDir, "ensure", polyfillImport: true, polyOptions: "<PolyEnsure>true</PolyEnsure>");

        Console.WriteLine("  Building with PolyArgumentExceptions...");
        (allSizes["argex"], sourceSizes["argex"]) = BuildAllFrameworksAndMeasure(projectDir, "argex", polyfillImport: true, polyOptions: "<PolyArgumentExceptions>true</PolyArgumentExceptions>");

        Console.WriteLine("  Building with PolyStringInterpolation...");
        (allSizes["stringinterp"], sourceSizes["stringinterp"]) = BuildAllFrameworksAndMeasure(projectDir, "stringinterp", polyfillImport: true, polyOptions: "<PolyStringInterpolation>true</PolyStringInterpolation>");

        Console.WriteLine("  Building with PolyNullability...");
        (allSizes["nullability"], sourceSizes["nullability"]) = BuildAllFrameworksAndMeasure(projectDir, "nullability", polyfillImport: true, polyOptions: "<PolyNullability>true</PolyNullability>");

        return (allSizes, sourceSizes);
    }

    static (Dictionary<string, long> assemblySizes, Dictionary<string, long> sourceSizes) BuildAllFrameworksAndMeasure(string projectDir, string variant, bool polyfillImport, string polyOptions)
    {
        var variantDir = Path.Combine(projectDir, variant);
        Directory.CreateDirectory(variantDir);

        var splitDir = Path.GetFullPath(Path.Combine(ProjectFiles.SolutionDirectory, "Split"));
        var polyfillTargetsPath = Path.Combine(ProjectFiles.SolutionDirectory, "Polyfill", "Polyfill.targets");

        // Calculate source file size per framework based on what's included
        var sourceSizes = new Dictionary<string, long>();
        foreach (var framework in targetFrameworks)
        {
            long sourceSize = 0;
            if (polyfillImport)
            {
                var frameworkDir = Path.Combine(splitDir, framework);
                if (Directory.Exists(frameworkDir))
                {
                    IEnumerable<string> allFiles = Directory.GetFiles(frameworkDir, "*.cs", SearchOption.AllDirectories);

                    // Exclude directories based on polyOptions (matching Polyfill.targets logic)
                    if (!polyOptions.Contains("<PolyEnsure>true</PolyEnsure>"))
                    {
                        allFiles = allFiles.Where(_ => !_.Contains($"{Path.DirectorySeparatorChar}Ensure{Path.DirectorySeparatorChar}"));
                    }
                    if (!polyOptions.Contains("<PolyArgumentExceptions>true</PolyArgumentExceptions>"))
                    {
                        allFiles = allFiles.Where(_ => !_.Contains($"{Path.DirectorySeparatorChar}ArgumentExceptions{Path.DirectorySeparatorChar}"));
                    }
                    if (!polyOptions.Contains("<PolyStringInterpolation>true</PolyStringInterpolation>"))
                    {
                        allFiles = allFiles.Where(_ => !_.Contains($"{Path.DirectorySeparatorChar}StringInterpolation{Path.DirectorySeparatorChar}"));
                    }
                    if (!polyOptions.Contains("<PolyNullability>true</PolyNullability>"))
                    {
                        allFiles = allFiles.Where(_ => !_.Contains($"{Path.DirectorySeparatorChar}Nullability{Path.DirectorySeparatorChar}"));
                    }

                    // Calculate compressed size (EmbedUntrackedSources uses deflate compression)
                    sourceSize = allFiles.Sum(file =>
                    {
                        var content = File.ReadAllBytes(file);
                        using var output = new MemoryStream();
                        using (var deflate = new DeflateStream(output, CompressionLevel.Optimal, leaveOpen: true))
                        {
                            deflate.Write(content);
                        }
                        return output.Length;
                    });
                }
            }
            sourceSizes[framework] = sourceSize;
        }

        // Include Split source files based on TargetFramework, then use Polyfill.targets for Remove logic
        var polyfillSourceIncludes = polyfillImport
            ? $"""
                 <ItemGroup>
                   <Compile Include="{splitDir}\$(TargetFramework)\**\*.cs" />
                 </ItemGroup>
               """
            : "";
        var polyfillImportLines = polyfillImport
            ? $"""
                 <Import Project="{polyfillTargetsPath}" />
               """
            : "";

        var allFrameworks = string.Join(";", targetFrameworks);

        var csproj = $"""
                      <Project Sdk="Microsoft.NET.Sdk">
                        <PropertyGroup>
                          <TargetFrameworks>{allFrameworks}</TargetFrameworks>
                          <OutputType>Library</OutputType>
                          <EnableDefaultItems>false</EnableDefaultItems>
                          <NoWarn>$(NoWarn);NU1902;NU1903;PolyfillTargetsForNuget</NoWarn>
                          <LangVersion>14.0</LangVersion>
                          <DebugType>embedded</DebugType>
                          <DebugSymbols>true</DebugSymbols>
                          <Deterministic>true</Deterministic>
                          <SuppressTfmSupportBuildWarnings>true</SuppressTfmSupportBuildWarnings>
                          {polyOptions}
                        </PropertyGroup>
                        <ItemGroup>
                          <PackageReference Include="System.Memory" Condition="'$(TargetFrameworkIdentifier)' == '.NETStandard' or '$(TargetFrameworkIdentifier)' == '.NETFramework' or $(TargetFramework.StartsWith('netcoreapp'))" Version="4.5.5" />
                          <PackageReference Include="System.ValueTuple" Condition="$(TargetFramework.StartsWith('net46'))" Version="4.5.0" />
                          <PackageReference Include="System.Net.Http" Condition="$(TargetFramework.StartsWith('net4'))" Version="4.3.4" />
                          <PackageReference Include="System.Threading.Tasks.Extensions" Condition="'$(TargetFramework)' == 'netstandard2.0' or '$(TargetFramework)' == 'netcoreapp2.0' or '$(TargetFrameworkIdentifier)' == '.NETFramework'" Version="4.5.4" />
                          <PackageReference Include="System.Runtime.InteropServices.RuntimeInformation" Condition="$(TargetFramework.StartsWith('net4'))" Version="4.3.0" />
                          <PackageReference Include="System.IO.Compression" Condition="'$(TargetFrameworkIdentifier)' == '.NETFramework'" Version="4.3.0" />
                          <PackageReference Include="Microsoft.Bcl.AsyncInterfaces" Condition="'$(TargetFrameworkIdentifier)' == '.NETFramework' or '$(TargetFramework)' == 'netstandard2.0' or $(TargetFramework.StartsWith('netcoreapp2'))" Version="9.0.1" />
                        </ItemGroup>
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
            Arguments = $"build -c Release /maxcpucount:{Math.Max(1, Environment.ProcessorCount / 2)}",
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

        foreach (var framework in targetFrameworks)
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

        return (sizes, sourceSizes);
    }

    static List<SizeResult> ConvertToSizeResults(Dictionary<string, Dictionary<string, long>> allSizes)
    {
        var results = new List<SizeResult>();

        foreach (var framework in targetFrameworks)
        {
            results.Add(new()
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

    static List<SizeResult> ConvertToSizeResultsWithEmbed(Dictionary<string, Dictionary<string, long>> allSizes, Dictionary<string, Dictionary<string, long>> sourceSizes)
    {
        var results = new List<SizeResult>();

        foreach (var framework in targetFrameworks)
        {
            results.Add(new()
            {
                TargetFramework = framework,
                SizeWithoutPolyfill = allSizes["without"].GetValueOrDefault(framework, -1) + sourceSizes["without"].GetValueOrDefault(framework, 0),
                SizeWithPolyfill = allSizes["with"].GetValueOrDefault(framework, -1) + sourceSizes["with"].GetValueOrDefault(framework, 0),
                SizeWithEnsure = allSizes["ensure"].GetValueOrDefault(framework, -1) + sourceSizes["ensure"].GetValueOrDefault(framework, 0),
                SizeWithArgumentExceptions = allSizes["argex"].GetValueOrDefault(framework, -1) + sourceSizes["argex"].GetValueOrDefault(framework, 0),
                SizeWithStringInterpolation = allSizes["stringinterp"].GetValueOrDefault(framework, -1) + sourceSizes["stringinterp"].GetValueOrDefault(framework, 0),
                SizeWithNullability = allSizes["nullability"].GetValueOrDefault(framework, -1) + sourceSizes["nullability"].GetValueOrDefault(framework, 0)
            });
        }

        return results;
    }

    static void WriteTable(StreamWriter writer, List<SizeResult> results, string title)
    {
        writer.WriteLine($"### {title}");
        writer.WriteLine();
        writer.WriteLine("|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |");
        writer.WriteLine("|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|");

        foreach (var result in results)
        {
            var sizeDiff = result.SizeWithPolyfill - result.SizeWithoutPolyfill;
            var sizeDiffEnsure = result.SizeWithEnsure - result.SizeWithPolyfill;
            var sizeDiffArgEx = result.SizeWithArgumentExceptions - result.SizeWithPolyfill;
            var sizeDiffStringInterp = result.SizeWithStringInterpolation - result.SizeWithPolyfill;
            var sizeDiffNullability = result.SizeWithNullability - result.SizeWithPolyfill;

            Debug.Assert(sizeDiffEnsure > 0, $"sizeDiffEnsure should be positive for {result.TargetFramework}, but was {sizeDiffEnsure}");
            Debug.Assert(sizeDiffNullability > 0, $"sizeDiffNullability should be positive for {result.TargetFramework}, but was {sizeDiffNullability}");

            writer.WriteLine($"| {result.TargetFramework,-14} | {FormatSize(result.SizeWithoutPolyfill),14} | {FormatSize(result.SizeWithPolyfill),13} | {FormatSizeDiff(sizeDiff),9} | {FormatSizeDiff(sizeDiffEnsure),9} | {FormatSizeDiff(sizeDiffArgEx),18} | {FormatSizeDiff(sizeDiffStringInterp),19} | {FormatSizeDiff(sizeDiffNullability),11} |");
        }
    }

    static string FormatSize(long bytes)
    {
        if (bytes < 1024)
        {
            return string.Create(CultureInfo.InvariantCulture, $"{bytes:N0}bytes");
        }

        var kb = bytes / 1024.0;
        return string.Create(CultureInfo.InvariantCulture, $"{kb:N1}KB");
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
        public string TargetFramework { get; init; } = "";
        public long SizeWithoutPolyfill { get; init; }
        public long SizeWithPolyfill { get; init; }
        public long SizeWithEnsure { get; init; }
        public long SizeWithArgumentExceptions { get; init; }
        public long SizeWithStringInterpolation { get; init; }
        public long SizeWithNullability { get; init; }
    }
}
