using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Life.Entities
{
    public class Department : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
