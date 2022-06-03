using AutoMapper;

using Commands.Services;

using Domain;

using FluentValidation;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddAttachments;

public record AddAttachmentsCommand(Guid TransactionId, ICollection<AttachmentFile> Attachments) : IRequest<Unit>;

public class AddAttachmentsCommandValidator : AbstractValidator<AddAttachmentsCommand>
{
    public AddAttachmentsCommandValidator()
    {
        RuleFor(x => x.TransactionId).NotEmpty();
        RuleForEach(x => x.Attachments).SetValidator(new AttachmentValidator());
    }
}

public class AttachmentValidator : AbstractValidator<AttachmentFile>
{
    public AttachmentValidator()
    {
        RuleFor(x => x.FileName).NotEmpty();
        RuleFor(x => x.ContentType).NotEmpty();
        RuleFor(x => x.Bytes).NotEmpty();
    }
}

public class AddAttachmentsHandler : IRequestHandler<AddAttachmentsCommand, Unit>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAttachmentStorage _attachmentStorage;

    public AddAttachmentsHandler(ITransactionRepository transactionRepository, IAttachmentStorage attachmentStorage)
    {
        _transactionRepository = transactionRepository;
        _attachmentStorage = attachmentStorage;
    }

    public async Task<Unit> Handle(AddAttachmentsCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(request.TransactionId, cancellationToken);
        if (transaction == null)
        {
            throw new ArgumentException($"No transaction found for Id {request.TransactionId}", nameof(request.TransactionId));
        }
        
        var transactionAttachments = await StoreAttachmentsAsync(request, cancellationToken);
        
        transaction.AddAttachments(transactionAttachments);

        await _transactionRepository.SaveAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<List<TransactionAttachment>> StoreAttachmentsAsync(AddAttachmentsCommand request, CancellationToken cancellationToken)
    {
        var transactionAttachments = new List<TransactionAttachment>();
        var attachments = request.Attachments;
        foreach (var attachment in attachments)
        {
            var transactionAttachment = new TransactionAttachment(request.TransactionId, attachment.FileName);
            await _attachmentStorage.AddAsync(transactionAttachment.FullPath, attachment.ContentType, attachment.Bytes, cancellationToken);

            transactionAttachments.Add(transactionAttachment);
        }
        
        return transactionAttachments;
    }
}

public record Attachment(string Name, string ContentType, string BlobUri, byte[] Bytes);

public record AttachmentFile(string FileName, string ContentType, byte[] Bytes);