using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Queries.Transactions.ViewModels;

namespace Queries.Transactions.Handlers;

public class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDetail>
{
    private readonly HaSpManContext _dbContext;
    private readonly IMapper _mapper;

    public GetTransactionByIdHandler(HaSpManContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public Task<TransactionDetail> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        => _dbContext.FinancialYears
            .AsNoTracking()
            .SelectMany(x => x.Transactions)
            .Where(x => x.Id == request.Id)
            .ProjectTo<TransactionDetail>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
}
