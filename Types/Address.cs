using System.ComponentModel.DataAnnotations;

namespace Types
{
    public record Address(
       string Street,
       string City,
       string Country,
       string ZipCode,
       string HouseNumber)
    {
        public override string ToString()
        {
            return $"{Street} {HouseNumber}, {ZipCode} {City}, {Country}";
        }
    }
}