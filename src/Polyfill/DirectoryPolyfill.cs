#if !NET7_0_OR_GREATER

namespace Polyfills;

using System.IO;

static partial class Polyfill
{
    extension(Directory)
    {
        /// <summary>
        /// Creates a uniquely named, empty directory in the current user's temporary directory.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.createtempsubdirectory?view=net-11.0
        public static DirectoryInfo CreateTempSubdirectory(string? prefix = null)
        {
            var path = Path.Combine(Path.GetTempPath(), (prefix ?? "") + Path.GetRandomFileName());
            return Directory.CreateDirectory(path);
        }
    }
}

#endif
