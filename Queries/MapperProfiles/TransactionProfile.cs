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
            CreateMap<Transaction, TransactionSummary>();
        }
    }
}