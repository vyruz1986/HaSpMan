using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Persistence.Repositories;

using Types;

namespace Commands.Handlers.Transaction.EditTransaction
{
    public record EditTransactionCommand(
        Guid Id, 
        string CounterPartyName,
        Guid? MemberId,
        decimal Amount,
        Guid BankAccountId,
        DateTimeOffset ReceivedDateTime,
        string Description,
        ICollection<TransactionTypeAmount> TransactionTypeAmounts) : IRequest<Guid>;

    public class EditTransactionHandler : IRequestHandler<EditTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public EditTransactionHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(EditTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(request.Id);
            if (transaction == null)
            {
                throw new ArgumentException($"No transaction found for Id {request.Id}", nameof(request.Id));
            }

            transaction.ChangeCounterParty(request.CounterPartyName, request.MemberId);
            transaction.ChangeBankAccountId(request.BankAccountId);
            transaction.ChangeReceivedDateTime(request.ReceivedDateTime);
            transaction.ChangeAmount(request.Amount, request.TransactionTypeAmounts);
            transaction.ChangeDescription(request.Description);
            

            await _transactionRepository.SaveAsync(cancellationToken);
            return transaction.Id;
        }
    }
}
