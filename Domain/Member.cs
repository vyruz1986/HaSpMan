using Domain.Extensions;

using Types;

namespace Domain;
public class Member
{
    public Member(
        string firstName,
        string lastName,
        Address address,
        double membershipFee,
        string performedBy,
        DateTimeOffset? membershipExpiryDate = null,
        string email = "",
        string phoneNumber = ""
        )
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Cannot be null or empty", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Cannot be null or empty", nameof(lastName));

        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Either email or phone number needs to be provided for a new member.");

        if (string.IsNullOrWhiteSpace(address.City))
            throw new ArgumentException("Cannot be null or empty", nameof(address.City));
        if (string.IsNullOrWhiteSpace(address.Country))
            throw new ArgumentException("Cannot be null or empty", nameof(address.Country));
        if (string.IsNullOrWhiteSpace(address.Street))
            throw new ArgumentException("Cannot be null or empty", nameof(address.Street));
        if (string.IsNullOrWhiteSpace(address.ZipCode))
            throw new ArgumentException("Cannot be null or empty", nameof(address.ZipCode));
        if (string.IsNullOrWhiteSpace(address.HouseNumber))
            throw new ArgumentException("Cannot be null or empty", nameof(address.HouseNumber));

        if (membershipFee < 0)
            throw new ArgumentException("Membership fee can not be negative", nameof(membershipFee));

        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        MembershipFee = membershipFee;
        MembershipExpiryDate = membershipExpiryDate;

        AuditEvents = new List<AuditEvent>();
        AuditEvents.AddEvent("Created member", performedBy);
    }

#pragma warning disable 8618
    private Member() { } // Make EFCore happy
#pragma warning restore 8618

    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public DateTimeOffset? MembershipExpiryDate { get; private set; }
    public double MembershipFee { get; private set; }
    public ICollection<AuditEvent> AuditEvents { get; private set; }

    public string Name => $"{FirstName} {LastName}";

    public void ChangeName(string firstName, string lastName, string performedBy)
    {
        if (firstName == FirstName && lastName == LastName)
            return;

        FirstName = firstName;
        LastName = lastName;

        AuditEvents.AddEvent($"Changed name to {Name}", performedBy);
    }

    public void ChangeEmail(string email, string performedBy)
    {
        if (email == Email)
            return;

        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(PhoneNumber))
            throw new ArgumentException("Email can not be cleared while phone number is empty", nameof(email));

        Email = email;

        AuditEvents.AddEvent($"Changed email to {Email}", performedBy);
    }

    public void ChangePhoneNumber(string phoneNumber, string performedBy)
    {
        if (phoneNumber == PhoneNumber)
            return;

        if (string.IsNullOrWhiteSpace(phoneNumber) && string.IsNullOrWhiteSpace(Email))
            throw new ArgumentException("Phone number can not be cleared while email is empty", nameof(phoneNumber));

        PhoneNumber = phoneNumber;

        AuditEvents.AddEvent($"Changed phone number to {PhoneNumber}", performedBy);
    }

    public void ChangeAddress(Address address, string performedBy)
    {
        if (address == Address)
            return;

        Address = address;

        AuditEvents.AddEvent($"Changed address to {Address}", performedBy);
    }

    public void ChangeMembershipExpiryDate(DateTimeOffset? expiryDate, string performedBy)
    {
        if (expiryDate == MembershipExpiryDate)
            return;

        // We calculate memberships per month, so we'll use the last second of the month as an expiry date

        if (expiryDate.HasValue)
        {
            expiryDate = expiryDate.Value.EndOfMonth();
        }

        MembershipExpiryDate = expiryDate;

        AuditEvents.AddEvent($"Changed membership expiry date to {MembershipExpiryDate}", performedBy);
    }

    public void ChangeMembershipFee(double fee, string performedBy)
    {
        if (fee == MembershipFee)
            return;

        MembershipFee = fee;

        AuditEvents.AddEvent($"Changed membership fee to {MembershipFee}", performedBy);
    }

    public bool IsActive()
    {
        return MembershipExpiryDate == null
            || MembershipExpiryDate.Value.Date >= DateTimeOffset.Now.Date;
    }
}
