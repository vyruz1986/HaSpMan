using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.BankAccounts;

public record GetBankAccountByIdQuery(Guid Id) : IRequest<BankAccountDetail>;

public class GetBankAccountByIdHandler : IRequestHandler<GetBankAccountByIdQuery, BankAccountDetail>
{
    private readonly IMapper _mapper;
    private readonly HaSpManContext _context;

    public GetBankAccountByIdHandler(IMapper mapper, HaSpManContext context)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<BankAccountDetail> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
        => _context.BankAccounts
            .AsNoTracking()
            .Where(b => b.Id == request.Id)
            .ProjectTo<BankAccountDetail>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
}