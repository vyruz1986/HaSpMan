using System;
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
            var transactions = Domain.Transaction.CreateInternalBankTransaction(TransactionType.InternalBank, new Types.BankAccount(request.From.Name, request.From.Number, request.From.BankAccountId), new Types.BankAccount(request.To.Name, request.To.Number, request.To.BankAccountId), request.Amount,
                request.ReceivedDateTime, request.Description, request.FromSequence, request.ToSequence, request.Attachments);
            _transactionRepository.AddRange(transactions);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transactions.First().Id;
        }
    }
}