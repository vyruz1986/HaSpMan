using System;

using MediatR;

using Queries.Transactions.ViewModels;

namespace Queries.Transactions.Handlers
{
    public record GetTransactionByIdQuery(
        Guid Id) : IRequest<TransactionDetail>;
}