using System.Text;
using MotoGrid.Services.Storage;

namespace MotoGrid.Tests.Services;

public class LocalFileStorageTests : IDisposable
{
    private readonly string rootPath;
    private readonly LocalFileStorage storage;

    public LocalFileStorageTests()
    {
        this.rootPath = Path.Combine(Path.GetTempPath(), "motogrid-tests", Guid.NewGuid().ToString("N"));
        this.storage = new LocalFileStorage(new FileStorageOptions
        {
            RootPath = this.rootPath,
            RequestPath = "/uploads",
            MaxFileSizeBytes = 1024,
        });
    }

    [Fact]
    public async Task SaveAsync_ValidImage_SavesFileAndReturnsUrl()
    {
        using var content = new MemoryStream(Encoding.UTF8.GetBytes("fake image"));

        var url = await this.storage.SaveAsync(content, "bike.JPG", "posts");

        Assert.StartsWith("/uploads/posts/", url);
        Assert.EndsWith(".jpg", url);
        var savedFile = Path.Combine(this.rootPath, "posts", Path.GetFileName(url));
        Assert.True(File.Exists(savedFile));
    }

    [Theory]
    [InlineData("virus.exe")]
    [InlineData("script.js")]
    [InlineData("page.html")]
    [InlineData("noextension")]
    public async Task SaveAsync_NotAllowedExtension_Throws(string fileName)
    {
        using var content = new MemoryStream([1, 2, 3]);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => this.storage.SaveAsync(content, fileName, "posts"));
    }

    [Fact]
    public async Task SaveAsync_FileTooLarge_Throws()
    {
        using var content = new MemoryStream(new byte[2048]);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => this.storage.SaveAsync(content, "big.png", "posts"));
    }

    [Fact]
    public async Task SaveAsync_FolderWithPathTraversal_StaysInsideRoot()
    {
        using var content = new MemoryStream([1, 2, 3]);

        var url = await this.storage.SaveAsync(content, "a.png", "../../etc");

        Assert.StartsWith("/uploads/etc/", url);
        Assert.True(Directory.Exists(Path.Combine(this.rootPath, "etc")));
    }

    [Fact]
    public async Task DeleteAsync_ExistingFile_DeletesItAndReturnsTrue()
    {
        using var content = new MemoryStream([1, 2, 3]);
        var url = await this.storage.SaveAsync(content, "a.png", "listings");

        var result = await this.storage.DeleteAsync(url);

        Assert.True(result);
        Assert.False(File.Exists(Path.Combine(this.rootPath, "listings", Path.GetFileName(url))));
    }

    [Theory]
    [InlineData("/uploads/../secret.txt")]
    [InlineData("/other/file.png")]
    [InlineData("/uploads/missing.png")]
    [InlineData("")]
    public async Task DeleteAsync_InvalidOrMissingFile_ReturnsFalse(string url)
    {
        var result = await this.storage.DeleteAsync(url);

        Assert.False(result);
    }

    [Fact]
    public void Constructor_WithoutRootPath_Throws()
    {
        Assert.ThrowsAny<ArgumentException>(() => new LocalFileStorage(new FileStorageOptions { RootPath = "" }));
    }

    public void Dispose()
    {
        if (Directory.Exists(this.rootPath))
        {
            Directory.Delete(this.rootPath, recursive: true);
        }

        GC.SuppressFinalize(this);
    }
}
