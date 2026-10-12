#if !NET7_0_OR_GREATER
namespace Polyfills;

using System;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable once RedundantUsingDirective
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

static partial class Polyfill
{
    extension(File)
    {
        /// <summary>
        /// Gets the UnixFileMode of the file on the path.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.file.getunixfilemode?view=net-11.0#system-io-file-getunixfilemode(system-string)
        [UnsupportedOSPlatform("windows")]
        public static UnixFileMode GetUnixFileMode(string path)
        {
#if FeatureRuntimeInformation
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException();
            }
#else
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                throw new PlatformNotSupportedException();
            }
#endif
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("path is a zero-length string, or contains one or more invalid characters. Query for invalid characters by using the GetInvalidPathChars() method.");
            }

            return UnixFileModeHelper.GetMode(Path.GetFullPath(path));
        }

        /// <summary>
        /// Sets the specified UnixFileMode of the file on the specified pat
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="mode">The Unix file mode.</param>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.file.setunixfilemode?view=net-11.0#system-io-file-setunixfilemode(system-string-system-io-unixfilemode)
        [UnsupportedOSPlatform("windows")]
        public static void SetUnixFileMode(string path, UnixFileMode mode)
        {
#if FeatureRuntimeInformation
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException();
            }
#else
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                throw new PlatformNotSupportedException();
            }
#endif
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("path is a zero-length string, or contains one or more invalid characters. Query for invalid characters by using the GetInvalidPathChars() method.");
            }

            var octal = new int[4]
            {
                0,
                0,
                0,
                0
            };

            var fileModeStrings = mode.ToString()
                .Replace("UnixFileMode.", "")
                .Replace(",", string.Empty)
                .Replace(Environment.NewLine, string.Empty)
                .Split(' ');

            foreach (var fileModeString in fileModeStrings)
            {
                switch (fileModeString.ToLower())
                {
                    //The digits are set to 0 by default, so we don't need to do anything.
                    case "none":
                        break;
                    case "userread":
                        octal[1] += 4;
                        break;
                    case "userwrite":
                        octal[1] += 2;
                        break;
                    case "userexecute":
                        octal[1] += 1;
                        break;
                    case "groupread":
                        octal[2] += 4;
                        break;
                    case "groupwrite":
                        octal[2] += 2;
                        break;
                    case "groupexecute":
                        octal[2] += 1;
                        break;
                    case "otherread":
                        octal[3] += 4;
                        break;
                    case "otherwrite":
                        octal[3] += 2;
                        break;
                    case "otherexecute":
                        octal[3] += 1;
                        break;
                    case "setuser":
                        octal[0] += 4;
                        break;
                    case "stickybit":
                        octal[0] += 1;
                        break;
                    case "setgroup":
                        octal[0] += 2;
                        break;
                    default:
                        throw new("Invalid notation detected");
                }
            }

            UnixFileModeHelper.SetMode(Path.GetFullPath(path), string.Concat(octal));
        }
    }

    [ExcludeFromCodeCoverage]
    [DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
    [global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
    internal static class UnixFileModeHelper
    {
        // GNU stat (Linux): the permissions in octal, for example 644 or 4755
        static readonly string[] gnuStat = ["-c", "%a"];

        // BSD stat (macOS, FreeBSD): the whole of st_mode in octal, for example 100644
        static readonly string[] bsdStat = ["-f", "%p"];

        internal static UnixFileMode GetMode(string fullPath)
        {
            var first = gnuStat;
            var second = bsdStat;
#if FeatureRuntimeInformation
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                first = bsdStat;
                second = gnuStat;
            }
#endif

            // each stat rejects the options of the other, so fall back when the platform was guessed wrongly
            if (Run("stat", first, fullPath, out var output, out var error) != 0 &&
                Run("stat", second, fullPath, out output, out _) != 0)
            {
                throw CreateException(fullPath, error);
            }

            return ParseMode(output);
        }

        internal static void SetMode(string fullPath, string octal)
        {
            if (Run("chmod", [octal], fullPath, out _, out var error) != 0)
            {
                throw CreateException(fullPath, error);
            }
        }

        /// <summary>
        /// Parses the octal mode written by stat. The low twelve bits are the permissions, and have the same values
        /// as <see cref="UnixFileMode"/>. Any higher bits are the file type, which BSD stat includes.
        /// </summary>
        internal static UnixFileMode ParseMode(string output)
        {
            var text = output.Trim();
            if (text.Length is 0 or > 10)
            {
                throw new FormatException($"Unexpected output from stat: '{text}'.");
            }

            var value = 0;
            foreach (var ch in text)
            {
                if (ch is < '0' or > '7')
                {
                    throw new FormatException($"Unexpected output from stat: '{text}'.");
                }

                value = value * 8 + (ch - '0');
            }

            return (UnixFileMode) (value & 0xFFF);
        }

        static Exception CreateException(string fullPath, string error)
        {
            if (!File.Exists(fullPath) &&
                !Directory.Exists(fullPath))
            {
                return new FileNotFoundException($"Could not find file '{fullPath}'.", fullPath);
            }

            var message = error.Trim();
            if (message.Length == 0)
            {
                message = $"Unable to access the mode of '{fullPath}'.";
            }

            return new IOException(message);
        }

        static int Run(string fileName, string[] options, string fullPath, out string output, out string error)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = false,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            foreach (var option in options)
            {
                startInfo.ArgumentList.Add(option);
            }

            // an argument of its own, so spaces and quotes in the path need no escaping here
            startInfo.ArgumentList.Add(fullPath);

            using var process = Process.Start(startInfo)!;

            // read both streams before waiting, so a full pipe cannot block the process
            var errorTask = process.StandardError.ReadToEndAsync();
            output = process.StandardOutput.ReadToEnd();
            error = errorTask.GetAwaiter().GetResult();
            process.WaitForExit();
            return process.ExitCode;
        }
    }
}
#endif
