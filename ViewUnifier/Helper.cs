using System.Text;

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

    public static async Task DirectoryScanAsync(List<PathNode> pathNodes, int directoryId, string path, int dirLevel)
    {
        dirLevel++;

        var files = Directory.EnumerateFiles(path);
        foreach (var file in files)
        {
            var fileNode = new PathNode(pathNodes.Count + 1, directoryId, file, PathType.File, dirLevel);

            pathNodes.Add(fileNode);
        }

        var directories = Directory.EnumerateDirectories(path);
        foreach (var directory in directories)
        {
            var dirNode = new PathNode(pathNodes.Count + 1, directoryId, directory, PathType.Directory, dirLevel);

            pathNodes.Add(dirNode);
            await DirectoryScanAsync(pathNodes, dirNode.Id, directory, dirLevel);
        }
    }

    public static void PrintDirectoryStruct(StringBuilder sb, PathNode pathNode)
    {
        for (int i = 0; i < pathNode.Level; i++)
        {
            sb.Append("   ");
        }
        var docName = pathNode.Path.Split("\\").Last();
        sb.AppendLine($"{pathNode.Id}. {docName} - {pathNode.Path}");
    }

    internal static void PrintLayoutToRemove(List<PathNode> pathNodes)
    {
        var sb = new StringBuilder();

        sb.AppendLine("===== View Unifier =====");
        sb.AppendLine("");
        sb.AppendLine("");
        sb.AppendLine("Remove specific Directory or File");
        sb.AppendLine("(Leave blank to finish)");
        sb.AppendLine("");

        foreach (var pathNode in pathNodes)
        {
            PrintDirectoryStruct(sb, pathNode);
        }
        Console.Clear();
        Console.WriteLine(sb.ToString());
    }
}
