using Commands.Extensions;
using Commands.Services;

using Domain.Interfaces;

namespace Commands.Handlers.BankAccount.EditBankAccount;

public class EditBankAccountHandler : IRequestHandler<EditBankAccountCommand, Guid>
{
    private readonly IBankAccountRepository _repository;
    private readonly IUserAccessor _userAccessor;

    public EditBankAccountHandler(IBankAccountRepository repository, IUserAccessor userAccessor)
    {
        _repository = repository;
        _userAccessor = userAccessor;
    }

    public async Task<Guid> Handle(EditBankAccountCommand request, CancellationToken cancellationToken)
    {
        var bankAccount = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (bankAccount is null)
        {
            throw new ArgumentException($"No bankaccount found for Id {request.Id}", nameof(request.Id));
        }

        var performingUser = _userAccessor.User.GetName()!;

        bankAccount.ChangeName(request.Name, performingUser);
        bankAccount.ChangeAccountNumber(request.AccountNumber, performingUser);

        await _repository.SaveAsync(cancellationToken);
        return bankAccount.Id;
    }
}