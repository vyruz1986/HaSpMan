using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Queries.BankAccounts.ViewModels;

namespace Queries.BankAccounts.Handlers
{
    public class GetBankAccountsHandler : IRequestHandler<GetBankAccounts, ICollection<BankAccount>>
    {
        private readonly HaSpManContext _context;

        public GetBankAccountsHandler(HaSpManContext context)
        {
            _context = context;
        }

        public async Task<ICollection<BankAccount>> Handle(GetBankAccounts request, CancellationToken cancellationToken)
        {
            return await _context.BankAccounts
                .Select(b => new BankAccount(b.Id, b.Name, b.AccountNumber))
                .ToListAsync(cancellationToken);
        }
    }
}