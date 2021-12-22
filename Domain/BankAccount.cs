namespace Domain;

public class BankAccount
{
    public BankAccount(string name, string accountNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Account name must be specified", nameof(name));

        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Account number must be specified", nameof(accountNumber));

        Id = Guid.NewGuid();
        Name = name;
        AccountNumber = accountNumber;
    }
#pragma warning disable 8618
    private BankAccount() { } // Make EFCore happy
#pragma warning restore 8618

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string AccountNumber { get; private set; }
}
