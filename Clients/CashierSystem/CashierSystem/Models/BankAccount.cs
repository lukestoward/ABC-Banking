using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CashierSystem.Models
{
    public class BankAccountDetails
    {

        public Guid Id { get; set; }

        public DateTime DateOpened { get; set; }

        [Required, MinLength(8), MaxLength(8)]
        [Index("IX_AccountNumber", 1, IsUnique = true)]
        public string AccountNumber { get; set; }

        [Required, MinLength(6), MaxLength(6)]
        public string SortCode { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal Balance { get; set; }

        [Required]
        public float InterestRate { get; set; }
    }
}