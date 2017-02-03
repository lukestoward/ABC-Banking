using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashierSystem.Models
{
    public class CustomerPartial
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string PostCode { get; set; }
    }
}