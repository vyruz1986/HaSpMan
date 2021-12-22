namespace Domain;

public abstract class Transaction
{
#pragma warning disable 8618
    protected Transaction() { } // Make EFCore happy
#pragma warning restore 8618
    protected Transaction(
        string counterPartyName,
        Guid bankAccountId,
        decimal amount,
        DateTimeOffset receivedDateTime,
        string description,
        ICollection<TransactionAttachment> attachments,
        Guid? memberId,
        ICollection<TransactionTypeAmount> transactionTypeAmounts)
    {
        Id = Guid.NewGuid();
        DateFiled = DateTimeOffset.Now;

        if (string.IsNullOrWhiteSpace(counterPartyName))
        {
            throw new ArgumentNullException(nameof(counterPartyName), "Cannot be null");
        }

        CounterPartyName = counterPartyName;

        BankAccountId = bankAccountId;
        MemberId = memberId;

        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative", nameof(amount));
        }
        Amount = amount;

        if (receivedDateTime > DateTimeOffset.Now)
        {
            throw new ArgumentException($"Received date can not be set in the future: {receivedDateTime}",
                nameof(receivedDateTime));
        }
        ReceivedDateTime = receivedDateTime;
        Description = description;
        Attachments = attachments ?? throw new ArgumentNullException(nameof(attachments), "Cannot be null");

        TransactionTypeAmounts = transactionTypeAmounts ??
                                 throw new ArgumentNullException(nameof(transactionTypeAmounts), "Cannot be null");
    }

    public Guid? MemberId { get; private set; }

    public Guid Id { get; private set; }
    public DateTimeOffset ReceivedDateTime { get; private set; }
    public decimal Amount { get; private set; }
    public string CounterPartyName { get; private set; }
    public Guid BankAccountId { get; private set; }

    public string Description { get; private set; }

    public DateTimeOffset DateFiled { get; private set; }
    public ICollection<TransactionAttachment> Attachments { get; private set; }

    public ICollection<TransactionTypeAmount> TransactionTypeAmounts { get; private set; }

    public bool IsTransactionForMember => MemberId.HasValue;

    public void ChangeCounterParty(string counterPartyName, Guid? memberId)
    {
        if (memberId == MemberId && counterPartyName == CounterPartyName)
        {
            return;
        }

        MemberId = memberId;
        CounterPartyName = counterPartyName;
    }

    public void ChangeBankAccountId(Guid bankAccountId)
    {
        if (bankAccountId == BankAccountId)
        {
            return;
        }

        BankAccountId = bankAccountId;
    }

    public void ChangeReceivedDateTime(DateTimeOffset receivedDateTime)
    {
        if (receivedDateTime > DateTimeOffset.Now)
        {
            throw new ArgumentException($"Received date is set to be in the future: {receivedDateTime}",
                nameof(receivedDateTime));
        }

        if (receivedDateTime == ReceivedDateTime)
        {
            return;
        }

        ReceivedDateTime = receivedDateTime;
    }

    public void ChangeAmount(decimal amount, ICollection<TransactionTypeAmount> transactionTypeAmounts)
    {
        var sumOfTransactionTypeAmounts = transactionTypeAmounts.Sum(x => x.Amount);
        if (amount != sumOfTransactionTypeAmounts)
        {
            throw new ArgumentException(
                $"Sum of the transaction type amounts ({sumOfTransactionTypeAmounts}) does not match total amount: {amount}", nameof(amount));
        }

        if (amount == Amount && transactionTypeAmounts.SequenceEqual(TransactionTypeAmounts))
        {
            return;
        }

        Amount = amount;
        TransactionTypeAmounts = transactionTypeAmounts;
    }

    public void ChangeDescription(string description)
    {
        if (description == Description)
        {
            return;
        }

        Description = description;
    }
}
public class DebitTransaction : Transaction
{
    private DebitTransaction() : base()
    {

    }
    public DebitTransaction(string counterPartyName, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
        string description, ICollection<TransactionAttachment> attachments, Guid? memberId, ICollection<TransactionTypeAmount> transactionTypeAmounts) :
        base(counterPartyName, bankAccountId, amount, receivedDateTime, description, attachments, memberId, transactionTypeAmounts)
    {

    }
}
public class CreditTransaction : Transaction
{

    private CreditTransaction() : base()
    {

    }

    public CreditTransaction(string counterPartyName, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
        string description, ICollection<TransactionAttachment> attachments, Guid? memberId, ICollection<TransactionTypeAmount> transactionTypeAmounts) :
        base(counterPartyName, bankAccountId, amount, receivedDateTime, description, attachments, memberId, transactionTypeAmounts)
    {

    }
}

