using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    

    public abstract class Transaction       
    {
        private Transaction(Party from, Party to, decimal amount, DateTime receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments)        {
            Id = Guid.NewGuid();
            DateFiled = DateTime.UtcNow;

            To = to ?? throw new ArgumentNullException(nameof(to), "Cannot be null");
            From = from ?? throw new ArgumentNullException(nameof(from), "Cannot be null");

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

        public Transaction CreateDebitFixedCosts(Party from, Party to, decimal amount, DateTime receivedDateTime,
            string description, int sequence, ICollection<TransactionAttachment> attachments)
        {
            return new DebitFixedCostsTransaction(from, to, amount, receivedDateTime, description, sequence, attachments);
        }


        public Transaction CreateDebitAcquisitionGoodsAndServices(Party @from, Party to, decimal amount, DateTime receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments)
        {
            return new DebitAcquisitionGoodsAndServicesTransaction(@from, to, amount, receivedDateTime, description, sequence, attachments);
        }

        public Transaction CreateDebitAcquisitionGoodsAndServices(Party @from, Party to, decimal amount, DateTime receivedDateTime, string description, int sequence, ICollection<TransactionAttachment> attachments)
        {
            return new DebitAcquisitionGoodsAndServicesTransaction(@from, to, amount, receivedDateTime, description, sequence, attachments);
        }
        public Guid Id { get;  }
        public DateTime ReceivedDateTime { get; }
        public decimal Amount { get; }

        public Party To { get; }
        public Party From { get; }

        public string Description { get; }

        public DateTime DateFiled { get;}
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

        public class Party
        {
            public Party(Guid memberId, string name)
            {
                if (memberId == default)
                {
                    throw new ArgumentException("Cannot be default value", nameof(memberId));
                }
                MemberId = memberId ;

                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Cannot be null or empty", nameof(name));
                }
                Name = name;
            }

            public Party(string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Cannot be null or empty", nameof(name));
                }
                Name = name;
            }
            public Guid MemberId { get; }
            public string Name { get; }
        }

        private class DebitFixedCostsTransaction : Transaction
        {
            public DebitFixedCostsTransaction(Party from, Party to, decimal amount, DateTime receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) :base(from, to, amount, receivedDateTime, description, sequence, attachments)           {
                
            }
        }

        private class DebitAcquisitionGoodsAndServicesTransaction : Transaction
        {

            public DebitAcquisitionGoodsAndServicesTransaction(Party from, Party to, decimal amount, DateTime receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(from, to, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }

        private class DebitAcquisitionConsumables : Transaction
        {
            public DebitAcquisitionConsumables(Party from, Party to, decimal amount, DateTime receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(from, to, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }

        public class DebitBankCostsTransaction : Transaction
        {
            public DebitBankCostsTransaction(Party from, Party to, decimal amount, DateTime receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(from, to, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }

        public class CreditMemberFeeTransaction : Transaction
        {
            public CreditMemberFeeTransaction(Party from, Party to, decimal amount, DateTime receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(from, to, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }

        public class CreditWorkshopFeeTransaction : Transaction
        {
            public CreditWorkshopFeeTransaction(Party from, Party to, decimal amount, DateTime receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(from, to, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }

        public class CreditDonationTransaction : Transaction
        {
            public CreditDonationTransaction(Party from, Party to, decimal amount, DateTime receivedDateTime,
                string description, int sequence, ICollection<TransactionAttachment> attachments) : base(from, to, amount, receivedDateTime, description, sequence, attachments)
            {

            }
        }
    }

}
