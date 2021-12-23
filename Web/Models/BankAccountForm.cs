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
    public string AccountNumber { get; set; }

    [Required]
    public string Name { get; set; }
}