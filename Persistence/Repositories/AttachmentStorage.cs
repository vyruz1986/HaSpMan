
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using Domain.Interfaces;

using Microsoft.Extensions.Options;

using Persistence.Configuration;

using Types;

namespace Persistence.Repositories;

public class AttachmentStorage : IAttachmentStorage
{
    private readonly StorageOptions _storageOptions;

    public AttachmentStorage(IOptions<StorageOptions> options)
    {
        _storageOptions = options.Value;
    }
    public async Task AddAsync(string path, string contentType, byte[] bytes, CancellationToken cancellationToken)
    {
        var container = new BlobContainerClient(_storageOptions.ConnectionString, _storageOptions.StorageContainerName);
        await container.CreateIfNotExistsAsync(PublicAccessType.None
            , new Dictionary<string, string>(), cancellationToken);
        var blob = container.GetBlobClient(path);

        using var memoryStream = new MemoryStream(bytes);
        await blob.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = contentType }, cancellationToken: cancellationToken);

    }

    public async Task<Attachment> GetAsync(string blobUri, CancellationToken cancellationToken)
    {
        var container = new BlobContainerClient(_storageOptions.ConnectionString, _storageOptions.StorageContainerName);
        var blobClient = container.GetBlobClient(blobUri);
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
