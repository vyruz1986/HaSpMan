using Bogus;

using Domain.Test.FakerGenerators;

using FluentAssertions;

using Xunit;

namespace Domain.Test.FinancialYear;

public class When_closing_a_financial_year
{
    [Fact]
    public void It_should_mark_the_year_as_closed()
    {
        var financialYear = FakerGenerator.FinancialYear.Generate();

        financialYear.Close();

        financialYear.IsClosed.Should().BeTrue();
    }

    [Fact]
    public void All_Linked_Transactions_Should_Become_Read_only()
    {
        // Given
        var financialYear = FakerGenerator.FinancialYear.Generate();
        var creditTransactions = FakerGenerator.CreditTransaction.GenerateBetween(10, 50);

        foreach (var transaction in creditTransactions)
            financialYear.AddTransaction(transaction);

        // When
        financialYear.Close();

        // Then
        financialYear.Transactions.Should().AllSatisfy(t =>
        {
            t.ReadOnly.Should().BeTrue();
        });
    }
}