using Microsoft.EntityFrameworkCore;

using Persistence;

using Queries.BankAccounts;

namespace Queries.GetBankAccounts;

public record GetBankAccounts() : IRequest<ICollection<BankAccountDetail>>;

public class GetBankAccountsHandler : IRequestHandler<GetBankAccounts, ICollection<BankAccountDetail>>
{
    private readonly IDbContextFactory<HaSpManContext> _contextFactory;

    public GetBankAccountsHandler(IDbContextFactory<HaSpManContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<ICollection<BankAccountDetail>> Handle(GetBankAccounts request, CancellationToken cancellationToken)
    {
        var context = _contextFactory.CreateDbContext();
        return await context.BankAccounts
            .Select(b => new BankAccountDetail(b.Id, b.Name, b.AccountNumber))
            .ToListAsync(cancellationToken);
    }
}