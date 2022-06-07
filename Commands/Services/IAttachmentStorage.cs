using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using Commands.Handlers.Transaction.AddAttachments;

using Microsoft.Extensions.Options;

namespace Commands.Services;

public interface IAttachmentStorage
{
    Task AddAsync(string path, string contentType, byte[] bytes, CancellationToken cancellationToken);

    Task<Attachment> GetAsync(string blobUri, CancellationToken cancellationToken);
    void Delete(string blobUri, CancellationToken cancellationToken);
}

public class AttachmentStorage : IAttachmentStorage
{
    private readonly BlobContainerClient _container;

    public AttachmentStorage(IOptions<StorageOptions> options)
    {
        var storageOptions = options.Value;
        _container = new BlobContainerClient(storageOptions.ConnectionString, storageOptions.StorageContainerName);
    }
    public async Task AddAsync(string path, string contentType, byte[] bytes, CancellationToken cancellationToken)
    {
        await _container.CreateIfNotExistsAsync(PublicAccessType.BlobContainer, new Dictionary<string, string>(), cancellationToken);
        var blob = _container.GetBlobClient(path);

        using var memoryStream = new MemoryStream(bytes);
        await blob.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = contentType }, cancellationToken: cancellationToken);
        
    }

    public async Task<Attachment> GetAsync(string blobUri, CancellationToken cancellationToken)
    {
        var blobClient = _container.GetBlobClient(blobUri);
        var response = await blobClient.DownloadAsync(cancellationToken);
        var content = response.Value;
        var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);
        var filename = Path.GetFileName(blobUri);
        using var memoryStream = new MemoryStream();
        await content.Content.CopyToAsync(memoryStream, cancellationToken);
        return new Attachment(filename, properties.Value.ContentType, blobUri, memoryStream.GetBuffer());
    }

    public void Delete(string blobUri, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class StorageOptions
{
    public StorageOptions(string storageContainerName, string connectionString)
    {
        StorageContainerName = storageContainerName;
        ConnectionString = connectionString;
    }

    public string StorageContainerName { get; set; }
    public string ConnectionString { get; set; }
}