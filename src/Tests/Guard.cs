// ReSharper disable RedundantUsingDirective
#region CallerArgumentExpression

using System.IO;

static class Guard
{
    public static void FileExists(string path, [CallerArgumentExpression("path")] string argumentName = "")
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException($"File not found. Path: {path}", argumentName);
        }
    }
}


static class GuardUsage
{
    public static string[] Method(string path)
    {
        Guard.FileExists(path);
        return File.ReadAllLines(path);
    }
}
#endregion