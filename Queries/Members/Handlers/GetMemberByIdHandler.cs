using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Queries.Members.ViewModels;

namespace Queries.Members.Handlers
{
    public class GetMemberByIdHandler : IRequestHandler<GetMemberByIdQuery, MemberDetail>
    {
        private readonly IMapper _mapper;
        private readonly IDbContextFactory<HaSpManContext> _contextFactory;

        public GetMemberByIdHandler(IMapper mapper, IDbContextFactory<HaSpManContext> contextFactory)
        {
            _mapper = mapper;
            _contextFactory = contextFactory;
        }

        public async Task<MemberDetail> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var context = _contextFactory.CreateDbContext();
            var member = await context.Members.SingleAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);

            return _mapper.Map<MemberDetail>(member);
        }
    }
}