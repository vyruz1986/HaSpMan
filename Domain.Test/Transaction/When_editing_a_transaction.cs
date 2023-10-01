using Bogus;

using Domain.Test.FakerGenerators;

using FluentAssertions;

using Xunit;

namespace Domain.Test.Transaction;
public class When_editing_a_transaction
{
    private readonly Faker _faker = new();

    [Fact]
    public void It_should_throw_an_exception_when_locked()
    {
        var bankAccountId = Guid.NewGuid();

        var transaction = new CreditTransaction("Random counter party", bankAccountId, 20m, DateTimeOffset.Now,
            "A description", new List<TransactionAttachment>(), null, new List<TransactionTypeAmount>());
        var financialYear = FakerGenerator.FinancialYear.Generate();
        financialYear.AddTransaction(transaction);
        financialYear.Close();

        var exception = Assert.Throws<InvalidOperationException>(() => financialYear.ChangeTransaction(transaction.Id,
            "Changed counter party name", transaction.MemberId,
            transaction.BankAccountId, transaction.ReceivedDateTime, transaction.TransactionTypeAmounts,
            transaction.Description));

        exception.Message.Should().Be("Financial year is already closed");
    }

    [Fact]
    public void It_should_throw_an_exception_when_readonly_and_changing_amount()
    {
        // Given
        var transaction = FakerGenerator.CreditTransaction.Generate();
        transaction.SetAsReadOnly();

        // When
        var action = () =>
        {
            var transactionTypeAmounts = FakerGenerator.TransactionTypeAmount.GenerateBetween(1, 5);
            transaction.ChangeAmount(transactionTypeAmounts.Sum(a => a.Amount), transactionTypeAmounts);
        };

        // Then
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void It_should_throw_an_exception_when_readonly_and_changing_counterparty()
    {
        // Given
        var transaction = FakerGenerator.CreditTransaction.Generate();
        transaction.SetAsReadOnly();

        // When
        var action = () => transaction.ChangeCounterParty(_faker.Person.FullName, null);

        // Then
        action.Should().Throw<InvalidOperationException>();
    }


    [Fact]
    public void It_should_throw_an_exception_when_readonly_and_changing_bankaccountid()
    {
        // Given
        var transaction = FakerGenerator.CreditTransaction.Generate();
        transaction.SetAsReadOnly();

        // When
        var action = () => transaction.ChangeBankAccountId(_faker.Random.Guid());

        // Then
        action.Should().Throw<InvalidOperationException>();
    }


    [Fact]
    public void It_should_throw_an_exception_when_readonly_and_changing_received_datetime()
    {
        // Given
        var transaction = FakerGenerator.CreditTransaction.Generate();
        transaction.SetAsReadOnly();

        // When
        var action = () => transaction.ChangeReceivedDateTime(_faker.Date.RecentOffset());

        // Then
        action.Should().Throw<InvalidOperationException>();
    }


    [Fact]
    public void It_should_throw_an_exception_when_readonly_and_changing_description()
    {
        // Given
        var transaction = FakerGenerator.CreditTransaction.Generate();
        transaction.SetAsReadOnly();

        // When
        var action = () => transaction.ChangeDescription(string.Join(' ', _faker.Lorem.Words()));

        // Then
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void It_should_throw_an_exception_when_readonly_and_adding_attachments()
    {
        // Given
        var transaction = FakerGenerator.CreditTransaction.Generate();
        transaction.SetAsReadOnly();

        // When
        var action = () =>
        {
            var attachments = FakerGenerator.TransactionAttachment.GenerateBetween(1, 5);
            transaction.AddAttachments(attachments);
        };

        // Then
        action.Should().Throw<InvalidOperationException>();
    }
}
