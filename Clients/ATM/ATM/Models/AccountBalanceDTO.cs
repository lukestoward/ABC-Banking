using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class AccountBalanceDTO
    {
        public decimal Balance { get; set; }

        public decimal AvailableBalance { get; set; }
    }
}