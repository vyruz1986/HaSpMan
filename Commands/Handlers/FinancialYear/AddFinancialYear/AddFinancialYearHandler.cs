using Domain;
using Domain.Interfaces;

using Microsoft.Extensions.Options;

namespace Commands.Handlers.FinancialYear.AddFinancialYear;

public class AddFinancialYearHandler : IRequestHandler<AddFinancialYearCommand, Domain.FinancialYear>
{
    private readonly IFinancialYearRepository _financialYearRepository;
    private readonly FinancialYearConfiguration _financialYearOptions;

    public AddFinancialYearHandler(IFinancialYearRepository financialYearRepository,
        IOptions<FinancialYearConfiguration> financialYearOptions)
    {
        _financialYearRepository = financialYearRepository;
        _financialYearOptions = financialYearOptions.Value;
    }
    public async Task<Domain.FinancialYear> Handle(AddFinancialYearCommand request, CancellationToken cancellationToken)
    {

        var lastFinancialYear = await _financialYearRepository.GetMostRecentAsync(cancellationToken);

        // In case this is the first year we create, assume we want it to be this year.
        // Otherwise, just add a new year
        var year = lastFinancialYear == null ? DateTime.Now.Year : lastFinancialYear.EndDate.Year;

        var startDate = new DateTimeOffset(new DateTime(year, _financialYearOptions.StartDate.Month, _financialYearOptions.StartDate.Day));

        var financialYear = new Domain.FinancialYear(
            startDate,
            startDate.AddYears(1).AddDays(-1),
            new List<Domain.Transaction>());

        _financialYearRepository.Add(financialYear);
        await _financialYearRepository.SaveChangesAsync(cancellationToken);
        return financialYear;
    }
}