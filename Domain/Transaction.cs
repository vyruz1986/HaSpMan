using System;
using System.Collections.Generic;

using Types;

namespace Domain
{
    public abstract class Transaction       
    {
        private Transaction(TransactionType transactionType, CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId)
        {
            Id = Guid.NewGuid();
            DateFiled = DateTimeOffset.UtcNow;

            TransactionType = transactionType;
            CounterParty = counterParty ?? throw new ArgumentNullException(nameof(counterParty), "Cannot be null");

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
        public CounterParty CounterParty { get; private set; }
        public Guid BankAccountId { get; private set; }

        public string Description { get; private set; }

        public DateTimeOffset DateFiled { get; private set; }
        public int Sequence { get; private set; }
        public ICollection<TransactionAttachment> Attachments { get; private set; }

        public static Transaction CreateDebitTransaction(TransactionType transactionType, CounterParty counterParty, Guid account, decimal amount, DateTimeOffset receivedDateTime,
            string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId)
        {
            return new DebitTransaction(transactionType, counterParty, account, amount, receivedDateTime, description, sequence, attachments, memberId);
        }

        public static Transaction CreateCreditTransaction(TransactionType transactionType, CounterParty counterParty, Guid bankAccount, decimal amount,
            DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId)
        {
            return new CreditTransaction(transactionType, counterParty, bankAccount, amount, receivedDateTime, description, sequence,
                attachments, memberId);
        }
        public static IReadOnlyList<Transaction> CreateInternalBankTransaction(TransactionType transactionType, Types.BankAccount from, Types.BankAccount to, decimal amount,
            DateTimeOffset receivedDateTime, string description, int fromSequence, int toSequence, ICollection<TransactionAttachment> attachments)
        {
            return new List<Transaction>()
            {
                new CreditTransaction(transactionType, new CounterParty(to.Name), from.BankAccountId, amount, receivedDateTime, description,
                    fromSequence, attachments, null),
                new DebitTransaction(transactionType, new CounterParty(from.Name), to.BankAccountId, amount, receivedDateTime, description,
                    toSequence, attachments, null)
            };
        }

        public class TransactionAttachment
        {
            public TransactionAttachment(string blobUri, string name)
            {
                if (string.IsNullOrWhiteSpace(blobUri))
                {
                    throw new ArgumentException("Cannot be null or empty", nameof(blobUri));
                }
                BlobURI = blobUri;

                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Cannot be null or empty", nameof(name));
                }
                Name = name;
            }

            public string BlobURI { get; }
            public string Name { get; }

        }

        #region Specific transactions
        public class DebitTransaction : Transaction
        {
            public DebitTransaction(TransactionType transactionType, CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId) :
                    base(transactionType, counterParty, bankAccountId, amount, receivedDateTime, description, sequence, attachments, memberId)           {
                
            }
        }
        public class CreditTransaction : Transaction
        {
            public CreditTransaction(TransactionType transactionType, CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments, Guid? memberId) : 
                    base(transactionType, counterParty, bankAccountId, amount, receivedDateTime, description, sequence, attachments, memberId)
            {

            }
        }
        
#endregion
    }

}
