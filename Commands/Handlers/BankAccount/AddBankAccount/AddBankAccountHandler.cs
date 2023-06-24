using Commands.Extensions;
using Commands.Services;

using Persistence.Repositories;

namespace Commands.Handlers.BankAccount.AddBankAccount;

public class AddBankAccountHandler : IRequestHandler<AddBankAccountCommand, Guid>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IUserAccessor _userAccessor;

    public AddBankAccountHandler(IBankAccountRepository bankAccountRepository, IUserAccessor userAccessor)
    {
        _bankAccountRepository = bankAccountRepository;
        _userAccessor = userAccessor;
    }

    public async Task<Guid> Handle(AddBankAccountCommand request, CancellationToken ct)
    {
        var performingUser = _userAccessor.User.GetName()!;
        var newAccount = new Domain.BankAccount(request.Name, request.AccountNumber, performingUser);
        _bankAccountRepository.Add(newAccount);
        await _bankAccountRepository.SaveAsync(ct);

        return newAccount.Id;
    }
}
