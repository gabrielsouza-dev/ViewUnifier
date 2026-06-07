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
        var unifiedText = string.Empty;
        foreach (var document in _documents)
        {
            unifiedText += $"=== {document.Name} - {document.Path} ===\n";
            unifiedText += document.Text;
            unifiedText += "\n\n";
        }
        var path = Path.Combine(Environment.CurrentDirectory, _documentName);
        await File.WriteAllTextAsync(path, unifiedText);
    }

    public async Task AddDocumentAsync(string path)
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
