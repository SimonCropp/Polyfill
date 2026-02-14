#if FeatureCompression
namespace Polyfills;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

static partial class Polyfill
{
#if !NET10_0_OR_GREATER

    /// <summary>
    /// Opens the entry from the zip archive.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchiveentry.openasync?view=net-11.0
    public static Task<Stream> OpenAsync(this ZipArchiveEntry target, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(target.Open());
    }

    /// <summary>
    /// Archives a file by compressing it and adding it to the zip archive.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.createentryfromfileasync?view=net-11.0
    public static Task<ZipArchiveEntry> CreateEntryFromFileAsync(this ZipArchive target, string sourceFileName, string entryName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(target.CreateEntryFromFile(sourceFileName, entryName));
    }

    /// <summary>
    /// Archives a file by compressing it and adding it to the zip archive.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.createentryfromfileasync?view=net-11.0
    public static Task<ZipArchiveEntry> CreateEntryFromFileAsync(this ZipArchive target, string sourceFileName, string entryName, CompressionLevel compressionLevel, CancellationToken cancellationToken = default) =>
        Task.FromResult(target.CreateEntryFromFile(sourceFileName, entryName, compressionLevel));

    /// <summary>
    /// Extracts all the files in the zip archive to a directory on the file system.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectory?view=net-11.0#system-io-compression-zipfileextensions-extracttodirectory(system-io-compression-ziparchive-system-string)
    public static Task ExtractToDirectoryAsync(this ZipArchive target, string destinationDirectoryName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        target.ExtractToDirectory(destinationDirectoryName, false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Extracts all the files in the zip archive to a directory on the file system.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectoryasync?view=net-11.0#system-io-compression-zipfileextensions-extracttodirectoryasync(system-io-compression-ziparchive-system-string-system-boolean-system-threading-cancellationtoken)
    public static Task ExtractToDirectoryAsync(this ZipArchive target, string destinationDirectoryName, bool overwriteFiles, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        target.ExtractToDirectory(destinationDirectoryName, overwriteFiles);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Extracts an entry in the zip archive to a file.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttofileasync?view=net-11.0#system-io-compression-zipfileextensions-extracttofileasync(system-io-compression-ziparchiveentry-system-string-system-threading-cancellationtoken)
    public static Task ExtractToFileAsync(this ZipArchiveEntry source, string destinationFileName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        source.ExtractToFile(destinationFileName);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Extracts an entry in the zip archive to a file.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttofileasync?view=net-11.0#system-io-compression-zipfileextensions-extracttofileasync(system-io-compression-ziparchiveentry-system-string-system-boolean-system-threading-cancellationtoken)
    public static Task ExtractToFileAsync(this ZipArchiveEntry source, string destinationFileName, bool overwrite, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        source.ExtractToFile(destinationFileName, overwrite);
        return Task.CompletedTask;
    }
#endif

#if !NET11_0_OR_GREATER

    /// <summary>
    /// Opens the entry from the zip archive with the specified access mode.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchiveentry.open?view=net-11.0
    public static Stream Open(this ZipArchiveEntry target, FileAccess access) =>
        target.Open();

#if FeatureValueTask
    /// <summary>
    /// Asynchronously opens the entry from the zip archive with the specified access mode.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchiveentry.openasync?view=net-11.0
    public static ValueTask<Stream> OpenAsync(this ZipArchiveEntry target, FileAccess access, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return new ValueTask<Stream>(target.Open());
    }
#endif
#endif

#if !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_0_OR_GREATER

    /// <summary>
    /// Extracts all the files in the zip archive to a directory on the file system.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectory?view=net-11.0#system-io-compression-zipfileextensions-extracttodirectory(system-io-compression-ziparchive-system-string-system-boolean)
    public static void ExtractToDirectory(this ZipArchive target, string destinationDirectoryName, bool overwriteFiles)
    {
        var directoryInfo = Directory.CreateDirectory(destinationDirectoryName);
        var destinationDirectoryFullPath = directoryInfo.FullName;

        if (!destinationDirectoryFullPath.EndsWith(Path.DirectorySeparatorChar))
        {
            destinationDirectoryFullPath = string.Concat(destinationDirectoryFullPath, Path.DirectorySeparatorChar);
        }

        foreach (var source in target.Entries)
        {
            var fullName = SanitizeEntryFilePath(source.FullName);

            var fileDestinationPath = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, fullName));

            if (!fileDestinationPath.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
            {
                throw new IOException("Extracting Zip entry would have resulted in a file outside the specified destination directory.");
            }

            if (Path.GetFileName(fileDestinationPath).Length == 0)
            {
                if (source.Length != 0)
                {
                    throw new IOException("Zip entry name ends in directory separator character but contains data.");
                }

                Directory.CreateDirectory(fileDestinationPath);

                // It is a directory
                continue;
            }

            source.ExtractToFile(fileDestinationPath, overwrite: overwriteFiles);
        }
    }

    static readonly char[] invalidPathChars = Path.GetInvalidPathChars();

    static string SanitizeEntryFilePath(string fullName)
    {
        var sanitized = new char[fullName.Length];
        for (var index = 0; index < fullName.Length; index++)
        {
            var ch = fullName[index];
            if (Array.IndexOf(invalidPathChars, ch) >= 0)
            {
                sanitized[index] = '_';
                continue;
            }

            sanitized[index] = ch;
        }

        return new string(sanitized);
    }
#endif
}
#endif
