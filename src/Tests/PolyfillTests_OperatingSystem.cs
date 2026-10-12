// ReSharper disable RedundantUsingDirective
// ReSharper disable UnnecessaryWhitespace
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_SimpleTypes

using System.Runtime.InteropServices;
using OSPlatform = System.Runtime.InteropServices.OSPlatform;
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeObjectCreationWhenTypeEvident

[SuppressMessage("Style", "IDE0007:Use implicit type")]
partial class PolyfillTests
{
#if FeatureRuntimeInformation
    [Test]
    public async Task IsOperatingSystemWindows()
    {
        bool actual = OperatingSystem.IsWindows();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsOperatingSystemLinux()
    {
        bool actual = OperatingSystem.IsLinux();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsOperatingSystemMacOS()
    {
        bool actual = OperatingSystem.IsMacOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsMacOSVersionAtLeast_CurrentPlatform()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            await Assert.That(OperatingSystem.IsMacOSVersionAtLeast(1)).IsFalse();
            return;
        }

        // every runtime these tests target needs a macOS newer than 10.12
        await Assert.That(OperatingSystem.IsMacOSVersionAtLeast(10)).IsTrue();
        await Assert.That(OperatingSystem.IsMacOSVersionAtLeast(10, 12)).IsTrue();
        await Assert.That(OperatingSystem.IsMacOSVersionAtLeast(10, 12, 0)).IsTrue();
        await Assert.That(OperatingSystem.IsMacOSVersionAtLeast(1000)).IsFalse();
    }

    [Test]
    public async Task IsAndroidVersionAtLeast_CurrentPlatform()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID")))
        {
            await Assert.That(OperatingSystem.IsAndroidVersionAtLeast(1)).IsFalse();
            return;
        }

        await Assert.That(OperatingSystem.IsAndroidVersionAtLeast(1)).IsTrue();
        await Assert.That(OperatingSystem.IsAndroidVersionAtLeast(1000)).IsFalse();
    }

#if !NET
    // OperatingSystemCache only exists where OperatingSystem.Is*VersionAtLeast is polyfilled
    [Test]
    // sw_vers -productVersion
    [Arguments("14.5\n", "14.5.0.0")]
    [Arguments("10.15.7\n", "10.15.7.0")]
    [Arguments("15\n", "15.0.0.0")]
    // getprop ro.build.version.release
    [Arguments("14\n", "14.0.0.0")]
    [Arguments("8.1.0\r\n", "8.1.0.0")]
    [Arguments("ProductVersion:\t14.5\n", "14.5.0.0")]
    public async Task OperatingSystemCache_ParseVersion_CommandOutput(string output, string expected)
    {
        var version = Polyfills.OperatingSystemCache.ParseVersion(output);

        await Assert.That(version).IsEqualTo(Version.Parse(expected));
        // a version is at least itself, with or without the trailing components
        await Assert.That(version >= new Version(version.Major, version.Minor, version.Build, version.Revision)).IsTrue();
        await Assert.That(version >= new Version(version.Major, 0, 0, 0)).IsTrue();
    }
#endif

#if NETSTANDARD2_1 || NETCOREAPP3_1_OR_GREATER
    [Test]
    public async Task IsOperatingSystemFreeBSD()
    {
        bool actual = OperatingSystem.IsFreeBSD();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD);

        await Assert.That(actual).IsEqualTo(expected);
    }
#endif


    [Test]
    public async Task IsOperatingSystemIOS()
    {
        bool actual = OperatingSystem.IsIOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("IOS"));

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsOperatingSystemAndroid()
    {
        bool actual = OperatingSystem.IsAndroid();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID"));

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsOperatingSystemTvOS()
    {
        bool actual = OperatingSystem.IsTvOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("TVOS"));

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsOperatingSystemWatchOS()
    {
        bool actual = OperatingSystem.IsWatchOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("WATCHOS"));

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsOperatingSystemBrowser()
    {
        bool actual = OperatingSystem.IsBrowser();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER"));

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsOperatingSystemWasi()
    {
        bool actual = OperatingSystem.IsWasi();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("WASI"));

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsWindowsVersionAtLeast_CurrentVersion()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            await Assert.That(OperatingSystem.IsWindowsVersionAtLeast(1)).IsFalse();
            return;
        }

        // e.g. "Microsoft Windows 10.0.19045"
        var match = Regex.Match(RuntimeInformation.OSDescription, @"(\d+)\.(\d+)\.(\d+)");
        await Assert.That(match.Success).IsTrue();
        int major = int.Parse(match.Groups[1].Value);
        int minor = int.Parse(match.Groups[2].Value);
        int build = int.Parse(match.Groups[3].Value);

        // the running version is at least itself, however many components are requested
        await Assert.That(OperatingSystem.IsWindowsVersionAtLeast(major)).IsTrue();
        await Assert.That(OperatingSystem.IsWindowsVersionAtLeast(major, minor)).IsTrue();
        await Assert.That(OperatingSystem.IsWindowsVersionAtLeast(major, minor, build)).IsTrue();
        await Assert.That(OperatingSystem.IsWindowsVersionAtLeast(major, minor, build - 1)).IsTrue();

        await Assert.That(OperatingSystem.IsWindowsVersionAtLeast(major, minor, build + 1)).IsFalse();
        await Assert.That(OperatingSystem.IsWindowsVersionAtLeast(major, minor + 1)).IsFalse();
        await Assert.That(OperatingSystem.IsWindowsVersionAtLeast(major + 1)).IsFalse();
    }

#if !NET
    // OperatingSystemCache only exists where OperatingSystem.Is*VersionAtLeast is polyfilled
    [Test]
    [Arguments("Microsoft Windows 10.0.19045 ", "10.0.19045.0")]
    [Arguments("Microsoft Windows 10.0.19045", "10.0.19045.0")]
    [Arguments("Microsoft Windows 6.1.7601 Service Pack 1", "6.1.7601.0")]
    [Arguments("Microsoft Windows 10.0.22631.4460", "10.0.22631.4460")]
    [Arguments("10.0", "10.0.0.0")]
    public async Task OperatingSystemCache_ParseVersion(string input, string expected)
    {
        var version = Polyfills.OperatingSystemCache.ParseVersion(input);

        await Assert.That(version).IsEqualTo(Version.Parse(expected));
    }
#endif
#endif
}