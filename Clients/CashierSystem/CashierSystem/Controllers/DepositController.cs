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
    public class DepositController : Controller
    {
        // GET: Deposit
        [HttpGet]
        public ActionResult NewDeposit()
        {
            var model= new DepositRequest();
            return View(model);
        }

        [HttpPost]
        public ActionResult NewDeposit(DepositRequest model)
        {
            if (ModelState.IsValid == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DepositService service = new DepositService();
            bool success = service.ProcessDepositRequest(model);

            return View()
        }
    }
}