using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Domain;

using MediatR;

using Persistence.Repositories;

using Types;

namespace Commands.Handlers.Transaction.AddInternalBankTransaction
{
    public class AddInternalBankTransactionHandler : IRequestHandler<AddInternalBankTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddInternalBankTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddInternalBankTransactionCommand request, CancellationToken cancellationToken)
        {
            var lastFromSequence = await _transactionRepository.GetLastTransactionForBankAccount(request.From.BankAccountId);
            var lastToSequence =
                await _transactionRepository.GetLastTransactionForBankAccount(request.To.BankAccountId);
            var fromSequence = ++lastFromSequence;
            var toSequence = ++lastToSequence;

            var transactions = Domain.Transaction.CreateInternalBankTransaction(new Types.BankAccount(request.From.Name, request.From.Number, request.From.BankAccountId), new Types.BankAccount(request.To.Name, request.To.Number, request.To.BankAccountId), request.Amount,
                request.ReceivedDateTime, request.Description, fromSequence, toSequence, request.Attachments, new List<TransactionTypeAmount>());
            _transactionRepository.AddRange(transactions);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transactions.First().Id;
        }
    }
}