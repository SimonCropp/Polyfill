using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


// ReSharper disable RedundantIfElseBlock

// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable SuggestVarOrType_BuiltInTypes

// ReSharper disable ArrangeConstructorOrDestructorBody
// ReSharper disable UnnecessaryWhitespace
// ReSharper disable RedundantNameQualifier
// ReSharper disable ArrangeMethodOrOperatorBody
// ReSharper disable InconsistentNaming
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

#if !NET8_0_OR_GREATER

namespace System;

/// <summary>
/// Represents information about an operating system, such as the version and platform identifier. This class cannot be inherited.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyPublic
public
#endif
sealed class OperatingSystem : ICloneable, ISerializable
{
    /// <summary>
    /// Gets a PlatformID enumeration value that identifies the operating system platform.
    /// </summary>
    public PlatformID Platform { get; private set;}

    /// <summary>
    /// Gets a Version object that identifies the operating system.
    /// </summary>
    public System.Version Version { get; private set; }

    /// <summary>
    /// Gets the concatenated string representation of the platform identifier, version, and service pack that are currently installed on the operating system.
    /// </summary>
    public string VersionString { get; private set; }

    /// <summary>
    /// Gets the concatenated string representation of the platform identifier, version, and service pack that are currently installed on the operating system.
    /// </summary>
    public string ServicePack { get; private set;}

    /// <summary>
    /// Represents information about an operating system, such as the version and platform identifier. This class cannot be inherited.
    /// </summary>
    public OperatingSystem()
    {
        if (IsWindows())
        {
            Version = GetWindowsVersion();
        }
        else if (IsMacOS())
        {
            Version = GetMacOSVersion();
        }
        else if (IsLinux())
        {
            Version = GetLinuxVersion();
        }
        else if (IsFreeBSD())
        {
            Version = GetFreeBSDVersion();
        }
        else if (IsMacCatalyst())
        {
            Version = IsMacOS() ? GetMacOSVersion() : GetFallbackOsVersion();
        }
        else if (IsAndroid())
        {
            Version = GetAndroidVersion();
        }
        else if (IsIOS() || IsWatchOS() || IsTvOS())
        {
            Version = GetFallbackOsVersion();
        }
        else
        {
           Version = GetFallbackOsVersion();

           ServicePack = Environment.OSVersion.ServicePack;
           VersionString = Environment.OSVersion.VersionString;
        }

        ServicePack = Environment.OSVersion.ServicePack;
        VersionString = Environment.OSVersion.VersionString;
    }

    /// <summary>
    /// Initializes a new instance of the OperatingSystem class, using the specified platform identifier value and version object.
    /// </summary>
    /// <param name="platform">One of the PlatformID values that indicates the operating system platform.</param>
    /// <param name="version">A Version object that indicates the version of the operating system.</param>
    ///
    /// <exception cref="ArgumentException">Platform is not a PlatformID enumeration value.</exception>
    /// <exception cref="NullReferenceException">Version is null.</exception>
    public OperatingSystem(PlatformID platform, Version version)
    {
        if (version == null)
        {
            throw new NullReferenceException();
        }

        if (platform != PlatformID.Win32NT
            && platform != PlatformID.Win32Windows
            && platform != PlatformID.Win32S
            && platform != PlatformID.WinCE
            && platform != PlatformID.Xbox
            && platform != PlatformID.MacOSX
            && platform != PlatformID.Unix)
        {
            throw new ArgumentException($"Platform {platform.ToString()} is not a PlatformID enumeration value.");
        }

        Version = version;
        Platform = platform;

       // ServicePack =version.
        VersionString = ToString();
    }

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

        return Version.Parse(versionString.Split(Environment.NewLine.ToCharArray()).First());
    }

    private static Version GetLinuxVersion()
    {
        string versionString = Environment.OSVersion.VersionString
            .Replace("Unix", string.Empty).Replace(" ", string.Empty);

        return Version.Parse(versionString);
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
    /// <param name="platform">The case-insensitive platform name. Examples: Browser, Linux, FreeBSD, Android, iOS, macOS, tvOS, watchOS, Windows.</param>
    /// <returns>true if the current application is running on the specified platform; false otherwise.</returns>
    public static bool IsOSPlatform(string platform)
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
    public static bool IsOSPlatformVersionIsAtLeast(string platform, int major, int minor = 0, int build = 0, int revision = 0)
    {
        return IsOSPlatform(platform) && IsOsVersionAtLeast(major, minor, build, revision);
    }

    /// <summary>
    /// Indicates whether the current application is running on Windows.
    /// </summary>
    /// <returns>true if the current application is running on Windows; false otherwise.</returns>
    public static bool IsWindows()
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
    public static bool IsWindowsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
    {
        return IsWindows() && GetWindowsVersion() >= new Version(major, minor, build, revision);
    }

    /// <summary>
    /// Indicates whether the current application is running on macOS.
    /// </summary>
    /// <returns>true if the current application is running on macOS; false otherwise.</returns>
    public static bool IsMacOS()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }

    /// <summary>
    /// Indicates whether the current application is running on Mac Catalyst.
    /// </summary>
    /// <returns>true if the current application is running on Mac Catalyst; false otherwise.</returns>
    public static bool IsMacCatalyst()
    {
        return IsMacOS() || IsIOS();
    }

    /// <summary>
    /// Checks if the macOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified macOS version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <returns>true if the current application is running on an macOS version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsMacOSVersionAtLeast(int major, int minor, int build = 0)
    {
        return IsMacOS() && GetMacOSVersion() >= new Version(major, minor, build);
    }

    /// <summary>
    /// Check for the Mac Catalyst version (iOS version as presented in Apple documentation) with a ≤ version comparison. Used to guard APIs that were added in the given Mac Catalyst release.
    /// </summary>
    /// <param name="major">The version major number.</param>
    /// <param name="minor">The version minor number.</param>
    /// <param name="build">The version build number.</param>
    /// <returns>true if the Mac Catalyst version is greater or equal than the specified version comparison; false otherwise.</returns>
    public static bool IsMacCatalystVersionAtLeast(int major, int minor, int build = 0)
    {
        return IsMacCatalyst() && IsOsVersionAtLeast(major, minor, build);
    }

    /// <summary>
    /// Indicates whether the current application is running on Linux.
    /// </summary>
    /// <returns>true if the current application is running on Linux; false otherwise.</returns>
    public static bool IsLinux()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }

    /// <summary>
    /// Indicates whether the current application is running on FreeBSD.
    /// </summary>
    /// <returns>true if the current application is running on FreeBSD; false otherwise.</returns>
    public static bool IsFreeBSD()
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
    public static bool IsFreeBSDVersionAtLeast(int major, int minor, int build = 0, int revision = 0)
    {
        return GetFreeBSDVersion() >= new Version(major, minor, build, revision);
    }

    /// <summary>
    /// Indicates whether the current application is running on iOS or MacCatalyst.
    /// </summary>
    /// <returns>true if the current application is running on iOS or MacCatalyst; false otherwise.</returns>
    public static bool IsIOS()
    {
        string description = RuntimeInformation.OSDescription.ToLower();
        return description.Contains("ios") ||
               description.Contains("ipados")||
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
    public static bool IsIOSVersionAtLeast(int major, int minor, int build = 0)
    {
        return IsIOS() && IsOsVersionAtLeast(major, minor, build);
    }

    /// <summary>
    /// Indicates whether the current application is running on tvOS.
    /// </summary>
    /// <returns>true if the current application is running on tvOS; false otherwise.</returns>
    public static bool IsTvOS()
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
    public static bool IsTvOSVersionAtLeast(int major, int minor, int build = 0)
    {
        return IsTvOS() && IsOsVersionAtLeast(major, minor, build);
    }

    /// <summary>
    /// Indicates whether the current application is running on Android.
    /// </summary>
    /// <returns>true if the current application is running on Android; false otherwise.</returns>
    public static bool IsAndroid()
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
    public static bool IsAndroidVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
    {
        return IsAndroid() &&  GetAndroidVersion() >= new Version(major, minor, build, revision);
    }

    /// <summary>
    /// Indicates whether the current application is running on watchOS.
    /// </summary>
    /// <returns>true if the current application is running on watchOS; false otherwise.</returns>
    public static bool IsWatchOS()
    {
        string description = RuntimeInformation.OSDescription.ToLower();

        return IsIOS() || description.Contains("watchos");
    }

    /// <summary>
    /// Checks if the watchOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified watchOS version.
    /// </summary>
    /// <param name="major">The major release number.</param>
    /// <param name="minor">The minor release number.</param>
    /// <param name="build">The build release number.</param>
    /// <returns>true if the current application is running on a watchOS version that is at least what was specified in the parameters; false otherwise.</returns>
    public static bool IsWatchOSVersionAtLeast(int major, int minor, int build = 0)
    {
        return IsWatchOS() && IsOsVersionAtLeast(major, minor, build);
    }

    /// <summary>
    /// Indicates whether the current application is running as WASI.
    /// </summary>
    /// <returns>true if running as WASI; false otherwise.</returns>
    public static bool IsWASI()
    {
        return RuntimeInformation.FrameworkDescription.ToLower().Contains("wasi");
    }

    /// <summary>
    /// Indicates whether the current application is running as WASM in a browser.
    /// </summary>
    /// <returns>true if running as WASM; false otherwise.</returns>
    public static bool IsBrowser()
    {
        return RuntimeInformation.FrameworkDescription.Contains(".NET WebAssembly");
    }

    /// <summary>
    /// Converts the value of this OperatingSystem object to its equivalent string representation.
    /// </summary>
    /// <returns>The string representation of the values returned by the Platform, Version, and ServicePack properties.</returns>
    /// <remarks>For a list of Windows operating system versions and their corresponding version numbers, see Operating System Version.</remarks>
    public override string ToString()
    {
        if (string.IsNullOrEmpty(ServicePack))
        {
            return $"{Platform}{Version}";
        }
        else
        {
            return $"{Platform} {Version} {ServicePack}";
        }
    }

    private static bool IsOsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
    {
        return Environment.OSVersion.Version >= new Version(major, minor, build, revision);
    }

    private static Version GetFallbackOsVersion()
    {
        return Environment.OSVersion.Version;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Creates an OperatingSystem object that is identical to this instance.
    /// </summary>
    /// <returns>An OperatingSystem object that is a copy of this instance.</returns>
    public object Clone()
    {
        return new OperatingSystem(Platform, Version);
    }
}

#endif