namespace Domain;

public class TransactionAttachment
{
#pragma warning disable 8618
    private TransactionAttachment() { } // Make EFCore happy
#pragma warning restore 8618
    public TransactionAttachment(string blobUri, string name)
    {
        if (string.IsNullOrWhiteSpace(blobUri))
        {
            throw new ArgumentException("Cannot be null or empty", nameof(blobUri));
        }
        BlobURI = blobUri;

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Cannot be null or empty", nameof(name));
        }
        Name = name;
    }

    public string BlobURI { get; private set; }
    public string Name { get; private set; }

}
