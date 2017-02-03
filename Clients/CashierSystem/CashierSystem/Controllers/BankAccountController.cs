using System;
using System.Net;
using System.Web.Mvc;
using CashierSystem.Models;
using CashierSystem.WebService;

namespace CashierSystem.Controllers
{
    public class BankAccountController : Controller
    {
        [HttpGet]
        public ActionResult GetAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAccount(string accountNumber, string sortCode)
        {
            if (accountNumber == null || sortCode == null)
            {
                ModelState.AddModelError("", "Please check the values you have provided.");
                return View();
            }

            BankAccountService service = new BankAccountService();
            BankAccountDetails account = service.GetBankAccount(accountNumber, sortCode);

            if (account == null)
            {
                ViewBag.Error = "Account not found";
                return View();
            }
            else
            {
                return View("Details", account);
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            var model = new NewBankAccount();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(NewBankAccount model)
        {
            BankAccountService service = new BankAccountService();
            bool success = service.OpenAccount(model);

            if (success == false)
            {
                //Generic error for now... no time to handle properly
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return View();
        }


        [HttpGet]
        public ActionResult Close()
        {
            return View(new CloseAccount());
        }

        [HttpPost]
        public ActionResult Close(CloseAccount model)
        {
            BankAccountService service = new BankAccountService();
            bool success = service.CloseAccount(model);

            if (success == false)
            {
                //Generic error for now... no time to handle properly
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return View();
        }
    }
}
