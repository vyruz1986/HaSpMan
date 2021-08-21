using System;
using Bogus;
using Domain;
using FluentAssertions;
using Types;
using Xunit;

namespace Test.Domain
{
   public class MemberTests
   {
      private Faker _f = new Faker();

      [Fact]
      public void SimplePropertiesTest()
      {
         var testPerson = new
         {
            FirstName = _f.Person.FirstName,
            LastName = _f.Person.LastName,
            Address = new
            {
               City = _f.Person.Address.City,
               ZipCode = _f.Person.Address.ZipCode,
               Street = _f.Person.Address.Street,
               HouseNumber = _f.Random.AlphaNumeric(3),
               Country = _f.Address.Country()
            },
            Email = _f.Person.Email,
            PhoneNumber = _f.Person.Phone
         };
         var testMemberAddress = new Address
         (
            testPerson.Address.Street,
            testPerson.Address.City,
            testPerson.Address.Country,
            testPerson.Address.ZipCode,
            testPerson.Address.HouseNumber
         );
         var testMember = new Member(
            testPerson.FirstName,
            testPerson.LastName,
            testMemberAddress,
            testPerson.Email,
            testPerson.PhoneNumber);

         testMember.Should().BeEquivalentTo(testPerson);
      }

      [Fact]
      public void EmptyFirstNameTest()
      {
         Action act = () => new Member(" ", _f.Person.LastName, GetAddress(), _f.Person.Email, _f.Person.Phone);
         act.Should().Throw<ArgumentException>();
      }

      [Fact]
      public void EmptyLastNameTest()
      {
         Action act = () => new Member(_f.Person.FirstName, " ", GetAddress(), _f.Person.Email, _f.Person.Phone);
         act.Should().Throw<ArgumentException>();
      }

      [Fact]
      public void NoContactDetailsTest()
      {
         Action act = () => new Member(_f.Person.FirstName, _f.Person.LastName, GetAddress());
         Action act2 = () => new Member(_f.Person.FirstName, _f.Person.LastName, GetAddress(), _f.Person.Email);
         Action act3 = () => new Member(_f.Person.FirstName, _f.Person.LastName, GetAddress(), "", _f.Person.Phone);

         act.Should().Throw<ArgumentException>();
         act2.Should().NotThrow();
         act3.Should().NotThrow();

      }

      private Address GetAddress()
      {
         return new Address(
            _f.Person.Address.Street,
            _f.Person.Address.City,
            _f.Address.Country(),
            _f.Person.Address.ZipCode,
            _f.Random.AlphaNumeric(3)
         );
      }
   }
}