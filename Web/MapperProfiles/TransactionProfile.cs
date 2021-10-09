using AutoMapper;

using Commands;
using Commands.Handlers.Transaction.AddDebitTransaction;

using Domain;

using Types;

using Web.Models;

namespace Web.MapperProfiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionForm, AddDebitTransactionCommand>();

        }
    }
}