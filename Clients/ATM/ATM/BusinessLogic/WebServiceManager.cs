using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ATM.Models;

namespace ATM.BusinessLogic
{
    public class WebServiceManager
    {
        public WebServiceManager()
        {

        }

        //public async Task<object> HandleWithdrawRequest(WithdrawFunds request)
        //{
        //    try
        //    {
        //        string badStaticStringUrl = "https://localhost:3278/";

        //        //1. validate request values
        //        //2. make the request to the web service
        //        //3. handle the response
        //        //4. return something useful to the controller
        //    }
        //    catch (Exception ex)
        //    {
                
        //        throw;
        //    }
        //}

        public async Task<decimal> HandleGetBalanceRequest(string accNumber, string sortCode)
        {
            string badHardCodedStringUrl = "http://localhost:56698/api/Account/GetAccountBalance";

            BalanceRequestDTO data = new BalanceRequestDTO
            {
                AccountNumber = accNumber,
                SortCode = sortCode
            };

            string response = MakeHttpPostRequest(badHardCodedStringUrl, data);


            return 0.00m;
        }

        private string MakeHttpPostRequest(string url, object data)
        {
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.
            request.Method = "POST";

            //Seralise data to json string
            var json = new JavaScriptSerializer().Serialize(data);

            // convert it to a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();

            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            //Deserialise to an object
            //var responseObject = new JavaScriptSerializer().DeserializeObject(responseFromServer);
           
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            //Return
            return responseFromServer;
        }
    }
}