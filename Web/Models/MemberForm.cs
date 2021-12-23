using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class MemberForm
{
    [Required]
    [StringLength(50)]
    public string? FirstName { get; set; }


    [Required]
    [StringLength(50)]
    public string? LastName { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [StringLength(50)]
    public string? PhoneNumber { get; set; }

    [Required]
    [StringLength(200)]
    public string? Street { get; set; }

    [Required]
    [StringLength(50)]
    public string? City { get; set; }

    [Required]
    [StringLength(50)]
    public string? Country { get; set; }

    [Required]
    [StringLength(10)]
    public string? ZipCode { get; set; }

    [Required]
    [StringLength(15)]
    public string? HouseNumber { get; set; }

    [Required]
    [Range(0, 500)]
    public double MembershipFee { get; set; }

    public DateTime? MembershipExpiryDate { get; set; }
}