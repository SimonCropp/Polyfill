#pragma warning disable

#if !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;

static partial class Polyfill
{
    extension(File)
    {
        /// <summary>
        /// Creates a hard link located in <paramref name="path"/> that refers to the same file content as <paramref name="pathToTarget"/>.
        /// </summary>
        /// <param name="path">The path where the hard link should be created.</param>
        /// <param name="pathToTarget">The path of the hard link target.</param>
        /// <returns>A <see cref="FileInfo"/> instance that wraps the newly created file.</returns>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.file.createhardlink
        public static FileSystemInfo CreateHardLink(string path, string pathToTarget)
        {
            var fullPath = Path.GetFullPath(path);
            HardLinkHelper.ValidatePath(pathToTarget, nameof(pathToTarget));
            HardLinkHelper.CreateHardLink(fullPath, Path.GetFullPath(pathToTarget));
            return new FileInfo(path);
        }
    }

    extension(FileInfo target)
    {
        /// <summary>
        /// Creates a hard link located in <see cref="FileSystemInfo.Name"/> that refers to the same file content as <paramref name="pathToTarget"/>.
        /// </summary>
        /// <param name="pathToTarget">The path of the hard link target.</param>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo.createashardlink
        public void CreateAsHardLink(string pathToTarget)
        {
            HardLinkHelper.ValidatePath(pathToTarget, nameof(pathToTarget));
            HardLinkHelper.CreateHardLink(target.FullName, Path.GetFullPath(pathToTarget));
        }
    }
}

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
static class HardLinkHelper
{
    internal static void ValidatePath(string path, string paramName)
    {
        if (path is null)
        {
            throw new ArgumentNullException(paramName);
        }

        if (path.Length == 0)
        {
            throw new ArgumentException("Empty file name is not legal.", paramName);
        }
    }

    internal static void CreateHardLink(string path, string pathToTarget)
    {
#if FeatureRuntimeInformation
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
#else
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
#endif
        {
            if (!CreateHardLinkW(path, pathToTarget, IntPtr.Zero))
            {
                throw new IOException(
                    $"Unable to create hard link '{path}' to '{pathToTarget}'.",
                    Marshal.GetHRForLastWin32Error());
            }
        }
        else
        {
            if (link(pathToTarget, path) != 0)
            {
                throw new IOException(
                    $"Unable to create hard link '{path}' to '{pathToTarget}'.",
                    Marshal.GetHRForLastWin32Error());
            }
        }
    }

    [DllImport("kernel32.dll", EntryPoint = "CreateHardLinkW", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern bool CreateHardLinkW(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

    [DllImport("libc", SetLastError = true)]
    static extern int link(string oldpath, string newpath);
}

#endif
