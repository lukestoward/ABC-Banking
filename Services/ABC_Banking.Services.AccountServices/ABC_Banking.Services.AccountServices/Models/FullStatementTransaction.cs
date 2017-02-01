using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABC_Banking.Services.AccountServices.Models
{
    public class FullStatementTransaction
    {
        public DateTime DateTime { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public decimal PaidIn { get; set; }

        public decimal PaidOut { get; set; }
    }
}