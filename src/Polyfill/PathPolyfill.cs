#if !NET9_0_OR_GREATER

namespace Polyfills;

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

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

        /// <summary>
        /// Determines whether a path includes a file name extension.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.ispathrooted?view=net-11.0#system-io-path-ispathrooted(system-readonlyspan((system-char)))
        public static bool IsPathRooted(ReadOnlySpan<char> path) =>
            Path.IsPathRooted(path.ToString());
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
                // ReSharper disable once RedundantSuppressNullableWarningExpression
                return path!.Substring(0, path.Length - 1);
            }

            return path;
        }

        /// <summary>
        /// Concatenates a span of paths into a single path.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.join?view=net-11.0#system-io-path-join(system-string())
        public static string Join(params string?[] paths)
        {
            if (paths.Length == 0)
            {
                return string.Empty;
            }

            var builder = new System.Text.StringBuilder();

            foreach (var path in paths)
            {
                if (string.IsNullOrEmpty(path))
                {
                    continue;
                }

                if (builder.Length > 0 &&
                    !IsDirectorySeparator(builder[builder.Length - 1]) &&
                    !IsDirectorySeparator(path![0]))
                {
                    builder.Append(Path.DirectorySeparatorChar);
                }

                builder.Append(path);
            }

            return builder.ToString();
        }
#endif
        static bool IsRoot(string path) =>
            Path.IsPathRooted(path) && Path.GetDirectoryName(path) == null;

        static bool IsDirectorySeparator(char c) =>
            c == Path.DirectorySeparatorChar ||
            c == Path.AltDirectorySeparatorChar;

#if !NETCOREAPP2_0_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
        /// <summary>
        /// Returns a relative path from one path to another.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getrelativepath?view=net-11.0
        public static string GetRelativePath(string relativeTo, string path)
        {
            if (relativeTo is null)
            {
                throw new ArgumentNullException(nameof(relativeTo));
            }

            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (relativeTo.Length == 0)
            {
                throw new ArgumentException("The path is empty.", nameof(relativeTo));
            }

            if (path.Length == 0)
            {
                throw new ArgumentException("The path is empty.", nameof(path));
            }

            relativeTo = Path.GetFullPath(relativeTo);
            path = Path.GetFullPath(path);

            var ignoreCase = !IsPathCaseSensitive();
            var comparison = StringComparison.Ordinal;
            if (ignoreCase)
            {
                comparison = StringComparison.OrdinalIgnoreCase;
            }

            // Paths with different roots have nothing to be relative to, so the "to" path is returned as is
            if (!string.Equals(Path.GetPathRoot(relativeTo), Path.GetPathRoot(path), comparison))
            {
                return path;
            }

            var commonLength = GetCommonPathLength(relativeTo, path, ignoreCase);
            if (commonLength == 0)
            {
                return path;
            }

            // Trailing separators aren't significant for comparison
            var relativeToLength = relativeTo.Length;
            if (IsDirectorySeparator(relativeTo[relativeToLength - 1]))
            {
                relativeToLength--;
            }

            var pathEndsInSeparator = IsDirectorySeparator(path[path.Length - 1]);
            var pathLength = path.Length;
            if (pathEndsInSeparator)
            {
                pathLength--;
            }

            // Effectively the same path
            if (relativeToLength == pathLength &&
                commonLength >= relativeToLength)
            {
                return ".";
            }

            // The paths share a root, so the result is one ".." for each segment of the "from" path past
            // the common part, followed by what is left of the "to" path:
            //
            //  C:\Foo      C:\Bar      common 3 -> ..\Bar
            //  C:\Foo      C:\Foo\Bar  common 6 -> Bar
            //  C:\Foo\Bar  C:\Bar\Bar  common 3 -> ..\..\Bar\Bar
            //  C:\Foo\Foo  C:\Foo\Bar  common 7 -> ..\Bar
            StringBuilder? builder = null;
            if (commonLength < relativeToLength)
            {
                builder = new(Math.Max(relativeTo.Length, path.Length));
                builder.Append("..");

                for (var index = commonLength + 1; index < relativeToLength; index++)
                {
                    if (IsDirectorySeparator(relativeTo[index]))
                    {
                        builder.Append(Path.DirectorySeparatorChar);
                        builder.Append("..");
                    }
                }
            }
            else if (IsDirectorySeparator(path[commonLength]))
            {
                // No parent segments, so skip the separator that follows the common part
                commonLength++;
            }

            // The rest of the "to" path, with its trailing separator added back
            var differenceLength = pathLength - commonLength;
            if (pathEndsInSeparator)
            {
                differenceLength++;
            }

            if (builder == null)
            {
                if (differenceLength <= 0)
                {
                    return string.Empty;
                }

                return path.Substring(commonLength, differenceLength);
            }

            if (differenceLength > 0)
            {
                builder.Append(Path.DirectorySeparatorChar);
                builder.Append(path, commonLength, differenceLength);
            }

            return builder.ToString();
        }

        // Windows and macOS file systems are case insensitive by default
        static bool IsPathCaseSensitive()
        {
            if (Path.DirectorySeparatorChar == '\\')
            {
                return false;
            }

#if FeatureRuntimeInformation
            return !RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
#else
            return Environment.OSVersion.Platform != PlatformID.MacOSX;
#endif
        }

        // The length of the leading part the two paths share, ending on a segment boundary
        static int GetCommonPathLength(string first, string second, bool ignoreCase)
        {
            var commonChars = 0;
            var shortest = Math.Min(first.Length, second.Length);
            while (commonChars < shortest)
            {
                var left = first[commonChars];
                var right = second[commonChars];
                if (left != right &&
                    !(ignoreCase && char.ToUpperInvariant(left) == char.ToUpperInvariant(right)))
                {
                    break;
                }

                commonChars++;
            }

            if (commonChars == 0)
            {
                return 0;
            }

            // One path is a prefix of the other and stops at the end of a segment
            if (commonChars == first.Length &&
                (commonChars == second.Length || IsDirectorySeparator(second[commonChars])))
            {
                return commonChars;
            }

            if (commonChars == second.Length &&
                IsDirectorySeparator(first[commonChars]))
            {
                return commonChars;
            }

            // The match may have stopped in the middle of a segment, for example C:\Foodie and C:\Foobar
            while (commonChars > 0 &&
                   !IsDirectorySeparator(first[commonChars - 1]))
            {
                commonChars--;
            }

            return commonChars;
        }
#endif

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
