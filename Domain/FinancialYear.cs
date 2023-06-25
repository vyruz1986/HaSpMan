namespace Domain;

public class FinancialYear
{
#pragma warning disable CS8618
    public FinancialYear(){ } // Make EFCore happy
#pragma warning restore CS8618
    public FinancialYear(DateTimeOffset startDate, DateTimeOffset endDate, ICollection<Transaction> transactions)
    {
        StartDate = startDate;
        EndDate = endDate;
        Transactions = transactions;
    }

    public Guid Id { get; private set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; private set;}

    public bool IsClosed { get; private set; }

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