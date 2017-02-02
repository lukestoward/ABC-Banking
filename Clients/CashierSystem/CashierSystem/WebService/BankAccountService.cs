using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using CashierSystem.Models;

namespace CashierSystem.WebService
{
    internal class BankAccountService : WebServiceBase
    {

        public object GetAccountDetails(string accountNumber, string sortCode)
        {
            //Serialisable object
            object model = new
            {
                AccountNumber = accountNumber,
                Sortcode = sortCode
            };

            //Webservice address
            string badStaticString = "http://localhost:56698//api/Account/GetAccount";

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

        public bool OpenAccount(NewBankAccount model)
        {
            //Webservice address
            string badStaticString = "http://localhost:56698//api/Account/OpenAccount";

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

        public bool CloseAccount(CloseAccount model)
        {
            //Webservice address
            string badStaticString = "http://localhost:56698//api/Account/CloseAccount";

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

    }
}