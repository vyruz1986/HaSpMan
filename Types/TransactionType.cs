using System.ComponentModel;

namespace Types;

public enum TransactionType
{
    [Description("Donation")]
    DebitDonation,
    [Description("Membership fee")]
    DebitMemberFee,
    [Description("Workshop fee")]
    DebitWorkshopFee,
    [Description("Acquisition of consumables")]
    CreditAcquisitionConsumables,
    [Description("Acquisition of goods and services")]
    CreditAcquisitionGoodsAndServices,
    [Description("Bank costs")]
    CreditBankCosts,
    [Description("Fixed costs")]
    CreditFixedCosts,
    [Description("Internal bank transfer")]
    InternalBank
}
