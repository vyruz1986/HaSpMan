using System;

using AutoMapper;

using Commands.Handlers.Transaction.AddCreditTransaction;
using Commands.Handlers.Transaction.AddDebitTransaction;

using Domain;

using Queries.Members.Handlers;
using Queries.Members.Handlers.AutocompleteMember;
using Queries.Members.Handlers.SearchMembers;
using Queries.Members.ViewModels;
using Queries.Transactions.ViewModels;

using Types;

using Web.Models;

namespace Web.MapperProfiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionForm, AddDebitTransactionCommand>()
                .ForCtorParam(nameof(AddDebitTransactionCommand.CounterPartyName), o => o.MapFrom(x => x.Counterparty.Name))
                .ForMember(x => x.CounterPartyName,  o => o.MapFrom(a => a.Counterparty.Name))
                .ForCtorParam(nameof(AddDebitTransactionCommand.MemberId), o => o.MapFrom(x => x.Counterparty.MemberId))
                .ForMember(x => x.MemberId, o => o.MapFrom(x => x.Counterparty.MemberId));
            
            CreateMap<TransactionForm, AddCreditTransactionCommand>()
                .ForCtorParam(nameof(AddDebitTransactionCommand.CounterPartyName), o => o.MapFrom(x => x.Counterparty.Name))
                .ForMember(x => x.CounterPartyName, o => o.MapFrom(x => x.Counterparty.Name))
                .ForCtorParam(nameof(AddDebitTransactionCommand.MemberId), o => o.MapFrom(x => x.Counterparty.MemberId))
                .ForMember(x => x.MemberId, o => o.MapFrom(x => x.Counterparty.MemberId));
            ;
            CreateMap<TransactionTypeAmountForm, TransactionTypeAmount>();

            CreateMap<TransactionDetail, TransactionForm>()
                .ForMember(
                    x => x.ReceivedDateTime,
                    o =>
                        o.MapFrom(x => x.ReceivedDateTime.DateTime))
                .ForMember(x => x.Counterparty,
                    o => o.MapFrom(x => new AutocompleteCounterparty(x.CounterPartyName, x.MemberId)));
                
            CreateMap<TransactionTypeAmount, TransactionTypeAmountForm>();
        }
    }
}