using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Web.Models
{
    public class TransactionFormAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not TransactionForm transactionForm)
                return new ValidationResult($"Value is not of type {typeof(TransactionForm)}");
            if (transactionForm.TransactionTypeAmounts.Count == 0)
            {
                return new ValidationResult("Value requires at least one transaction type");
            }
            var sumOfTransactionType = transactionForm.TransactionTypeAmounts.Sum(x => x.Amount);
            if (sumOfTransactionType == transactionForm.Amount)
            {
                return null;
            }
            return new ValidationResult($"Sum of transaction types {sumOfTransactionType:C} is not equal to the sum of the transaction: {transactionForm.Amount:C}");

        }
    }
}