using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddDebitTransaction
{
    public class AddDebitTransaction : IRequestHandler<AddDebitCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddDebitTransaction(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddDebitCommand request, CancellationToken cancellationToken)
        {
            var transaction = Domain.Transaction.CreateDebitTransaction(request.TransactionType, request.CounterParty, request.BankAccountId, request.Amount,
                request.ReceivedDateTime, request.Description, request.Sequence, request.Attachments, request.MemberId);
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}