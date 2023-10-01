
using Commands.Handlers.Transaction.AddAttachments;

using Domain;

using FluentValidation;

namespace Commands.Handlers.Transaction.AddCreditTransaction;

public record AddCreditTransactionCommand(
    string CounterPartyName,
    Guid BankAccountId,
    DateTimeOffset ReceivedDateTime,
    string Description,
    Guid? MemberId,
    ICollection<TransactionTypeAmount> TransactionTypeAmounts,
    ICollection<AttachmentFile> NewAttachmentFiles,
    Guid FinancialYearId) : IRequest<Guid>;

public class AddCreditTransactionCommandValidator : AbstractValidator<AddCreditTransactionCommand>
{
    public AddCreditTransactionCommandValidator()
    {
        RuleFor(x => x.CounterPartyName)
            .NotEmpty()
            .MaximumLength(120);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.BankAccountId)
            .NotEmpty();

        When(x => x.MemberId != null, () =>
        {
            RuleFor(x => x.MemberId)
                .NotEmpty();
        });

        RuleFor(x => x.ReceivedDateTime)
            .NotEmpty()
            .LessThanOrEqualTo(x => DateTimeOffset.Now);

        RuleForEach(x => x.TransactionTypeAmounts)
            .SetValidator(new TransactionTypeValidator());

        RuleForEach(x => x.NewAttachmentFiles)
            .SetValidator(new AttachmentValidator());

        RuleFor(x => x.FinancialYearId)
            .NotEmpty();
    }
}
