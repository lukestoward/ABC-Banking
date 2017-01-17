using System.Threading.Tasks;
using System.Web.Http;
using ABC_Banking.Services.Deposit.Models;
using ABC_Banking.Services.Deposit.Validation;

namespace ABC_Banking.Services.Deposit.Controllers
{
    /// <summary>
    /// API access to process a deposit request
    /// </summary>
    public class DepositController : ApiController
    {
        /// <summary>
        /// Endpoint to process a deposit request made via an ATM
        /// </summary>
        /// <param name="model"></param>
        /// <returns>HttpActionResult</returns>
        [HttpPost]
        public async Task<IHttpActionResult> ProcessDepositATM(DepositRequest model)
        {
            //Pass the deposit request to the handler
            DepositHandler handler = new DepositHandler();
            IValidationResult result = await handler.Process(model);

            //Check for any errors
            if (result.HasError())
            {
                AddModelStateErrors(result);
                return BadRequest(ModelState);
            }

            //Return with 200
            return Ok();
        }

        /// <summary>
        /// Endpoint to process a deposit request made via a deposit box slip
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> ProcessDepositBox(DepositRequest model)
        {
            //Pass the deposit request to the handler
            DepositHandler handler = new DepositHandler();
            IValidationResult result = await handler.Process(model);

            //Check for any errors
            if (result.HasError())
            {
                AddModelStateErrors(result);
                return BadRequest(ModelState);
            }

            //Return with 200
            return Ok();
        }

        /// <summary>
        /// Endpoint to process a deposit request made via a face to face cashier transaction
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> ProcessDepositCashier(DepositRequest model)
        {
            //Pass the deposit request to the handler
            DepositHandler handler = new DepositHandler();
            IValidationResult result = await handler.Process(model);

            //Check for any errors
            if (result.HasError())
            {
                AddModelStateErrors(result);
                return BadRequest(ModelState);
            }

            //Return with 200
            return Ok();
        }

        private void AddModelStateErrors(IValidationResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
