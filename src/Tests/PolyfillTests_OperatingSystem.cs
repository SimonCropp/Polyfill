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
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("Android"));

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsOperatingSystemTvOS()
    {
        bool actual = OperatingSystem.IsTvOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("tvOS"));

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsOperatingSystemWatchOS()
    {
        bool actual = OperatingSystem.IsWatchOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("watchOS"));

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsOperatingSystemBrowser()
    {
        bool actual = OperatingSystem.IsBrowser();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("Browser"));

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task IsOperatingSystemWasi()
    {
        bool actual = OperatingSystem.IsWasi();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("wasi"));

        await Assert.That(actual).IsEqualTo(expected);
    }
#endif
}