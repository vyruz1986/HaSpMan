namespace Queries.BankAccounts;


public record BankAccountDetail(Guid Id, string Name, string AccountNumber);
public record BankAccountDetailWithTotal(Guid Id, string Name, string AccountNumber, decimal TotalAmount, long NumberOfTransactions);
