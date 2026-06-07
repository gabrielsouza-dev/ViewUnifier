public static class Helper 
{
    public static PathType IsFileOrDiretory(string path)
    {
        if (Directory.Exists(path))
            return PathType.Directory;

        if (File.Exists(path))
            return PathType.File;

        return PathType.None;
    }

    public static async Task DirectoryScanAsync(ViewUnifier viewUnifier, string path)
    {
        var files = Directory.EnumerateFiles(path);

        foreach (var file in files)
        {
           await viewUnifier.AddDocumentAsync(file);
        }

        var directories = Directory.EnumerateDirectories(path);

        foreach (var directory in directories)
        {
            await DirectoryScanAsync(viewUnifier, directory);
        }
    }
}
