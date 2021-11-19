using System.ComponentModel;

namespace Types
{
    public enum TransactionType
    {
        [Description("Donation")]
        CreditDonation,
        [Description("Membership fee")]
        CreditMemberFee,
        [Description("Workshop fee")]
        CreditWorkshopFee,
        [Description("Acquisition of consumables")]
        DebitAcquisitionConsumables,
        [Description("Acquisition of goods and services")]
        DebitAcquisitionGoodsAndServices,
        [Description("Bank costs")]
        DebitBankCosts,
        [Description("Fixed costs")]
        DebitFixedCosts,
        [Description("Internal bank transfer")]
        InternalBank
    }
}