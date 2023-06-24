using Microsoft.EntityFrameworkCore;

using Persistence;
using Persistence.Repositories;

namespace Commands.Handlers.FinancialYear.AddFinancialYear;

public class AddFinancialYearHandler : IRequestHandler<AddFinancialYearCommand, Guid>
{
    private readonly IFinancialYearRepository _financialYearRepository;
    private readonly HaSpManContext _haSpManContext;

    public AddFinancialYearHandler(IFinancialYearRepository financialYearRepository, HaSpManContext haSpManContext)
    {
        _financialYearRepository = financialYearRepository;
        _haSpManContext = haSpManContext;
    }
    public async Task<Guid> Handle(AddFinancialYearCommand request, CancellationToken cancellationToken)
    {
        await EnsureStartDateNotInExistingFinancialYear(request, cancellationToken);
        var financialYear = new Domain.FinancialYear(request.StartDate, new List<Domain.Transaction>());
        _financialYearRepository.Add(financialYear);
        await _financialYearRepository.SaveChangesAsync(cancellationToken);
        return financialYear.Id;
    }

    private async Task EnsureStartDateNotInExistingFinancialYear(AddFinancialYearCommand request, CancellationToken cancellationToken)
    {
        var financialYearAlreadyInExistingYear = await _haSpManContext.FinancialYears
            .AsNoTracking()
            .AnyAsync(x => x.EndDate <= request.StartDate, cancellationToken);

        if (financialYearAlreadyInExistingYear)
            throw new InvalidOperationException("Start date of year is in already existing year");
    }
}