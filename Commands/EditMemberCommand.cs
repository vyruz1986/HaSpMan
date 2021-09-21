using System;

using FluentValidation;

using MediatR;

using Types;

namespace Commands
{
    public record EditMemberCommand(
       Guid Id,
       string FirstName,
       string LastName,
       Address Address,
       string Email,
       string PhoneNumber,
       double MembershipFee,
       DateTimeOffset? MembershipExpiryDate
    ) : IRequest;

    public class EditMemberCommandValidator : AbstractValidator<EditMemberCommand>
    {
        public EditMemberCommandValidator()
        {
            RuleFor(x => x.FirstName)
               .NotEmpty()
               .MaximumLength(50);

            RuleFor(x => x.LastName)
               .NotEmpty()
               .MaximumLength(50);

            RuleFor(x => x.Email)
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(x => x.PhoneNumber)
               .NotEmpty()
               .MaximumLength(50);

            RuleFor(x => x.Address)
               .SetValidator(new EditMemberCommandAddressValidator());
        }

        public class EditMemberCommandAddressValidator : AbstractValidator<Address>
        {
            public EditMemberCommandAddressValidator()
            {
                RuleFor(x => x.Street)
                   .NotEmpty()
                   .MaximumLength(200);

                RuleFor(x => x.City)
                   .NotEmpty()
                   .MaximumLength(50);

                RuleFor(x => x.Country)
                   .NotEmpty()
                   .MaximumLength(50);

                RuleFor(x => x.ZipCode)
                   .NotEmpty()
                   .MaximumLength(10);

                RuleFor(x => x.HouseNumber)
                   .NotEmpty()
                   .MaximumLength(15);
            }
        }
    }
}
