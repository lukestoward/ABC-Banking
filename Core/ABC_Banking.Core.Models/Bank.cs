using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABC_Banking.Core.Models
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string AddressLine1 { get; set; }

        [Required]
        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string County { get; set; }

        [Required, DataType(DataType.PostalCode)]
        public string PostCode { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }       
    }
}
