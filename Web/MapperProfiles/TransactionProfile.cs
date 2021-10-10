using System;

using AutoMapper;

using Commands.Handlers.Transaction.AddDebitTransaction;

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
            CreateMap<TransactionForm, AddDebitTransactionCommand>();
            CreateMap<TransactionTypeAmountForm, TransactionTypeAmount>();

            CreateMap<TransactionDetail, TransactionForm>()
                .ForMember(x => x.ReceivedDateTime, o => o.MapFrom(x => ToDateTime(x.ReceivedDateTime)));
            CreateMap<TransactionTypeAmount, TransactionTypeAmountForm>();
        }

        private static DateTime? ToDateTime(DateTimeOffset? dateTimeOffset)
        {
            if (dateTimeOffset == null)
                return null;

            return dateTimeOffset!.Value.DateTime;
        }
    }
}