using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ABC_Banking.Services.AccountServices.Models;

namespace ABC_Banking.Services.AccountServices.Controllers
{
    public class AccountController : ApiController
    {

        /// <summary>
        /// Used to open a new account for an existing customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult OpenAccount(BankAccountDTO model)
        {
            return Ok();
        }

        /// <summary>
        /// When a bank account needs to be closed. 
        /// Criteria must first be meet before closure.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CloseAccount(BankAccountDTO model)
        {
            return Ok();
        }

        /// <summary>
        /// Returns the balance of the bank account.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetAccountBalance(BankAccountDTO model)
        {
            AccountServices services = new AccountServices();
            var balance = await services.GetAccountBalance(model);

            return Ok(balance);
        }

    }
}
