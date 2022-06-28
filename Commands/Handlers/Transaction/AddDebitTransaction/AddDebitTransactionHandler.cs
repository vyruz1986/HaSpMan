using Commands.Handlers.Transaction.AddAttachments;

using Domain;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddDebitTransaction;

public class AddDebitTransactionHandler : IRequestHandler<AddDebitTransactionCommand, Guid>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMediator _mediator;

    public AddDebitTransactionHandler(ITransactionRepository transactionRepository, IMediator mediator)
    {
        _transactionRepository = transactionRepository;
        _mediator = mediator;
    }
    public async Task<Guid> Handle(AddDebitTransactionCommand request, CancellationToken cancellationToken)
    {
        var totalAmount = request.TransactionTypeAmounts.Sum(x => x.Amount);
        var transaction = new DebitTransaction(request.CounterPartyName, request.BankAccountId, totalAmount,
            request.ReceivedDateTime, request.Description, new List<TransactionAttachment>(), 
            request.MemberId, 
            request.TransactionTypeAmounts);

        _transactionRepository.Add(transaction);
        await _transactionRepository.SaveAsync(cancellationToken);


        await _mediator.Send(new AddAttachmentsCommand(transaction.Id, request.AttachmentFiles), cancellationToken);

        return transaction.Id;
    }
}
