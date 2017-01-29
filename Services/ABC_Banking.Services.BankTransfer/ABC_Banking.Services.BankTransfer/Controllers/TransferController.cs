using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ABC_Banking.Core.Validation;
using ABC_Banking.Services.BankTransfer.Models;

namespace ABC_Banking.Services.BankTransfer.Controllers
{
    public class TransferController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> ProcessBankTransfer(TransferRequest model)
        {
            //Pass the deposit request to the handler
            TransferHandler handler = new TransferHandler();
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
