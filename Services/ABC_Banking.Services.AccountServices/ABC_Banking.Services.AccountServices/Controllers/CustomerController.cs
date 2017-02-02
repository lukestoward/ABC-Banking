using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABC_Banking.Services.AccountServices.Models;

namespace ABC_Banking.Services.AccountServices.Controllers
{
    public class CustomerController : ApiController
    {
        [HttpPost]
        public IHttpActionResult RegisterCustomer(CustomerDTO model)
        {
            return Ok();
        }
    }
}
