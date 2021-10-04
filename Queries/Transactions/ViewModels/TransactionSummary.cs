using System;

using Types;

namespace Queries.Transactions.ViewModels
{
    public record TransactionSummary(
        Guid Id, 
        string CounterParty, 
        string BankAccount, 
        string Amount, 
        DateTimeOffset ReceivedDateTime, 
        TransactionType TransactionType);
}
