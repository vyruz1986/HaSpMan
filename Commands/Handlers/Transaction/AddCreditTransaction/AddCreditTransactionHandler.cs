
using Domain;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddCreditTransaction;

public class AddCreditTransactionHandler : IRequestHandler<AddCreditTransactionCommand, Guid>
{
    private readonly ITransactionRepository _transactionRepository;

    public AddCreditTransactionHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
    public async Task<Guid> Handle(AddCreditTransactionCommand request, CancellationToken cancellationToken)
    {
        var totalAmount = request.TransactionTypeAmounts.Sum(x => x.Amount);
        var transaction = new CreditTransaction(request.CounterPartyName, request.BankAccountId, totalAmount,
            request.ReceivedDateTime, request.Description, new List<TransactionAttachment>(), request.MemberId, request.TransactionTypeAmounts);
        _transactionRepository.Add(transaction);
        await _transactionRepository.SaveAsync(cancellationToken);
        return transaction.Id;
    }
}
