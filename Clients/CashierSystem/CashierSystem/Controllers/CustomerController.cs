using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using CashierSystem.Models;
using CashierSystem.WebService;

namespace CashierSystem.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerService service;

        public CustomerController()
        {
            service = new CustomerService();    
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new RegisterCustomer();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RegisterCustomer model)
        {
            bool success = service.CreateNewCustomer(model);

            if (success == false)
            {
                //Generic error for now... no time to handle properly
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays a list of all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            //Get all customers
            List<CustomerPartial> customers = service.GetAllCustomers();

            return View(customers);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            CustomerDetailsViewModel customer = service.GetCustomerDetails(id);

            if (customer == null)
            {
                ViewBag.Error = "Unable to load customer details.";
                RedirectToAction("Index");
            }

            return View(customer);
        }

    }
}