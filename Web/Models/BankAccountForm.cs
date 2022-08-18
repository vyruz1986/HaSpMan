using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class BankAccountForm
{
    public BankAccountForm()
    {
        AccountNumber = string.Empty;
        Name = string.Empty;
    }

    [Required]
    [MaxLength(34)]
    [MinLength(5)]
    [RegularExpression(@"^([A-Z]{2}[ \-]?[0-9]{2})(?=(?:[ \-]?[A-Z0-9]){9,30}$)((?:[ \-]?[A-Z0-9]{3,5}){2,7})([ \-]?[A-Z0-9]{1,3})?$", ErrorMessage = "Please enter a valid IBAN.")]
    public string AccountNumber { get; set; }

    [Required]
    public string Name { get; set; }
}