using System;
using Domain.ValueObjects;

namespace Domain
{
   public class Member : Entity
   {
      public Member(string firstName, string lastName, Address address, string email = "", string phoneNumber = "")
      {
         if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Cannot be null or empty", nameof(firstName));

         if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Cannot be null or empty", nameof(lastName));

         if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Either email or ponenumber needs to be provided for a new member.");

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

         Id = Guid.NewGuid();
         FirstName = firstName;
         LastName = lastName;
         Address = address;
         Email = email;
         PhoneNumber = phoneNumber;
      }

#pragma warning disable 8618
      private Member() { } // Make EFCore happy
#pragma warning restore 8618
      public string FirstName { get; private set; }
      public string LastName { get; private set; }
      public string Email { get; private set; }
      public string PhoneNumber { get; private set; }
      public Address Address { get; private set; }
   }
};