using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CashierSystem.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace CashierSystem.WebService
{
    internal class CustomerService : WebServiceBase
    {
        private string account_services_api_base = "http://localhost:56698//api/";

        public bool CreateNewCustomer(RegisterCustomer model)
        {
            //Webservice address
            string badStaticString = account_services_api_base + "Customer/RegisterCustomer";

            //1. Validate values
            //Do some checks here...

            //2. Make HTTP Request to web service
            HttpCustomResult response = MakeHttpRequest(badStaticString, model);

            //3. Handle response
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Success
                return true;
            }
            else
            {
                //Something went wrong. We don't care what at this time.
                return false;
            }
        }

        public List<CustomerPartial> GetAllCustomers()
        {
            //Webservice address
            string badStaticString = account_services_api_base + "Customer/GetCustomers";
            
            //1. Make HTTP Request to web service
            HttpCustomResult response = MakeHttpRequest(badStaticString, null, HttpVerbs.Get);

            //3. Handle response
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Success
                List<CustomerPartial> customers = JsonConvert.DeserializeObject<List<CustomerPartial>>(response.MessageBody);
                return customers;
            }
            else
            {
                //Something went wrong. We don't care what at this time.
                return null;
            }
        }

        public CustomerDetailsViewModel GetCustomerDetails(Guid id)
        {

            //Webservice address
            string badStaticString = account_services_api_base + "Customer/GetCustomerDetails?Id=" + id;

            //1. Make HTTP Request to web service
            HttpCustomResult response = MakeHttpRequest(badStaticString, null, HttpVerbs.Get);

            //3. Handle response
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Success
                CustomerDetailsViewModel customer = JsonConvert.DeserializeObject<CustomerDetailsViewModel>(response.MessageBody);
                return customer;
            }
            else
            {
                //Something went wrong. We don't care what at this time.
                return null;
            }
        }
    }
}