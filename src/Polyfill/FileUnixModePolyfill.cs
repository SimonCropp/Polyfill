#if !NET7_0_OR_GREATER
namespace Polyfills;

using System;
using System.IO;
using System.Diagnostics;
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

            var startInfo = new ProcessStartInfo
            {
                FileName = "stat",
                Arguments = $"-c %a {Path.GetFullPath(path)}",
                RedirectStandardOutput = true,
                RedirectStandardInput = false,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var process = Process.Start(startInfo)!;

            process.WaitForExit();

            var output = process.StandardOutput.ReadToEnd();

            output = output.Length == 4 ? output : output.Insert(0, "0");

            // Digit 1 (out of 4) is for Special permissions
            var mode = output[0] switch
            {
                '1' => UnixFileMode.StickyBit,
                '2' => UnixFileMode.SetUser,
                '4' => UnixFileMode.SetGroup,
                _ => UnixFileMode.None
            };

            // Digit 2 (out of 4) for Users
            mode = output[1] switch
            {
                '0' => mode | UnixFileMode.None,
                '1' => mode | UnixFileMode.UserExecute,
                '2' => mode | UnixFileMode.UserWrite,
                '3' => mode | UnixFileMode.UserExecute | UnixFileMode.UserWrite,
                '4' => mode | UnixFileMode.UserRead,
                '5' => mode | UnixFileMode.UserRead | UnixFileMode.UserExecute,
                '6' => mode | UnixFileMode.UserRead | UnixFileMode.UserWrite,
                '7' => mode | UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute,
                _ => throw new("Invalid octal notation detected")
            };

            // Digit 3 (out of 4) for Groups
            mode = output[2] switch
            {
                '0' => mode | UnixFileMode.None,
                '1' => mode | UnixFileMode.GroupExecute,
                '2' => mode | UnixFileMode.GroupWrite,
                '3' => mode | UnixFileMode.GroupExecute | UnixFileMode.GroupWrite,
                '4' => mode | UnixFileMode.GroupRead,
                '5' => mode | UnixFileMode.GroupRead | UnixFileMode.GroupExecute,
                '6' => mode | UnixFileMode.GroupRead | UnixFileMode.GroupWrite,
                '7' => mode | UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute,
                _ => throw new("Invalid octal notation detected")
            };

            // Digit 4 (out of 4) for Others
            mode = output[3] switch
            {
                '0' => mode | UnixFileMode.None,
                '1' => mode | UnixFileMode.OtherExecute,
                '2' => mode | UnixFileMode.OtherWrite,
                '3' => mode | UnixFileMode.OtherExecute | UnixFileMode.OtherWrite,
                '4' => mode | UnixFileMode.OtherRead,
                '5' => mode | UnixFileMode.OtherRead | UnixFileMode.OtherExecute,
                '6' => mode | UnixFileMode.OtherRead | UnixFileMode.OtherWrite,
                '7' => mode | UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute,
                _ => throw new("Invalid octal notation detected")
            };

            return mode;
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
                        octal[0] += 2;
                        break;
                    case "stickybit":
                        octal[0] += 1;
                        break;
                    case "setgroup":
                        octal[0] += 4;
                        break;
                    default:
                        throw new("Invalid notation detected");
                }
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = "chmod",
                Arguments = $"{string.Concat(octal)} {Path.GetFullPath(path)}",
                RedirectStandardOutput = true,
                RedirectStandardInput = false,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var process = Process.Start(startInfo)!;
            process.WaitForExit();
        }
    }
}
#endif
