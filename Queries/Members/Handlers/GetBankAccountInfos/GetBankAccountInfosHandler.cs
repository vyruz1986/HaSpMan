using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.Members.Handlers.GetBankAccountInfos;

public class GetBankAccountInfosHandler : IRequestHandler<GetBankAccountInfos, IReadOnlyList<BankAccountInfo>>
{
    private readonly HaSpManContext _context;

    public GetBankAccountInfosHandler(HaSpManContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyList<BankAccountInfo>> Handle(GetBankAccountInfos request, CancellationToken cancellationToken)
        => await _context.BankAccounts
            .AsNoTracking()
            .Select(x => new BankAccountInfo(x.Id, x.Name))
            .ToListAsync(cancellationToken);
}
