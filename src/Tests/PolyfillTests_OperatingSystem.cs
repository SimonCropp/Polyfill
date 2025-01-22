// ReSharper disable RedundantUsingDirective
// ReSharper disable UnnecessaryWhitespace
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_SimpleTypes

using System.Runtime.InteropServices;

using OSPlatform = System.Runtime.InteropServices.OSPlatform;
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable PartialTypeWithSinglePart

namespace Tests;

[SuppressMessage("Style", "IDE0007:Use implicit type")]
partial class PolyfillTests
{
#if !NET8_0_OR_GREATER
     private OperatingSystem operatingSystem = new OperatingSystem(Environment.OSVersion.Platform, Environment.OSVersion.Version);

    [Test]
    public void IsOperatingSystemWindows()
    {
        bool actual = operatingSystem.IsWindows();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemLinux()
    {
        bool actual = operatingSystem.IsLinux();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemMacOS()
    {
        bool actual = operatingSystem.IsMacOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        Assert.AreEqual(expected, actual);
    }

#if NETSTANDARD2_1 || NETCOREAPP3_1_OR_GREATER
    [Test]
    public void IsOperatingSystemFreeBSD()
    {
        bool actual = operatingSystem.IsFreeBSD();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD);

        Assert.AreEqual(expected, actual);
    }
#endif


    [Test]
    public void IsOperatingSystemIOS()
    {
        bool actual = operatingSystem.IsIOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("IOS"));

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemAndroid()
    {
        bool actual = operatingSystem.IsAndroid();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("Android"));

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemTvOS()
    {
        bool actual = operatingSystem.IsTvOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("tvOS"));

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemWatchOS()
    {
        bool actual = operatingSystem.IsWatchOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("watchOS"));

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemBrowser()
    {
        bool actual = operatingSystem.IsBrowser();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("Browser"));

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemWasi()
    {
        bool actual = operatingSystem.IsWasi();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("wasi"));

        Assert.AreEqual(expected, actual);
    }
    #endif
}