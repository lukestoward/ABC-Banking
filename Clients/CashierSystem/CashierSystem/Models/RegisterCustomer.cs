using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CashierSystem.Models
{
    public class RegisterCustomer
    {
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
    }
}