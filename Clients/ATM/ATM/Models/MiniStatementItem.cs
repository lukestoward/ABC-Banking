using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class MiniStatementItem
    {
        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Amount { get; set; }
    }
}