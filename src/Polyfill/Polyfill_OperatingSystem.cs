// ReSharper disable RedundantUsingDirective
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
// ReSharper disable ArrangeMethodOrOperatorBody
// ReSharper disable InconsistentNaming
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable InvalidXmlDocComment

#pragma warning disable

namespace Polyfills;


static partial class Polyfill
{
    #if !NET8_0_OR_GREATER

    private static Version GetWindowsVersion()
    {
        return Version.Parse(RuntimeInformation.OSDescription
            .Replace("Microsoft Windows", string.Empty)
            .Replace(" ", string.Empty));
    }

    private static Version GetMacOSVersion()
    {
        string versionString = RunProcess(
            CreateProcess("/usr/bin/sw_vers", ""))
            .Replace("ProductVersion:", string.Empty)
            .Replace(" ", string.Empty);

        return Version.Parse(versionString.Split(Environment.NewLine.ToCharArray())[0]);
    }

    private static Version GetFreeBSDVersion()
    {
        string versionString = Environment.OSVersion.VersionString
            .Replace("Unix", string.Empty).Replace("FreeBSD", string.Empty)
            .Replace("-release", string.Empty).Replace(" ", string.Empty);

        return Version.Parse(versionString);
    }

    private static Version GetAndroidVersion()
    {
        string result = RunProcess(
                CreateProcess("getprop", "ro.build.version.release"))
            .Replace(" ", string.Empty);

        return Version.Parse(result);
    }

    private static Process CreateProcess(string targetFileName, string arguments)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = targetFileName,
            Arguments = arguments,
            RedirectStandardInput = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process output = new Process
        {
            StartInfo = processStartInfo
        };

        return output;
    }

    private static string RunProcess(Process process)
    {
        process.Start();

        process.WaitForExit();

        return process.StandardOutput.ReadToEnd();
    }

    /// <summary>
    /// Indicates whether the current application is running on the specified platform.
    /// </summary>
    /// <param name="operatingSystem"></param>
    /// <param name="platform">The case-insensitive platform name. Examples: Browser, Linux, FreeBSD, Android, iOS, macOS, tvOS, watchOS, Windows.</param>
    /// <returns>true if the current application is running on the specified platform; false otherwise.</returns>
    public static bool IsOSPlatform(this OperatingSystem operatingSystem, string platform)
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Create(platform));
    }

    /// <summary>
    /// Checks if the operating system version is greater than or equal to the specified platform version. This method can be used to guard APIs that were added in the specified OS version.
    /// </summary>
    /// <param name="platform">The case-insensitive platform name. Examples: Browser, Linux, FreeBSD, Android, iOS, macOS, tvOS, watchOS, Windows.</param>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number (optional).</param>
    /// <param name="build">The build release number (optional).</param>
    /// <param name="revision">The revision release number (optional).</param>
    /// <returns>true if the current application is running on the specified platform and is at least in the version specified in the parameters; false otherwise.</returns>
    public static bool IsOSPlatformVersionIsAtLeast(this OperatingSystem operatingSystem, string platform, int major, int minor = 0, int build = 0, int revision = 0)
    {
        return IsOSPlatform(operatingSystem, platform) && IsOsVersionAtLeast(major, minor, build, revision);
    }

    /// <summary>
    /// Indicates whether the current application is running on Windows.
    /// </summary>
    /// <param name="operatingSystem"></param>
    /// <returns>true if the current application is running on Windows; false otherwise.</returns>
    public static bool IsWindows(this OperatingSystem operatingSystem)
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    /// <summary>
    /// Checks if the Windows version (returned by RtlGetVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified Windows version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <param name="revision">The revision release number.</param>
    /// <returns>true if the current application is running on a Windows version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsWindowsVersionAtLeast(this OperatingSystem operatingSystem, int major, int minor = 0, int build = 0, int revision = 0)
    {
        return IsWindows(operatingSystem) &&
               GetWindowsVersion() >= new Version(major, minor, build, revision);
    }

    /// <summary>
    /// Indicates whether the current application is running on macOS.
    /// </summary>
    /// <returns>true if the current application is running on macOS; false otherwise.</returns>
    public static bool IsMacOS(this OperatingSystem operatingSystem)
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }

    /// <summary>
    /// Indicates whether the current application is running on Mac Catalyst.
    /// </summary>
    /// <returns>true if the current application is running on Mac Catalyst; false otherwise.</returns>
    public static bool IsMacCatalyst(this OperatingSystem operatingSystem)
    {
        return IsMacOS(operatingSystem) || IsIOS(operatingSystem);
    }

    /// <summary>
    /// Checks if the macOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified macOS version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <returns>true if the current application is running on an macOS version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsMacOSVersionAtLeast(this OperatingSystem operatingSystem, int major, int minor, int build = 0)
    {
        return IsMacOS(operatingSystem) && GetMacOSVersion() >= new Version(major, minor, build);
    }

    /// <summary>
    /// Check for the Mac Catalyst version (iOS version as presented in Apple documentation) with a ≤ version comparison. Used to guard APIs that were added in the given Mac Catalyst release.
    /// </summary>
    /// <param name="major">The version major number.</param>
    /// <param name="minor">The version minor number.</param>
    /// <param name="build">The version build number.</param>
    /// <returns>true if the Mac Catalyst version is greater or equal than the specified version comparison; false otherwise.</returns>
    public static bool IsMacCatalystVersionAtLeast(this OperatingSystem operatingSystem, int major, int minor, int build = 0)
    {
        return IsMacCatalyst(operatingSystem) && IsOsVersionAtLeast(major, minor, build);
    }

    /// <summary>
    /// Indicates whether the current application is running on Linux.
    /// </summary>
    /// <returns>true if the current application is running on Linux; false otherwise.</returns>
    public static bool IsLinux(this OperatingSystem operatingSystem)
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }

    /// <summary>
    /// Indicates whether the current application is running on FreeBSD.
    /// </summary>
    /// <returns>true if the current application is running on FreeBSD; false otherwise.</returns>
    public static bool IsFreeBSD(this OperatingSystem operatingSystem)
    {
        return RuntimeInformation.OSDescription.ToLower().Contains("freebsd");
    }

    /// <summary>
    ///Checks if the FreeBSD version (returned by the Linux command uname) is greater than or equal to the specified version.
    /// This method can be used to guard APIs that were added in the specified version.
    /// </summary>
    /// <param name="major">The version major number.</param>
    /// <param name="minor">The version minor number.</param>
    /// <param name="build">The version build number.</param>
    /// <param name="revision">The version revision number.</param>
    /// <returns>true if the current application is running on a FreeBSD version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsFreeBSDVersionAtLeast(this OperatingSystem operatingSystem, int major, int minor, int build = 0, int revision = 0)
    {
        return GetFreeBSDVersion() >= new Version(major, minor, build, revision);
    }

    /// <summary>
    /// Indicates whether the current application is running on iOS or MacCatalyst.
    /// </summary>
    /// <returns>true if the current application is running on iOS or MacCatalyst; false otherwise.</returns>
    public static bool IsIOS(this OperatingSystem operatingSystem)
    {
        string description = RuntimeInformation.OSDescription.ToLower();
        return description.Contains("ios") ||
               description.Contains("ipados") ||
               (description.Contains("iphone") &&
                description.Contains("os"));
    }

    /// <summary>
    /// Checks if the iOS/MacCatalyst version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified iOS version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <returns>true if the current application is running on an iOS/MacCatalyst version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsIOSVersionAtLeast(this OperatingSystem operatingSystem, int major, int minor, int build = 0)
    {
        return IsIOS(operatingSystem) && IsOsVersionAtLeast(major, minor, build);
    }

    /// <summary>
    /// Indicates whether the current application is running on tvOS.
    /// </summary>
    /// <returns>true if the current application is running on tvOS; false otherwise.</returns>
    public static bool IsTvOS(this OperatingSystem operatingSystem)
    {
        return RuntimeInformation.OSDescription.ToLower().Contains("tvos");
    }

    /// <summary>
    /// Checks if the tvOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified tvOS version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <returns>true if the current application is running on a tvOS version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsTvOSVersionAtLeast(this OperatingSystem operatingSystem, int major, int minor, int build = 0)
    {
        return IsTvOS(operatingSystem) && IsOsVersionAtLeast(major, minor, build);
    }

    /// <summary>
    /// Indicates whether the current application is running on Android.
    /// </summary>
    /// <returns>true if the current application is running on Android; false otherwise.</returns>
    public static bool IsAndroid(this OperatingSystem operatingSystem)
    {
        try
        {
            string result = RunProcess(CreateProcess("uname", "-o"))
                .Replace(" ", string.Empty);

            return result.ToLower().Equals("android");
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// Checks if the Android version (returned by the Linux command uname) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <param name="revision">The revision release number.</param>
    /// <returns>true if the current application is running on an Android version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsAndroidVersionAtLeast(this OperatingSystem operatingSystem, int major, int minor = 0, int build = 0, int revision = 0)
    {
        return IsAndroid(operatingSystem) &&  GetAndroidVersion() >= new Version(major, minor, build, revision);
    }

    /// <summary>
    /// Indicates whether the current application is running on watchOS.
    /// </summary>
    /// <returns>true if the current application is running on watchOS; false otherwise.</returns>
    public static bool IsWatchOS(this OperatingSystem operatingSystem)
    {
        string description = RuntimeInformation.OSDescription.ToLower();

        return IsIOS(operatingSystem) || description.Contains("watchos");
    }

    /// <summary>
    /// Checks if the watchOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified watchOS version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <returns>true if the current application is running on a watchOS version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsWatchOSVersionAtLeast(this OperatingSystem operatingSystem, int major, int minor, int build = 0)
    {
        return IsWatchOS(operatingSystem) && IsOsVersionAtLeast(major, minor, build);
    }

    /// <summary>
    /// Indicates whether the current application is running as WASI.
    /// </summary>
    /// <returns>true if running as WASI; false otherwise.</returns>
    public static bool IsWASI(this OperatingSystem operatingSystem)
    {
        return RuntimeInformation.FrameworkDescription.ToLower().Contains("wasi");
    }

    /// <summary>
    /// Indicates whether the current application is running as WASM in a browser.
    /// </summary>
    /// <returns>true if running as WASM; false otherwise.</returns>
    public static bool IsBrowser(this OperatingSystem operatingSystem)
    {
        return RuntimeInformation.FrameworkDescription.Contains(".NET WebAssembly");
    }

    private static bool IsOsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
    {
        return Environment.OSVersion.Version >= new Version(major, minor, build, revision);
    }

    #endif
}