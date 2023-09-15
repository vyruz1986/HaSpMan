using Domain;

using Queries.Transactions.ViewModels;

using TransactionAttachment = Domain.TransactionAttachment;
using TransactionTypeAmount = Domain.TransactionTypeAmount;

namespace Queries.MapperProfiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionDetail>()
            .ForCtorParam(nameof(TransactionDetail.TransactionTypeAmounts),
                o => o.MapFrom(x => x.TransactionTypeAmounts))
            .ForMember(x => x.TransactionTypeAmounts, o => o.MapFrom(x => x.TransactionTypeAmounts))
            .ForCtorParam(nameof(TransactionDetail.TransactionAttachments), o => o.MapFrom(x => x.Attachments))
            .ForMember(x => x.TransactionAttachments, o => o.MapFrom(x => x.Attachments))
            .ForCtorParam(nameof(TransactionDetail.Id), o => o.MapFrom(x => x.Id))
            .ForCtorParam(nameof(TransactionDetail.Amount), o => o.MapFrom(x => x.Amount))
            .ForCtorParam(nameof(TransactionDetail.BankAccountId), o => o.MapFrom(x => x.BankAccountId))
            .ForCtorParam(nameof(TransactionDetail.CounterPartyName), o => o.MapFrom(x => x.CounterPartyName))
            .ForCtorParam(nameof(TransactionDetail.Description), o => o.MapFrom(x => x.Description))
            .ForCtorParam(nameof(TransactionDetail.MemberId), o => o.MapFrom(x => x.MemberId))
            .ForCtorParam(nameof(TransactionDetail.ReceivedDateTime), o => o.MapFrom(x => x.ReceivedDateTime));

        CreateMap<TransactionAttachment, Queries.Transactions.ViewModels.TransactionAttachment>()
            .ForCtorParam(nameof(Transactions.ViewModels.TransactionAttachment.Name), o => o.MapFrom(x => x.Name))
            .ForCtorParam(nameof(Transactions.ViewModels.TransactionAttachment.FullPath), o => o.MapFrom(x => x.FullPath));
        CreateMap<TransactionTypeAmount, Queries.Transactions.ViewModels.TransactionTypeAmount>()
            .ForCtorParam(nameof(Transactions.ViewModels.TransactionTypeAmount.Amount),
                o => o.MapFrom(x => x.Amount))
            .ForCtorParam(nameof(Transactions.ViewModels.TransactionTypeAmount.TransactionType),
                o => o.MapFrom(x => x.TransactionType));

    }
}