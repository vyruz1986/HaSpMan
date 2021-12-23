using AutoMapper;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.EditTransaction;

public class EditTransactionHandler : IRequestHandler<EditTransactionCommand, Guid>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public EditTransactionHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }
    public async Task<Guid> Handle(EditTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(request.Id, cancellationToken);
        if (transaction == null)
        {
            throw new ArgumentException($"No transaction found for Id {request.Id}", nameof(request.Id));
        }


        var totalAmount = request.TransactionTypeAmounts.Sum(x => x.Amount);

        transaction.ChangeCounterParty(request.CounterPartyName, request.MemberId);
        transaction.ChangeBankAccountId(request.BankAccountId);
        transaction.ChangeReceivedDateTime(request.ReceivedDateTime);
        transaction.ChangeAmount(totalAmount, request.TransactionTypeAmounts);
        transaction.ChangeDescription(request.Description);


        await _transactionRepository.SaveAsync(cancellationToken);
        return transaction.Id;
    }
}
