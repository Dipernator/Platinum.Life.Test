using System.Web;

namespace Platinum.Life.Entities
{
    public class Attachment : BaseEntity
    {
        public int PaymentRequisitionId { get; set; }
        public string Url { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
