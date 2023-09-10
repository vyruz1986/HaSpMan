using Commands.Handlers.FinancialYear.AddFinancialYear;
using Commands.Handlers.Transaction.AddAttachments;

using Domain;
using Domain.Interfaces;

namespace Commands.Handlers.Transaction.AddDebitTransaction;

public class AddDebitTransactionHandler : IRequestHandler<AddDebitTransactionCommand, Guid>
{
    private readonly IFinancialYearRepository _financialYearRepository;
    private readonly IMediator _mediator;

    public AddDebitTransactionHandler(IFinancialYearRepository financialYearRepository, IMediator mediator)
    {
        _financialYearRepository = financialYearRepository;
        _mediator = mediator;
    }
    public async Task<Guid> Handle(AddDebitTransactionCommand request, CancellationToken cancellationToken)
    {
        var financialYear =
            await _financialYearRepository.GetFinancialYearByDateAsync(request.ReceivedDateTime, cancellationToken)
            ?? await _mediator.Send(new AddFinancialYearCommand(), cancellationToken);

        var totalAmount = request.TransactionTypeAmounts.Sum(x => x.Amount);
        var transaction = new DebitTransaction(request.CounterPartyName, request.BankAccountId, totalAmount,
            request.ReceivedDateTime, request.Description, new List<TransactionAttachment>(),
            request.MemberId,
            request.TransactionTypeAmounts);

        financialYear.AddTransaction(transaction);

        await _financialYearRepository.SaveChangesAsync(cancellationToken);

        await _mediator.Send(new AddAttachmentsCommand(transaction.Id, request.NewAttachmentFiles), cancellationToken);

        return transaction.Id;
    }
}
