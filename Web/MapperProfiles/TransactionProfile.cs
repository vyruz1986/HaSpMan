using AutoMapper;

using Commands.Handlers;
using Commands.Handlers.Transaction.AddCreditTransaction;
using Commands.Handlers.Transaction.AddDebitTransaction;

using Domain;

using Queries.Members.Handlers.AutocompleteMember;
using Queries.Transactions.ViewModels;

using Web.Models;

using TransactionAttachment = Domain.TransactionAttachment;


namespace Web.MapperProfiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<TransactionForm, AddDebitTransactionCommand>()
            .ForCtorParam(nameof(AddDebitTransactionCommand.CounterPartyName), o => o.MapFrom(x => x.Counterparty.Name))
            .ForMember(x => x.CounterPartyName, o => o.MapFrom(a => a.Counterparty.Name))
            .ForCtorParam(nameof(AddDebitTransactionCommand.MemberId), o => o.MapFrom(x => x.Counterparty.MemberId))
            .ForMember(x => x.MemberId, o => o.MapFrom(x => x.Counterparty.MemberId));

        CreateMap<TransactionForm, AddCreditTransactionCommand>()
            .ForCtorParam(nameof(AddCreditTransactionCommand.CounterPartyName), o => o.MapFrom(x => x.Counterparty.Name))
            .ForMember(x => x.CounterPartyName, o => o.MapFrom(x => x.Counterparty.Name))
            .ForCtorParam(nameof(AddCreditTransactionCommand.MemberId), o => o.MapFrom(x => x.Counterparty.MemberId))
            .ForMember(x => x.MemberId, o => o.MapFrom(x => x.Counterparty.MemberId))
            .ForCtorParam(nameof(AddCreditTransactionCommand.AttachmentFiles), o => o.MapFrom(x => x.TransactionAttachments))
            .ForMember(x => x.AttachmentFiles, o => o.MapFrom(x => x.TransactionAttachments))
            .ForCtorParam(nameof(AddCreditTransactionCommand.TransactionTypeAmounts), o => o.MapFrom(x => x.TransactionTypeAmounts));

        CreateMap<TransactionTypeAmountForm, TransactionTypeAmount>();

        CreateMap<TransactionDetail, TransactionForm>()
            .ForMember(
                x => x.ReceivedDateTime,
                o =>
                    o.MapFrom(x => x.ReceivedDateTime.DateTime))
            .ForMember(x => x.Counterparty,
                o => o.MapFrom(x => new AutocompleteCounterparty(x.CounterPartyName, x.MemberId)))
            .ForMember(x => x.TransactionAttachments, o => o.Ignore());

        CreateMap<TransactionTypeAmount, TransactionTypeAmountForm>();

        CreateMap<TransactionAttachmentFile, TransactionAttachment>()
            .ForCtorParam(nameof(TransactionAttachment.Name), o => o.MapFrom(x => x.Name))
            .ForMember(x => x.FullPath, opt => opt.Ignore());
        CreateMap<TransactionAttachment, TransactionAttachmentFile>()
            .ForCtorParam(nameof(TransactionAttachmentFile.Name), o => o.MapFrom(x => x.Name));
    }
}