using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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

            var lastSequence = await _transactionRepository.GetLastTransactionForBankAccount(request.BankAccountId);
            var currentSequence = ++lastSequence;
            var transaction = Domain.Transaction.CreateDebitTransaction(request.CounterPartyName, request.BankAccountId, request.Amount,
                request.ReceivedDateTime, request.Description, currentSequence, new List<TransactionAttachment>(), request.MemberId, request.TransactionTypeAmounts);

            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}