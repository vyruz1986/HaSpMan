using FluentAssertions;

using Xunit;

namespace Domain.Test.Transaction;
public class When_editing_a_transaction
{
    
    [Fact]
    public void It_should_throw_an_exception_when_locked()
    {
        var bankAccountId = Guid.NewGuid();

        var transaction = new CreditTransaction("Random counter party", bankAccountId, 20m, DateTimeOffset.Now,
            "A description", new List<TransactionAttachment>(), null, new List<TransactionTypeAmount>());
        var financialYear = new Domain.FinancialYear(new DateTimeOffset(new DateTime(2022,9,1)), new DateTimeOffset(new DateTime(2023,8,31)), new List<Domain.Transaction>()
        {
            transaction
        });
        financialYear.Close();

        var exception = Assert.Throws<InvalidOperationException>(() => financialYear.ChangeTransaction(transaction.Id,
            "Changed counter party name", transaction.MemberId,
            transaction.BankAccountId, transaction.ReceivedDateTime, transaction.TransactionTypeAmounts,
            transaction.Description));

        exception.Message.Should().Be("Financial year is already closed");
    }
}
