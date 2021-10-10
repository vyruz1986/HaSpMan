using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

using Types;

namespace Commands.Handlers.Transaction.AddCreditTransaction
{
    public class AddCreditTransactionHandler : IRequestHandler<AddCreditTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddCreditTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddCreditTransactionCommand request, CancellationToken cancellationToken)
        {
            var lastSequence = await _transactionRepository.GetLastTransactionForBankAccount(request.BankAccountId);
            var currentSequence = ++lastSequence;
            var transaction = Domain.Transaction.CreateCreditTransaction(request.CounterPartyName, request.BankAccountId, request.Amount,
                request.ReceivedDateTime, request.Description, currentSequence, new List<TransactionAttachment>(), request.MemberId, new List<TransactionTypeAmount>());
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}