using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CashierSystem.Models
{
    public class CustomerDetailsViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string AddressLine1 { get; set; }


        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        [Required, DataType(DataType.PostalCode)]
        public string PostCode { get; set; }
    }
}