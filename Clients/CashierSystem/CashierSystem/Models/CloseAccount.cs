using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CashierSystem.Models
{
    public class CloseAccount
    {
        [MinLength(8), MaxLength(8)]
        public string AccountNumber { get; set; }

        [MinLength(6), MaxLength(6)]
        public string SortCode { get; set; }
    }
}