using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.Members.Handlers.AutocompleteMember
{
    public class AutocompleteCounterpartyHandler : IRequestHandler<AutocompleteCounterpartyQuery, AutocompleteCounterpartyResponse>
    {
        private readonly IDbContextFactory<HaSpManContext> _contextFactory;

        public AutocompleteCounterpartyHandler(IDbContextFactory<HaSpManContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<AutocompleteCounterpartyResponse> Handle(AutocompleteCounterpartyQuery request, CancellationToken cancellationToken)
        {
            var context = _contextFactory.CreateDbContext();

            if (request.IsMemberSearch)
            {
                var members = await context.Members
                    .Where(x =>
                        x.FirstName.ToLower().Contains(request.SearchString.ToLower()) ||
                        x.LastName.ToLower().Contains(request.SearchString.ToLower()))
                    .Select(x => new AutocompleteCounterparty(x.Name, x.Id))
                    .ToListAsync(cancellationToken: cancellationToken);
                return new AutocompleteCounterpartyResponse(members);
            }
            

            var counterParties = await context.Transactions
                .Where(x =>
                    x.MemberId == null &&
                    x.CounterPartyName.ToLower().Contains(request.SearchString.ToLower()))
                .Select(x => new AutocompleteCounterparty(x.CounterPartyName, null))
                .ToListAsync(cancellationToken);

            return new AutocompleteCounterpartyResponse(counterParties);
        }
    }
}