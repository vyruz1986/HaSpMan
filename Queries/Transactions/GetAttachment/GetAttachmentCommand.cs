using Persistence.Repositories;

using Types;

namespace Queries.Transactions.GetAttachment;

public record GetAttachmentQuery(Guid TransactionId, string FileName) : IRequest<Attachment>;

public class GetAttachmentHandler : IRequestHandler<GetAttachmentQuery, Attachment>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAttachmentStorage _attachmentStorage;

    public GetAttachmentHandler(ITransactionRepository transactionRepository, IAttachmentStorage attachmentStorage)
    {
        _transactionRepository = transactionRepository;
        _attachmentStorage = attachmentStorage;
    }
    public async Task<Attachment> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
    {
        
        var transactionId = request.TransactionId;
        var transaction = await _transactionRepository.GetByIdAsync(transactionId, cancellationToken)
            ?? throw new ArgumentException($"No transaction found for Id {request.TransactionId}", nameof(request.TransactionId));

        var attachment = transaction.Attachments.SingleOrDefault(x => x.Name == request.FileName)
            ?? throw new ArgumentException($"No attachment found with name {request.FileName}", nameof(request.FileName));

        var file = await _attachmentStorage.GetAsync(attachment.FullPath, cancellationToken);
        return file;        
    }
}