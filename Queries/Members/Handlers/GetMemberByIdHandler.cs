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
        private readonly HaSpManContext _haSpManContext;

        public GetMemberByIdHandler(IMapper mapper, HaSpManContext haSpManContext)
        {
            _mapper = mapper;
            _haSpManContext = haSpManContext;
        }

        public async Task<MemberDetail> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _haSpManContext.Members.SingleAsync(m => m.Id == request.Id);

            return _mapper.Map<MemberDetail>(member);
        }
    }
}