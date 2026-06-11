using System.Text;

public class ViewUnifier
{
    private string _documentName { get; set; }
    private List<Document> _documents { get; set; } = new();

    public ViewUnifier(string documentName)
    {
        _documentName = documentName;
    }

    public async Task BuildAsync()
    {
        var sb = new StringBuilder();
        foreach (var document in _documents)
        {
            sb.AppendLine($"=== {document.Name} - {document.Path} ===");
            sb.AppendLine(document.Text);
            sb.AppendLine();
        }
        var path = Path.Combine(Environment.CurrentDirectory, _documentName);
        await File.WriteAllTextAsync(path, sb.ToString());
    }

    public async Task AddDocumentsAsync(string[] paths)
    {
        foreach (var path in paths)
        {
            var docName = path.Split("\\").Last();
            var text = await DocumentReaderAsync(path);

            if (!string.IsNullOrEmpty(text))
            {
                var document = new Document(docName, path, text);
                _documents.Add(document);
                PrintScrean();
            }
        }
    }

    public async Task<string?> DocumentReaderAsync(string path)
    {
        var bytes = await File.ReadAllBytesAsync(path);

        var utf8 = new UTF8Encoding(false, true);

        try
        {
            var text = utf8.GetString(bytes);
            return text;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void PrintScrean()
    {
        Console.SetCursorPosition(0, 0);
        var sb = new StringBuilder();

        sb.AppendLine("===== View Unifier =====");
        sb.AppendLine("");
        sb.AppendLine($"Output File Name: {_documentName}");
        sb.AppendLine($"Files Merged: {_documents.Count}");
        sb.AppendLine($"Last File Processed: {_documents.Last().Name}");

        Console.Write(sb.ToString());
    }
}
