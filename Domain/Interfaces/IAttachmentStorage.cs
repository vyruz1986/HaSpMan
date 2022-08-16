using Types;

namespace Domain.Interfaces;

public interface IAttachmentStorage
{
    Task AddAsync(string path, string contentType, byte[] bytes, CancellationToken cancellationToken);

    Task<Attachment> GetAsync(string blobUri, CancellationToken cancellationToken);
    void Delete(string blobUri, CancellationToken cancellationToken);
}