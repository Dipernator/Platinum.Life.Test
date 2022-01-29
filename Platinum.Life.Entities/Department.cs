namespace Platinum.Life.Entities
{
    public class Department : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
    }
}
