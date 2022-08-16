
using Commands.Handlers.Transaction.AddAttachments;

using Domain;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddCreditTransaction;

public class AddCreditTransactionHandler : IRequestHandler<AddCreditTransactionCommand, Guid>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMediator _mediator;

    public AddCreditTransactionHandler(ITransactionRepository transactionRepository, IMediator mediator)
    {
        _transactionRepository = transactionRepository;
        _mediator = mediator;
    }
    public async Task<Guid> Handle(AddCreditTransactionCommand request, CancellationToken cancellationToken)
    {
        var totalAmount = request.TransactionTypeAmounts.Sum(x => x.Amount);
        var transaction = new CreditTransaction(request.CounterPartyName, request.BankAccountId, totalAmount,
            request.ReceivedDateTime, request.Description, new List<TransactionAttachment>(), request.MemberId, 
            request.TransactionTypeAmounts);
        _transactionRepository.Add(transaction);
        await _transactionRepository.SaveAsync(cancellationToken);

        await _mediator.Send(new AddAttachmentsCommand(transaction.Id, request.AttachmentFiles), cancellationToken);

        return transaction.Id;
    }
}
