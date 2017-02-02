using CashierSystem.Models;

namespace CashierSystem.WebService
{
    internal class DepositService : WebServiceBase
    {
        public bool ProcessDepositRequest(DepositRequest request)
        {
            //Webservice address
            string badStaticString = "http://localhost:50025/api/Deposit/ProcessDepositATM";

            //1. Validate values
            //Do some checks here...

            //2. Make HTTP Request to web service
            var response = MakeHttpRequest(badStaticString, request);

            //3. Handle response


            //4. Return to controller

            return false;
        }
    }
}