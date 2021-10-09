using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Types;

namespace Web.Models
{
    public class TransactionForm
    {
        public TransactionForm()
        {
            TransactionType = TransactionType.CreditDonation;
        }

        [Required] 
        [MaxLength(50)] 
        public string? CounterPartyName { get; set; }
        
        public TransactionType TransactionType { get; set; }
        
        public Guid? MemberId { get; set; }
        [Required]
        public Guid? BankAccountId { get; set; }
        
        [Required]
        public decimal? Amount { get; set; }
        
        [Required]
        public DateTime? ReceivedDateTime { get; set; }

        public string? Description { get; set; }
    }
}
