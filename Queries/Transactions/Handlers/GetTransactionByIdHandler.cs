using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Queries.Transactions.ViewModels;

namespace Queries.Transactions.Handlers;

public class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDetail>
{
    private readonly IDbContextFactory<HaSpManContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetTransactionByIdHandler(IDbContextFactory<HaSpManContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }
    public async Task<TransactionDetail> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var context = _contextFactory.CreateDbContext();
        var transaction = await context.Transactions.SingleAsync(x => x.Id == request.Id, cancellationToken);

        return _mapper.Map<TransactionDetail>(transaction);
    }
}
