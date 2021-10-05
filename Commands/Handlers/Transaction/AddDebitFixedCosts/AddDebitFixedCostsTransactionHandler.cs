using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddDebitFixedCosts
{
    public class AddDebitFixedCostsTransactionHandler : IRequestHandler<AddDebitFixedCostsTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddDebitFixedCostsTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddDebitFixedCostsTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = Domain.Transaction.CreateDebitFixedCosts(request.CounterParty, request.BankAccountId, request.Amount,
                request.ReceivedDateTime, request.Description, request.Sequence, request.Attachments);
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}