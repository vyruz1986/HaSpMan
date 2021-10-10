using System;
using System.ComponentModel.DataAnnotations;

namespace Types
{
    public record Address(
       string Street,
       string City,
       string Country,
       string ZipCode,
       string HouseNumber)
    {
        public override string ToString()
        {
            return $"{Street} {HouseNumber}, {ZipCode} {City}, {Country}";
        }
    }
    
    public record BankAccount(
        string Name,
        string Number,
        Guid BankAccountId);

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
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return TransactionType == other.TransactionType && Amount == other.Amount;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TransactionTypeAmount)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)TransactionType, Amount);
        }
    };

}