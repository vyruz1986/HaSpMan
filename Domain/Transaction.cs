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
        private Transaction(TransactionType transactionType, string counterPartyName, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId)
        {
            Id = Guid.NewGuid();
            DateFiled = DateTimeOffset.UtcNow;

            TransactionType = transactionType;
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
        }

        public Guid? MemberId { get; private set; }

        public Guid Id { get; private set; }
        public DateTimeOffset ReceivedDateTime { get; private set; }
        public decimal Amount { get; private set; }

        public TransactionType TransactionType { get; private set; }
        public string CounterPartyName { get; private set; }
        public Guid BankAccountId { get; private set; }

        public string Description { get; private set; }

        public DateTimeOffset DateFiled { get; private set; }
        public int Sequence { get; private set; }
        public ICollection<TransactionAttachment> Attachments { get; private set; }

        public static Transaction CreateDebitTransaction(TransactionType transactionType, string counterPartyName, Guid account, decimal amount, DateTimeOffset receivedDateTime,
            string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId)
        {
            return new DebitTransaction(transactionType, counterPartyName, account, amount, receivedDateTime, description, sequence, attachments, memberId);
        }

        public static Transaction CreateCreditTransaction(TransactionType transactionType, string counterPartyName, Guid bankAccount, decimal amount,
            DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId)
        {
            return new CreditTransaction(transactionType, counterPartyName, bankAccount, amount, receivedDateTime, description, sequence,
                attachments, memberId);
        }
        public static IReadOnlyList<Transaction> CreateInternalBankTransaction(TransactionType transactionType, Types.BankAccount from, Types.BankAccount to, decimal amount,
            DateTimeOffset receivedDateTime, string description, int fromSequence, int toSequence, ICollection<TransactionAttachment> attachments)
        {
            return new List<Transaction>()
            {
                new CreditTransaction(transactionType, to.Name, from.BankAccountId, amount, receivedDateTime, description,
                    fromSequence, attachments, null),
                new DebitTransaction(transactionType, from.Name, to.BankAccountId, amount, receivedDateTime, description,
                    toSequence, attachments, null)
            };
        }

        

        #region Specific transactions
        public class DebitTransaction : Transaction
        {
            public DebitTransaction(TransactionType transactionType, string counterPartyName, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId) :
                    base(transactionType, counterPartyName, bankAccountId, amount, receivedDateTime, description, sequence, attachments, memberId)           {
                
            }
        }
        public class CreditTransaction : Transaction
        {
            public CreditTransaction(TransactionType transactionType, string counterPartyName, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId) : 
                    base(transactionType, counterPartyName, bankAccountId, amount, receivedDateTime, description, sequence, attachments, memberId)
            {

            }
        }
        
#endregion
    }
}
