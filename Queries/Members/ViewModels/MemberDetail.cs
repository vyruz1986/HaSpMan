using System;

namespace Queries.Members.ViewModels
{
    public record MemberDetail(
        Guid Id,
        string FirstName,
        string LastName,
        Types.Address Address,
        string Email,
        string PhoneNumber,
        double MembershipFee,
        DateTimeOffset MembershipExpiryDate
    );
}