using System;

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

    public class TransactionTypeAmount
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
    };

}