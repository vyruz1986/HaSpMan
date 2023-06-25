using Commands.Handlers.FinancialYear.AddFinancialYear;

using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

using Moq;

using Persistence;
using Persistence.Repositories;

using Xunit;

namespace Commands.Test.FinancialYear;

public class When_adding_a_financial_year
{
    private readonly Mock<IFinancialYearRepository> _financialYearRepositoryMock;
    private readonly HaSpManContext _haspmanDbContext;

    public When_adding_a_financial_year()
    {
        _financialYearRepositoryMock = new Mock<IFinancialYearRepository>();
        _haspmanDbContext = new HaSpManContext(new DbContextOptionsBuilder<HaSpManContext>()
            
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        SUT = new AddFinancialYearHandler(_financialYearRepositoryMock.Object, _haspmanDbContext);
    }

    public AddFinancialYearHandler SUT { get; set; }

    [Fact]
    public async Task It_should_add_a_new_financial_year()
    {

        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var endDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1).AddDays(-1));
        _financialYearRepositoryMock.Setup(x => x.Add(It.Is<Domain.FinancialYear>(year => year.StartDate == startDate))).Verifiable();

        await SUT.Handle(new AddFinancialYearCommand(startDate), CancellationToken.None);

        _financialYearRepositoryMock
            .Verify(x => 
                x.Add(It.Is<Domain.FinancialYear>(year => year.StartDate == startDate)));
        _financialYearRepositoryMock
            .Verify(x => 
                x.Add(It.Is<Domain.FinancialYear>(year => year.EndDate == endDate)));
    }
}