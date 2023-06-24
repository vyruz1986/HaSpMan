namespace Domain;

public class FinancialYear
{
#pragma warning disable CS8618
    public FinancialYear(){ } // Make EFCore happy
#pragma warning restore CS8618
    public FinancialYear(DateOnly startDate, ICollection<Transaction> transactions)
    {
        StartDate = startDate;
        EndDate = startDate.AddYears(1).AddDays(-1);
        Transactions = transactions;
    }

    public Guid Id { get; private set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; private set;}

    public bool IsClosed { get; set; }

    public ICollection<Transaction> Transactions { get; set; }

    public void Close()
    {
        if (IsClosed)
        {
            throw new Exception("Financial year is already closed");
        }
        IsClosed = true;
        foreach (var transaction in Transactions)
        {
            transaction.Lock();
        }
    }

    public void AddTransaction(Transaction transaction)
    {
        if (IsClosed)
        {
            throw new Exception("Financial year is closed");
        }
        Transactions.Add(transaction);
    }
}