using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Domain;

using MediatR;

using Persistence.Repositories;

using Types;

namespace Commands.Handlers
{
    public class AddDebitFixedCostsTransactionHandler : IRequestHandler<AddDebitFixedCostsTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddDebitFixedCostsTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddDebitFixedCostsTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = Transaction.CreateDebitFixedCosts(request.CounterParty, request.BankAccount, request.Amount,
                request.ReceivedDateTime, request.Description, request.Sequence, request.Attachments);
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync();
            return transaction.Id;
        }
    }

    public record AddDebitFixedCostsTransactionCommand(
        CounterParty CounterParty, BankAccount BankAccount, decimal Amount,
        DateTime ReceivedDateTime, string Description, int Sequence, ICollection<Transaction.TransactionAttachment> Attachments) : IRequest<Guid>
    {

    }
}
