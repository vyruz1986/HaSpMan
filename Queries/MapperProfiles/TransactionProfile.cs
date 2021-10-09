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
                .ForMember(x => x.TransactionType, o => o.MapFrom(x => x.TransactionType))
                .ForMember(x => x.ReceivedDateTime, o => o.MapFrom(x => x.ReceivedDateTime))
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
                .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount))
                .ForMember(x => x.MemberId, o => o.MapFrom(x => x.MemberId));
        }
    }
}