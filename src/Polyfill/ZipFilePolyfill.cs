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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0#system-io-compression-zipfile-extracttodirectoryasync(system-string-system-string-system-threading-cancellationtoken)
        public static Task ExtractToDirectoryAsync(
            string sourceArchiveFileName,
            string destinationDirectoryName,
            CancellationToken cancellationToken = default) =>
            ExtractToDirectoryAsync(sourceArchiveFileName, destinationDirectoryName, false, cancellationToken);

        /// <summary>
        /// Asynchronously extracts all the files in the specified archive to a directory on the file system.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0#system-io-compression-zipfile-extracttodirectoryasync(system-string-system-string-system-boolean-system-threading-cancellationtoken)
        public static Task ExtractToDirectoryAsync(
            string sourceArchiveFileName,
            string destinationDirectoryName,
            bool overwriteFiles,
            CancellationToken cancellationToken = default) =>
            Task.Run(() => ExtractToDirectoryPolyfill(sourceArchiveFileName, destinationDirectoryName, overwriteFiles), cancellationToken);

        /// <summary>
        /// Asynchronously extracts all the files in the specified archive to a directory on the file system and uses the specified character encoding for entry names.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0#system-io-compression-zipfile-extracttodirectoryasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken)
        public static Task ExtractToDirectoryAsync(
            string sourceArchiveFileName,
            string destinationDirectoryName,
            Encoding entryNameEncoding,
            CancellationToken cancellationToken = default) =>
            ExtractToDirectoryAsync(sourceArchiveFileName, destinationDirectoryName, entryNameEncoding, false, cancellationToken);

        /// <summary>
        /// Asynchronously extracts all the files in the specified archive to a directory on the file system and uses the specified character encoding for entry names.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.extracttodirectoryasync?view=net-11.0#system-io-compression-zipfile-extracttodirectoryasync(system-string-system-string-system-text-encoding-system-boolean-system-threading-cancellationtoken)
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
            if (!overwrite)
            {
                ZipFile.ExtractToDirectory(sourceArchiveFile, destinationDirectory);
                return;
            }

            ExtractToDirectoryWithOverwrite(sourceArchiveFile, destinationDirectory, null);
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
            if (!overwrite)
            {
                ZipFile.ExtractToDirectory(sourceArchiveFile, destinationDirectory, encoding);
                return;
            }

            ExtractToDirectoryWithOverwrite(sourceArchiveFile, destinationDirectory, encoding);
#endif
        }

#if !NET8_0_OR_GREATER
        // Mirrors the BCL overwriteFiles:true behavior: overwrite conflicting files in place,
        // leaving any other pre-existing files in the destination untouched, rather than
        // deleting the entire destination directory first.
        static void ExtractToDirectoryWithOverwrite(
            string sourceArchiveFile,
            string destinationDirectory,
            Encoding? encoding)
        {
            var destinationRoot = Path.GetFullPath(destinationDirectory);
            Directory.CreateDirectory(destinationRoot);

            var normalizedRoot = destinationRoot;
            if (normalizedRoot[normalizedRoot.Length - 1] != Path.DirectorySeparatorChar)
            {
                normalizedRoot += Path.DirectorySeparatorChar;
            }

            using var archive = encoding == null
                ? ZipFile.OpenRead(sourceArchiveFile)
                : ZipFile.Open(sourceArchiveFile, ZipArchiveMode.Read, encoding);
            foreach (var entry in archive.Entries)
            {
                var destinationPath = Path.GetFullPath(Path.Combine(destinationRoot, entry.FullName));

                // Guard against entries that would escape the destination directory (zip slip).
                if (!destinationPath.StartsWith(normalizedRoot, StringComparison.Ordinal))
                {
                    throw new IOException("Extracting Zip entry would have resulted in a file outside the specified destination directory.");
                }

                if (entry.Name.Length == 0)
                {
                    // The entry is a directory.
                    Directory.CreateDirectory(destinationPath);
                    continue;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
                entry.ExtractToFile(destinationPath, overwrite: true);
            }
        }
#endif

        /// <summary>
        /// Asynchronously opens a ZipArchive on the specified archiveFileName in the specified ZipArchiveMode mode.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.openasync?view=net-11.0#system-io-compression-zipfile-openasync(system-string-system-io-compression-ziparchivemode-system-threading-cancellationtoken)
        public static Task<ZipArchive> OpenAsync(
            string archiveFileName,
            ZipArchiveMode mode,
            CancellationToken cancellationToken = default) =>
            Task.Run(() => ZipFile.Open(archiveFileName, mode), cancellationToken);

        /// <summary>
        /// Asynchronously opens a ZipArchive on the specified archiveFileName in the specified ZipArchiveMode mode.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.openasync?view=net-11.0#system-io-compression-zipfile-openasync(system-string-system-io-compression-ziparchivemode-system-text-encoding-system-threading-cancellationtoken)
        public static Task<ZipArchive> OpenAsync(
            string archiveFileName,
            ZipArchiveMode mode,
            Encoding? entryNameEncoding,
            CancellationToken cancellationToken = default) =>
            Task.Run(() => ZipFile.Open(archiveFileName, mode, entryNameEncoding), cancellationToken);

        /// <summary>
        /// Asynchronously opens a ZipArchive on the specified path for reading.
        /// The specified file is opened with FileMode.Open.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.openreadasync?view=net-11.0#system-io-compression-zipfile-openreadasync(system-string-system-threading-cancellationtoken)
        public static Task<ZipArchive> OpenReadAsync(
            string archiveFileName,
            CancellationToken cancellationToken = default) =>
            Task.Run(() => ZipFile.OpenRead(archiveFileName), cancellationToken);

        /// <summary>
        /// Asynchronously creates a zip archive that contains the files and directories from the specified directory.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.createfromdirectoryasync?view=net-11.0#system-io-compression-zipfile-createfromdirectoryasync(system-string-system-string-system-threading-cancellationtoken)
        public static Task CreateFromDirectoryAsync(
            string sourceDirectoryName,
            string destinationArchiveFileName,
            CancellationToken cancellationToken = default) =>
            CreateFromDirectoryAsync(sourceDirectoryName, destinationArchiveFileName, CompressionLevel.Optimal, false, cancellationToken);

        /// <summary>
        /// Asynchronously creates a zip archive that contains the files and directories from the specified directory and uses the specified compression level.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.createfromdirectoryasync?view=net-11.0#system-io-compression-zipfile-createfromdirectoryasync(system-string-system-string-system-io-compression-compressionlevel-system-boolean-system-threading-cancellationtoken)
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
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfile.createfromdirectoryasync?view=net-11.0#system-io-compression-zipfile-createfromdirectoryasync(system-string-system-string-system-io-compression-compressionlevel-system-boolean-system-text-encoding-system-threading-cancellationtoken)
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
