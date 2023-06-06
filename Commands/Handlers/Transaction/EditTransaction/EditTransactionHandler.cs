using Commands.Handlers.Transaction.AddAttachments;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.EditTransaction;

public class EditTransactionHandler : IRequestHandler<EditTransactionCommand, Guid>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public EditTransactionHandler(ITransactionRepository transactionRepository, IMapper mapper, IMediator mediator)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(EditTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ArgumentException($"No transaction found for Id {request.Id}", nameof(request.Id));

        var totalAmount = request.TransactionTypeAmounts.Sum(x => x.Amount);

        transaction.ChangeCounterParty(request.CounterPartyName, request.MemberId);
        transaction.ChangeBankAccountId(request.BankAccountId);
        transaction.ChangeReceivedDateTime(request.ReceivedDateTime);
        transaction.ChangeAmount(totalAmount, request.TransactionTypeAmounts);
        transaction.ChangeDescription(request.Description);

        await _transactionRepository.SaveAsync(cancellationToken);

        await _mediator.Send(new AddAttachmentsCommand(transaction.Id, request.NewAttachmentFiles), cancellationToken);

        return transaction.Id;
    }
}
