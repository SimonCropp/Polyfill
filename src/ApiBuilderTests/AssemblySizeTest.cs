[TestFixture]
public class AssemblySizeTest
{
    static string solutionDirectory = SolutionDirectoryFinder.Find();

    static readonly string[] TargetFrameworks =
    [
        "net461",
        "net462",
        "net47",
        "net471",
        "net472",
        "net48",
        "net481",
        "net6.0-windows",
        "netstandard2.0",
        "netstandard2.1",
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

            foreach (var tfm in TargetFrameworks)
            {
                Console.WriteLine($"Processing {tfm}...");

                // Measure without EmbedUntrackedSources
                var result = MeasureFramework(tempDir, tfm, embedUntrackedSources: false);
                results.Add(result);

                // Measure with EmbedUntrackedSources
                var resultWithEmbed = MeasureFramework(tempDir, tfm, embedUntrackedSources: true);
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
        writer.WriteLine("|                | size without polyfill | size with polyfill | size difference | size difference using PolyEnsure | size difference with PolyArgumentExceptions | size difference with PolyStringInterpolation | size difference with PolyNullability |");
        writer.WriteLine("|----------------|-----------------------|--------------------|-----------------|----------------------------------|---------------------------------------------|----------------------------------------------|--------------------------------------|");

        foreach (var result in results)
        {
            var sizeDiff = result.SizeWithPolyfill - result.SizeWithoutPolyfill;
            var sizeDiffEnsure = result.SizeWithEnsure - result.SizeWithoutPolyfill;
            var sizeDiffArgEx = result.SizeWithArgumentExceptions - result.SizeWithoutPolyfill;
            var sizeDiffStringInterp = result.SizeWithStringInterpolation - result.SizeWithoutPolyfill;
            var sizeDiffNullability = result.SizeWithNullability - result.SizeWithoutPolyfill;

            writer.WriteLine($"| {result.TargetFramework,-14} | {FormatSize(result.SizeWithoutPolyfill),21} | {FormatSize(result.SizeWithPolyfill),18} | {FormatSizeDiff(sizeDiff),15} | {FormatSizeDiff(sizeDiffEnsure),32} | {FormatSizeDiff(sizeDiffArgEx),43} | {FormatSizeDiff(sizeDiffStringInterp),44} | {FormatSizeDiff(sizeDiffNullability),36} |");
        }
    }

    static string FormatSize(long bytes)
    {
        if (bytes < 0)
        {
            return "N/A";
        }

        return $"{bytes:N0} bytes";
    }

    static string FormatSizeDiff(long bytes)
    {
        if (bytes < 0)
        {
            return "N/A";
        }

        return $"+{bytes:N0} bytes";
    }

    static SizeResult MeasureFramework(string baseDir, string targetFramework, bool embedUntrackedSources)
    {
        var result = new SizeResult {TargetFramework = targetFramework};
        var embedSuffix = embedUntrackedSources ? "_embed" : "";
        var projectDir = Path.Combine(baseDir, $"{targetFramework}{embedSuffix}");
        Directory.CreateDirectory(projectDir);

        var embedProperty = embedUntrackedSources ? "<EmbedUntrackedSources>true</EmbedUntrackedSources>" : "";

        // Build without polyfill
        result.SizeWithoutPolyfill = BuildAndMeasure(projectDir, targetFramework, "without", embedProperty, polyfillImport: false, polyOptions: "");

        // Build with polyfill (no extra options)
        result.SizeWithPolyfill = BuildAndMeasure(projectDir, targetFramework, "with", embedProperty, polyfillImport: true, polyOptions: "");

        // Build with PolyEnsure
        result.SizeWithEnsure = BuildAndMeasure(projectDir, targetFramework, "ensure", embedProperty, polyfillImport: true, polyOptions: "<PolyEnsure>true</PolyEnsure>");

        // Build with PolyArgumentExceptions
        result.SizeWithArgumentExceptions = BuildAndMeasure(projectDir, targetFramework, "argex", embedProperty, polyfillImport: true, polyOptions: "<PolyArgumentExceptions>true</PolyArgumentExceptions>");

        // Build with PolyStringInterpolation
        result.SizeWithStringInterpolation = BuildAndMeasure(projectDir, targetFramework, "stringinterp", embedProperty, polyfillImport: true, polyOptions: "<PolyStringInterpolation>true</PolyStringInterpolation>");

        // Build with PolyNullability
        result.SizeWithNullability = BuildAndMeasure(projectDir, targetFramework, "nullability", embedProperty, polyfillImport: true, polyOptions: "<PolyNullability>true</PolyNullability>");

        return result;
    }

    static long BuildAndMeasure(string projectDir, string targetFramework, string variant, string embedProperty, bool polyfillImport, string polyOptions)
    {
        var variantDir = Path.Combine(projectDir, variant);
        Directory.CreateDirectory(variantDir);

        var polyfillImportLines = polyfillImport
            ? $"""
                 <Import Project="{solutionDirectory}\TestIncludes.targets" />
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