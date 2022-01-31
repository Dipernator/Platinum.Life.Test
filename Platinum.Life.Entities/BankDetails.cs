using System.ComponentModel.DataAnnotations;

namespace Platinum.Life.Entities
{
    public class BankDetails : BaseEntity
    {
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string AccountHolder { get; set; }
        [Required]
        public string Bank { get; set; }
        [Required]
        public string BranchCode { get; set; }
    }
}
