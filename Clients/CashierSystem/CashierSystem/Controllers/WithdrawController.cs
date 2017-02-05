using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CashierSystem.Models;
using CashierSystem.WebService;

namespace CashierSystem.Controllers
{
    public class WithdrawController : Controller
    {
        [HttpGet]
        public ActionResult Withdraw()
        {
            //Demo values
            WithdrawRequest model = new WithdrawRequest
            {
                AccountHolderName = "Luke Stoward",
                AccountNumber = "12345678",
                CardNumber = "4485456965871348",
                DateRequested = DateTime.UtcNow,
                Pin = 1234,
                SortCode = "445566",
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Withdraw(WithdrawRequest model)
        {
            if (ModelState.IsValid == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WithdrawService service = new WithdrawService();
            bool success = service.ProcessWithdrawRequest(model);

            if (success)
            {
                TempData["Message"] = "Transaction Complete";
            }

            return RedirectToAction("Withdraw");
        }
    }
}