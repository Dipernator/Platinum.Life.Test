using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Life.Entities
{
    public class BankDetails : BaseEntity
    {
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public string Bank { get; set; }
        public string BranchCode { get; set; }
    }
}
