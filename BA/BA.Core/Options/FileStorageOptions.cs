namespace BA.Core.Options;

public class FileStorageOptions
{
    public string DownloadsFolder { get; set; } = default!;
    public List<string> AllowedExtensions { get; set; } = new();
}