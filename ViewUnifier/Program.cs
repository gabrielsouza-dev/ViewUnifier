
Console.WriteLine("===== View Unifier =====");
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
    Console.Write($"Path {paths.Count + 1}: ");
    input = Console.ReadLine();
    if (!string.IsNullOrEmpty(input))
        paths.Add(input);

} while (!string.IsNullOrEmpty(input));


Console.Clear();
var viewUnifier = new ViewUnifier(documentName);
foreach (var path in paths)
{
    var pathType = Helper.IsFileOrDiretory(path);

    switch (pathType)
    {
        case PathType.File:
            await viewUnifier.AddDocumentAsync(path);
            break;
        case PathType.Directory:
            await Helper.DirectoryScanAsync(viewUnifier, path);

            break;
        case PathType.None:
            break;
    }
}

await viewUnifier.BuildAsync();

Console.WriteLine();
var finalPath = Path.Combine(Environment.CurrentDirectory, documentName)!;
Console.WriteLine($"Output file created at: {finalPath}");
Console.WriteLine("Press any key to continue...");
Console.ReadKey();