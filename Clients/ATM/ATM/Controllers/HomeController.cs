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
        public decimal ViewBalance(BalanceRequestDTO model)
        {
            WebServiceManager services = new WebServiceManager();
            var balance = services.HandleGetBalanceRequest(model.AccountNumber, model.SortCode);

            return balance.Balance;
        }

        [HttpGet]
        public PartialViewResult WithdrawFunds()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> WithdrawFunds(WithdrawFunds model)
        {
            WebServiceManager services = new WebServiceManager();
            //await services.HandleWithdrawRequest(model);
            return View();
        }

        [HttpPost]
        public ActionResult ViewStatment()
        {

            return null;
        }
    }
}