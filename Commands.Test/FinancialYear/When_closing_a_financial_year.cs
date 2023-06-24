using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Commands.Handlers.FinancialYear.AddFinancialYear;
using Commands.Handlers.FinancialYear.CloseFinancialYear;

using Domain;

using FluentAssertions;

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

public class When_closing_a_financial_year
{
    private readonly Mock<IFinancialYearRepository> _financialYearRepositoryMock;

    public When_closing_a_financial_year()
    {
        _financialYearRepositoryMock = new Mock<IFinancialYearRepository>();
        SUT = new CloseFinancialYearHandler(_financialYearRepositoryMock.Object);
    }

    public CloseFinancialYearHandler SUT { get; set; }

    [Fact]
    public async Task It_should_mark_the_year_as_closed()
    {
        var financialYear = new Domain.FinancialYear(DateOnly.FromDateTime(DateTime.Now),
            new List<Transaction>());
        _financialYearRepositoryMock
            .Setup(x => x.GetById(financialYear.Id, CancellationToken.None))
            .ReturnsAsync(financialYear);
;       await SUT.Handle(new CloseFinancialYearCommand(financialYear.Id), CancellationToken.None);
        
        financialYear.IsClosed.Should().BeTrue();
    }

    [Fact]
    public async Task It_should_mark_all_related_transactions_as_as_locked()
    {
        var financialYear = new Domain.FinancialYear(DateOnly.FromDateTime(DateTime.Now),
            new List<Transaction>()
            {
                new CreditTransaction("Random counter party", Guid.NewGuid(), 20,DateTimeOffset.Now, "A description", new List<TransactionAttachment>(), null, new List<TransactionTypeAmount>())
            });
        _financialYearRepositoryMock
            .Setup(x => x.GetById(financialYear.Id, CancellationToken.None))
            .ReturnsAsync(financialYear);
        ;
        await SUT.Handle(new CloseFinancialYearCommand(financialYear.Id), CancellationToken.None);

        financialYear.IsClosed.Should().BeTrue();
        financialYear.Transactions.Should().OnlyContain(x => x.Locked);
    }

    [Fact]
    public async Task It_should_throw_an_exception_when_no_year_is_found()
    {
        var newGuid = Guid.NewGuid();
        var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
       {
           await SUT.Handle(new CloseFinancialYearCommand(newGuid), CancellationToken.None);
       });
        Assert.Equal($"No financial year found by Id {newGuid} (Parameter 'Id')", exception.Message);
    }
}