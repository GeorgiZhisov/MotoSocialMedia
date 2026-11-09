namespace MotoGrid.Services.Storage;

/// <summary>
/// Stores uploaded images and videos.
/// The rest of the application depends only on this interface, so the local disk
/// storage can later be replaced by a cloud storage (e.g. Azure Blob Storage)
/// without changing the controllers and the services (Strategy pattern + Dependency Injection).
/// </summary>
public interface IFileStorage
{
    /// <summary>
    /// Saves the file and returns its public URL (e.g. "/uploads/posts/abc.jpg").
    /// </summary>
    /// <exception cref="InvalidOperationException">The file type or size is not allowed.</exception>
    Task<string> SaveAsync(Stream content, string fileName, string folder, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a file by its public URL. Returns false if the file does not exist.
    /// </summary>
    Task<bool> DeleteAsync(string url, CancellationToken cancellationToken = default);
}
