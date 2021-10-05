﻿using System;
using System.Collections.Generic;

using Types;

namespace Domain
{
    public abstract class Transaction       
    {
        private Transaction(CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments)        {
            Id = Guid.NewGuid();
            DateFiled = DateTimeOffset.UtcNow;

            CounterParty = counterParty ?? throw new ArgumentNullException(nameof(counterParty), "Cannot be null");

            BankAccountId = bankAccountId;
            
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

        public static Transaction CreateDebitFixedCosts(CounterParty counterParty, Guid account, decimal amount, DateTimeOffset receivedDateTime,
            string description, int sequence, ICollection<TransactionAttachment> attachments)
        {
            return new DebitFixedCostsTransaction(counterParty, account, amount, receivedDateTime, description, sequence, attachments);
        }


        public static Transaction CreateDebitAcquisitionGoodsAndServices(CounterParty counterParty, Guid bankAccount, decimal amount, DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments)
        {
            return new DebitAcquisitionGoodsAndServicesTransaction(counterParty, bankAccount, amount, receivedDateTime, description, sequence, attachments);
        }

        public static Transaction CreateDebitAcquisitionConsumables(CounterParty counterParty, Guid bankAccount, decimal amount, DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments)
        {
            return new DebitAcquisitionConsumablesTransaction(counterParty, bankAccount, amount, receivedDateTime, description, sequence,
                attachments);
        }

        public static Transaction CreateDebitBankCostsTransaction(CounterParty counterParty, Guid bankAccount, decimal amount,
            DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments)
        {
            return new DebitBankCostsTransaction(counterParty, bankAccount, amount, receivedDateTime, description, sequence,
                attachments);
        }

        public static Transaction CreateCreditMemberFeeTransaction(CounterParty counterParty, Guid bankAccountId, decimal amount,
            DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments)
        {
            return new CreditMemberFeeTransaction(counterParty, bankAccountId, amount, receivedDateTime, description, sequence,
                attachments);
        }

        public static Transaction CreateCreditWorkshopFeeTransaction(CounterParty counterParty, Guid bankAccount, decimal amount,
            DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments)
        {
            return new CreditWorkshopFeeTransaction(counterParty, bankAccount, amount, receivedDateTime, description, sequence,
                attachments);
        }

        public static Transaction CreateCreditDonationTransaction(CounterParty counterParty, Guid bankAccount, decimal amount,
            DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments)
        {
            return new CreditDonationTransaction(counterParty, bankAccount, amount, receivedDateTime, description, sequence,
                attachments);
        }

        public static IReadOnlyList<Transaction> CreateInternalBankTransaction(Types.BankAccount from, Types.BankAccount to, decimal amount,
            DateTimeOffset receivedDateTime, string description, int fromSequence, int toSequence, ICollection<TransactionAttachment> attachments)
        {
            return new List<Transaction>()
            {
                new InternalBankTransaction(new CounterParty(to.Name), from.BankAccountId, amount, receivedDateTime, description,
                    fromSequence, attachments),
                new InternalBankTransaction(new CounterParty(from.Name), to.BankAccountId, amount, receivedDateTime, description,
                    toSequence, attachments)
            };
        }

        public Guid Id { get;  }
        public DateTimeOffset ReceivedDateTime { get; }
        public decimal Amount { get; }

        public CounterParty CounterParty { get; }
        public Guid BankAccountId { get; }

        public string Description { get; }

        public DateTimeOffset DateFiled { get;}
        public int Sequence { get; }
        public ICollection<TransactionAttachment> Attachments { get; }

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
        public class DebitFixedCostsTransaction : Transaction
        {
            public DebitFixedCostsTransaction(CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) :base(counterParty, bankAccountId, amount, receivedDateTime, description, sequence, attachments)           {
                
            }
        }

        public class DebitAcquisitionGoodsAndServicesTransaction : Transaction
        {

            public DebitAcquisitionGoodsAndServicesTransaction(CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(counterParty, bankAccountId, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }

        public class DebitAcquisitionConsumablesTransaction : Transaction
        {
            public DebitAcquisitionConsumablesTransaction(CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(counterParty, bankAccountId, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }

        public class DebitBankCostsTransaction : Transaction
        {
            public DebitBankCostsTransaction(CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(counterParty, bankAccountId, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }

        public class CreditMemberFeeTransaction : Transaction
        {
            public CreditMemberFeeTransaction(CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(counterParty, bankAccountId, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }

        public class CreditWorkshopFeeTransaction : Transaction
        {
            public CreditWorkshopFeeTransaction(CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(counterParty, bankAccountId, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }

        public class CreditDonationTransaction : Transaction
        {
            public CreditDonationTransaction(CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(counterParty, bankAccountId, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }

        public class InternalBankTransaction : Transaction
        {
            public InternalBankTransaction(CounterParty counterParty, Guid bankAccountId, decimal amount, DateTimeOffset receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments) 
                : base(counterParty, bankAccountId, amount, receivedDateTime, description, sequence, attachments)
            {
            }
        }
#endregion
    }

}
