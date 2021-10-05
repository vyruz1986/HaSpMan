using System;

using AutoMapper;

using Domain;

using Queries.Transactions.ViewModels;

using Types;

namespace Queries.MapperProfiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionSummary>()
                .ForMember(x => x.CounterParty, o => o.MapFrom(x => x.CounterParty.Name))
                .ForMember(x => x.BankAccountId, o => o.MapFrom(x => x.BankAccountId))
                .ForMember(x => x.TransactionType, o => o.MapFrom(new TransactionTypeResolver()));
        }
    }

    public class TransactionTypeResolver : IValueResolver<Transaction, TransactionSummary, TransactionType>
    { 
        public TransactionType Resolve(Transaction source, TransactionSummary destination, TransactionType destMember,
            ResolutionContext context)
        {
            var transactionType = source switch
            {
                Transaction.DebitAcquisitionConsumablesTransaction => TransactionType.DebitAcquisitionConsumables,
                Transaction.DebitAcquisitionGoodsAndServicesTransaction => TransactionType.DebitAcquisitionGoodsAndServices,
                Transaction.DebitFixedCostsTransaction => TransactionType.DebitFixedCosts,
                Transaction.DebitBankCostsTransaction => TransactionType.DebitBankCosts,
                Transaction.CreditDonationTransaction => TransactionType.CreditDonation,
                Transaction.CreditMemberFeeTransaction => TransactionType.CreditMemberFee,
                Transaction.CreditWorkshopFeeTransaction => TransactionType.CreditWorkshopFee,
                Transaction.InternalBankTransaction => TransactionType.InternalBank,
                _ => throw new ArgumentOutOfRangeException(nameof(source), "Unknown transaction type"),
            };
            return transactionType;
        }
    }
}