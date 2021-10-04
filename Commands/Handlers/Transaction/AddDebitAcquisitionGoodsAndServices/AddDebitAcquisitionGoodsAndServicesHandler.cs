using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddDebitAcquisitionGoodsAndServices
{
    public class AddDebitAcquisitionGoodsAndServicesHandler : IRequestHandler<AddDebitAcquisitionGoodsAndServicesCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddDebitAcquisitionGoodsAndServicesHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddDebitAcquisitionGoodsAndServicesCommand request, CancellationToken cancellationToken)
        {
            var transaction = Domain.Transaction.CreateDebitAcquisitionGoodsAndServices(request.CounterParty, request.BankAccount, request.Amount,
                request.ReceivedDateTime, request.Description, request.Sequence, request.Attachments);
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}