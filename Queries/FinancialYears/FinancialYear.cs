namespace Queries.FinancialYears;

public record FinancialYear(
    Guid Id,
    DateTimeOffset StartDateTimeOffset,
    DateTimeOffset EndDateTimeOffset,
    bool IsClosed,
    string Name);
