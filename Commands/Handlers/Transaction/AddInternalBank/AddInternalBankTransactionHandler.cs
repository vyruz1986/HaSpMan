using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddInternalBank
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
            var transactions = Domain.Transaction.CreateInternalBankTransaction(request.From, request.To, request.Amount,
                request.ReceivedDateTime, request.Description, request.FromSequence, request.ToSequence, request.Attachments);
            _transactionRepository.AddRange(transactions);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transactions.First().Id;
        }
    }
}