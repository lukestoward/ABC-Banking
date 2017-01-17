using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ABC_Banking.Core.Models.BankAccounts;

namespace ABC_Banking.Core.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        
        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string County { get; set; }

        [Required, DataType(DataType.PostalCode)]
        public string PostCode { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }

        
        [NotMapped]
        public string FullName => FirstName + " " + Surname;
    }
}
