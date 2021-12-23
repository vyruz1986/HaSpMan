using System.ComponentModel;

namespace Web.Pages.Transactions
{
    public enum TransactionTypeGroup
    {
        [Description("Debet")]
        Debit,
        [Description("Credit")]
        Credit
    }
}