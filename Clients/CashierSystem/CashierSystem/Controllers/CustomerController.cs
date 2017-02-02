using System.Net;
using System.Web.Mvc;
using CashierSystem.Models;
using CashierSystem.WebService;

namespace CashierSystem.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            var model = new RegisterCustomer();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RegisterCustomer model)
        {
            CustomerService service = new CustomerService();
            bool success = service.CreateNewCustomer(model);

            if (success == false)
            {
                //Generic error for now... no time to handle properly
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Manage()
        {

            return View();
        }

    }
}