using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Persistence;

using Queries.Transactions.ViewModels;

namespace Queries.Transactions.Handlers
{
    public class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDetail>
    {
        private readonly IDbContextFactory<HaSpManContext> _context;
        private readonly IMapper _mapper;

        public GetTransactionByIdHandler(IDbContextFactory<HaSpManContext> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TransactionDetail> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var context = _context.CreateDbContext();
            var transaction = await context.Transactions.SingleAsync(x => x.Id == request.Id, cancellationToken);

            return _mapper.Map<TransactionDetail>(transaction);
        }
    }
}
