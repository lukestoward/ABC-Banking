using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ABC_Banking.Core.Models;
using ABC_Banking.Core.Models.BankAccounts;

namespace ABC_Banking.Services.AccountServices.Models
{
    public class BankAccountDetailsDTO
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public DateTime DateOpened { get; set; }

        public string AccountNumber { get; set; }

        public string SortCode { get; set; }

        public Guid CustomerId { get; set; }

        public decimal Balance { get; set; }

        public float InterestRate { get; set; }
    }
}