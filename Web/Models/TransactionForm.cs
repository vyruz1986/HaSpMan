using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class TransactionForm
    {
        public string? CounterPartyName { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? BankAccountId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? ReceivedDateTime { get; set; }
    }
}
