using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ABC_Banking.Core.Models;
using ABC_Banking.Core.Validation;
using ABC_Banking.Services.AccountServices.Models;

namespace ABC_Banking.Services.AccountServices.Controllers
{
    public class CustomerController : ApiController
    {
        private CustomerServices services;

        public CustomerController()
        {
            services = new CustomerServices();
        }

        [HttpPost]
        public async Task<IHttpActionResult> RegisterCustomer(CustomerDTO model)
        {
            ValidationResult result = await services.CreateCustomer(model);

            if (result.HasError())
            {
                return BadRequest(result.Errors.ToString());
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCustomers()
        {
            List<Customer> customers = await services.GetCustomers();

            if (customers == null)
            {
                return BadRequest("Unable to return customers.");
            }

            return Ok(customers);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerDetails(Guid Id)
        {
            Customer customer = await services.GetCustomerDetails(Id);

            if (customer == null)
            {
                return BadRequest("Unable to return customers.");
            }

            return Ok(customer);
        }
    }
}
