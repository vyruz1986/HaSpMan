using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Queries.Members.Handlers.AutocompleteMember;

using Types;
namespace Web.Models;

public class TransactionForm
{

    public TransactionForm()
    {
        Counterparty = new AutocompleteCounterparty(string.Empty, null);
        TransactionTypeAmounts = new List<TransactionTypeAmountForm>()
        {
            new(TransactionType.DebitWorkshopFee, 0)
        };
        TransactionAttachments = new List<TransactionAttachment>();
    }

    [NotMapped]
    public AutocompleteCounterparty Counterparty { get; set; }

    [Required]
    public Guid? BankAccountId { get; set; }

    [Required]
    public DateTime? ReceivedDateTime { get; set; }

    public string? Description { get; set; }

    [ValidateComplexType]
    public ICollection<TransactionTypeAmountForm> TransactionTypeAmounts { get; set; }

    public ICollection<TransactionAttachment> TransactionAttachments { get; set; }
}

public class TransactionTypeAmountForm
{
    public TransactionTypeAmountForm(TransactionType transactionType, decimal amount)
    {
        TransactionType = transactionType;
        Amount = amount;
    }
    [Required]
    public TransactionType TransactionType { get; set; }


    [Required]
    [Range(0, 10000000)]
    public decimal Amount { get; set; }
}

public class TransactionAttachment
{
    public TransactionAttachment(string fileName, string contentType, byte[] bytes)
    {
        FileName = fileName;
        ContentType = contentType;
        Bytes = bytes;
    }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Bytes { get; set; }
}