using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.Members.Handlers.AutocompleteMember
{
    public class AutocompleteMembersHandler : IRequestHandler<AutocompleteMembersQuery, AutocompleteMemberResponse>
    {
        private readonly IDbContextFactory<HaSpManContext> _contextFactory;

        public AutocompleteMembersHandler(IDbContextFactory<HaSpManContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<AutocompleteMemberResponse> Handle(AutocompleteMembersQuery request, CancellationToken cancellationToken)
        {
            var context = _contextFactory.CreateDbContext();
            var members = await context.Members
                .Where(x => 
                    x.FirstName.ToLower().Contains(request.SearchString.ToLower()) || 
                    x.LastName.ToLower().Contains(request.SearchString.ToLower()))
                .Select(x => new SearchMembers.AutocompleteMember(x.Name, x.Id))          
                .ToListAsync(cancellationToken: cancellationToken);

            var counterParties = await context.Transactions
                .Where(x =>
                    !x.IsTransactionForMember &&
                    x.CounterPartyName.ToLower().Contains(request.SearchString.ToLower()))
                .Select(x => new SearchMembers.AutocompleteMember(x.CounterPartyName, null))
                .ToListAsync(cancellationToken);

            var items = members.Union(counterParties).ToList();

            return new AutocompleteMemberResponse(items);
        }
    }
}