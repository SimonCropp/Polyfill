public class RoslynComponentTargetsTest
{
    [Test]
    public void IsRoslynComponent_SuppressesPolyfillTargetsForNugetWarning()
    {
        using var tempDir = new TempDirectory();
        var polyfillTargetsPath = Path.Combine(ProjectFiles.SolutionDirectory, "Polyfill", "Polyfill.targets");
        var splitDir = Path.GetFullPath(Path.Combine(ProjectFiles.SolutionDirectory, "Split"));

        // Use TargetFrameworks (plural) with netstandard2.0 to trigger MaxNetRequired
        // (contains 'netstandard' but not 'net9'), then verify IsRoslynComponent suppresses it
        var csproj = $"""
                      <Project Sdk="Microsoft.NET.Sdk">
                        <PropertyGroup>
                          <TargetFrameworks>netstandard2.0</TargetFrameworks>
                          <OutputType>Library</OutputType>
                          <EnableDefaultItems>false</EnableDefaultItems>
                          <IsRoslynComponent>true</IsRoslynComponent>
                          <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
                          <LangVersion>14.0</LangVersion>
                          <SuppressTfmSupportBuildWarnings>true</SuppressTfmSupportBuildWarnings>
                        </PropertyGroup>
                        <ItemGroup>
                          <Compile Include="{splitDir}\netstandard2.0\**\*.cs" />
                        </ItemGroup>
                        <Import Project="{polyfillTargetsPath}" />
                        <ItemGroup>
                          <Compile Include="Class1.cs" />
                        </ItemGroup>
                      </Project>
                      """;

        File.WriteAllText(Path.Combine(tempDir, "TestProject.csproj"), csproj);
        File.WriteAllText(Path.Combine(tempDir, "Class1.cs"), "public class Class1 { }");

        var output = BuildProject(tempDir);

        if (output.Contains("PolyfillTargetsForNuget"))
        {
            throw new($"Expected no PolyfillTargetsForNuget warning for IsRoslynComponent project, but got:\n{output}");
        }
    }

    [Test]
    public void NonRoslynComponent_EmitsPolyfillTargetsForNugetWarning()
    {
        using var tempDir = new TempDirectory();
        var polyfillTargetsPath = Path.Combine(ProjectFiles.SolutionDirectory, "Polyfill", "Polyfill.targets");
        var splitDir = Path.GetFullPath(Path.Combine(ProjectFiles.SolutionDirectory, "Split"));

        // Use TargetFrameworks (plural) with netstandard2.0 to trigger MaxNetRequired
        // (contains 'netstandard' but not 'net9')
        var csproj = $"""
                      <Project Sdk="Microsoft.NET.Sdk">
                        <PropertyGroup>
                          <TargetFrameworks>netstandard2.0</TargetFrameworks>
                          <OutputType>Library</OutputType>
                          <EnableDefaultItems>false</EnableDefaultItems>
                          <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
                          <LangVersion>14.0</LangVersion>
                          <SuppressTfmSupportBuildWarnings>true</SuppressTfmSupportBuildWarnings>
                        </PropertyGroup>
                        <ItemGroup>
                          <Compile Include="{splitDir}\netstandard2.0\**\*.cs" />
                        </ItemGroup>
                        <Import Project="{polyfillTargetsPath}" />
                        <ItemGroup>
                          <Compile Include="Class1.cs" />
                        </ItemGroup>
                      </Project>
                      """;

        File.WriteAllText(Path.Combine(tempDir, "TestProject.csproj"), csproj);
        File.WriteAllText(Path.Combine(tempDir, "Class1.cs"), "public class Class1 { }");

        var output = BuildProject(tempDir);

        if (!output.Contains("PolyfillTargetsForNuget"))
        {
            throw new($"Expected PolyfillTargetsForNuget warning for non-RoslynComponent project, but it was not found in:\n{output}");
        }
    }

    static string BuildProject(string directory)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "build -c Release",
            WorkingDirectory = directory,
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
                 Build failed:
                 {output}
                 {error}
                 """);
        }

        return output + error;
    }
}
