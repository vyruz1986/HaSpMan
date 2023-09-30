
using Commands.Handlers.FinancialYear.AddFinancialYear;
using Commands.Handlers.Transaction.AddAttachments;

using Domain;
using Domain.Interfaces;

namespace Commands.Handlers.Transaction.AddCreditTransaction;

public class AddCreditTransactionHandler : IRequestHandler<AddCreditTransactionCommand, Guid>
{
    private readonly IFinancialYearRepository _financialYearRepository;
    private readonly IMediator _mediator;

    public AddCreditTransactionHandler(IFinancialYearRepository financialYearRepository, IMediator mediator)
    {
        _financialYearRepository = financialYearRepository;
        _mediator = mediator;
    }
    public async Task<Guid> Handle(AddCreditTransactionCommand request, CancellationToken cancellationToken)
    {

        var financialYear =
            await _financialYearRepository.GetFinancialYearByTransactionId(request.FinancialYearId, cancellationToken)
            ?? await _mediator.Send(new AddFinancialYearCommand(), cancellationToken);

        var totalAmount = request.TransactionTypeAmounts.Sum(x => x.Amount);
        var transaction = new CreditTransaction(request.CounterPartyName, request.BankAccountId, totalAmount,
            request.ReceivedDateTime, request.Description, new List<TransactionAttachment>(), request.MemberId,
            request.TransactionTypeAmounts);

        financialYear.AddTransaction(transaction);
        ;
        await _financialYearRepository.SaveChangesAsync(cancellationToken);

        await _mediator.Send(new AddAttachmentsCommand(transaction.Id, request.NewAttachmentFiles), cancellationToken);

        return transaction.Id;
    }
}
