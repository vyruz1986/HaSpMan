using Types;

namespace Domain;

public class TransactionTypeAmount : IEquatable<TransactionTypeAmount>
{
    public TransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public TransactionTypeAmount(
        TransactionType transactionType,
        decimal amount)
    {
        TransactionType = transactionType;
        Amount = amount;
    }

    public bool Equals(TransactionTypeAmount? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return TransactionType == other.TransactionType && Amount == other.Amount;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != GetType())
            return false;
        return Equals((TransactionTypeAmount)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)TransactionType, Amount);
    }
};
