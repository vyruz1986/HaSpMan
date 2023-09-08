using FluentAssertions;

using Xunit;

namespace Domain.Test.FinancialYear;

public class When_closing_a_financial_year
{
    [Fact]
    public void It_should_mark_the_year_as_closed()
    {
        var startDate = DateTimeOffset.Now;
        var financialYear = new Domain.FinancialYear(startDate, startDate.AddYears(1).AddDays(-1), new List<Domain.Transaction>());
        
;       financialYear.Close();
        
        financialYear.IsClosed.Should().BeTrue();
    }
}