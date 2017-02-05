using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CashierSystem.Models;
using Newtonsoft.Json;

namespace CashierSystem.WebService
{
    internal class BankAccountService : WebServiceBase
    {
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

        public BankAccountDetails GetBankAccount(string accountNumber, string sortCode)
        {
            //Webservice address
            string badStaticString = "http://localhost:56698//api/Account/GetBankAccount?AccountNumber=" + accountNumber
                + "&SortCode=" + sortCode;

            //1. Validate values
            //Do some checks here...

            object model = new
            {
                AccountNumber = accountNumber,
                SortCode = sortCode
            };

            //2. Make HTTP Request to web service
            HttpCustomResult response = MakeHttpRequest(badStaticString, model, HttpVerbs.Get);

            //3. Handle response
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Success
                return JsonConvert.DeserializeObject<BankAccountDetails>(response.MessageBody);
            }
            else
            {
                //Something went wrong. We don't care what at this time.
                return null;
            }
        }
    }
}