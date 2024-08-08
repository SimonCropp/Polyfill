#region CallerArgumentExpression

static class FileUtil
{
    public static void FileExists(string path, [CallerArgumentExpression("path")] string argumentName = "")
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException($"File not found. Path: {path}", argumentName);
        }
    }
}

static class FileUtilUsage
{
    public static string[] Method(string path)
    {
        FileUtil.FileExists(path);
        return File.ReadAllLines(path);
    }
}
#endregion