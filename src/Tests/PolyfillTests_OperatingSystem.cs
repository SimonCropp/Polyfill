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

[SuppressMessage("Style", "IDE0007:Use implicit type")]
partial class PolyfillTests
{
#if FeatureRuntimeInformation
    [Test]
    public void IsOperatingSystemWindows()
    {
        bool actual = OperatingSystem.IsWindows();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemLinux()
    {
        bool actual = OperatingSystem.IsLinux();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemMacOS()
    {
        bool actual = OperatingSystem.IsMacOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        Assert.AreEqual(expected, actual);
    }

#if NETSTANDARD2_1 || NETCOREAPP3_1_OR_GREATER
    [Test]
    public void IsOperatingSystemFreeBSD()
    {
        bool actual = OperatingSystem.IsFreeBSD();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD);

        Assert.AreEqual(expected, actual);
    }
#endif


    [Test]
    public void IsOperatingSystemIOS()
    {
        bool actual = OperatingSystem.IsIOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("IOS"));

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemAndroid()
    {
        bool actual = OperatingSystemPolyfill.IsAndroid();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("Android"));

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemTvOS()
    {
        bool actual = OperatingSystem.IsTvOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("tvOS"));

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemWatchOS()
    {
        bool actual = OperatingSystem.IsWatchOS();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("watchOS"));

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemBrowser()
    {
        bool actual = OperatingSystem.IsBrowser();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("Browser"));

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsOperatingSystemWasi()
    {
        bool actual = OperatingSystem.IsWasi();
        bool expected = RuntimeInformation.IsOSPlatform(OSPlatform.Create("wasi"));

        Assert.AreEqual(expected, actual);
    }
#endif
}