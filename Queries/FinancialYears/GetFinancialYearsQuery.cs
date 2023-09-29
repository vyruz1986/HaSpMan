using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.FinancialYears;

public record GetFinancialYearsQuery() : IRequest<IReadOnlyList<FinancialYear>>;
public record FinancialYear(Guid Id, DateTimeOffset StartDateTimeOffset, DateTimeOffset EndDateTimeOffset, bool IsClosed);

public class GetFinancialYearsHandler : IRequestHandler<GetFinancialYearsQuery, IReadOnlyList<FinancialYear>>
{
    private readonly IDbContextFactory<HaSpManContext> _contextFactory;

    public GetFinancialYearsHandler(IDbContextFactory<HaSpManContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task<IReadOnlyList<FinancialYear>> Handle(GetFinancialYearsQuery request,
        CancellationToken cancellationToken)
    {
        var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var financialYears = await context.FinancialYears.ToListAsync(cancellationToken);

        return financialYears
            .OrderByDescending(x => x.StartDate)
            .Select(x => new FinancialYear(x.Id, x.StartDate, x.EndDate, x.IsClosed))
            .ToList();
    }
}