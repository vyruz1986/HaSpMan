using System;
using System.Collections.Generic;

using Types;

namespace Domain
{
    public  class Transaction
    {
#pragma warning disable 8618
        private Transaction() { } // Make EFCore happy
#pragma warning restore 8618
        private Transaction(string counterPartyName, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId,
            ICollection<TransactionTypeAmount> transactionTypeAmounts)
        {
            Id = Guid.NewGuid();
            DateFiled = DateTimeOffset.UtcNow;
            
            if (string.IsNullOrWhiteSpace(counterPartyName))
            {
                throw new ArgumentNullException(nameof(counterPartyName), "Cannot be null");
            }
            
            CounterPartyName = counterPartyName;

            BankAccountId = bankAccountId;
            MemberId = memberId;
            
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative", nameof(amount));
            }
            Amount = amount;
            
            ReceivedDateTime = receivedDateTime;
            Description = description;
            Sequence = sequence;
            Attachments = attachments ?? throw new ArgumentNullException(nameof(attachments), "Cannot be null");

            TransactionTypeAmounts = transactionTypeAmounts ??
                                     throw new ArgumentNullException(nameof(transactionTypeAmounts), "Cannot be null");
        }

        public Guid? MemberId { get; private set; }

        public Guid Id { get; private set; }
        public DateTimeOffset ReceivedDateTime { get; private set; }
        public decimal Amount { get; private set; }
        public string CounterPartyName { get; private set; }
        public Guid BankAccountId { get; private set; }

        public string Description { get; private set; }

        public DateTimeOffset DateFiled { get; private set; }
        public int Sequence { get; private set; }
        public ICollection<TransactionAttachment> Attachments { get; private set; }

        public ICollection<TransactionTypeAmount> TransactionTypeAmounts { get; private set; }

        public static Transaction CreateDebitTransaction(string counterPartyName, Guid account, decimal amount, DateTimeOffset receivedDateTime,
            string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId, ICollection<TransactionTypeAmount> transactionTypeAmounts)
        {
            return new DebitTransaction(counterPartyName, account, amount, receivedDateTime, description, sequence, attachments, memberId, transactionTypeAmounts);
        }

        public static Transaction CreateCreditTransaction(string counterPartyName, Guid bankAccount, decimal amount,
            DateTimeOffset receivedDateTime, string description, int sequence, 
            ICollection<TransactionAttachment> attachments, Guid? memberId, ICollection<TransactionTypeAmount> transactionTypeAmounts)
        {
            return new CreditTransaction(counterPartyName, bankAccount, amount, receivedDateTime, description, sequence,
                attachments, memberId, transactionTypeAmounts);
        }
        public static IReadOnlyList<Transaction> CreateInternalBankTransaction(Types.BankAccount from, Types.BankAccount to, decimal amount,
            DateTimeOffset receivedDateTime, string description, int fromSequence, int toSequence, 
            ICollection<TransactionAttachment> attachments, 
            ICollection<TransactionTypeAmount> transactionTypeAmounts)
        {
            return new List<Transaction>()
            {
                new CreditTransaction(to.Name, from.BankAccountId, amount, receivedDateTime, description,
                    fromSequence, attachments, null, transactionTypeAmounts),
                new DebitTransaction(from.Name, to.BankAccountId, amount, receivedDateTime, description,
                    toSequence, attachments, null, transactionTypeAmounts)
            };
        }

        

        #region Specific transactions
        public class DebitTransaction : Transaction
        {
            public DebitTransaction(string counterPartyName, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId, ICollection<TransactionTypeAmount> transactionTypeAmounts) :
                    base(counterPartyName, bankAccountId, amount, receivedDateTime, description, sequence, attachments, memberId, transactionTypeAmounts)           {
                
            }
        }
        public class CreditTransaction : Transaction
        {
            public CreditTransaction(string counterPartyName, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId, ICollection<TransactionTypeAmount> transactionTypeAmounts) : 
                    base(counterPartyName, bankAccountId, amount, receivedDateTime, description, sequence, attachments, memberId, transactionTypeAmounts)
            {

            }
        }
        
#endregion
    }

}
