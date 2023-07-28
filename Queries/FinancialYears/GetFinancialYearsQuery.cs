using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.FinancialYears;

public record GetFinancialYearsQuery() : IRequest<IReadOnlyList<FinancialYear>>;
public record FinancialYear(Guid Id, DateTimeOffset StartDateTimeOffset, DateTimeOffset EndDateTimeOffset, bool IsCloded);


public class GetFinancialYearsHandler : IRequestHandler<GetFinancialYearsQuery, IReadOnlyList<FinancialYear>>
{
    private readonly IDbContextFactory<HaSpManContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetFinancialYearsHandler(IDbContextFactory<HaSpManContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
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