namespace Domain;

public class FinancialYearConfiguration
{
#pragma warning disable CS8618
    public FinancialYearConfiguration()
    {

    }// Make EFCore happy
#pragma warning restore CS8618

    public FinancialYearConfiguration(DateTimeOffset startDate)
    {
        StartDate = startDate;
    }
    public Guid Id { get; set; }
    public DateTimeOffset StartDate { get; private set; }

    public TimeSpan Duration = TimeSpan.FromDays(365);
}