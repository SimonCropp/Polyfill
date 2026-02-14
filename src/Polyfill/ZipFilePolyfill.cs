#if !NET10_0_OR_GREATER && FeatureCompression

namespace Polyfills;

using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    extension(ZipFile)
    {
        /// <summary>
        /// Asynchronously extracts all the files in the specified archive to a directory on the file system.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0
        public static Task ExtractToDirectoryAsync(
            string sourceArchiveFileName,
            string destinationDirectoryName,
            CancellationToken cancellationToken = default) =>
            ExtractToDirectoryAsync(sourceArchiveFileName, destinationDirectoryName, false, cancellationToken);

        /// <summary>
        /// Asynchronously extracts all the files in the specified archive to a directory on the file system.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0
        public static Task ExtractToDirectoryAsync(
            string sourceArchiveFileName,
            string destinationDirectoryName,
            bool overwriteFiles,
            CancellationToken cancellationToken = default) =>
            Task.Run(() => ExtractToDirectoryPolyfill(sourceArchiveFileName, destinationDirectoryName, overwriteFiles), cancellationToken);

        /// <summary>
        /// Asynchronously extracts all the files in the specified archive to a directory on the file system and uses the specified character encoding for entry names.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0
        public static Task ExtractToDirectoryAsync(
            string sourceArchiveFileName,
            string destinationDirectoryName,
            Encoding entryNameEncoding,
            CancellationToken cancellationToken = default) =>
            ExtractToDirectoryAsync(sourceArchiveFileName, destinationDirectoryName, entryNameEncoding, false, cancellationToken);

        /// <summary>
        /// Asynchronously extracts all the files in the specified archive to a directory on the file system and uses the specified character encoding for entry names.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0
        public static Task ExtractToDirectoryAsync(
            string sourceArchiveFileName,
            string destinationDirectoryName,
            Encoding entryNameEncoding,
            bool overwriteFiles,
            CancellationToken cancellationToken = default) =>
            Task.Run(() => ExtractToDirectoryPolyfill(sourceArchiveFileName, destinationDirectoryName, entryNameEncoding, overwriteFiles), cancellationToken);

        // Helper method to handle overwriteFiles polyfill for pre-.NET 8.0
        static void ExtractToDirectoryPolyfill(
            string sourceArchiveFile,
            string destinationDirectory,
            bool overwrite)
        {
#if NET8_0_OR_GREATER
            ZipFile.ExtractToDirectory(sourceArchiveFile, destinationDirectory, overwrite);
#else
            if (overwrite && Directory.Exists(destinationDirectory))
            {
                Directory.Delete(destinationDirectory, true);
            }
            ZipFile.ExtractToDirectory(sourceArchiveFile, destinationDirectory);
#endif
        }

        static void ExtractToDirectoryPolyfill(
            string sourceArchiveFile,
            string destinationDirectory,
            Encoding encoding,
            bool overwrite)
        {
#if NET8_0_OR_GREATER
            ZipFile.ExtractToDirectory(sourceArchiveFile, destinationDirectory, encoding, overwrite);
#else
            if (overwrite && Directory.Exists(destinationDirectory))
            {
                Directory.Delete(destinationDirectory, true);
            }
            ZipFile.ExtractToDirectory(sourceArchiveFile, destinationDirectory, encoding);
#endif
        }

        /// <summary>
        /// Asynchronously creates a zip archive that contains the files and directories from the specified directory.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.createfromdirectoryasync?view=net-11.0
        public static Task CreateFromDirectoryAsync(
            string sourceDirectoryName,
            string destinationArchiveFileName,
            CancellationToken cancellationToken = default) =>
            CreateFromDirectoryAsync(sourceDirectoryName, destinationArchiveFileName, CompressionLevel.Optimal, false, cancellationToken);

        /// <summary>
        /// Asynchronously creates a zip archive that contains the files and directories from the specified directory and uses the specified compression level.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.createfromdirectoryasync?view=net-11.0
        public static Task CreateFromDirectoryAsync(
            string sourceDirectoryName,
            string destinationArchiveFileName,
            CompressionLevel compressionLevel,
            bool includeBaseDirectory,
            CancellationToken cancellationToken = default) =>
            Task.Run(() => ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, compressionLevel, includeBaseDirectory), cancellationToken);

        /// <summary>
        /// Asynchronously creates a zip archive that contains the files and directories from the specified directory, uses the specified compression level and character encoding for entry names.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.createfromdirectoryasync?view=net-11.0
        public static Task CreateFromDirectoryAsync(
            string sourceDirectoryName,
            string destinationArchiveFileName,
            CompressionLevel compressionLevel,
            bool includeBaseDirectory,
            Encoding entryNameEncoding,
            CancellationToken cancellationToken = default) =>
            Task.Run(() => ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, compressionLevel, includeBaseDirectory, entryNameEncoding), cancellationToken);
    }
}

#endif
