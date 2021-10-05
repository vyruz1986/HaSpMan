using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddCreditWorkshopFee
{
    public class AddCreditWorkshopFeeTransactionHandler : IRequestHandler<AddCreditWorkshopFeeTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddCreditWorkshopFeeTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddCreditWorkshopFeeTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = Domain.Transaction.CreateCreditWorkshopFeeTransaction(request.CounterParty, request.BankAccountId, request.Amount,
                request.ReceivedDateTime, request.Description, request.Sequence, request.Attachments);
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}