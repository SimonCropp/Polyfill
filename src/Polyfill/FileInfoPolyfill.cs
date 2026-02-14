#pragma warning disable

#if !NET11_0_OR_GREATER

namespace Polyfills;

using System.IO;

static partial class Polyfill
{
    extension(FileInfo target)
    {
        /// <summary>
        /// Creates a hard link located in <see cref="FileSystemInfo.Name"/> that refers to the same file content as <paramref name="pathToTarget"/>.
        /// </summary>
        /// <param name="pathToTarget">The path of the hard link target.</param>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo.createashardlink?view=net-11.0
        public void CreateAsHardLink(string pathToTarget)
        {
            HardLinkHelper.ValidatePath(pathToTarget, nameof(pathToTarget));
            HardLinkHelper.CreateHardLink(target.FullName, Path.GetFullPath(pathToTarget));
        }
    }
}

#endif
