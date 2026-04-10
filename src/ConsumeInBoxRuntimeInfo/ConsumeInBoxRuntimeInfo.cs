#pragma warning disable

using System;

// Validates OperatingSystem polyfills compile on frameworks where
// RuntimeInformation is in-box (net471+) without requiring the
// System.Runtime.InteropServices.RuntimeInformation NuGet package.
class ConsumeInBoxRuntimeInfo
{
    void OperatingSystem_Methods()
    {
        var isOSPlatform = OperatingSystem.IsOSPlatform("windows");
        var isOSPlatformWindows10 = OperatingSystem.IsOSPlatformVersionAtLeast("windows", 10, 0, 10240);

        var isWindows = OperatingSystem.IsWindows();
        var isWindows11 = OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22000);

        var isMacOS = OperatingSystem.IsMacOS();
        var isMacOsSonoma = OperatingSystem.IsMacOSVersionAtLeast(14);
        var isMacCatalyst = OperatingSystem.IsMacCatalyst();
        var isMacCatalyst17 = OperatingSystem.IsMacCatalystVersionAtLeast(17);

        var isLinux = OperatingSystem.IsLinux();

        var isFreeBSD = OperatingSystem.IsFreeBSD();
        var isFreeBSD14 = OperatingSystem.IsFreeBSDVersionAtLeast(14, 0);

        var isIOS = OperatingSystem.IsIOS();
        var isIOS18 = OperatingSystem.IsIOSVersionAtLeast(18);

        var isAndroid = OperatingSystem.IsAndroid();
        var isAndroid13 = OperatingSystem.IsAndroidVersionAtLeast(13);

        var isTvOS = OperatingSystem.IsTvOS();
        var isTvOS17 = OperatingSystem.IsTvOSVersionAtLeast(17);

        var isWatchOS = OperatingSystem.IsWatchOS();
        var isWatchOS11 = OperatingSystem.IsWatchOSVersionAtLeast(11);

        var isWasi = OperatingSystem.IsWasi();
        var isBrowser = OperatingSystem.IsBrowser();
    }
}
