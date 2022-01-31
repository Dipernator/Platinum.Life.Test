using System;
using System.ComponentModel.DataAnnotations;

namespace Platinum.Life.Entities
{
    public class PaymentRequisition : BaseEntity
    {
        public string UserId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public DateTime DateOfInvoice { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public int StatusId { get; set; }
        public virtual Attachment Attachment { get; set; }
        public virtual Signature Signature { get; set; }
        public virtual BankDetails BankDetails { get; set; }
    }

    public enum PaymentRequisitionStatus
    {
        New = 1,                // Create
        PendingSignature = 2,   // Sending manager
        Approved = 3,           // Manager approved
        Declined = 4,           // Manager decline 
        Deleted = 5             // Deleted
    }
}
