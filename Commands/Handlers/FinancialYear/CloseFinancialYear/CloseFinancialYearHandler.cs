using Domain.Interfaces;

namespace Commands.Handlers.FinancialYear.CloseFinancialYear;

public class CloseFinancialYearHandler : IRequestHandler<CloseFinancialYearCommand>
{
    private readonly IFinancialYearRepository _financialYearRepository;

    public CloseFinancialYearHandler(IFinancialYearRepository financialYearRepository)
    {
        _financialYearRepository = financialYearRepository;
    }
    public async Task Handle(CloseFinancialYearCommand request, CancellationToken cancellationToken)
    {
        var financialYear = await _financialYearRepository.GetByIdAsync(request.Id, cancellationToken)
                            ?? throw new ArgumentException($"No financial year found by Id {request.Id}", nameof(request.Id));
        financialYear.Close();

        await _financialYearRepository.SaveChangesAsync(cancellationToken);

    }
}