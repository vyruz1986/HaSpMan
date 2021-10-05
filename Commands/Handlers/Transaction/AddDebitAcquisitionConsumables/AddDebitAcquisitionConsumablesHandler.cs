using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddDebitAcquisitionConsumables
{
    public class AddDebitAcquisitionConsumablesHandler : IRequestHandler<AddDebitAcquisitionConsumablesCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddDebitAcquisitionConsumablesHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddDebitAcquisitionConsumablesCommand request, CancellationToken cancellationToken)
        {
            var transaction = Domain.Transaction.CreateDebitAcquisitionConsumables(request.CounterParty, request.BankAccountId, request.Amount,
                request.ReceivedDateTime, request.Description, request.Sequence, request.Attachments);
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}