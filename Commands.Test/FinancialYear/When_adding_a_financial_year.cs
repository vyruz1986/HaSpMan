using Commands.Handlers.FinancialYear.AddFinancialYear;

using Domain;
using Domain.Interfaces;

using Microsoft.Extensions.Options;

using Moq;

using Xunit;

namespace Commands.Test.FinancialYear;

public class When_adding_a_financial_year
{
    private readonly Mock<IFinancialYearRepository> _financialYearRepositoryMock;

    public When_adding_a_financial_year()
    {
        _financialYearRepositoryMock = new Mock<IFinancialYearRepository>();

        var financialYearConfigurationOptions = Options.Create(new FinancialYearConfiguration()
        {
            StartDate = new DateTimeOffset(new DateTime(2022, 9, 1)),
            EndDate = new DateTimeOffset(new DateTime(2023, 8, 31))
        });

        SUT = new AddFinancialYearHandler(_financialYearRepositoryMock.Object, financialYearConfigurationOptions);
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
                        new DateTimeOffset(new DateTime(2023, 8, 31)), new List<Domain.Transaction>()));

                _financialYearRepositoryMock
                    .Setup(x =>
                        x.Add(It.Is<Domain.FinancialYear>(financialYear => financialYear.StartDate == startDate)))
                    .Verifiable();
                _financialYearRepositoryMock
                    .Setup(x =>
                        x.Add(It.Is<Domain.FinancialYear>(financialYear => financialYear.EndDate == endDate)))
                    .Verifiable();

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