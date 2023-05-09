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
            new(TransactionType.DebitMemberFee, null)
        };
        TransactionAttachments = new List<TransactionAttachment>();
    }

    public Guid Id { get; set; }

    [ValidateComplexType, Required]
    public AutocompleteCounterparty Counterparty { get; set; }

    [Required]
    public Guid? BankAccountId { get; set; }

    [Required]
    public DateTime? ReceivedDateTime { get; set; }

    [Required(AllowEmptyStrings = false), StringLength(1000)]
    public string? Description { get; set; }

    [ValidateComplexType]
    public ICollection<TransactionTypeAmountForm> TransactionTypeAmounts { get; set; }


    public ICollection<TransactionAttachment> TransactionAttachments { get; set; }
    public DateTime? NewMembershipExpirationDate { get; set; }
    public bool ApplyMembershipCalculation { get; set; }
}

public class TransactionTypeAmountForm
{
    public TransactionTypeAmountForm(TransactionType transactionType, decimal? amount)
    {
        TransactionType = transactionType;
        Amount = amount;
    }
    [Required, EnumDataType(typeof(TransactionType))]
    public TransactionType? TransactionType { get; set; } = null;


    [Required]
    [Range(0.01, 10000000)]
    public decimal? Amount { get; set; }
}

public class TransactionAttachment
{
    public TransactionAttachment(string fileName, string contentType, string unsafePath)
    {
        FileName = fileName;
        ContentType = contentType;
        UnsafePath = unsafePath;
    }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public string UnsafePath { get; set; }
}