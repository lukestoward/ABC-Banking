using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class WithdrawResponse
    {
        public bool Approved { get; set; }

        public string Reason { get; set; }
    }
}