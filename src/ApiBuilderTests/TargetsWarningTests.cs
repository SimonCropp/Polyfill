using System.Diagnostics;

// Runs the PolyfillValidateNugetTargets target from Polyfill.targets against a project that
// only imports the targets file, and checks when the PolyfillTargetsForNuget warning is raised.
public class TargetsWarningTests
{
    static string targetsPath = Path.Combine(ProjectFiles.SolutionDirectory, "Polyfill", "Polyfill.targets");

    // every .NET target up to the newest
    [Test]
    [Arguments("net8.0;net9.0;net10.0;net11.0", false)]
    [Arguments("net11.0;net6.0", false)]
    // newest .NET target missing
    [Arguments("net8.0;net9.0", true)]
    [Arguments("net8.0;net9.0;net10.0", true)]
    [Arguments("netcoreapp3.1;net6.0", true)]
    // every .NET Framework target up to the newest
    [Arguments("net462;net472;net48", false)]
    [Arguments("net462;net481", false)]
    // newest .NET Framework target missing
    [Arguments("net462;net472", true)]
    [Arguments("net462;net472;net11.0", true)]
    // netstandard needs both the newest .NET Framework and the newest .NET target
    [Arguments("netstandard2.0;net48;net11.0", false)]
    [Arguments("netstandard2.0;net11.0", true)]
    [Arguments("netstandard2.0;net48", true)]
    [Arguments("netstandard2.0;netstandard2.1", true)]
    public async Task MultiTargetWarning(string targetFrameworks, bool expectWarning)
    {
        var output = RunValidate(targetFrameworks, "-p:GeneratePackageOnBuild=true");

        await Assert.That(output.Contains("PolyfillTargetsForNuget")).IsEqualTo(expectWarning);
    }

    [Test]
    public async Task MultiTargetWarning_OnlyWhenPacking()
    {
        var output = RunValidate("net8.0;net9.0");

        await Assert.That(output).DoesNotContain("PolyfillTargetsForNuget");
    }

    [Test]
    public async Task MultiTargetWarning_Suppressed()
    {
        var output = RunValidate("net8.0;net9.0", "-p:GeneratePackageOnBuild=true", "-p:NoWarn=PolyfillTargetsForNuget");

        await Assert.That(output).DoesNotContain("PolyfillTargetsForNuget");
    }

    static string RunValidate(string targetFrameworks, params string[] arguments)
    {
        var directory = Path.Combine(Path.GetTempPath(), "PolyfillTargetsWarning", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        try
        {
            var project = Path.Combine(directory, "Validate.proj");
            File.WriteAllText(
                project,
                $"""
                 <Project>
                   <Import Project="{targetsPath}" />
                 </Project>
                 """);

            var first = targetFrameworks.Split(';')[0];
            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                WorkingDirectory = directory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            startInfo.ArgumentList.Add("msbuild");
            startInfo.ArgumentList.Add(project);
            startInfo.ArgumentList.Add("-nologo");
            startInfo.ArgumentList.Add("-nodeReuse:false");
            startInfo.ArgumentList.Add("-t:PolyfillValidateNugetTargets");
            startInfo.ArgumentList.Add($"-p:TargetFramework={first}");
            startInfo.ArgumentList.Add($"-p:TargetFrameworks={targetFrameworks.Replace(";", "%3B")}");
            foreach (var argument in arguments)
            {
                startInfo.ArgumentList.Add(argument);
            }

            using var process = Process.Start(startInfo)!;
            var error = process.StandardError.ReadToEndAsync();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new(
                    $"""
                     msbuild failed for {targetFrameworks}:
                     {output}
                     {error.Result}
                     """);
            }

            return output + error.Result;
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }
}
