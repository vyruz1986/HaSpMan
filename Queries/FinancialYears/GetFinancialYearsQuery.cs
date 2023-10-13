using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.FinancialYears;

public record GetFinancialYearsQuery() : IRequest<IReadOnlyList<FinancialYear>>;

public class GetFinancialYearsHandler : IRequestHandler<GetFinancialYearsQuery, IReadOnlyList<FinancialYear>>
{
    private readonly HaSpManContext _context;

    public GetFinancialYearsHandler(HaSpManContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyList<FinancialYear>> Handle(GetFinancialYearsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.FinancialYears
            .AsNoTracking()
            .OrderByDescending(x => x.StartDate)
            .Select(x => new FinancialYear(x.Id, x.StartDate, x.EndDate, x.IsClosed, x.Name))
            .ToListAsync(cancellationToken);
    }
}