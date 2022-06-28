namespace Domain;

public class TransactionAttachment
{
#pragma warning disable 8618
    private TransactionAttachment() { } // Make EFCore happy
#pragma warning restore 8618
    public TransactionAttachment(Guid transactionId, string name)
    {
        
        FullPath = Path.Combine(transactionId.ToString(), name);

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Cannot be null or empty", nameof(name));
        }
        Name = name;
    }

    public string FullPath { get; private set; }
    public string Name { get; private set; }

}
