using AutoMapper;

using Commands.Handlers;
using Commands.Handlers.Transaction.AddCreditTransaction;
using Commands.Handlers.Transaction.AddDebitTransaction;
using Commands.Handlers.Transaction.EditTransaction;

using Domain;

using Queries.Members.Handlers.AutocompleteMember;
using Queries.Transactions.ViewModels;

using Web.Models;

using AttachmentFile = Commands.Handlers.AttachmentFile;
using TransactionAttachment = Domain.TransactionAttachment;
using TransactionTypeAmount = Domain.TransactionTypeAmount;


namespace Web.MapperProfiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<TransactionForm, AddDebitTransactionCommand>()
            .ForCtorParam(nameof(AddDebitTransactionCommand.CounterPartyName), o => o.MapFrom(x => x.Counterparty.Name))
            .ForMember(x => x.CounterPartyName, o => o.MapFrom(a => a.Counterparty.Name))
            .ForCtorParam(nameof(AddDebitTransactionCommand.MemberId), o => o.MapFrom(x => x.Counterparty.MemberId))
            .ForMember(x => x.MemberId, o => o.MapFrom(x => x.Counterparty.MemberId))
            .ForCtorParam(nameof(AddCreditTransactionCommand.AttachmentFiles), o => o.MapFrom(x => x.TransactionAttachments))
            .ForMember(x => x.AttachmentFiles, o => o.MapFrom(x => x.TransactionAttachments))
            .ForCtorParam(nameof(AddCreditTransactionCommand.TransactionTypeAmounts), o => o.MapFrom(x => x.TransactionTypeAmounts));

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
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(
                x => x.ReceivedDateTime,
                o =>
                    o.MapFrom(x => x.ReceivedDateTime.DateTime))
            .ForMember(x => x.Counterparty,
                o => o.MapFrom(x => new AutocompleteCounterparty(x.CounterPartyName, x.MemberId)))
            .ForMember(x => x.TransactionAttachments, o => o.MapFrom(x => x.TransactionAttachments));

        CreateMap<TransactionTypeAmount, TransactionTypeAmountForm>();
        
        CreateMap<Models.TransactionAttachment, AttachmentFile>()
            .ForCtorParam(nameof(AttachmentFile.FileName), o => o.MapFrom(x => x.FileName))
            .ForMember(x => x.FileName, o => o.MapFrom(x => x.FileName))
            .ForCtorParam(nameof(AttachmentFile.UnsafePath), o => o.MapFrom(x => x.UnsafePath))
            .ForMember(x => x.UnsafePath, o => o.MapFrom(x => x.UnsafePath))
            .ForCtorParam(nameof(AttachmentFile.ContentType), o => o.MapFrom(x => x.ContentType))
            .ForMember(x => x.ContentType, o => o.MapFrom(x => x.ContentType));

        CreateMap<Domain.TransactionAttachment, Queries.Transactions.ViewModels.TransactionAttachment>();

        CreateMap<Queries.Transactions.ViewModels.TransactionAttachment, Web.Models.TransactionAttachment>()
            .ForCtorParam(nameof(Models.TransactionAttachment.FileName), o => o.MapFrom(x => x.Name))
            .ForCtorParam(nameof(Models.TransactionAttachment.ContentType), o => o.MapFrom(x => string.Empty))
            .ForCtorParam(nameof(Models.TransactionAttachment.UnsafePath), o => o.MapFrom(x => string.Empty))
            .ForMember(x => x.FileName, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.ContentType, o => o.Ignore())
            .ForMember(x => x.UnsafePath, o => o.Ignore());

        CreateMap<Queries.Transactions.ViewModels.TransactionTypeAmount, TransactionTypeAmountForm>()
            .ForCtorParam(nameof(TransactionTypeAmountForm.Amount), o => o.MapFrom(x => x.Amount))
            .ForCtorParam(nameof(TransactionTypeAmountForm.TransactionType), o => o.MapFrom(x => x.TransactionType))
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount))
            .ForMember(x => x.TransactionType, o => o.MapFrom(x => x.TransactionType));
    }
}