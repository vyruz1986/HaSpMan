using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Queries.Members.ViewModels;

namespace Queries.Members.Handlers.GetMemberById;

public class GetMemberByIdHandler : IRequestHandler<GetMemberByIdQuery, MemberDetail>
{
    private readonly IMapper _mapper;
    private readonly HaSpManContext _context;

    public GetMemberByIdHandler(IMapper mapper, HaSpManContext context)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<MemberDetail> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        => _context.Members
            .AsNoTracking()
            .Where(m => m.Id == request.Id)
            .ProjectTo<MemberDetail>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
}
