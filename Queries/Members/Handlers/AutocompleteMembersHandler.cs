using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.Members.Handlers
{
    public class AutocompleteMembersHandler : IRequestHandler<AutocompleteMembersQuery, AutocompleteMemberResponse>
    {
        private readonly HaSpManContext _context;

        public AutocompleteMembersHandler(HaSpManContext context)
        {
            _context = context;
        }
        public async Task<AutocompleteMemberResponse> Handle(AutocompleteMembersQuery request, CancellationToken cancellationToken)
        {
            var members = await _context.Members
                .Where(x => 
                    x.FirstName.Contains(request.SearchString) || 
                    x.LastName.Contains(request.SearchString))
                .Select(x => new AutocompleteMember(x.Name, x.Id))          
                .ToListAsync(cancellationToken: cancellationToken);

            return new AutocompleteMemberResponse(members);
        }
    }
}