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
        public ActionResult Index()
        {
            //Demo values
            string accountNumber = "12345678";
            string sortCode = "445566";

            //Get account information
            BankAccountService service = new BankAccountService();
            var bankAccount = service.GetAccountDetails(accountNumber, sortCode);

            return View(bankAccount);
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
                ViewBag.Message = "Transaction Complete";
            }

            return RedirectToAction("Index");
        }
    }
}