using Domain.Extensions;

using Types;

namespace Domain;

public class BankAccount
{
    public BankAccount(string name, string accountNumber, string performedBy)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Account name must be specified", nameof(name));

        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Account number must be specified", nameof(accountNumber));

        Id = Guid.NewGuid();
        Name = name;
        AccountNumber = accountNumber;

        AuditEvents = new List<AuditEvent>();
        AuditEvents.AddEvent("Created bankaccount", performedBy);
    }
#pragma warning disable 8618
    private BankAccount() { } // Make EFCore happy
#pragma warning restore 8618

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string AccountNumber { get; private set; }

    public ICollection<AuditEvent> AuditEvents { get; private set; }

    public void ChangeName(string name, string performedBy)
    {
        if (Name == name)
        {
            return;
        }

        Name = name;

        AuditEvents.AddEvent($"Changed name to {Name}", performedBy);
    }

    public void ChangeAccountNumber(string accountNumber, string performedBy)
    {
        if (AccountNumber == accountNumber)
        {
            return;
        }

        AccountNumber = accountNumber;

        AuditEvents.AddEvent($"Changed accountnumber to {AccountNumber}", performedBy);
    }
}
