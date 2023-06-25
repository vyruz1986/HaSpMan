using Domain.Interfaces;

namespace Commands.Handlers.FinancialYearConfiguration;

public class SetFinancialYearConfigurationHandler : IRequestHandler<SetFinancialYearConfigurationCommand>
{
    private readonly IFinancialYearConfigurationRepository _financialYearConfigurationRepository;

    public SetFinancialYearConfigurationHandler(IFinancialYearConfigurationRepository financialYearConfigurationRepository)
    {
        _financialYearConfigurationRepository = financialYearConfigurationRepository;
    }
    public async Task Handle(SetFinancialYearConfigurationCommand request, CancellationToken cancellationToken)
    {
        _financialYearConfigurationRepository.Set(new Domain.FinancialYearConfiguration(request.StartDate));
        await _financialYearConfigurationRepository.SaveChangesAsync(cancellationToken);
    }
}