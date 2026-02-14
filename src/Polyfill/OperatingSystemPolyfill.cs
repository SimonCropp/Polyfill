#if !NET8_0_OR_GREATER && FeatureRuntimeInformation

namespace Polyfills;

using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

static partial class Polyfill
{
    static bool IsOsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0) =>
        Environment.OSVersion.Version >= new Version(major, minor, build, revision);

    extension(OperatingSystem)
    {
#if !NET8_0_OR_GREATER

        /// <summary>
        /// Indicates whether the current application is running as WASI.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswasi?view=net-11.0
        public static bool IsWasi() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Create("WASI"));

#endif

#if !NET6_0_OR_GREATER

        /// <summary>
        /// Indicates whether the current application is running on Mac Catalyst.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismaccatalyst?view=net-11.0
        public static bool IsMacCatalyst() =>
            OperatingSystem.IsMacOS() ||
            OperatingSystem.IsIOS();

        /// <summary>
        /// Check for the Mac Catalyst version (iOS version as presented in Apple documentation) with a ≤ version comparison. Used to guard APIs that were added in the given Mac Catalyst release.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismaccatalystversionatleast?view=net-11.0
        public static bool IsMacCatalystVersionAtLeast(int major, int minor = 0, int build = 0) =>
            IsMacCatalyst() &&
            IsOsVersionAtLeast(major, minor, build);

#endif

#if !NET

        /// <summary>
        /// Checks if the macOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified macOS version.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismacosversionatleast?view=net-11.0
        public static bool IsMacOSVersionAtLeast(int major, int minor = 0, int build = 0) =>
            OperatingSystemCache.IsMacOSVersionAtLeast(major, minor, build);

        /// <summary>
        /// Checks if the FreeBSD version (returned by the Linux command uname) is greater than or equal to the specified version.
        /// This method can be used to guard APIs that were added in the specified version.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isfreebsdversionatleast?view=net-11.0
        public static bool IsFreeBSDVersionAtLeast(int major, int minor, int build = 0, int revision = 0) =>
            OperatingSystemCache.IsFreeBSDVersionAtLeast(major, minor, build, revision);

        /// <summary>
        /// Indicates whether the current application is running on Android.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isandroid?view=net-11.0
        public static bool IsAndroid() =>
            OperatingSystemCache.IsAndroid();

        /// <summary>
        /// Checks if the Android version (returned by the Linux command uname) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified version.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isandroidversionatleast?view=net-11.0
        public static bool IsAndroidVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0) =>
            OperatingSystemCache.IsAndroidVersionAtLeast(major, minor, build, revision);

        /// <summary>
        /// Checks if the Windows version (returned by RtlGetVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified Windows version.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswindowsversionatleast?view=net-11.0
        public static bool IsWindowsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0) =>
            OperatingSystemCache.IsWindowsVersionAtLeast(major, minor, build, revision);

       /// <summary>
        /// Indicates whether the current application is running on the specified platform.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isosplatform?view=net-11.0
        public static bool IsOSPlatform(string platform) =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Create(platform));

        /// <summary>
        /// Checks if the operating system version is greater than or equal to the specified platform version. This method can be used to guard APIs that were added in the specified OS version.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isosplatformversionatleast?view=net-11.0
        public static bool IsOSPlatformVersionAtLeast(string platform, int major, int minor = 0, int build = 0, int revision = 0) =>
            IsOSPlatform(platform) &&
            IsOsVersionAtLeast(major, minor, build, revision);

        /// <summary>
        /// Indicates whether the current application is running on Windows.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswindows?view=net-11.0
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <summary>
        /// Indicates whether the current application is running on macOS.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismacos?view=net-11.0
        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        /// <summary>
        /// Indicates whether the current application is running on iOS or MacCatalyst.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isios?view=net-11.0
        public static bool IsIOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Create("IOS"));

        /// <summary>
        /// Indicates whether the current application is running on Linux.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.islinux?view=net-11.0
        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        /// <summary>
        /// Indicates whether the current application is running on FreeBSD.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isfreebsd?view=net-11.0
        public static bool IsFreeBSD() =>
            RuntimeInformation.OSDescription.ToLower().Contains("freebsd");

        /// <summary>
        /// Checks if the iOS/MacCatalyst version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified iOS version.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isiosversionatleast?view=net-11.0
        public static bool IsIOSVersionAtLeast(int major, int minor = 0, int build = 0) =>
            IsIOS() &&
            IsOsVersionAtLeast(major, minor, build);

        /// <summary>
        /// Checks if the tvOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified tvOS version.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.istvosversionatleast?view=net-11.0
        public static bool IsTvOSVersionAtLeast(int major, int minor = 0, int build = 0) =>
            IsTvOS() &&
            IsOsVersionAtLeast(major, minor, build);

        /// <summary>
        /// Indicates whether the current application is running on tvOS.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.istvos?view=net-11.0
        public static bool IsTvOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Create("TVOS"));

        /// <summary>
        /// Indicates whether the current application is running on watchOS.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswatchos?view=net-11.0
        public static bool IsWatchOS() =>
            IsIOS() ||
            RuntimeInformation.OSDescription.ToLower().Contains("watchos");

        /// <summary>
        /// Checks if the watchOS version (returned by libobjc.get_operatingSystemVersion) is greater than or equal to the specified version. This method can be used to guard APIs that were added in the specified watchOS version.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswatchosversionatleast?view=net-11.0
        public static bool IsWatchOSVersionAtLeast(int major, int minor = 0, int build = 0) =>
            IsWatchOS() &&
            IsOsVersionAtLeast(major, minor, build);

        /// <summary>
        /// Indicates whether the current application is running as WASM in a browser.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isbrowser?view=net-11.0
        public static bool IsBrowser() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER"));

#endif
    }
}
#endif
