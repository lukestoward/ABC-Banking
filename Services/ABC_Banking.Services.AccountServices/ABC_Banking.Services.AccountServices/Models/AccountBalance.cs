using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABC_Banking.Services.AccountServices.Models
{
    public class AccountBalance
    {
        public decimal Balance { get; set; }

        public decimal AvailableBalance { get; set; }
    }
}