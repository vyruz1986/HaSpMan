using System.Collections.Generic;

using MediatR;

using Queries.BankAccounts.ViewModels;

namespace Queries
{
    public record GetBankAccounts() : IRequest<ICollection<BankAccount>>;
}