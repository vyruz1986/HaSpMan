
using AutoMapper;

using Domain;

using Queries.Transactions.ViewModels;

namespace Queries.MapperProfiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionSummary>();

            CreateMap<Transaction, TransactionDetail>();
        }
    }
}