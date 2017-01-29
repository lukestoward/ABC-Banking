using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABC_Banking.Services.BankTransfer.Models
{
    public class TransferRequest
    {

        [Required]
        public decimal TransferAmount { get; set; }

        [Required, MaxLength(18)]
        public string TransferReference { get; set; }

        [Required]
        public DateTime DateOfPayment { get; set; }

        #region BENEFACTOR DETAILS

        [Required]
        public string BenefactorFullName { get; set; }

        [Required, MaxLength(6), MinLength(6)]
        public string BenefactorSortCode { get; set; }

        [Required, MaxLength(8), MinLength(8)]
        public string BenefactorAccountNumber { get; set; }

        #endregion

        #region RECIPIENT DETAILS

        [Required]
        public string RecipientFullName { get; set; }

        [Required, MaxLength(6), MinLength(6)]
        public string RecipientSortCode { get; set; }

        [Required, MaxLength(8), MinLength(8)]
        public string RecipientAccountNumber { get; set; }

        #endregion
    }
}