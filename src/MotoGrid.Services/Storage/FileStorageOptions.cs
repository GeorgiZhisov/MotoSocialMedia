namespace MotoGrid.Services.Storage;

public class FileStorageOptions
{
    /// <summary>Physical folder where the files are saved (e.g. wwwroot/uploads).</summary>
    public string RootPath { get; set; } = null!;

    /// <summary>URL prefix of the saved files (e.g. "/uploads").</summary>
    public string RequestPath { get; set; } = "/uploads";

    public long MaxFileSizeBytes { get; set; } = MotoGrid.Common.Constants.ValidationConstants.Media.MaxFileSizeBytes;

    public ICollection<string> AllowedExtensions { get; set; } =
        new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".webp", ".gif", ".mp4", ".webm", ".gpx" };
}
