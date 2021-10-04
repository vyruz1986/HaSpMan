using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddCreditDonation
{
    public class AddCreditDonationTransactionHandler : IRequestHandler<AddCreditDonationTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddCreditDonationTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddCreditDonationTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = Domain.Transaction.CreateCreditDonationTransaction(request.CounterParty, request.BankAccount, request.Amount,
                request.ReceivedDateTime, request.Description, request.Sequence, request.Attachments);
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}