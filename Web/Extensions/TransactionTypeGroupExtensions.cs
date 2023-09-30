using Types;

using Web.Pages.Transactions;

namespace Web.Extensions;

public static class TransactionTypeGroupExtensions
{
    public static IReadOnlyList<TransactionType> GetScopedTransactionTypes(this TransactionTypeGroup group)
    {
        return group == TransactionTypeGroup.Debit
            ? new List<TransactionType>
            {
                TransactionType.DebitWorkshopFee,
                TransactionType.DebitDonation,
                TransactionType.DebitMemberFee,
                TransactionType.InternalBank,
                TransactionType.DebitSaleConsumables,
                TransactionType.DebitSaleGoodsAndServices,
                TransactionType.DebitRefund
            }.OrderBy(x => x.GetDescription()).ToList()
            : (IReadOnlyList<TransactionType>)new List<TransactionType>
            {
                TransactionType.CreditAcquisitionConsumables,
                TransactionType.CreditAcquisitionGoodsAndServices,
                TransactionType.CreditBankCosts,
                TransactionType.CreditFixedCosts,
                TransactionType.InternalBank
            }.OrderBy(x => x.GetDescription()).ToList();
    }
}
