using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ATM.BusinessLogic;
using ATM.Models;

namespace ATM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Allow a customer to view their account balance
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult ViewBalance(BalanceRequestDTO model)
        {
            WebServiceManager services = new WebServiceManager();
            var balance = services.HandleGetBalanceRequest(model.AccountNumber, model.SortCode);

            return View(balance.Balance);
        }

        [HttpGet]
        public PartialViewResult WithdrawFunds()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult WithdrawFunds(int amount)
        {
            //Hardcoded values for demo purposes only
            WithdrawFunds model = new WithdrawFunds
            {
                AccountHolderName = "Joe Blogs",
                AccountNumber = "12345678",
                CardNumber = "4485456965871348",
                PIN = 1234,
                SortCode = "445566",
                TotalCashValue = amount
            };

            WebServiceManager services = new WebServiceManager();
            WithdrawResponse response = services.HandleWithdrawRequest(model);

            if (response.Approved == false)
            {
                return View("WithdrawResult", response.Reason);
            }

            string successMessage = "Please collect your cash below";

            return View("WithdrawResult", null, successMessage);
        }

        [HttpGet]
        public ActionResult ViewStatement()
        {
            MiniStatementRequestDTO model = new MiniStatementRequestDTO
            {
                AccountNumber = "12345678",
                SortCode = "445566"
            };

            WebServiceManager services = new WebServiceManager();
            List<MiniStatementItem> statement = services.HandleMiniStatmentRequest(model);

            return View(statement);
        }
    }
}