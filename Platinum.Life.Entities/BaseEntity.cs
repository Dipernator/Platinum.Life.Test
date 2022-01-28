using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Life.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
