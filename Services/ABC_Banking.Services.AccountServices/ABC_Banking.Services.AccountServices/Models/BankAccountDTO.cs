using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABC_Banking.Services.AccountServices.Models
{
    /// <summary>
    /// Data Transfer Object (DTO) used to bind values sent via HTTP
    /// </summary>
    public class BankAccountDTO
    {

        [MinLength(8), MaxLength(8)]
        public string AccountNumber { get; set; }

        [MinLength(6), MaxLength(6)]
        public string SortCode { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
    }
}