using MediatR;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.Members.Handlers.GetBankAccountInfos;

public class GetBankAccountInfosHandler : IRequestHandler<GetBankAccountInfos, IReadOnlyList<BankAccountInfo>>
{
    private readonly IDbContextFactory<HaSpManContext> _contextFactory;

    public GetBankAccountInfosHandler(IDbContextFactory<HaSpManContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IReadOnlyList<BankAccountInfo>> Handle(GetBankAccountInfos request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory.CreateDbContext();
        return await context.BankAccounts
            .AsNoTracking()
            .Select(x => new BankAccountInfo(x.Id, x.Name))
            .ToListAsync(cancellationToken);

    }
}
