
Console.WriteLine("===== View Unifier =====");
Console.WriteLine("");
Console.WriteLine("This application was developed to collect and consolidate UTF-8 encoded text files for analysis and AI context generation. It enables users to select files dynamically based on directory structures, facilitating efficient content aggregation and processing.");
Console.WriteLine("");
Console.Write("Output File Name: ");
var documentName = Console.ReadLine()!;

var paths = new List<string>();
var input = string.Empty;

Console.WriteLine("");
Console.WriteLine("Add Directories and Files to Merge");
Console.WriteLine("(Leave blank to finish)");
Console.WriteLine("");

do
{
    input = string.Empty;

    Console.Write($"Path {paths.Count + 1}: ");
    input = Console.ReadLine();
    if (!string.IsNullOrEmpty(input))
        paths.Add(input);

} while (!string.IsNullOrEmpty(input));

var pathNodes = new List<PathNode>();
var dirLevel = 0;
foreach (var path in paths)
{
    var pathType = Helper.IsFileOrDiretory(path);

    switch (pathType)
    {
        case PathType.File:
            var fileStruct = new PathNode(pathNodes.Count + 1, 0, path, PathType.File, dirLevel);
            pathNodes.Add(fileStruct);
            break;
        case PathType.Directory:
            var pathNode = new PathNode(pathNodes.Count + 1, 0, path, PathType.Directory, dirLevel);
            pathNodes.Add(pathNode);
            await Helper.DirectoryScanAsync(pathNodes, pathNode.Id, path, dirLevel);

            break;
        case PathType.None:
            break;
    }
}

int inputInt;

Helper.PrintLayoutToRemove(pathNodes);
do
{
    input = string.Empty;

    Console.Write("Remove at: ");
    input = Console.ReadLine();
    if (string.IsNullOrEmpty(input)) break;

    var isNumber = int.TryParse(input, out inputInt);

    if (!isNumber)
    {
        Console.WriteLine("Id must be a number...");
        continue;
    }

    var selected = pathNodes.FirstOrDefault(f => f.Id == inputInt);

    if(selected is not null)
    {
        switch(selected.PathType)
        {
            case PathType.File:
                pathNodes.Remove(selected);
                break;
            case PathType.Directory:
                pathNodes.Remove(selected);
                pathNodes.RemoveAll(f => f.DirectoryId == selected.Id);
                break;
        }
        Console.WriteLine("File id has been removed!");
        Thread.Sleep(2000);
        Helper.PrintLayoutToRemove(pathNodes);
    }
    else
    {
        Console.WriteLine("Id not found...");
    }


} while (!string.IsNullOrEmpty(input));

var finalPaths = pathNodes.Where(f=>f.PathType == PathType.File).Select(f=>f.Path).ToArray();

Console.Clear();
var viewUnifier = new ViewUnifier(documentName);
await viewUnifier.AddDocumentsAsync(finalPaths);
await viewUnifier.BuildAsync();

Console.WriteLine();
var finalPath = Path.Combine(Environment.CurrentDirectory, documentName)!;
Console.WriteLine($"Output file created at: {finalPath}");
Console.WriteLine("Press any key to continue...");
Console.ReadKey();