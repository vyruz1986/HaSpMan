using Bogus;

using Types;

namespace Domain.Test.FakerGenerators;

public static class FakerGenerator
{
    public static Faker<Domain.FinancialYear> FinancialYear => new Faker<Domain.FinancialYear>()
        .CustomInstantiator(f => new(f.Date.RecentOffset()));

    public static Faker<CreditTransaction> CreditTransaction => new Faker<CreditTransaction>()
        .CustomInstantiator(f => new(
            counterPartyName: f.Company.CompanyName(),
            bankAccountId: f.Random.Guid(),
            amount: f.Finance.Amount(),
            receivedDateTime: f.Date.RecentOffset(),
            description: string.Join(' ', f.Lorem.Words()),
            attachments: TransactionAttachment.GenerateBetween(1, 5),
            memberId: f.Random.Guid(),
            transactionTypeAmounts: TransactionTypeAmount.GenerateBetween(1, 5)));

    public static Faker<TransactionTypeAmount> TransactionTypeAmount => new Faker<TransactionTypeAmount>()
        .CustomInstantiator(f => new(f.Random.Enum<TransactionType>(), f.Finance.Amount()));

    public static Faker<TransactionAttachment> TransactionAttachment => new Faker<TransactionAttachment>()
        .CustomInstantiator(f => new(f.Random.Guid(), f.System.FileName()));
}