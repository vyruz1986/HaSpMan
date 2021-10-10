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
        private readonly HaSpManContext _context;
        private readonly IMapper _mapper;

        public GetTransactionByIdHandler(HaSpManContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TransactionDetail> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _context.Transactions.SingleAsync(x => x.Id == request.Id, cancellationToken);

            return _mapper.Map<TransactionDetail>(transaction);
        }
    }
}
