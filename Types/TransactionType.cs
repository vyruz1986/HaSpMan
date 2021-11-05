using System.ComponentModel;

namespace Types
{
    public enum TransactionType
    {
        [Description("Donatie")]
        CreditDonation,
        [Description("Lidgeld")]
        CreditMemberFee,
        [Description("Workshop")]
        CreditWorkshopFee,
        [Description("Debet verbruiksgoederen")]
        DebitAcquisitionConsumables,
        [Description("Debet goederen en diensten")]
        DebitAcquisitionGoodsAndServices,
        [Description("Bankkosten")]
        DebitBankCosts,
        [Description("Vaste kosten")]
        DebitFixedCosts,
        [Description("Interne bank transfer")]
        InternalBank
    }
}