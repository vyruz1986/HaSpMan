using Domain;

using FluentValidation;

namespace Commands.Handlers.Transaction.AddDebitTransaction;

public record AddDebitTransactionCommand(
    string CounterPartyName,
    Guid BankAccountId,
    DateTimeOffset ReceivedDateTime,
    string Description,
    Guid? MemberId,
    ICollection<TransactionTypeAmount> TransactionTypeAmounts,
    ICollection<AttachmentFile> AttachmentFiles) : IRequest<Guid>;

public class AddDebitTransactionCommandValidator : AbstractValidator<AddDebitTransactionCommand>
{
    public AddDebitTransactionCommandValidator()
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
            .LessThanOrEqualTo(x => DateTimeOffset.Now)
            .NotEmpty();

        RuleForEach(x => x.TransactionTypeAmounts)
            .SetValidator(new TransactionTypeValidator());
    }
}
