using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABC_Banking.Services.AccountServices.Models
{
    public class MiniStatementTransaction
    {
        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Amount { get; set; }
    }
}