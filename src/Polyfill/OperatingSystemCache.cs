namespace Polyfills;

#if FeatureRuntimeInformation && !NET

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
static class OperatingSystemCache
{
    static Version? windowsVersion;
    static Version? freeBsdVersion;
    static Version? androidVersion;
    static bool? isAndroid;
    static Version? macOSVersion;

    static string RunProcess(string name, string arguments)
    {
        using var process = new Process()
        {
            StartInfo = new()
            {
                FileName = name,
                Arguments = arguments,
                RedirectStandardInput = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();
        process.WaitForExit();

        return process.StandardOutput.ReadToEnd();
    }

    public static bool IsMacOSVersionAtLeast(int major, int minor = 0, int build = 0)
    {
        if (!OperatingSystem.IsMacOS())
        {
            return false;
        }

        // prints only the version, e.g. "14.5" or "10.15.7"
        macOSVersion ??= ParseVersion(RunProcess("/usr/bin/sw_vers", "-productVersion"));

        return macOSVersion >= new Version(major, minor, build, 0);
    }

    public static bool IsFreeBSDVersionAtLeast(int major, int minor, int build = 0, int revision = 0)
    {
        if (!OperatingSystem.IsFreeBSD())
        {
            return false;
        }

        if (freeBsdVersion == null)
        {
            var versionString = Environment.OSVersion.VersionString
                .Replace("Unix", string.Empty)
                .Replace("FreeBSD", string.Empty)
                .Replace("-release", string.Empty)
                .Replace(" ", string.Empty);

            freeBsdVersion = Version.Parse(versionString);
        }

        return freeBsdVersion >= new Version(major, minor, build, revision);
    }

    public static bool IsWindowsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
    {
        if (!OperatingSystem.IsWindows())
        {
            return false;
        }

        if (windowsVersion == null)
        {
            var input = RuntimeInformation.OSDescription
                .Replace("Microsoft Windows", string.Empty)
                .Replace(" ", string.Empty);
            windowsVersion = Version.Parse(input);
        }

        return windowsVersion >= new Version(major, minor, build, revision);
    }

    public static bool IsAndroid()
    {
        if (!isAndroid.HasValue)
        {
            isAndroid = RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID"));
        }

        return isAndroid.Value;
    }

    public static bool IsAndroidVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
    {
        if (!IsAndroid())
        {
            return false;
        }

        // e.g. "14" or "8.1.0"
        androidVersion ??= ParseVersion(RunProcess("getprop", "ro.build.version.release"));

        return androidVersion >= new Version(major, minor, build, revision);
    }

    /// <summary>
    /// Parses the first version number found in <paramref name="input"/>, ignoring any surrounding text.
    /// Components that are not present are set to 0, so the result can be compared to a four part version.
    /// </summary>
    internal static Version ParseVersion(string input)
    {
        var start = 0;
        while (start < input.Length && !char.IsDigit(input[start]))
        {
            start++;
        }

        var end = start;
        while (end < input.Length && (char.IsDigit(input[end]) || input[end] == '.'))
        {
            end++;
        }

        var parts = input.Substring(start, end - start).Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
        var components = new int[4];
        for (var index = 0; index < parts.Length && index < components.Length; index++)
        {
            components[index] = int.Parse(parts[index], CultureInfo.InvariantCulture);
        }

        return new(components[0], components[1], components[2], components[3]);
    }
}
#endif