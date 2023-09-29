using Queries.Transactions.ViewModels;

using Web.Pages.Transactions;

namespace Web.Extensions;

public static class ViewModelExtensions
{
    public static TransactionTypeGroup GetTransactionTypeGroup(this TransactionDetail transactionDetail)
        => transactionDetail.TransactionType == TransactionType.Credit
            ? TransactionTypeGroup.Credit
            : TransactionTypeGroup.Debit;

    public static TransactionTypeGroup GetTransactionTypeGroup(this TransactionSummary transactionSummary)
        => transactionSummary.TransactionType == TransactionType.Credit
            ? TransactionTypeGroup.Credit
            : TransactionTypeGroup.Debit;
}