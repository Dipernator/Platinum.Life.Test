namespace Platinum.Life.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public System.DateTime CreateDateTime { get; set; } = System.DateTime.Now;
        public System.DateTime ModifiedDateTime { get; set; } = System.DateTime.Now;
    }
}
