using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Domain;

using MediatR;

using Persistence.Repositories;

using Types;

namespace Commands.Handlers.Transaction.AddDebitTransaction
{
    public class AddDebitTransactionHandler : IRequestHandler<AddDebitTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddDebitTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddDebitTransactionCommand request, CancellationToken cancellationToken)
        {
            var totalAmount = request.TransactionTypeAmounts.Sum(x => x.Amount);
            var transaction = Domain.Transaction.CreateDebitTransaction(request.CounterPartyName, request.BankAccountId, totalAmount,
                request.ReceivedDateTime, request.Description, new List<TransactionAttachment>(), request.MemberId, request.TransactionTypeAmounts);

            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}