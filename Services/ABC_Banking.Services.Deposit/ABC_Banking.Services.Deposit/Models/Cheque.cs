using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ABC_Banking.Services.Deposit.Models
{
    public class Cheque
    {
        /// <summary>
        /// The unique number on the cheque
        /// </summary>
        [Required]
        public long ChequeNumber { get; set; }

        /// <summary>
        /// Date specified on the chque
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Recipient name as written on the check
        /// </summary>
        [Required]
        public string RecipientName { get; set; }

        /// <summary>
        /// Cheque specified value
        /// </summary>
        [Required]
        public decimal Value { get; set; }

        /// <summary>
        /// Cheque originating sort code
        /// </summary>
        [Required]
        public int BenefactorSortCode { get; set; }

        /// <summary>
        /// Cheque originating account number
        /// </summary>
        [Required]
        public long BenefactorAccountNumber { get; set; }

        /// <summary>
        /// An image scan of the cheque being processed
        /// </summary>
        [Required]
        public byte[] ImageScan { get; set; }
    }
}