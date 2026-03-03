#if !NET9_0_OR_GREATER

namespace Polyfills;

using System;
using System.IO;

static partial class Polyfill
{
    extension(Path)
    {
#if FeatureMemory
#if !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_1_OR_GREATER
        /// <summary>
        /// Returns the directory information for the specified path represented by a character span.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getdirectoryname?view=net-11.0#system-io-path-getdirectoryname(system-readonlyspan((system-char)))
        public static ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char> path) =>
            Path.GetDirectoryName(path.ToString()).AsSpan();

        /// <summary>
        /// Returns the file name and extension of a file path that is represented by a read-only character span.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilename?view=net-11.0#system-io-path-getfilename(system-readonlyspan((system-char)))
        public static ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path) =>
            Path.GetFileName(path.ToString()).AsSpan();

        /// <summary>
        /// Returns the file name without the extension of a file path that is represented by a read-only character span.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension?view=net-11.0#system-io-path-getfilenamewithoutextension(system-readonlyspan((system-char)))
        public static ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char> path) =>
            Path.GetFileNameWithoutExtension(path.ToString()).AsSpan();

        /// <summary>
        /// Determines whether the path represented by the specified character span includes a file name extension.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension?view=net-11.0#system-io-path-getfilenamewithoutextension(system-readonlyspan((system-char)))
        public static bool HasExtension(ReadOnlySpan<char> path) =>
            Path.HasExtension(path.ToString());

        /// <summary>
        /// Returns the extension of the given path.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getextension?view=net-11.0#system-io-path-getextension(system-readonlyspan((system-char)))
        public static ReadOnlySpan<char> GetExtension(ReadOnlySpan<char> path) =>
            Path.GetExtension(path.ToString()).AsSpan();
#endif

#if !NET9_0_OR_GREATER
        /// <summary>
        /// Combines a span of strings into a path.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.combine?view=net-11.0#system-io-path-combine(system-readonlyspan((system-string)))
        public static string Combine(scoped ReadOnlySpan<string> paths) =>
            Path.Combine(paths.ToArray());
#endif

#if !NETCOREAPP3_0_OR_GREATER
        /// <summary>
        /// Returns a value that indicates whether the path, specified as a read-only span, ends in a directory separator.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.endsindirectoryseparator?view=net-11.0#system-io-path-endsindirectoryseparator(system-readonlyspan((system-char)))
        public static bool EndsInDirectorySeparator (ReadOnlySpan<char> path) =>
            EndsInDirectorySeparator(path.ToString());

        /// <summary>
        /// Trims one trailing directory separator beyond the root of the specified path.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.trimendingdirectoryseparator?view=net-11.0#system-io-path-trimendingdirectoryseparator(system-readonlyspan((system-char)))
        public static ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char> path) =>
            TrimEndingDirectorySeparator(path.ToString()).AsSpan();
#endif

#endif

#if !NETCOREAPP3_0_OR_GREATER
        /// <summary>
        /// Returns a value that indicates whether the specified path ends in a directory separator.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.endsindirectoryseparator?view=net-11.0#system-io-path-endsindirectoryseparator(system-string)
        public static bool EndsInDirectorySeparator(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            return IsDirectorySeparator(path[path.Length - 1]);
        }

        /// <summary>
        /// Trims one trailing directory separator beyond the root of the specified path.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.trimendingdirectoryseparator?view=net-11.0#system-io-path-trimendingdirectoryseparator(system-string)
        public static string TrimEndingDirectorySeparator(string path)
        {
            if (EndsInDirectorySeparator(path) &&
                !IsRoot(path))
            {
                return path!.Substring(0, path.Length - 1);
            }

            return path;
        }
#endif
        static bool IsRoot(string path) =>
            Path.IsPathRooted(path) && Path.GetDirectoryName(path) == null;

        static bool IsDirectorySeparator(char c) =>
            c == Path.DirectorySeparatorChar ||
            c == Path.AltDirectorySeparatorChar;

#if !NET7_0_OR_GREATER
        /// <summary>
        /// Determines whether the specified file or directory exists.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.exists?view=net-11.0
        public static bool Exists(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            string? fullPath;
            try
            {
                fullPath = Path.GetFullPath(path);
            }
            catch (Exception ex)
                when (ex is ArgumentException or
                          IOException or
                          UnauthorizedAccessException)
            {
                return false;
            }

            return File.Exists(fullPath) || Directory.Exists(fullPath);
        }
#endif
    }
}

#endif
