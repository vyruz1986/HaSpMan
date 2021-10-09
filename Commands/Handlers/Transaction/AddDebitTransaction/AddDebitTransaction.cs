using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

using Types;

namespace Commands.Handlers.Transaction.AddDebitTransaction
{
    public class AddDebitTransaction : IRequestHandler<AddDebitTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddDebitTransaction(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddDebitTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = Domain.Transaction.CreateDebitTransaction(request.TransactionType, request.CounterPartyName, request.BankAccountId, request.Amount,
                request.ReceivedDateTime, request.Description, 0, new List<TransactionAttachment>(), request.MemberId);
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}