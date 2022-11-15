using Types;

using Web.Pages.Transactions;

namespace Web;

public class TransactionTypes
{
    public static IReadOnlyList<TransactionType> GetScopedTransactionTypes(TransactionTypeGroup group)
    {
        if (group == TransactionTypeGroup.Debit)
        {
            return new List<TransactionType>
            {
                TransactionType.DebitWorkshopFee,
                TransactionType.DebitDonation,
                TransactionType.DebitMemberFee,
                TransactionType.InternalBank,
                TransactionType.DebitSaleConsumables,
                TransactionType.DebitSaleGoodsAndServices
            }.OrderBy(x => x.GetDescription()).ToList();
        }
        return new List<TransactionType>
        {
            TransactionType.CreditAcquisitionConsumables,
            TransactionType.CreditAcquisitionGoodsAndServices,
            TransactionType.CreditBankCosts,
            TransactionType.CreditFixedCosts,
            TransactionType.InternalBank
        }.OrderBy(x => x.GetDescription()).ToList();
    }

    public static TransactionTypeGroup GetTransactionTypeGroup(TransactionType transactionType)
    {
        return new List<TransactionType>
        {
            TransactionType.DebitWorkshopFee,
            TransactionType.DebitDonation,
            TransactionType.DebitMemberFee,
            TransactionType.InternalBank
        }.Contains(transactionType) ? TransactionTypeGroup.Debit : TransactionTypeGroup.Credit;
    }

}
