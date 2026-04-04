#if NETFRAMEWORK || NETSTANDARD && !NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

static partial class Polyfill
{
    /// <summary>
    /// Returns an enumerable collection of file names that match a search pattern and enumeration options in a specified path.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratefiles?view=net-11.0#system-io-directory-enumeratefiles(system-string-system-string-system-io-enumerationoptions)
    public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, EnumerationOptions enumerationOptions) =>
        ApplyEnumerationOptions(
            Directory.EnumerateFiles(path, searchPattern, ToSearchOption(enumerationOptions)),
            enumerationOptions);

    /// <summary>
    /// Returns an enumerable collection of directory names that match a search pattern and enumeration options in a specified path.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratedirectories?view=net-11.0#system-io-directory-enumeratedirectories(system-string-system-string-system-io-enumerationoptions)
    public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions) =>
        ApplyEnumerationOptions(
            Directory.EnumerateDirectories(path, searchPattern, ToSearchOption(enumerationOptions)),
            enumerationOptions);

    /// <summary>
    /// Returns an enumerable collection of file names and directory names that match a search pattern and enumeration options in a specified path.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratefilesystementries?view=net-11.0#system-io-directory-enumeratefilesystementries(system-string-system-string-system-io-enumerationoptions)
    public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions) =>
        ApplyEnumerationOptions(
            Directory.EnumerateFileSystemEntries(path, searchPattern, ToSearchOption(enumerationOptions)),
            enumerationOptions);

    /// <summary>
    /// Returns the names of files that match a search pattern and enumeration options in a specified path.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles?view=net-11.0#system-io-directory-getfiles(system-string-system-string-system-io-enumerationoptions)
    public static string[] GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions) =>
        EnumerateFiles(path, searchPattern, enumerationOptions).ToArray();

    /// <summary>
    /// Returns the names of subdirectories that match a search pattern and enumeration options in a specified path.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.getdirectories?view=net-11.0#system-io-directory-getdirectories(system-string-system-string-system-io-enumerationoptions)
    public static string[] GetDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions) =>
        EnumerateDirectories(path, searchPattern, enumerationOptions).ToArray();

    /// <summary>
    /// Returns the names of file system entries that match a search pattern and enumeration options in a specified path.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.getfilesystementries?view=net-11.0#system-io-directory-getfilesystementries(system-string-system-string-system-io-enumerationoptions)
    public static string[] GetFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions) =>
        EnumerateFileSystemEntries(path, searchPattern, enumerationOptions).ToArray();

    static SearchOption ToSearchOption(EnumerationOptions options) =>
        options.RecurseSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

    static IEnumerable<string> ApplyEnumerationOptions(IEnumerable<string> source, EnumerationOptions options)
    {
        foreach (var item in source)
        {
            FileAttributes attributes;
            try
            {
                attributes = File.GetAttributes(item);
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
