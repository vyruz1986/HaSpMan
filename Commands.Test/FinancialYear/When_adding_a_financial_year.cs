using Commands.Handlers.FinancialYear.AddFinancialYear;

using Domain;
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
    private readonly Mock<IFinancialYearConfigurationRepository> _financialYearConfigurationMock;

    public When_adding_a_financial_year()
    {
        _financialYearRepositoryMock = new Mock<IFinancialYearRepository>();
        _financialYearConfigurationMock = new Mock<IFinancialYearConfigurationRepository>();
        _haspmanDbContext = new HaSpManContext(new DbContextOptionsBuilder<HaSpManContext>()

            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        SUT = new AddFinancialYearHandler(_financialYearRepositoryMock.Object, _financialYearConfigurationMock.Object,
            _haspmanDbContext);
    }

    public AddFinancialYearHandler SUT { get; set; }




    public class And_no_financial_year_already_exists : When_adding_a_financial_year
    {
        [Fact]
        public async Task It_should_add_a_new_financial_year()
        {
            var startDate = new DateTimeOffset(new DateTime(DateTime.Now.Year, 9, 1));
            var endDate = new DateTimeOffset(new DateTime(DateTime.Now.AddYears(1).Year, 8, 31));
            _financialYearRepositoryMock
                .Setup(x =>
                    x.Add(It.Is<Domain.FinancialYear>(financialYear => financialYear.StartDate == startDate)))
                .Verifiable();
            _financialYearRepositoryMock
                .Setup(x =>
                    x.Add(It.Is<Domain.FinancialYear>(financialYear => financialYear.EndDate == endDate)))
                .Verifiable();

            _financialYearConfigurationMock.Setup(x => x.Get(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FinancialYearConfiguration(new DateTimeOffset(new DateTime(2021, 9, 1))));

            await SUT.Handle(new AddFinancialYearCommand(), CancellationToken.None);

            _financialYearRepositoryMock
                .Verify(x =>
                    x.Add(It.Is<Domain.FinancialYear>(financialYear => financialYear.StartDate == startDate)));
            _financialYearRepositoryMock
                .Verify(x =>
                    x.Add(It.Is<Domain.FinancialYear>(financialYear => financialYear.EndDate == endDate)));

        }


        public class And_a_financial_year_already_exists : When_adding_a_financial_year
        {
            [Fact]
            public async Task It_should_add_a_new_financial_year_following_the_last_year()
            {

                var startDate = new DateTimeOffset(new DateTime(2023, 9, 1));

                var endDate = new DateTimeOffset(new DateTime(2024, 8, 31));
                _financialYearRepositoryMock
                    .Setup(x =>
                        x.GetMostRecentAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new Domain.FinancialYear(new DateTimeOffset(new DateTime(2022, 9, 1)),
                        new DateTimeOffset(new DateTime(2023,8,31)), new List<Domain.Transaction>()));

                _financialYearRepositoryMock
                    .Setup(x =>
                        x.Add(It.Is<Domain.FinancialYear>(financialYear => financialYear.StartDate == startDate)))
                    .Verifiable();
                _financialYearRepositoryMock
                    .Setup(x =>
                        x.Add(It.Is<Domain.FinancialYear>(financialYear => financialYear.EndDate == endDate)))
                    .Verifiable();

                _financialYearConfigurationMock.Setup(x => x.Get(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new FinancialYearConfiguration(new DateTimeOffset(new DateTime(2021, 9, 1))));

                await SUT.Handle(new AddFinancialYearCommand(), CancellationToken.None);

                _financialYearRepositoryMock
                    .Verify(x =>
                        x.Add(It.Is<Domain.FinancialYear>(financialYear => financialYear.StartDate == startDate)));
                _financialYearRepositoryMock
                    .Verify(x =>
                        x.Add(It.Is<Domain.FinancialYear>(financialYear => financialYear.EndDate == endDate)));

            }
        }
    }
}