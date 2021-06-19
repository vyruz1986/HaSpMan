using System.Collections.Generic;

namespace HaSpMan.Domain.ValueObjects
{
   public class Address : ValueObject
   {
      public Address(string street, string city, string country, string zipCode, string houseNumber)
      {
         Street = street;
         City = city;
         Country = country;
         ZipCode = zipCode;
         HouseNumber = houseNumber;
      }

      public string Street { get; private set; }
      public string City { get; private set; }
      public string Country { get; private set; }
      public string ZipCode { get; private set; }
      public string HouseNumber { get; private set; }
      protected override IEnumerable<object> GetEqualityComponents()
      {
         yield return Street;
         yield return City;
         yield return Country;
         yield return ZipCode;
         yield return HouseNumber;
      }
   }
}