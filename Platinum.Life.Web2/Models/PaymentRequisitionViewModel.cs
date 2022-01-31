using Platinum.Life.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Platinum.Life.Web2.Models
{
    public class PaymentRequisitionViewModel : PaymentRequisition
    {
        public string StatusName { get; set; }
        public string DepartmentName { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByEmail { get; set; }
    }
}