using Platinum.Life.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Platinum.Life.Web2.Models
{
    public class Dashboard
    {
        public int TotalApproved { get; set; }
        public int TotalNew { get; set; }
        public int Total { get; set; }
    }
}