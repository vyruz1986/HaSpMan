using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Types;

namespace Queries.Transactions.GetAttachment;

public record GetAttachmentQuery(Guid TransactionId, string FileName) : IRequest<Attachment>;

public class GetAttachmentHandler : IRequestHandler<GetAttachmentQuery, Attachment>
{
    private readonly HaSpManContext _dbContext;
    private readonly IAttachmentStorage _attachmentStorage;

    public GetAttachmentHandler(HaSpManContext dbContext, IAttachmentStorage attachmentStorage)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _attachmentStorage = attachmentStorage ?? throw new ArgumentNullException(nameof(attachmentStorage));
    }
    public async Task<Attachment> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
    {
        var transactionId = request.TransactionId;
        var transaction =
            await _dbContext.FinancialYears
                .SelectMany(x => x.Transactions)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == transactionId, cancellationToken)
            ?? throw new ArgumentException($"No transaction found for Id {request.TransactionId}", nameof(request.TransactionId));

        var attachment = transaction.Attachments.SingleOrDefault(x => x.Name == request.FileName)
            ?? throw new ArgumentException($"No attachment found with name {request.FileName}", nameof(request.FileName));

        var file = await _attachmentStorage.GetAsync(attachment.FullPath, cancellationToken);
        return file;
    }
}