using System;

using FluentValidation;

using MediatR;

using Types;

namespace Commands
{
    public record AddMemberCommand(
      string FirstName,
      string LastName,
      Address Address,
      string Email,
      string PhoneNumber,
      double MembershipFee,
      DateTimeOffset MembershipExpiryDate
    ) : IRequest<Guid>;

    public class AddMemberCommandValidator : AbstractValidator<AddMemberCommand>
    {
        public AddMemberCommandValidator()
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
               .SetValidator(new AddMemberCommandAddressValidator());
        }

        public class AddMemberCommandAddressValidator : AbstractValidator<Address>
        {
            public AddMemberCommandAddressValidator()
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
                   .MaximumLength(5);
            }
        }
    }
}
