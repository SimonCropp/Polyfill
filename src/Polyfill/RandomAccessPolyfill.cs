#if NET6_0_OR_GREATER && !NET8_0_OR_GREATER

namespace Polyfills;

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

static partial class Polyfill
{
    extension(RandomAccess)
    {
#if !NET7_0_OR_GREATER

        /// <summary>
        /// Sets the length of the file to the given value.
        /// </summary>
        /// <param name="handle">The file handle of the file.</param>
        /// <param name="length">The new length of the file.</param>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.randomaccess.setlength?view=net-10.0
        public static void SetLength(SafeFileHandle handle, long length)
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            if (handle.IsInvalid)
            {
                throw new ArgumentException("Invalid handle.", nameof(handle));
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Non-negative number required.");
            }

            if (OperatingSystem.IsWindows())
            {
                SetLengthWindows(handle, length);
            }
            else
            {
                SetLengthUnix(handle, length);
            }
        }

        static void SetLengthWindows(SafeFileHandle handle, long length)
        {
            if (!SetFilePointerEx(handle, length, out _, 0 /* FILE_BEGIN */))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (!SetEndOfFile(handle))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        static void SetLengthUnix(SafeFileHandle handle, long length)
        {
            var result = ftruncate(handle.DangerousGetHandle().ToInt32(), length);
            if (result != 0)
            {
                throw new IOException($"ftruncate failed with error code {Marshal.GetLastWin32Error()}");
            }
        }

#endif

        /// <summary>
        /// Flushes the operating system buffers for the given file to disk.
        /// </summary>
        /// <param name="handle">The file handle.</param>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.randomaccess.flushtodisk?view=net-10.0
        public static void FlushToDisk(SafeFileHandle handle)
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            if (handle.IsInvalid)
            {
                throw new ArgumentException("Invalid handle.", nameof(handle));
            }

            if (OperatingSystem.IsWindows())
            {
                if (!FlushFileBuffers(handle))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else
            {
                var result = fsync(handle.DangerousGetHandle().ToInt32());
                if (result != 0)
                {
                    throw new IOException($"fsync failed with error code {Marshal.GetLastWin32Error()}");
                }
            }
        }

        // Windows P/Invoke declarations
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetFilePointerEx(SafeFileHandle hFile, long liDistanceToMove, out long lpNewFilePointer, uint dwMoveMethod);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetEndOfFile(SafeFileHandle hFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FlushFileBuffers(SafeFileHandle hHandle);

        // Unix P/Invoke declarations
        [DllImport("libc", SetLastError = true)]
        static extern int ftruncate(int fd, long length);

        [DllImport("libc", SetLastError = true)]
        static extern int fsync(int fd);
    }
}

#endif
