namespace Queries.Members.ViewModels;

public record MemberSummary(
    Guid Id,
    string Name,
    string Address,
    string Email,
    string PhoneNumber,
    bool IsActive
);
