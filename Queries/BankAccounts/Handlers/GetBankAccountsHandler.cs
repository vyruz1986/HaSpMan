using MediatR;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Queries.BankAccounts.ViewModels;

namespace Queries.BankAccounts.Handlers;

public class GetBankAccountsHandler : IRequestHandler<GetBankAccounts, ICollection<BankAccount>>
{
    private readonly IDbContextFactory<HaSpManContext> _contextFactory;

    public GetBankAccountsHandler(IDbContextFactory<HaSpManContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<ICollection<BankAccount>> Handle(GetBankAccounts request, CancellationToken cancellationToken)
    {
        var context = _contextFactory.CreateDbContext();
        return await context.BankAccounts
            .Select(b => new BankAccount(b.Id, b.Name, b.AccountNumber))
            .ToListAsync(cancellationToken);
    }
}
