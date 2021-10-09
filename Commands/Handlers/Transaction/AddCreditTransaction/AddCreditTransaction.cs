using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddCreditTransaction
{
    public class AddCreditTransaction : IRequestHandler<AddCreditTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddCreditTransaction(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddCreditTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = Domain.Transaction.CreateCreditTransaction(request.TransactionType, request.CounterParty, request.BankAccountId, request.Amount,
                request.ReceivedDateTime, request.Description, request.Sequence, request.Attachments, request.MemberId);
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}