using System.Threading.Tasks;
using System.Web.Http;
using ABC_Banking.Core.Validation;
using ABC_Banking.Services.Withdraw.Models;

namespace ABC_Banking.Services.Withdraw.Controllers
{
    public class WithdrawController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> ProcessWithdrawATM(WithdrawRequest model)
        {
            //Pass the withdraw request to the handler
            WithdrawHandler handler = new WithdrawHandler();
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
