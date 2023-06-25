using Domain.Interfaces;

using Persistence;
using Persistence.Repositories;

namespace Commands.Handlers.FinancialYear.AddFinancialYear;

public class AddFinancialYearHandler : IRequestHandler<AddFinancialYearCommand, Guid>
{
    private readonly IFinancialYearRepository _financialYearRepository;
    private readonly IFinancialYearConfigurationRepository _financialYearConfigurationRepository;

    private readonly HaSpManContext _haSpManContext;

    public AddFinancialYearHandler(IFinancialYearRepository financialYearRepository, 
        IFinancialYearConfigurationRepository financialYearConfigurationRepository,
        HaSpManContext haSpManContext)
    {
        _financialYearRepository = financialYearRepository;
        _financialYearConfigurationRepository = financialYearConfigurationRepository;
        _haSpManContext = haSpManContext;
    }
    public async Task<Guid> Handle(AddFinancialYearCommand request, CancellationToken cancellationToken)
    {
        var configuration = await _financialYearConfigurationRepository.Get(cancellationToken) 
            ?? throw new InvalidOperationException("Financial year configuration is missing");

        
        var lastFinancialYear = await _financialYearRepository.GetMostRecentAsync(cancellationToken);

        // In case this is the first year we create, assume we want it to be this year.
        // Otherwise, just add a new year
        var year = lastFinancialYear == null ? DateTime.Now.Year : lastFinancialYear.EndDate.Year;

        var startDate = new DateTimeOffset(new DateTime(year, configuration.StartDate.Month, configuration.StartDate.Day));

        var financialYear = new Domain.FinancialYear(
            startDate, 
            startDate.AddYears(1).AddDays(-1),
            new List<Domain.Transaction>());
        
        _financialYearRepository.Add(financialYear);
        await _financialYearRepository.SaveChangesAsync(cancellationToken);
        return financialYear.Id;
    }
}