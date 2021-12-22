using AutoMapper;

using Commands.Handlers.Transaction.AddCreditTransaction;
using Commands.Handlers.Transaction.AddDebitTransaction;

using Domain;

using Queries.Members.Handlers.SearchMembers;
using Queries.Transactions.ViewModels;

using Web.Models;

namespace Web.MapperProfiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<TransactionForm, AddDebitTransactionCommand>()
            .ForCtorParam(nameof(AddDebitTransactionCommand.CounterPartyName), o => o.MapFrom(x => x.Member.Name))
            .ForMember(x => x.CounterPartyName, o => o.MapFrom(a => a.Member.Name))
            .ForCtorParam(nameof(AddDebitTransactionCommand.MemberId), o => o.MapFrom(x => x.Member.MemberId))
            .ForMember(x => x.MemberId, o => o.MapFrom(x => x.Member.MemberId));

        CreateMap<TransactionForm, AddCreditTransactionCommand>()
            .ForCtorParam(nameof(AddDebitTransactionCommand.CounterPartyName), o => o.MapFrom(x => x.Member.Name))
            .ForMember(x => x.CounterPartyName, o => o.MapFrom(x => x.Member.Name))
            .ForCtorParam(nameof(AddDebitTransactionCommand.MemberId), o => o.MapFrom(x => x.Member.MemberId))
            .ForMember(x => x.MemberId, o => o.MapFrom(x => x.Member.MemberId));
        ;
        CreateMap<TransactionTypeAmountForm, TransactionTypeAmount>();

        CreateMap<TransactionDetail, TransactionForm>()
            .ForMember(
                x => x.ReceivedDateTime,
                o =>
                    o.MapFrom(x => x.ReceivedDateTime.DateTime))
            .ForMember(x => x.Member,
                o => o.MapFrom(x => new AutocompleteMember(x.CounterPartyName, x.MemberId)));

        CreateMap<TransactionTypeAmount, TransactionTypeAmountForm>();
    }
}