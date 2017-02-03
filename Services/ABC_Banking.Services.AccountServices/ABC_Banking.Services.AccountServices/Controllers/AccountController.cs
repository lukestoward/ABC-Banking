using System.Threading.Tasks;
using System.Web.Http;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Services.AccountServices.Models;

namespace ABC_Banking.Services.AccountServices.Controllers
{
    public class AccountController : ApiController
    {

        /// <summary>
        /// Retrieves a bank account from the database if it exists
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <param name="SortCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetBankAccount([FromUri]BankAccountDTO model)
        {
            if (ModelState.IsValid == false)
                return BadRequest("Please check values provided");

            AccountServices services = new AccountServices();
            BankAccountDetailsDTO account = await services.GetBankAccountDetails(model.AccountNumber, model.SortCode);

            if (account == null)
            {
                return BadRequest("Account could not be retrieved");
            }

            return Ok(account);
        }

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
        [HttpPost]
        public async Task<IHttpActionResult> GetAccountBalance(BankAccountDTO model)
        {
            AccountServices services = new AccountServices();
            var balance = await services.GetAccountBalance(model);

            if (balance == null)
            {
                return BadRequest("Unable to get bank account balance. Please check values provided");
            }

            return Ok(balance);
        }

    }
}
