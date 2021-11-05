using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Queries.Members.Handlers;
using Queries.Members.Handlers.SearchMembers;

using Types;

namespace Web.Models
{
    public class TransactionForm
    {
        public TransactionForm()
        {
            TransactionTypeAmounts = new List<TransactionTypeAmountForm>()
            {
                new(TransactionType.CreditWorkshopFee, 0)
            };
        }
        
        [NotMapped]
        public AutocompleteMember Member { get; set; }

        [Required]
        public Guid? BankAccountId { get; set; }
        
        [Required]
        public DateTime? ReceivedDateTime { get; set; }

        public string? Description { get; set; }

        public ICollection<TransactionTypeAmountForm> TransactionTypeAmounts { get; set; }
    }

    public class TransactionTypeAmountForm
    {
        public TransactionTypeAmountForm(TransactionType transactionType, decimal amount)
        {
            TransactionType = transactionType;
            Amount = amount;
        }
        [Required]
        public TransactionType TransactionType { get; set; }


        [Required]
        [Range(0, 10000000)]
        public decimal Amount { get; set; }
    }
}
