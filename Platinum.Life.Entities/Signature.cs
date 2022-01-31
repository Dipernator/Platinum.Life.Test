namespace Platinum.Life.Entities
{
    public class Signature : BaseEntity
    {
        public int PaymentRequisitionId { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
    }
}
