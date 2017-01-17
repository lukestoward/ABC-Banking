using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ABC_Banking.Services.Deposit.Models
{
    public class DepositRequest
    {
        /// <summary>
        /// DateTime stamp the deposit was made
        /// </summary>
        [Required]
        public DateTime DateRequested { get; set; }

        /// <summary>
        /// The account holders name
        /// </summary>
        [Required]
        public string AccountHolderName { get; set; }

        /// <summary>
        /// The bank account number
        /// </summary>
        [Required, MinLength(8), MaxLength(8)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// The bank account sort code, required format "123456" (no hyphens)
        /// </summary>
        [Required, MinLength(6), MaxLength(6)]
        public string SortCode { get; set; }

        /// <summary>
        /// Total cash value of the deposit transaction
        /// </summary>
        [Required]
        public decimal TotalCashValue { get; set; }

        /// <summary>
        /// A collection of cheques made as part of the deposit
        /// </summary>
        public ICollection<Cheque> Cheques { get; set; }

        /// <summary>
        /// Calculates the total deposit request value
        /// </summary>
        /// <returns>Total value of cash and cheque amounts</returns>
        public decimal TotalValue()
        {
            return TotalCashValue + Cheques.Sum(cheque => cheque.Value);
        }
    }
}