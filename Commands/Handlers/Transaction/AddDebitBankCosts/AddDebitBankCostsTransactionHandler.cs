using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddDebitBankCosts
{
    public class AddDebitBankCostsTransactionHandler : IRequestHandler<AddDebitBankCostsTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddDebitBankCostsTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddDebitBankCostsTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = Domain.Transaction.CreateDebitBankCostsTransaction(request.CounterParty, request.BankAccount, request.Amount,
                request.ReceivedDateTime, request.Description, request.Sequence, request.Attachments);
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}