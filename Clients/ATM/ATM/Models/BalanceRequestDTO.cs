using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class BalanceRequestDTO
    {
        public string AccountNumber { get; set; }

        public string SortCode { get; set; }
    }
}