static class SolutionDirectoryFinder
{
    public static string Find([CallerFilePath] string sourceFile = "")
    {
        if (TryFind(sourceFile, out var solutionDirectory))
        {
            return solutionDirectory;
        }

        throw new("Could not find solution directory");
    }

    public static bool TryFind(string sourceFile, [NotNullWhen(true)] out string? path)
    {
        var currentDirectory = Directory.GetParent(sourceFile)!.FullName;
        do
        {
            if (Directory.GetFiles(currentDirectory, "*.slnx").Length > 0)
            {
                path = currentDirectory;
                return true;
            }

            var parent = Directory.GetParent(currentDirectory);
            if (parent == null)
            {
                path = null;
                return false;
            }

            currentDirectory = parent.FullName;
        } while (true);
    }
}