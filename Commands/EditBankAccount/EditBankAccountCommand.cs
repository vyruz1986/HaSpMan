namespace Commands.EditBankAccount;

public record EditBankAccountCommand(
       Guid Id,
       string Name,
       string AccountNumber
    ) : IRequest<Guid>;