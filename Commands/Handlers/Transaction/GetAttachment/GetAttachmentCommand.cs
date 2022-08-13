using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Commands.Services;

using FluentValidation;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.GetAttachment;

public record GetAttachmentCommand(Guid TransactionId, string FileName) : IRequest<Attachment>;

public class GetAttachmentCommandValidator : AbstractValidator<GetAttachmentCommand>
{
    public GetAttachmentCommandValidator()
    {
        RuleFor(x => x.FileName).NotEmpty();
        RuleFor(x => x.TransactionId).NotEmpty();

    }
        
}

public class GetAttachmentHandler : IRequestHandler<GetAttachmentCommand, Attachment>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAttachmentStorage _attachmentStorage;

    public GetAttachmentHandler(ITransactionRepository transactionRepository, IAttachmentStorage attachmentStorage)
    {
        _transactionRepository = transactionRepository;
        _attachmentStorage = attachmentStorage;
    }
    public async Task<Attachment> Handle(GetAttachmentCommand request, CancellationToken cancellationToken)
    {
        
        var transactionId = request.TransactionId;
        var transaction = await _transactionRepository.GetByIdAsync(transactionId, cancellationToken);
        if (transaction == null)
        {
            throw new ArgumentException($"No transaction found for Id {request.TransactionId}", nameof(request.TransactionId));
        }

        var attachment = transaction.Attachments.SingleOrDefault(x => x.Name == request.FileName);
        if (attachment == null)
        {
            throw new ArgumentException($"No attachment found with name {request.FileName}", nameof(request.FileName));
        }

        var file = await _attachmentStorage.GetAsync(attachment.FullPath, cancellationToken);
        return file;
        

    }
}