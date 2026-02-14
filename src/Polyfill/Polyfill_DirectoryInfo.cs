#if NETFRAMEWORK || (NETSTANDARD && !NETSTANDARD2_1_OR_GREATER) || NETCOREAPP2_0

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Returns an enumerable collection of file information that matches a specified search pattern and enumeration options.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.enumeratefiles?view=net-11.0#system-io-directoryinfo-enumeratefiles(system-string-system-io-enumerationoptions)
    public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo target, string searchPattern, EnumerationOptions enumerationOptions) =>
        ApplyEnumerationOptions(
            target.EnumerateFiles(searchPattern, ToSearchOption(enumerationOptions)),
            enumerationOptions);

    /// <summary>
    /// Returns an enumerable collection of directory information that matches a specified search pattern and enumeration options.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.enumeratedirectories?view=net-11.0#system-io-directoryinfo-enumeratedirectories(system-string-system-io-enumerationoptions)
    public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo target, string searchPattern, EnumerationOptions enumerationOptions) =>
        ApplyEnumerationOptions(
            target.EnumerateDirectories(searchPattern, ToSearchOption(enumerationOptions)),
            enumerationOptions);

    /// <summary>
    /// Returns an enumerable collection of file system information that matches a specified search pattern and enumeration options.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.enumeratefilesysteminfos?view=net-11.0#system-io-directoryinfo-enumeratefilesysteminfos(system-string-system-io-enumerationoptions)
    public static IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(this DirectoryInfo target, string searchPattern, EnumerationOptions enumerationOptions) =>
        ApplyEnumerationOptions(
            target.EnumerateFileSystemInfos(searchPattern, ToSearchOption(enumerationOptions)),
            enumerationOptions);

    /// <summary>
    /// Returns an array of files that match a search pattern and enumeration options.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.getfiles?view=net-11.0#system-io-directoryinfo-getfiles(system-string-system-io-enumerationoptions)
    public static FileInfo[] GetFiles(this DirectoryInfo target, string searchPattern, EnumerationOptions enumerationOptions) =>
        target.EnumerateFiles(searchPattern, enumerationOptions).ToArray();

    /// <summary>
    /// Returns an array of subdirectories that match a search pattern and enumeration options.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.getdirectories?view=net-11.0#system-io-directoryinfo-getdirectories(system-string-system-io-enumerationoptions)
    public static DirectoryInfo[] GetDirectories(this DirectoryInfo target, string searchPattern, EnumerationOptions enumerationOptions) =>
        target.EnumerateDirectories(searchPattern, enumerationOptions).ToArray();

    /// <summary>
    /// Returns an array of file system entries that match a search pattern and enumeration options.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo.getfilesysteminfos?view=net-11.0#system-io-directoryinfo-getfilesysteminfos(system-string-system-io-enumerationoptions)
    public static FileSystemInfo[] GetFileSystemInfos(this DirectoryInfo target, string searchPattern, EnumerationOptions enumerationOptions) =>
        target.EnumerateFileSystemInfos(searchPattern, enumerationOptions).ToArray();

    static IEnumerable<T> ApplyEnumerationOptions<T>(IEnumerable<T> source, EnumerationOptions options)
        where T : FileSystemInfo
    {
        foreach (var item in source)
        {
            FileAttributes attributes;
            try
            {
                attributes = item.Attributes;
            }
            catch (Exception) when (options.IgnoreInaccessible)
            {
                continue;
            }

            if ((attributes & options.AttributesToSkip) != 0)
            {
                continue;
            }

            yield return item;
        }
    }
}

#endif
