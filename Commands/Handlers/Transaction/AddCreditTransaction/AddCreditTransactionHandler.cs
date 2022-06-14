
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
       
        var transaction = new CreditTransaction(request.CounterPartyName, request.BankAccountId, 4,
            request.ReceivedDateTime, request.Description, new List<TransactionAttachment>(), request.MemberId, new List<TransactionTypeAmount>());
        _transactionRepository.Add(transaction);
        await _transactionRepository.SaveAsync(cancellationToken);
        return transaction.Id;
    }
}
