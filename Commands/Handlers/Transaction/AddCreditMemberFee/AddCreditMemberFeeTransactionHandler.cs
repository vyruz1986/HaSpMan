using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Persistence.Repositories;

namespace Commands.Handlers.Transaction.AddCreditMemberFee
{
    public class AddCreditMemberFeeTransactionHandler : IRequestHandler<AddCreditMemberFeeTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddCreditMemberFeeTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(AddCreditMemberFeeTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = Domain.Transaction.CreateCreditMemberFeeTransaction(request.CounterParty, request.BankAccountId, request.Amount,
                request.ReceivedDateTime, request.Description, request.Sequence, request.Attachments);
            _transactionRepository.Add(transaction);
            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}