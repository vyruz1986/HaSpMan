using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Types;

namespace Queries.Transactions.GetAttachment;

public record GetAttachmentQuery(Guid TransactionId, string FileName) : IRequest<Attachment>;

public class GetAttachmentHandler : IRequestHandler<GetAttachmentQuery, Attachment>
{
    private readonly IDbContextFactory<HaSpManContext> _dbContextFactory;
    private readonly IAttachmentStorage _attachmentStorage;

    public GetAttachmentHandler(IDbContextFactory<HaSpManContext> dbContextFactory, IAttachmentStorage attachmentStorage)
    {
        _dbContextFactory = dbContextFactory;
        _attachmentStorage = attachmentStorage;
    }
    public async Task<Attachment> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
    {
        var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var transactionId = request.TransactionId;
        var transaction =
            await context.FinancialYears
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