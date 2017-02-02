using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using CashierSystem.Models;

namespace CashierSystem.WebService
{
    internal class WithdrawService : WebServiceBase
    {
        public bool ProcessWithdrawRequest(WithdrawRequest model)
        {
            //Webservice address
            string badStaticString = "http://localhost:2975/api/Withdraw/ProcessWithdrawATM";

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