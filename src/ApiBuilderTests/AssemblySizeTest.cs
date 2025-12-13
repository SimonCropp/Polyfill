[TestFixture]
public class AssemblySizeTest
{
    static string solutionDirectory = SolutionDirectoryFinder.Find();

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

            foreach (var framework in TargetFrameworks)
            {
                Console.WriteLine($"Processing {framework}...");

                var result = MeasureFramework(tempDir, framework, embedUntrackedSources: false);
                results.Add(result);

                var resultWithEmbed = MeasureFramework(tempDir, framework, embedUntrackedSources: true);
                resultsWithEmbed.Add(resultWithEmbed);
            }

            // Generate markdown
            var mdPath = Path.Combine(solutionDirectory, "..", "assemblySize.include.md");
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

    static void WriteTable(StreamWriter writer, List<SizeResult> results, string title)
    {
        writer.WriteLine($"### {title}");
        writer.WriteLine();
        writer.WriteLine("|                | without polyfill | with polyfill | difference | PolyEnsure | PolyArgumentExceptions | PolyStringInterpolation | PolyNullability |");
        writer.WriteLine("|----------------|------------------|---------------|------------|------------|------------------------|-------------------------|-----------------|");

        foreach (var result in results)
        {
            var sizeDiff = result.SizeWithPolyfill - result.SizeWithoutPolyfill;
            var sizeDiffEnsure = result.SizeWithEnsure - result.SizeWithoutPolyfill;
            var sizeDiffArgEx = result.SizeWithArgumentExceptions - result.SizeWithoutPolyfill;
            var sizeDiffStringInterp = result.SizeWithStringInterpolation - result.SizeWithoutPolyfill;
            var sizeDiffNullability = result.SizeWithNullability - result.SizeWithoutPolyfill;

            writer.WriteLine($"| {result.TargetFramework,-14} | {FormatSize(result.SizeWithoutPolyfill),16} | {FormatSize(result.SizeWithPolyfill),13} | {FormatSizeDiff(sizeDiff),10} | {FormatSizeDiff(sizeDiffEnsure),10} | {FormatSizeDiff(sizeDiffArgEx),22} | {FormatSizeDiff(sizeDiffStringInterp),23} | {FormatSizeDiff(sizeDiffNullability),15} |");
        }
    }

    static string FormatSize(long bytes)
    {
        if (bytes < 0)
        {
            return "N/A";
        }

        return $"{bytes/1024:N0} kb";
    }

    static string FormatSizeDiff(long bytes)
    {
        if (bytes < 0)
        {
            return "N/A";
        }

        return $"+{bytes/1024:N0} kb";
    }

    static SizeResult MeasureFramework(string baseDir, string framework, bool embedUntrackedSources)
    {
        var result = new SizeResult {TargetFramework = framework};
        var embedSuffix = embedUntrackedSources ? "_embed" : "";
        var projectDir = Path.Combine(baseDir, $"{framework}{embedSuffix}");
        Directory.CreateDirectory(projectDir);

        var embedProperty = embedUntrackedSources ? "<EmbedUntrackedSources>true</EmbedUntrackedSources>" : "";

        // Build without polyfill
        result.SizeWithoutPolyfill = BuildAndMeasure(projectDir, framework, "without", embedProperty, polyfillImport: false, polyOptions: "");

        // Build with polyfill (no extra options)
        result.SizeWithPolyfill = BuildAndMeasure(projectDir, framework, "with", embedProperty, polyfillImport: true, polyOptions: "");

        // Build with PolyEnsure
        result.SizeWithEnsure = BuildAndMeasure(projectDir, framework, "ensure", embedProperty, polyfillImport: true, polyOptions: "<PolyEnsure>true</PolyEnsure>");

        // Build with PolyArgumentExceptions
        result.SizeWithArgumentExceptions = BuildAndMeasure(projectDir, framework, "argex", embedProperty, polyfillImport: true, polyOptions: "<PolyArgumentExceptions>true</PolyArgumentExceptions>");

        // Build with PolyStringInterpolation
        result.SizeWithStringInterpolation = BuildAndMeasure(projectDir, framework, "stringinterp", embedProperty, polyfillImport: true, polyOptions: "<PolyStringInterpolation>true</PolyStringInterpolation>");

        // Build with PolyNullability
        result.SizeWithNullability = BuildAndMeasure(projectDir, framework, "nullability", embedProperty, polyfillImport: true, polyOptions: "<PolyNullability>true</PolyNullability>");

        return result;
    }

    static long BuildAndMeasure(string projectDir, string targetFramework, string variant, string embedProperty, bool polyfillImport, string polyOptions)
    {
        var variantDir = Path.Combine(projectDir, variant);
        Directory.CreateDirectory(variantDir);

        var polyfillImportLines = polyfillImport
            ? $"""
                 <Import Project="{solutionDirectory}\Polyfill\Polyfill.targets" />
               """
            : "";

        // Add package references needed for older frameworks when using polyfill
        var packageReferences = polyfillImport
            ? """
                <ItemGroup>
                  <PackageReference Include="System.Memory" Condition="$(TargetFrameworkIdentifier) == '.NETStandard' or $(TargetFrameworkIdentifier) == '.NETFramework' or $(TargetFramework.StartsWith('netcoreapp'))" Version="4.5.5" />
                  <PackageReference Include="System.ValueTuple" Condition="$(TargetFramework.StartsWith('net46'))" Version="4.5.0" />
                  <PackageReference Include="System.Net.Http" Condition="$(TargetFramework.StartsWith('net4'))" Version="4.3.4" />
                  <PackageReference Include="System.Threading.Tasks.Extensions" Condition="$(TargetFramework) == 'netstandard2.0' or $(TargetFramework) == 'netcoreapp2.0' or $(TargetFrameworkIdentifier) == '.NETFramework'" Version="4.5.4" />
                  <PackageReference Include="System.Runtime.InteropServices.RuntimeInformation" Condition="$(TargetFramework.StartsWith('net4'))" Version="4.3.0" />
                  <PackageReference Include="System.IO.Compression" Condition="$(TargetFrameworkIdentifier) == '.NETFramework'" Version="4.3.0" />
                </ItemGroup>
              """
            : "";

        var csproj = $"""
                      <Project Sdk="Microsoft.NET.Sdk">
                        <PropertyGroup>
                          <TargetFramework>{targetFramework}</TargetFramework>
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
                        {polyfillImportLines}
                        <ItemGroup>
                          <Compile Include="Class1.cs" />
                        </ItemGroup>
                      </Project>
                      """;

        var csprojPath = Path.Combine(variantDir, "TestProject.csproj");
        File.WriteAllText(csprojPath, csproj);

        // Create a minimal class file
        var classFile = """
                        namespace TestProject
                        {
                            public class Class1
                            {
                                public void Method1() { }
                            }
                        }
                        """;
        File.WriteAllText(Path.Combine(variantDir, "Class1.cs"), classFile);

        // Build the project
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "build -c Release --no-restore",
            WorkingDirectory = variantDir,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // First restore
        var restoreInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "restore",
            WorkingDirectory = variantDir,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var restoreProcess = Process.Start(restoreInfo)!)
        {
            restoreProcess.WaitForExit(120000);
        }

        using var process = Process.Start(startInfo)!;
        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        process.WaitForExit(120000);

        if (process.ExitCode != 0)
        {
            Console.WriteLine($"Build failed for {targetFramework} ({variant}):");
            Console.WriteLine(output);
            Console.WriteLine(error);
            return -1;
        }

        // Find the DLL
        var dllPath = Path.Combine(variantDir, "bin", "Release", targetFramework, "TestProject.dll");
        if (File.Exists(dllPath))
        {
            var fileInfo = new FileInfo(dllPath);
            return fileInfo.Length;
        }

        throw new($"DLL not found at {dllPath}");
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