using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.BankAccounts;

public record GetBankAccountByIdQuery(Guid Id) : IRequest<BankAccountDetail>;

public class GetBankAccountByIdHandler : IRequestHandler<GetBankAccountByIdQuery, BankAccountDetail>
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<HaSpManContext> _contextFactory;

    public GetBankAccountByIdHandler(IMapper mapper, IDbContextFactory<HaSpManContext> contextFactory)
    {
        _mapper = mapper;
        _contextFactory = contextFactory;
    }

    public async Task<BankAccountDetail> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var context = _contextFactory.CreateDbContext();
        var bankAccount = await context.BankAccounts.SingleAsync(b => b.Id == request.Id, cancellationToken: cancellationToken);

        return _mapper.Map<BankAccountDetail>(bankAccount);
    }
}